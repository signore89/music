using System.ComponentModel.DataAnnotations;

namespace Music.ViewsModels
{
    public class EditRoleViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "Укажите роль")]
        public string RoleName { get; set; }
        public string? Description { get; set; }
        public List<string>? Users { get; set; }
    }
}
