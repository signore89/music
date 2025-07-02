using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Models;
using Music.ViewsModels;

namespace Music.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                // Проверьте, существует ли уже эта роль
                bool roleExists = await _roleManager.RoleExistsAsync(roleModel?.RoleName);
                if (roleExists)
                {
                    ModelState.AddModelError("", "Role Already Exists");
                }
                else
                {
                    // Создание роли
                    // Нам просто нужно указать уникальное имя роли, чтобы создать новую роль
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = roleModel?.RoleName
                    };
                    // Сохраняет роль в базовой таблице AspNetRoles
                    IdentityResult result = await _roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        RedirectToAction(nameof(AlbumsController));
                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(roleModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string roleId)
        {
            //Сначала получите информацию о роли из базы данных
            IdentityRole? role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                // Обработка сценария, при котором роль не найдена
                return View("Error");
            }
            // Заполните EditRoleViewModel данными, полученными из базы данных
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                //Description = role.Description,
                Users = new List<string>()
                // При необходимости вы можете добавить сюда другие свойства
            };
            // Получить доступ ко всем пользователям
            foreach (var user in _userManager.Users.ToList())
            {
                // Если пользователь выполняет эту роль, добавьте его имя пользователя в
                // Свойство Users в EditRoleViewModel. 
                // Затем этот объект модели передаётся в представление для отображения
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            ViewBag.RollName = role.Name;
            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            //Сначала проверьте, существует ли роль
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult? result;
                    if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                    {
                    //Если IsSelected имеет значение true и пользователь ещё не получил эту роль, добавьте его
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                    //Если IsSelected имеет значение false и пользователь уже имеет эту роль, удалите пользователя
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        //Ничего не делайте, просто продолжайте цикл
                        continue;
                    }
                    //Если вы добавляете или удаляете какого-либо пользователя, пожалуйста, проверьте, успешно ли выполнен IdentityResult
                    if (result.Succeeded)
                    {
                        if (i < (model.Count - 1))
                            continue;
                        else
                            return RedirectToAction("EditRole", new { roleId = roleId });
                    }
            }
            return RedirectToAction("EditRole", new { roleId = roleId });
        }

        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
    }
}
