using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class GetCategoryDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
        public string Name { get; set; }

        public int CategoryDirId { get; set; }

        [Display(Name = "Папка")]
        public CategoryDir CategoryDir { get; set; }
    }
}
