using ServerAppNetworkForPhotographers.Files;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
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
        public string Status { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public PhotographerInfo PhotographerInfo { get; set; }

        [JsonIgnore]
        public List<Complaint> Complaints { get; set; }

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
            InitLists();

            Username = photographerDto.Username;
            Email = photographerDto.Email;
            UserId = photographerDto.UserId;

            Status = StatusPhotographer.Open;
        }

        public void Update(UpdatePhotographerDto photographerDto)
        {
            Username = photographerDto.Username;
            Email = photographerDto.Email;
            Name = photographerDto.Name;
            Country = photographerDto.Country;
            City = photographerDto.City;
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

        public GetPhotographerForListDto ToGetPhotographerForListDto()
        {
            return new GetPhotographerForListDto(Id, Username, Name, PhotoProfile);
        }

        public static List<GetPhotographerForListDto> ToListGetPhotographerForListDto(List<Photographer> photographers)
        {
            var getPhotographers = new List<GetPhotographerForListDto>();
            foreach (var photographer in photographers)
            {
                getPhotographers.Add(photographer.ToGetPhotographerForListDto());
            }
            return getPhotographers;
        }

        public void UpdateStatus()
        {
            Status = StatusPhotographer.Blocked;
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
