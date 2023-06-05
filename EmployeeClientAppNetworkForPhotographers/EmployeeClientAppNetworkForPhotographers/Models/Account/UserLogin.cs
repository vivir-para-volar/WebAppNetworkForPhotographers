using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Account
{
    public class UserLogin
    {
        [Display(Name = "Логин / Email")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 32 символов")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Должно быть длиннее 6 и короче 32 символов")]
        public string Password { get; set; }
    }
}
