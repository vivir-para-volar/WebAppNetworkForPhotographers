using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class CreateCategoryDto
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Папка")]
        public int CategoryDirId { get; set; }
    }
}
