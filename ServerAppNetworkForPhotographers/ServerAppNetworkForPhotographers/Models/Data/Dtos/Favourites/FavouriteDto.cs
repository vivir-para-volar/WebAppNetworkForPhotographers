﻿using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites
{
    public class FavouriteDto
    {
        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Range(1, int.MaxValue)]
        public int ContentId { get; set; }
    }
}
