using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskQueue.Components
{
    public class NavigationBarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //Если мы авторизовались отображаем NavigationBarView
            if (User.Identity.IsAuthenticated)
            {
                return View("NavigationBarView");
            }
            //В противном случае отображаем Default view
            return View();
        }
    }
}
