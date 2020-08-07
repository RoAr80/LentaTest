using InternshipControl.Data;
using InternshipControl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipControl.Repos
{
    public class EFAdminRepository : IAdminRepo
    {
        private AppDbContext context;

        public EFAdminRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<User> Users => context.Users;

        public void CreateUser(User user)
        {
            context.Add(user);
            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public User GetUser(string key) => context.Users.First(u => u.Id == key);
        

        public void UpdateUser(User user)
        {
            User u = context.Users.Find(user.Id);
            u.Email = user.Email;
            
            u.NameSurnameFathername = user.NameSurnameFathername;
            u.DateOfBirth = user.DateOfBirth;
            u.University = user.University;
            u.Course = user.Course;
            u.Faculty = user.Faculty;
            u.PhoneNumber = user.PhoneNumber;
            u.AdditionalInfo = user.AdditionalInfo;
            context.SaveChanges();
        }
    }
}
