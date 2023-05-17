using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs
{
    public class CreateCategoryDirDto
    {
        [Required]
        public string Name { get; set; }
    }
}
