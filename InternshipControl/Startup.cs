using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InternshipControl.Data;
using InternshipControl.Models;
using InternshipControl.Repos;
using InternshipControl.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InternshipControl
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
                //config.UseSqlServer(Configuration["ConnectionStrings:IntershipMSSQL"]);
                //config.UseSqlServer(Configuration["ConnectionStrings:IntershipMSSQLPC"]);
            });

            //services.AddScoped<IAdminRepo, EFAdminRepository>();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();            

            

            services.AddControllersWithViews();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Redirect to /login 
            //    options.LoginPath = "/";

            //    // Change cookie timeout to expire in 15 seconds
            //    options.ExpireTimeSpan = TimeSpan.FromSeconds(600);
            //});
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            CreateRoles(serviceProvider).Wait();
            IdentitySeedData.EnsurePopulated(app);            
            
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { CustomRoles.Admin, CustomRoles.Member };
            //IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    //create the roles and seed them to the database: 
                    await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
