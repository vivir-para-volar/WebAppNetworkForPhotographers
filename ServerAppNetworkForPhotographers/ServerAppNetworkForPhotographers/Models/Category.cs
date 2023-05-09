using ServerAppNetworkForPhotographers.Models.Dtos.Categories;
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
        public List<Content> Contents { get; set; }

        public Category()
        {
            InitLists();
        }

        public Category(CreateCategoryDto categoryDto)
        {
            InitLists();

            Name = categoryDto.Name;
            CategoryDirId = categoryDto.CategoryDirId;
        }

        public void Update(UpdateCategoryDto categoryDto)
        {
            Name = categoryDto.Name;
        }

        private void InitLists()
        {
            Contents = new List<Content>();
        }
    }
}
