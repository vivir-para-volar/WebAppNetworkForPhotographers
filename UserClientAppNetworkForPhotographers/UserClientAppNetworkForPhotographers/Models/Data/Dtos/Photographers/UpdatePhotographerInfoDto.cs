namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers
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

        public UpdatePhotographerInfoDto(PhotographerWithInfoDto photographer)
        {
            PhotographerId = photographer.Id;
            Description = photographer.Description;
            Awards = photographer.Awards;
            Website = photographer.Website;
            Vk = photographer.Vk;
            Telegram = photographer.Telegram;
            WhatsApp = photographer.WhatsApp;
            Viber = photographer.Viber;
        }
    }
}
