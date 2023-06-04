using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class PhotographerWithInfoDto
    {
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 32 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Должно быть длиннее 2 и короче 32 символов")]
        public string? Name { get; set; }

        [Display(Name = "Страна")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "Должно быть длиннее 2 и короче 64 символов")]
        public string? Country { get; set; }

        [Display(Name = "Город")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "Должно быть длиннее 2 и короче 64 символов")]
        public string? City { get; set; }

        public string? PhotoProfile { get; set; }



        [Display(Name = "Описание")]
        [StringLength(1024, MinimumLength = 2)]
        public string? Description { get; set; }

        [Display(Name = "Награды")]
        [StringLength(1024, MinimumLength = 2)]
        public string? Awards { get; set; }

        public string? Website { get; set; }
        public string? Vk { get; set; }
        public string? Telegram { get; set; }
        public string? WhatsApp { get; set; }
        public string? Viber { get; set; }


        public PhotographerWithInfoDto() { }

        public PhotographerWithInfoDto(Photographer photographer)
        {
            Id = photographer.Id;
            Username = photographer.Username;
            Email = photographer.Email;
            Name = photographer.Name;
            Country = photographer.Country;
            City = photographer.City;
            PhotoProfile = photographer.PhotoProfile;

            Description = photographer.PhotographerInfo.Description;
            Awards = photographer.PhotographerInfo.Awards;
            Website = photographer.PhotographerInfo.Website;
            Vk = photographer.PhotographerInfo.Vk;
            Telegram = photographer.PhotographerInfo.Telegram;
            WhatsApp = photographer.PhotographerInfo.WhatsApp;
            Viber = photographer.PhotographerInfo.Viber;
        }
    }
}
