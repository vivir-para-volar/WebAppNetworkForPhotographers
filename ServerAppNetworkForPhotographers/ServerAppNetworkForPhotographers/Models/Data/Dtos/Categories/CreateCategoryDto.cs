﻿using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int CategoryDirId { get; set; }
    }
}