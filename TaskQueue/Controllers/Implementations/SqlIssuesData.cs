using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskQueue.Controllers.Interfaces;
using TaskQueue.DAL.Context;
using TaskQueue.Domain;
using TaskQueue.ViewModels;

namespace TaskQueue.Controllers.Implementations
{
    public class SqlIssuesData : IIssuesData
    {
        private readonly TaskQueueContext _db;
        public SqlIssuesData(TaskQueueContext db)
        {
            this._db = db;
        }
        public void AddNew(IssueViewModel issue)
        {
            if (issue is null) { throw new ArgumentException(nameof(issue)); }
            if (_db.Issues.Any(i=>i.Id == issue.Id)) { return; }

            issue.Status = _db.Statuses.FirstOrDefault(i => i.Id == issue.Status.Id);

            Issue new_issue = new Issue
            {
                Content = issue.Content,
                CreationDate = issue.CreationDate,
                ExecutionDate = issue.ExecutionDate,
                Header = issue.Header,
                Status = issue.Status
            };

            _db.Issues.Add(new_issue);
        }

        public void Delete(int Id)
        {
            var issue = GetById(Id);
            if (issue is null) { return; }
            _db.Issues.Remove(issue);
        }

        public IEnumerable<Issue> GetAll()
        {
            return _db.Issues.Include(s => s.Status).AsEnumerable();
        }

        public Issue GetById(int Id) => _db.Issues.Include(s => s.Status).FirstOrDefault(f => f.Id == Id);

        public IEnumerable<Status> GetStatuses()
        {
            return _db.Statuses.AsEnumerable();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
