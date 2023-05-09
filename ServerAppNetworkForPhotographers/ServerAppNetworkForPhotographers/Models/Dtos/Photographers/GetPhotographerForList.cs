using ServerAppNetworkForPhotographers.Files;

namespace ServerAppNetworkForPhotographers.Models.Dtos.Photographers
{
    public class GetPhotographerForList
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? PhotoProfile { get; set; }

        public GetPhotographerForList(int id, string username, string? name, string? photoProfile)
        {
            Id = id;
            Username = username;
            Name = name;
            PhotoProfile = photoProfile;
        }
    }
}
