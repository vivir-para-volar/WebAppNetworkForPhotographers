namespace ServerAppNetworkForPhotographers.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string? BlogPathMainPhoto { get; set; }
        public string? BlogBody { get; set; }

        public int PhotographerId { get; set; }
        public Photographer Photographer { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Category> Categories { get; set; }

        public Content()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Photos = new List<Photo>();
            Categories = new List<Category>();
        }
    }
}
