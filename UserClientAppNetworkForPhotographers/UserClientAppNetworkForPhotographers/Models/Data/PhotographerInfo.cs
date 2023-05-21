using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserClientAppNetworkForPhotographers.Models.Data
{
    public class PhotographerInfo
    {
        public int Id { get; set; }

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

        public int PhotographerId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        public PhotographerInfo() { }

        public PhotographerInfo(int photographerId)
        {
            PhotographerId = photographerId;
        }
    }
}
