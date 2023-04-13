namespace ServerAppNetworkForPhotographers.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string PathPhoto { get; set; }

        public int ContentId { get; set; }
        public Content Content { get; set; }

        public PhotoInfo PhotoInfo { get; set; }
    }
}
