namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class GetPhotographerForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? PhotoProfile { get; set; }

        public GetPhotographerForListDto(int id, string username, string? photoProfile)
        {
            Id = id;
            Username = username;
            PhotoProfile = photoProfile;
        }
    }
}
