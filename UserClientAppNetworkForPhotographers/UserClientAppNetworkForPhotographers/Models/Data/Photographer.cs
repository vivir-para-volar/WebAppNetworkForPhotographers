using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserClientAppNetworkForPhotographers.Models.Data
{
    public class Photographer
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
        public string Status { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public PhotographerInfo PhotographerInfo { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; }

        [JsonIgnore]
        public List<Like> Likes { get; set; }

        [JsonIgnore]
        public List<Comment> Comments { get; set; }

        [JsonIgnore]
        public List<Favourite> Favourites { get; set; }

        public Photographer()
        {
            InitLists();
        }

        private void InitLists()
        {
            Contents = new List<Content>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
            Favourites = new List<Favourite>();
        }
    }
}
