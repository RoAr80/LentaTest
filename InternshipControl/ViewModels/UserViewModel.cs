using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipControl.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //public string Password { get; set; }
        [Required]
        [Display(Name = "ФИО")]
        public string NameSurnameFathername { get; set; }
        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Название ВУЗа")]
        public string University { get; set; }
        [Required]
        [Display(Name = "Курс")]
        public int Course { get; set; }
        [Required]
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "О себе")]
        public string AdditionalInfo { get; set; }
    }
}
