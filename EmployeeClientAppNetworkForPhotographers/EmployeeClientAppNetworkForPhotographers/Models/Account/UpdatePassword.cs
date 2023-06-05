using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Account
{
    public class UpdatePassword
    {
        public int PhotographerId { get; set; }

        [Display(Name = "Старый пароль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Должно быть длиннее 6 и короче 32 символов")]
        public string OldPassword { get; set; }

        [Display(Name = "Новый пароль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Должно быть длиннее 6 и короче 32 символов")]
        public string NewPassword { get; set; }

        [JsonIgnore]
        [Display(Name = "Подтвердите пароль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [Compare(nameof(NewPassword), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
