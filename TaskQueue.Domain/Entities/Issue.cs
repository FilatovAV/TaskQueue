using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskQueue.Domain
{
    [Table("Issues")]
    public class Issue
    {
        [HiddenInput(DisplayValue = false)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
