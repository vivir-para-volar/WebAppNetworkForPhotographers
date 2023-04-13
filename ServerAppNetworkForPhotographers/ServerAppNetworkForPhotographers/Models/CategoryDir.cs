namespace ServerAppNetworkForPhotographers.Models
{
    public class CategoryDir
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Category> Categories { get; set; }

        public CategoryDir()
        {
            Categories = new List<Category>();
        }
    }
}
