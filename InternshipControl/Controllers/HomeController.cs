using InternshipControl.Models;
using InternshipControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipControl.Controllers
{
    public class HomeController : Controller
    {
        UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;        

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {            
            if (this.User.Identity.IsAuthenticated)
            {                
                User applicationUser = await _userManager.GetUserAsync(this.User);
                if (applicationUser != null)
                {
                    UserViewModel model = new UserViewModel
                    {
                        Email = applicationUser.Email,
                        //Password = "*******",
                        NameSurnameFathername = applicationUser.NameSurnameFathername,
                        DateOfBirth = applicationUser.DateOfBirth,
                        University = applicationUser.University,
                        Course = applicationUser.Course,
                        Faculty = applicationUser.Faculty,
                        PhoneNumber = applicationUser.PhoneNumber,
                        AdditionalInfo = applicationUser.AdditionalInfo
                    };
                    return View(model);
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        



    }
}
