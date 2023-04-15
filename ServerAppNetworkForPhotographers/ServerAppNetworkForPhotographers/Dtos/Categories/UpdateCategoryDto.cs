using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.Categories
{
    public class UpdateCategoryDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
