using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskQueue.Controllers.Interfaces;

namespace TaskQueue.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesData issuesData;

        public IssuesController(IIssuesData issuesData)
        {
            this.issuesData = issuesData;
        }
        public IActionResult Index()
        {
            return View(issuesData.GetAll());
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

            return View(issue);
        }

        [HttpPost]
        public IActionResult Edit(Issue issue)
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
                var db_employee = issuesData.GetById(issue.Id);
                if (db_employee is null)
                {
                    return NotFound();
                }
                db_employee.Content = issue.Content;
                db_employee.CreationDate = issue.CreationDate;
                db_employee.ExecutionDate = issue.ExecutionDate;
                db_employee.Header = issue.Header;
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

            return RedirectToAction("Index");
        }
    }
}