using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class CreateContentPostDto
    {
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(512, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 512 символов")]
        public string Title { get; set; }

        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        public List<int> CategoriesIds { get; set; }


        [JsonIgnore]
        public List<GetCategoryDirDto> CategoryDirs { get; set; }


        public CreateContentPostDto()
        {
            CategoryDirs = new List<GetCategoryDirDto>();
        }
    }
}
