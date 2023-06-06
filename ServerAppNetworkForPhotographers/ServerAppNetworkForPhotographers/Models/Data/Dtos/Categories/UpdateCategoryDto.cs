using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class UpdateCategoryDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 4)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryDirId { get; set; }
    }
}
