using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class PhotoInfo
    {
        public int Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? XResolution { get; set; }
        public int? YResolution { get; set; }
        public double? ApertureValue { get; set; }
        public int? ISOSpeedRatings { get; set; }
        public int? FocalLength { get; set; }
        public int? FocalLengthIn35mmFilm { get; set; }


        public int PhotoId { get; set; }

        [JsonIgnore]
        public Photo Photo { get; set; }
    }
}
