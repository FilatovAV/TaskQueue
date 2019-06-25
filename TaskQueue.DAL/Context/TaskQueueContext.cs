using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskQueue.Domain;
using TaskQueue.Domain.Entities;

namespace TaskQueue.DAL.Context
{
    public class TaskQueueContext: IdentityDbContext<User>
    {
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public TaskQueueContext(DbContextOptions<TaskQueueContext> options)
            :base(options)
        {

        }
    }
}
