using ServerAppNetworkForPhotographers.Dtos.Categories;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryDirId { get; set; }

        [JsonIgnore]
        public CategoryDir CategoryDir { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; } = new List<Content>();

        public Category() { }

        public Category(CreateCategoryDto categoryDto)
        {
            Name = categoryDto.Name;
            CategoryDirId = categoryDto.CategoryDirId;
        }

        public void Update(UpdateCategoryDto categoryDto)
        {
            Name = categoryDto.Name;
        }
    }
}
