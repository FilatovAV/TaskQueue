using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskQueue.Domain;

namespace TaskQueue.ViewModels
{
    public class IssueViewModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        [Display(Name = "Creation date"), DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Execution date"), DataType(DataType.DateTime)]
        public DateTime? ExecutionDate { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
        //public Issue Issue { get; set; }
        public virtual IEnumerable<Status> Statuses { get; set; } = new List<Status>();
    }
}
