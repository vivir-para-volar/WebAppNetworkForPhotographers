namespace ServerAppNetworkForPhotographers.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }

        public int PhotographerId { get; set; }
        public Photographer Photographer { get; set; }

        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
