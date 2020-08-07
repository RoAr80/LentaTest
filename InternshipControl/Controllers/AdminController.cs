using InternshipControl.Models;
using InternshipControl.Repos;
using InternshipControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipControl.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    public class AdminController : Controller
    {        
        UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {            
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            User applicationUser = await _userManager.GetUserAsync(this.User);            
            return View(_userManager.Users.Where(u => u.Id != applicationUser.Id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Approve(string id) => await UpdateApprove(id, true);
        
        [HttpPost]
        public async Task<ActionResult> Remove(string id) => await UpdateApprove(id, false);
        
        private async Task<ActionResult> UpdateApprove(string id, bool approve)
        {
            User user = await _userManager.FindByIdAsync(id);
            User admin = await _userManager.GetUserAsync(this.User);

            if (user != null)
            {                
                user.Approved = approve;
                //user.WhoEdited = (await _userManager.GetRolesAsync(admin)).FirstOrDefault().ToString();
                user.WhoEdited = CustomRoles.Admin;
                user.EditDate = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            AdminUserViewModel model = new AdminUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                //Password = "*******",
                NameSurnameFathername = user.NameSurnameFathername,
                DateOfBirth = user.DateOfBirth,
                University = user.University,
                Course = user.Course,
                Faculty = user.Faculty,
                PhoneNumber = user.PhoneNumber,
                AdditionalInfo = user.AdditionalInfo
            };            

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Id = model.Id;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.NameSurnameFathername = model.NameSurnameFathername;
                    user.DateOfBirth = model.DateOfBirth;
                    user.University = model.University;
                    user.Course = model.Course;
                    user.Faculty = model.Faculty;
                    user.PhoneNumber = model.PhoneNumber;
                    user.AdditionalInfo = model.AdditionalInfo;
                    user.EditDate = DateTime.Now;
                    user.WhoEdited = CustomRoles.Admin;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
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

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
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
                    WhoCreated = CustomRoles.Admin,
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, CustomRoles.Member);
                    return RedirectToAction("Index");
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
    }
}
