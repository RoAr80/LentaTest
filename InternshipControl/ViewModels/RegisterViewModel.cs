using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipControl.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string NameSurnameFathername { get; set; }

        [Required]
        [DataType(DataType.Date)]
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
        [Display(Name = "Информация о себе")]
        public string AdditionalInfo { get; set; }
    }
}
