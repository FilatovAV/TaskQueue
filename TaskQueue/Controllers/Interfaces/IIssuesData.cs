using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskQueue.Domain;
using TaskQueue.ViewModels;

namespace TaskQueue.Controllers.Interfaces
{
    public interface IIssuesData
    {
        /// <summary> Получить все задачи </summary>
        IEnumerable<Issue> GetAll();
        /// <summary> Получить все возможные статусы </summary>
        IEnumerable<Status> GetStatuses();
        /// <summary> Получить задачу по Id </summary>
        Issue GetById(int Id);
        /// <summary> Добавить задачу </summary>
        void AddNew(IssueViewModel issue);
        /// <summary> Удалить задачу </summary>
        void Delete(int Id);
        /// <summary> Сохранить изменения </summary>
        void SaveChanges();
    }
}
