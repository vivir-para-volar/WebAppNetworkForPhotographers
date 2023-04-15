using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.CategoryDirs
{
    public class UpdateCategoryDirDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
