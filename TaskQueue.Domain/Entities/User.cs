using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskQueue.Domain.Entities
{
    public class User: IdentityUser
    {
        //Определение администратора
        public const string AdminUserName = "Admin";
        public const string DefaultAdminPassword = "12345";
        public const string RoleAdmin = "Administrator";
        public const string RoleUser = "User";
    }
}
