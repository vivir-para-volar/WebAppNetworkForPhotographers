using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Categories;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class Category
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
        public string Name { get; set; }

        public int CategoryDirId { get; set; }

        [JsonIgnore]
        public CategoryDir CategoryDir { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; }

        public Category()
        {
            InitLists();
        }

        public Category(GetCategoryDto categoryDto)
        {
            InitLists();

            Id = categoryDto.Id;
            Name = categoryDto.Name;
            CategoryDirId = categoryDto.CategoryDirId;
        }

        private void InitLists()
        {
            Contents = new List<Content>();
        }
    }
}
