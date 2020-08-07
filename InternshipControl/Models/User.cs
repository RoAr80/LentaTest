using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipControl.Models
{
    public class User : IdentityUser
    {
        public string NameSurnameFathername { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set;}
        public string University { get; set; }
        public int Course { get; set; }
        public string Faculty { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public string WhoCreated { get; set; }
        public string WhoEdited { get; set; }
        public bool Approved { get; set; }


    }
}
