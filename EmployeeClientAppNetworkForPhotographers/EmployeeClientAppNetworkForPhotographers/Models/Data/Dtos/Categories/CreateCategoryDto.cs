using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        [Display(Name = "Название")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Папка")]
        public int CategoryDirId { get; set; }
    }
}
