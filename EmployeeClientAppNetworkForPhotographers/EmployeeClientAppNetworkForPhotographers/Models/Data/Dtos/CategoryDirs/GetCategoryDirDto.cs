namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs
{
    public class GetCategoryDirDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
    }
}
