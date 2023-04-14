using ServerAppNetworkForPhotographers.Dtos.Photographers;
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
        public string? PathProfilePhoto { get; set; }
        public DateTime LastLoginDate { get; set; }

        [JsonIgnore]
        public PhotographerInfo PhotographerInfo { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; } = new List<Content>();

        [JsonIgnore]
        public List<Like> Likes { get; set; } = new List<Like>();

        [JsonIgnore]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Photographer() { }

        public Photographer(CreatePhotographerDto photographerDto)
        {
            Username = photographerDto.Username;
            Email = photographerDto.Email;
            LastLoginDate = DateTime.Now;
        }

        public void Update(UpdatePhotographerDto photographerDto)
        {
            Username = photographerDto.Username;
            Email = photographerDto.Email;
            Name = photographerDto.Name;
            Country = photographerDto.Country;
            City = photographerDto.City;
        }
    }
}
