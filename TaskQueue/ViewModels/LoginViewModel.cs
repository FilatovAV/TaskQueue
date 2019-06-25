using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskQueue.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User name"), MaxLength(256, ErrorMessage = "Allowed length of 256 characters")]
        public string UserName { get; set; }
        [Display(Name = "Password"), Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }

        /// <summary>Адрес перенаправления пользователя при успешной авторизации</summary>
        public string ReturnUrl { get; set; }
    }
}
