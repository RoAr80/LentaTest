using InternshipControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipControl.Repos
{
    public interface IAdminRepo
    {
        IEnumerable<User> Users { get; }
        User GetUser(string key);       
        void UpdateUser(User u);
        void DeleteUser(User u);
        void CreateUser(User u);
    }
}
