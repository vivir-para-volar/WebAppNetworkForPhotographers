﻿using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
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

        public PhotoInfo() { }

        public PhotoInfo(CreatePhotoInfoDto photoInfoDto)
        {
            PhotoId = photoInfoDto.PhotoId;

            Make = photoInfoDto.Make;
            Model = photoInfoDto.Model;
            Width = photoInfoDto.Width;
            Height = photoInfoDto.Height;
            XResolution = photoInfoDto.XResolution;
            YResolution = photoInfoDto.YResolution;
            ApertureValue = photoInfoDto.ApertureValue;
            ISOSpeedRatings = photoInfoDto.ISOSpeedRatings;
            FocalLength = photoInfoDto.FocalLength;
            FocalLengthIn35mmFilm = photoInfoDto.FocalLengthIn35mmFilm;
        }
    }
}
