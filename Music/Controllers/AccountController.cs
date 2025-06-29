using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using Music.ViewsModels;

namespace Music.Controllers
{
    public class AccountController : Controller
    {
        //userManager будет содержать экземпляр UserManager
        public required UserManager<ApplicationUser> userManager;
        
        //signInManager будет содержать экземпляр SignInManager
        public required SignInManager<ApplicationUser> signInManager;
        
        // Службы UserManager и SignInManager внедряются в AccountController
        //использование инъекции конструктора
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Скопируйте данные из RegisterViewModel в IdentityUser
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                // Сохраняйте данные пользователей в таблице базы данных AspNetUsers
                var result = await userManager.CreateAsync(user, model.Password);
                // Если пользователь успешно создан, авторизуйтесь с помощью
                // SignInManager и перенаправление на действие index в HomeController
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                // Если есть какие-либо ошибки, добавьте их в объект ModelState
                // который будет отображаться с помощью тега summary
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Обрабатывать успешный вход в систему
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    // Обработка случая с двухфакторной аутентификацией
                }
                if (result.IsLockedOut)
                {
                    // Обрабатывать сценарий блокировки
                }
                else
                {
                    // Сбой обработки
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            // Если мы зашли так далеко, значит, что-то пошло не так, выведите форму заново
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
