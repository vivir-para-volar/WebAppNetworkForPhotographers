using ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Models
{
    public class CategoryDir
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Category> Categories { get; set; }

        public CategoryDir()
        {
            InitLists();
        }

        public CategoryDir(CreateCategoryDirDto categoryDirDto)
        {
            InitLists();

            Name = categoryDirDto.Name;
        }

        public void Update(UpdateCategoryDirDto categoryDirDto)
        {
            Name = categoryDirDto.Name;
        }

        private void InitLists()
        {
            Categories = new List<Category>();
        }
    }
}
