using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class GetPhotographerForProfileDto
    {
        public int AppUserId { get; set; }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        public string? PhotoProfile { get; set; }
        
        public PhotographerInfo PhotographerInfo { get; set; }

        public List<GetContentForListDto> Posts { get; set; }
        public List<GetContentForListDto> Blogs { get; set; }

        public GetPhotographerForProfileDto() { }

        public GetPhotographerForProfileDto(Photographer photographer) 
        {
            Id = photographer.Id;
            Username = photographer.Username;
            Email = photographer.Email;
            Name = photographer.Name;
            Country = photographer.Country;
            City = photographer.City;

            PhotoProfile = photographer.PhotoProfile;

            PhotographerInfo = photographer.PhotographerInfo;
        }
    }
}
