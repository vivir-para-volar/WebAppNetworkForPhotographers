namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.PhotographersInfo
{
    public class UpdatePhotographerInfoDto
    {
        public int PhotographerId { get; set; }
        public string? Description { get; set; }
        public string? Awards { get; set; }
        public string? Website { get; set; }
        public string? Vk { get; set; }
        public string? Telegram { get; set; }
        public string? WhatsApp { get; set; }
        public string? Viber { get; set; }

        public UpdatePhotographerInfoDto(Photographer photographer)
        {
            PhotographerId = photographer.Id;
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
