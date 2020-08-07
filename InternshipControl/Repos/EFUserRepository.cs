using InternshipControl.Data;
using InternshipControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipControl.Repos
{
    public class EFUserRepository : IUserRepo
    {
        private AppDbContext context;

        public EFUserRepository(AppDbContext ctx)
        {
            context = ctx;
        }

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
