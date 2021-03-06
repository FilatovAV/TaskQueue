﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskQueue.DAL.Context;
using TaskQueue.Domain;

namespace TaskQueue
{
    public class TaskQueueContextInitializer
    {
        private readonly TaskQueueContext _db;

        public TaskQueueContextInitializer(TaskQueueContext db)
        {
            this._db = db;
        }

        public async Task InitializeAsync()
        {
            await _db.Database.MigrateAsync();

            if (await _db.Issues.AnyAsync())
            {
                return;
            }
            //Создаем статусы
            using (var transaction = _db.Database.BeginTransaction())
            {
                List<Status> _Statuses = new List<Status>
                {
                    new Status{Id = 1, Name = "Active"},
                    new Status{Id = 2, Name = "Executed"},
                    new Status{Id = 3, Name = "Await"},
                };

                await _db.Statuses.AddRangeAsync(_Statuses);

                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Statuses] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Statuses] OFF");

                transaction.Commit();
            }
            //Создаем задачи
            //Уберем секунды из текущей даты
            DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now.ToString("g"));
            using (var transaction = _db.Database.BeginTransaction())
            {
                List<Issue> _Issues = new List<Issue> {
                    new Issue{ Id = 1, Header = "First task", Content = "Contents of the first task", CreationDate = dateTimeNow, ExecutionDate = dateTimeNow.Date + new TimeSpan(dateTimeNow.Hour + 1, dateTimeNow.Minute, 0), StatusId = 1 },
                    new Issue{ Id = 2, Header = "Second task", Content = "Content of the second task", CreationDate = dateTimeNow, ExecutionDate = dateTimeNow.Date + new TimeSpan(dateTimeNow.Hour + 2, dateTimeNow.Minute + 50, 0), StatusId = 1 },
                    new Issue{ Id = 3, Header = "Third task", Content = "Content of the third task", CreationDate = dateTimeNow, ExecutionDate = dateTimeNow.Date + new TimeSpan(dateTimeNow.Hour + 3, dateTimeNow.Minute, 0), StatusId = 2 },
                    new Issue{ Id = 4, Header = "Fourth task", Content = "Content of the fourth task", CreationDate = dateTimeNow, ExecutionDate = dateTimeNow.Date + new TimeSpan(dateTimeNow.Hour + 3, dateTimeNow.Minute + 20, 0), StatusId = 3 }
                };

                await _db.Issues.AddRangeAsync(_Issues);

                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Issues] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Issues] OFF");

                transaction.Commit();
            }
        }
    }
}
