using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OxyNode.Entities.Identity;
using OxyNode.ViewModels.Identity;

namespace OxyNode.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;


        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            var user = new User
            {
                UserName = Model.UserName,
                Email = Model.EmailAddress
            };

            _Logger.LogInformation("Регистрация нового пользователя" + Model.UserName);

            var registration_result = await _UserManager.CreateAsync(user, Model.Password);
            if (registration_result.Succeeded)
            {
                _Logger.LogInformation($"Пользователь {Model.UserName} успешно зарегистрирован");
                await _SignInManager.SignInAsync(user, false);
                _Logger.LogInformation($"Пользователь { Model.UserName } вошёл в систему");
                return RedirectToAction("Index", "Home");
            }

            foreach (var identity_error in registration_result.Errors)
                ModelState.AddModelError("", identity_error.Description);

            // вывести ошибки в лог
            _Logger.LogWarning("Ошибка при регистрации пользователя {0}:{1}",
                Model.UserName,
                string.Join(", ", registration_result.Errors.Select(e => e.Description)));

            return View(Model);
        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false
                );

            if (login_result.Succeeded)
            {
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            return View(Model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
