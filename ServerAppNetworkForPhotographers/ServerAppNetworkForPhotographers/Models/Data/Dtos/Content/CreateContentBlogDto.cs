﻿using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Content
{
    public class CreateContentBlogDto
    {
        [Required]
        [StringLength(256, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        public string BlogBody { get; set; }

        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Required]
        public List<int> CategoriesIds { get; set; }
    }
}
