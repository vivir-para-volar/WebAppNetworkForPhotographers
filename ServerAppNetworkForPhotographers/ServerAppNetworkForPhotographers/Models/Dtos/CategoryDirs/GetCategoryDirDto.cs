namespace ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs
{
    public class GetCategoryDirDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; }

        public GetCategoryDirDto(int id, string name, List<Category> categories)
        {
            Id = id;
            Name = name;
            Categories = categories;
        }
    }
}
