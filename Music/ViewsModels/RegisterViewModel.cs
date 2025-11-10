using System.ComponentModel.DataAnnotations;

namespace Music.ViewsModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль обязательно")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
