using ServerAppNetworkForPhotographers.Files;
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
        public string? PhotoProfile { get; set; }
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

        public async Task ConvertProfilePhoto()
        {
            if (PhotoProfile != null)
            {
                PhotoProfile = await FileInteraction.GetBase64ProfilePhoto(PhotoProfile);
            }
        }

        public async Task UpdateProfilePhoto(IFormFile photo)
        {
            DeleteProfilePhoto();
            PhotoProfile = await FileInteraction.SaveProfilePhoto(photo);
        }

        public void DeleteProfilePhoto()
        {
            if (PhotoProfile != null)
            {
                FileInteraction.DeleteProfilePhoto(PhotoProfile);
            }

            PhotoProfile = null;
        }

        public async Task<GetPhotographerForList> ToGetPhotographerForList()
        {
            await ConvertProfilePhoto();
            return new GetPhotographerForList(Id, Username, Name, PhotoProfile);
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
