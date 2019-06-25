using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskQueue.ViewModels
{
    public class RegisterUserViewModel
    {
        [Display(Name = "User name"), MaxLength(256, ErrorMessage = "Allowed length of 256 characters")]
        public string UserName { get; set; }
        [Display(Name = "Password"), Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password"), Required, DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
