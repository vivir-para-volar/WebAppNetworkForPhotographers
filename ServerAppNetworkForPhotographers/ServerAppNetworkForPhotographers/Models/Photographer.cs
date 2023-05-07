using ServerAppNetworkForPhotographers.Models.Dtos.Photographers;
using System.Text.Json.Serialization;

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
        public string? PhotoName { get; set; }
        public DateTime LastLoginDate { get; set; }

        [JsonIgnore]
        public PhotographerInfo PhotographerInfo { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; }

        [JsonIgnore]
        public List<Like> Likes { get; set; }

        [JsonIgnore]
        public List<Comment> Comments { get; set; }

        [JsonIgnore]
        public List<Favourite> Favourites { get; set; }

        public Photographer()
        {
            InitLists();
        }

        public Photographer(CreatePhotographerDto photographerDto)
        {
            Username = photographerDto.Username;
            Email = photographerDto.Email;
            LastLoginDate = DateTime.Now;

            InitLists();
        }

        public void Update(UpdatePhotographerDto photographerDto)
        {
            Username = photographerDto.Username;
            Email = photographerDto.Email;
            Name = photographerDto.Name;
            Country = photographerDto.Country;
            City = photographerDto.City;
        }

        private void InitLists()
        {
            Contents = new List<Content>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
            Favourites = new List<Favourite>();
        }
    }
}
