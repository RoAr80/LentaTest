using InternshipControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipControl.Repos
{
    public interface IUserRepo
    {
        void UpdateUser(User u);

    }
}
