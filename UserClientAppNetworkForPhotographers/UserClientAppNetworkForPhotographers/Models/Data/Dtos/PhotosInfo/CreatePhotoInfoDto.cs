using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo
{
    public class CreatePhotoInfoDto
    {
        [Range(1, int.MaxValue)]
        public int PhotoId { get; set; }

        [StringLength(64, MinimumLength = 1)]
        public string? Make { get; set; }

        [StringLength(64, MinimumLength = 1)]
        public string? Model { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? XResolution { get; set; }
        public int? YResolution { get; set; }
        public double? ApertureValue { get; set; }
        public int? ISOSpeedRatings { get; set; }
        public int? FocalLength { get; set; }
        public int? FocalLengthIn35mmFilm { get; set; }
    }
}
