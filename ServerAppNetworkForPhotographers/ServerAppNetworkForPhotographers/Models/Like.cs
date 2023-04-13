namespace ServerAppNetworkForPhotographers.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public Photographer Photographer { get; set; }

        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
