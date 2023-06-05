using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs
{
    public class CreateCategoryDirDto
    {
        [Required]
        public string Name { get; set; }
    }
}
