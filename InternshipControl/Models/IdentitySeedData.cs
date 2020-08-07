using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using InternshipControl.Data;
using InternshipControl.ViewModels;
using System;
using InternshipControl.Repos;

namespace InternshipControl.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "admin";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {            

            UserManager<User> _userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<User>>();
            SignInManager<User> _signInManager =  app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<SignInManager<User>>();            

            User user = await _userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new User();
                user.UserName = adminUser;
                user.Email = adminUser;
                user.NameSurnameFathername = "admin";
                user.DateOfBirth = DateTime.Parse("25/01/1998");
                user.University = "admin";
                user.Course = 1;
                user.Faculty = "admin";
                user.PhoneNumber = "admin";
                user.AdditionalInfo = "admin";                
                await _userManager.CreateAsync(user, adminPassword);                
                await _userManager.AddToRoleAsync(user, CustomRoles.Admin);
                

            }
        }
    }
}
