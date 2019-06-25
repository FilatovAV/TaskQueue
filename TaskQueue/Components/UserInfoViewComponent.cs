using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskQueue.Components
{
    [ViewComponent(Name = "UserInfo")]
    public class UserInfoViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("userInfoView");
            }

            return View();
        }
    }
}
