using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskQueue.Domain.Entities;
using TaskQueue.ViewModels;

namespace TaskQueue.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Login() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var _login_result = await _signInManager.PasswordSignInAsync(
                login.UserName, login.Password, login.RememberMe, false);

            if (_login_result.Succeeded)
            {
                if (Url.IsLocalUrl(login.ReturnUrl))
                {
                    return Redirect(login.ReturnUrl);
                } else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Username or password is incorrect.");

            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            //Выйти из сеанса и удалить соответствующий cookies
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Создаем нового пользователя
            var new_user = new User() { UserName = model.UserName };
            //Выполняем попытку зарегистрировать его с указанным паролем
            var creation_result = await _userManager.CreateAsync(new_user, model.Password);

            //В случае успешной регистрации
            if (creation_result.Succeeded)
            {
                //Входим новым пользователем в систему (без сохранения)
                await _signInManager.SignInAsync(new_user, false);
                //И переходим на главную страницу сайта
                return RedirectToAction("Index", "Home");
            }

            //---------------------------------------------------------------------------------
            //Если зарегистрировать пользователя не удалось
            foreach (var item in creation_result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }
        public IActionResult AccesDenied() => View();
    }
}