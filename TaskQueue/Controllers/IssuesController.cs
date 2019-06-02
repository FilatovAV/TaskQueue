using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskQueue.Controllers.Interfaces;
using TaskQueue.Domain;
using TaskQueue.ViewModels;

namespace TaskQueue.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesData issuesData;

        public IssuesController(IIssuesData issuesData)
        {
            this.issuesData = issuesData;
        }
        public IActionResult Index(bool get_date)
        {
            if (get_date)
            {
                return View(issuesData.GetTasksToClose(120));
            }
            else
            {
                return View(issuesData.GetAll());
            }
        }
        public IActionResult Details(int id)
        {
            var issue = issuesData.GetById(id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        public IActionResult Edit(int? id)
        {
            Issue issue;
            if (id != null)
            {
                issue = issuesData.GetById((int)id);
                if (issue is null)
                {
                    return NotFound();
                }
            }
            else
            {
                issue = new Issue();
                issue.CreationDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            }


            var issue_model = new IssueViewModel
            {
                Content = issue.Content,
                CreationDate = issue.CreationDate,
                ExecutionDate = issue.ExecutionDate,
                Header = issue.Header,
                Id = issue.Id,
                Status = issue.Status,
                StatusId = issue.StatusId,
                //Issue = issue,
                Statuses = issuesData.GetStatuses()
            };

            return View(issue_model);
        }

        [HttpPost]
        public IActionResult Edit(IssueViewModel issue)
        {
            if (!ModelState.IsValid)
            {
                return View(issue);
            }

            //Валидация модели данных (доп.)
            if (issue.ExecutionDate != null)
            {
                if (issue.ExecutionDate < issue.CreationDate)
                {
                    ModelState.AddModelError("ExecutionDate", "The creation date cannot exceed the due date!");
                    return View(issue);
                }
            }

            if (issue.Id > 0)
            {
                var issues = issuesData.GetById(issue.Id);
                if (issues is null)
                {
                    return NotFound();
                }
                issues.Content = issue.Content;
                issues.CreationDate = issue.CreationDate;
                issues.ExecutionDate = issue.ExecutionDate;
                issues.Header = issue.Header;
                issues.StatusId = issue.Status.Id;
            }
            else
            {
                issuesData.AddNew(issue);
            }

            issuesData.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var issue = issuesData.GetById((int)id);
            if (issue == null)
            {
                return NotFound();
            }
            issuesData.Delete((int)id);
            issuesData.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}