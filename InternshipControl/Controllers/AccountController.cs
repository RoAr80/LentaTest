using InternshipControl.Models;
using InternshipControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipControl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;        

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;            
        }

        [HttpGet]        
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {           
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,                    
                    NameSurnameFathername = model.NameSurnameFathername,
                    DateOfBirth = model.DateOfBirth,
                    University = model.University,
                    Course = model.Course,
                    Faculty = model.Faculty,
                    PhoneNumber = model.PhoneNumber,
                    AdditionalInfo = model.AdditionalInfo,
                    CreateDate = DateTime.Now,
                    WhoCreated = CustomRoles.Member,
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, CustomRoles.Member);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            User user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel model = new UserViewModel
            {
                Email = user.Email,
                //Password = "*******",
                NameSurnameFathername = user.NameSurnameFathername,
                DateOfBirth = user.DateOfBirth,
                University = user.University,
                Course = user.Course,
                Faculty = user.Faculty,
                PhoneNumber = user.PhoneNumber,
                AdditionalInfo = user.AdditionalInfo,                
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(this.User);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.NameSurnameFathername = model.NameSurnameFathername;
                    user.DateOfBirth = model.DateOfBirth;
                    user.University = model.University;
                    user.Course = model.Course;
                    user.Faculty = model.Faculty;
                    user.PhoneNumber = model.PhoneNumber;
                    user.AdditionalInfo = model.AdditionalInfo;
                    user.WhoEdited = CustomRoles.Member;
                    user.EditDate = DateTime.Now;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
