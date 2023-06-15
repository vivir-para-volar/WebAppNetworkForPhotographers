using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class CreateContentBlogDto
    {
        [Required]
        [StringLength(512, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        public string BlogBody { get; set; }

        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Required]
        public List<int> CategoriesIds { get; set; }


        [JsonIgnore]
        public List<GetCategoryDirDto> CategoryDirs { get; set; }


        public CreateContentBlogDto()
        {
            CategoryDirs = new List<GetCategoryDirDto>();
        }
    }
}
