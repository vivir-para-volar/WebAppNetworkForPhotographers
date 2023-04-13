namespace ServerAppNetworkForPhotographers.Models
{
    public class Photographer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? PathProfilePhoto { get; set; }
        public DateTime LastLoginDate { get; set; }

        public PhotographerInfo PhotographerInfo { get; set; }

        public List<Content> Contents { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }

        public Photographer()
        {
            Contents = new List<Content>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }

        public Photographer(string Username, string Email)
        {
            Contents = new List<Content>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }
}
