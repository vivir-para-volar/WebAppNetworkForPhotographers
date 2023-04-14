﻿using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.PhotographersInfo
{
    public class UpdatePhotographerInfoDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        public string? Description { get; set; }
        public string? Awards { get; set; }
        public string? Website { get; set; }
        public string? Vk { get; set; }
        public string? Telegram { get; set; }
        public string? WhatsApp { get; set; }
        public string? Viber { get; set; }
    }
}
