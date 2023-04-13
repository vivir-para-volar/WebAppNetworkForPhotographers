namespace ServerAppNetworkForPhotographers.Models
{
    public class PhotoInfo
    {
        public int Id { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}
