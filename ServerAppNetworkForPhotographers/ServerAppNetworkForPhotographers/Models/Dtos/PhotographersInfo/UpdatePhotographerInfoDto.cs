using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Dtos.PhotographersInfo
{
    public class UpdatePhotographerInfoDto
    {
        [Range(0, int.MaxValue)]
        public int PhotographerId { get; set; }

        [StringLength(1024, MinimumLength = 2)]
        public string? Description { get; set; }

        [StringLength(1024, MinimumLength = 2)]
        public string? Awards { get; set; }

        public string? Website { get; set; }
        public string? Vk { get; set; }
        public string? Telegram { get; set; }
        public string? WhatsApp { get; set; }
        public string? Viber { get; set; }
    }
}
