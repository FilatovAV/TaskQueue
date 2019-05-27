using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskQueue.Domain;

namespace TaskQueue.DAL.Context
{
    public class TaskQueueContext: DbContext
    {
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public TaskQueueContext(DbContextOptions<TaskQueueContext> options)
            :base(options)
        {

        }
    }
}
