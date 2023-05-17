﻿using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryDirId { get; set; }
    }
}
