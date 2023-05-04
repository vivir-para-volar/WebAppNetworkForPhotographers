using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? BlogMainPhotoName { get; set; }
        public string? BlogBody { get; set; }

        public int PhotographerId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public List<Comment> Comments { get; set; }

        [JsonIgnore]
        public List<Like> Likes { get; set; }

        [JsonIgnore]
        public List<Photo> Photos { get; set; }

        [JsonIgnore]
        public List<Category> Categories { get; set; }

        [JsonIgnore]
        public List<Complaint> Complaints { get; set; }

        [JsonIgnore]
        public List<Favourite> Favourites { get; set; }

        public Content()
        {
            InitLists();
        }

        private void InitLists()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Photos = new List<Photo>();
            Categories = new List<Category>();
            Complaints = new List<Complaint>();
            Favourites = new List<Favourite>();
        }
    }
}
