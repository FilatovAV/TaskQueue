using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskQueue
{
    public class Issue
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExecutionDate { get; set; }
    }
}
