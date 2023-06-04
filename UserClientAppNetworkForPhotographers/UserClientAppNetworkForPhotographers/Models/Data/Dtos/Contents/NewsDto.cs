namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class NewsDto
    {
        public int PhotographerId { get; set; }
        public string? TypeContent { get; set; }
        public int[]? CategoriesIds { get; set; }

        public NewsDto() { }

        public NewsDto(int photographerId)
        {
            PhotographerId = photographerId;
        }
    }
}
