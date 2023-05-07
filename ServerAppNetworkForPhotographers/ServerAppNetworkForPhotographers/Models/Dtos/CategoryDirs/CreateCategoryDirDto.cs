using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs
{
    public class CreateCategoryDirDto
    {
        [Required]
        public string Name { get; set; }
    }
}
