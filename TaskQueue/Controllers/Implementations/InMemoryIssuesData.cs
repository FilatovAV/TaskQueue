using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskQueue.Controllers.Interfaces;
using TaskQueue.Domain;

namespace TaskQueue.Controllers.Implementations
{
    public class InMemoryIssuesData : IIssuesData
    {
        private static List<Issue> _Issues = new List<Issue> {
            new Issue{ Id=1, Header = "First task", Content = "Contents of the first task", CreationDate = DateTime.Now, ExecutionDate = DateTime.Now.AddHours(1) },
            new Issue{ Id=2, Header = "Second task", Content = "Content of the second task", CreationDate = DateTime.Now, ExecutionDate = DateTime.Now.AddHours(2) },
            new Issue{ Id=3, Header = "Third task", Content = "Content of the third task", CreationDate = DateTime.Now, ExecutionDate = DateTime.Now.AddHours(2.1) },
            new Issue{ Id=4, Header = "Fourth task", Content = "Content of the fourth task", CreationDate = DateTime.Now, ExecutionDate = DateTime.Now.AddHours(3.3) }
        };


        public void AddNew(Issue issue)
        {
            if (issue is null) { throw new ArgumentException(nameof(issue)); }
            if (_Issues.Contains(issue) || _Issues.Any(i => i.Id == issue.Id)) { return; }
            issue.Id = _Issues.Count() == 0 ? 1 : _Issues.Max(i => i.Id) + 1;
            _Issues.Add(issue);
        }

        public void Delete(int id)
        {
            var issue = GetById(id);
            if (issue is null) { return; }
            _Issues.Remove(issue);
        }

        public IEnumerable<Issue> GetAll() => _Issues;

        public Issue GetById(int Id) => _Issues.FirstOrDefault(f => f.Id == Id);

        public void SaveChanges()
        {
            //throw new NotImplementedException();
        }
    }
}
