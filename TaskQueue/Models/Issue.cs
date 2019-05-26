using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskQueue
{
    public class Issue
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        public string Content { get; set; }
        [Display(Name = "Creation date"), DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Execution date"), DataType(DataType.DateTime)]
        public DateTime? ExecutionDate { get; set; }
    }
}
