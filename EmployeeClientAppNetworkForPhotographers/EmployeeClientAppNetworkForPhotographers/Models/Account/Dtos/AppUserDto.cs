using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Account.Dtos
{
    public class AppUserDto
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 32 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Роль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        public string Role { get; set; }
    }
}
