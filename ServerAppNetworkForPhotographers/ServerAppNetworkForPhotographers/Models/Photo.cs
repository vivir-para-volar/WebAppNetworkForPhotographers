﻿using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoName { get; set; }

        public int ContentId { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        [JsonIgnore]
        public PhotoInfo PhotoInfo { get; set; }
    }
}
