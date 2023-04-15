using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.CategoryDirs
{
    public class CreateCategoryDirDto
    {
        [Required]
        public string Name { get; set; }
    }
}
