using ServerAppNetworkForPhotographers.Models.Dtos.PhotographersInfo;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class PhotographerInfo
    {
        public int Id { get; set; }
        public string? Description { get; set; }
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

        public void Update(UpdatePhotographerInfoDto photographerInfoDto)
        {
            Description = photographerInfoDto.Description;
            Awards = photographerInfoDto.Awards;
            Website = photographerInfoDto.Website;
            Vk = photographerInfoDto.Vk;
            Telegram = photographerInfoDto.Telegram;
            WhatsApp = photographerInfoDto.WhatsApp;
            Viber = photographerInfoDto.Viber;
        }
    }
}
