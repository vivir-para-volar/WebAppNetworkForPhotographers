using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs
{
    public class CreateCategoryDirDto
    {
        [Required]
        [StringLength(64, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
