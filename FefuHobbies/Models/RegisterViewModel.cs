using System.ComponentModel.DataAnnotations;

namespace FefuHobbies.Models
{
	public class RegisterViewModel
    {
        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Имя")]
        //public string Name { get; set; }
        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Фамилия")]
        //public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
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
        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
