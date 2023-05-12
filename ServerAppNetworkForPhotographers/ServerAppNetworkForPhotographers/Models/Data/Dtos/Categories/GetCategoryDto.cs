namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryDirId { get; set; }
        public CategoryDir CategoryDir { get; set; }

        public GetCategoryDto(int id, string name, CategoryDir categoryDir)
        {
            Id = id;
            Name = name;
            CategoryDirId = categoryDir.Id;
            CategoryDir = categoryDir;
        }
    }
}
