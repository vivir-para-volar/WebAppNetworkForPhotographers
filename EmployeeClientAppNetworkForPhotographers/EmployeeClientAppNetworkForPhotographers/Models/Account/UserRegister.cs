﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Account
{
    public class UserRegister
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 32 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Должно быть длиннее 6 и короче 32 символов")]
        public string Password { get; set; }

        [JsonIgnore]
        [Display(Name = "Подтвердите пароль")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}
