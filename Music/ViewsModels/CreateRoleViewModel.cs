using System.ComponentModel.DataAnnotations;

namespace Music.ViewsModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Роль")]
        public string RoleName { get; set; }
    }
}
