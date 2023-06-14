using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs
{
    public class CreateCategoryDirDto
    {
        [Display(Name = "Название папки")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
        public string Name { get; set; }
    }
}
