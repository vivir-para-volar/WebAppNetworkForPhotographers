﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs
{
    public class UpdateCategoryDirDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
