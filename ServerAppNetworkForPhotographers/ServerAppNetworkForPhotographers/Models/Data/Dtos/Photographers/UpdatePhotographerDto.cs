﻿using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class UpdatePhotographerDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(32, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(64, MinimumLength = 2)]
        public string? Country { get; set; }

        [StringLength(64, MinimumLength = 2)]
        public string? City { get; set; }
    }
}
