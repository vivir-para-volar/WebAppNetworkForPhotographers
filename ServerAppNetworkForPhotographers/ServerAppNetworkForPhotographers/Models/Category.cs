namespace ServerAppNetworkForPhotographers.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryDirId { get; set; }
        public CategoryDir CategoryDir { get; set; }

        public List<Content> Contents { get; set; }

        public Category() { 
            Contents = new List<Content>();
        }
    }
}
