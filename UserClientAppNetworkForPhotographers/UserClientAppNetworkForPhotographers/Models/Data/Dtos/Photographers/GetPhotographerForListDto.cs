namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class GetPhotographerForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? PhotoProfile { get; set; }
    }
}
