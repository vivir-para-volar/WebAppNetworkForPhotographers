namespace ServerAppNetworkForPhotographers.Models.Dtos.Content
{
    public class GetContentDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string? BlogMainPhoto { get; set; }
        public string? BlogBody { get; set; }

        public int PhotographerId { get; set; }

        public int CountLikes { get; set; }
        public int CountComments { get; set; }
        public int CountFavourites { get; set; }

        public List<Category> Categories { get; set; }
        public List<Photo> Photos { get; set; }

        public GetContentDto(Models.Content content, int countLikes, int countComments, int countFavourites)
        {
            Id = content.Id;
            Type = content.Type;
            Title = content.Title;
            CreatedAt = content.CreatedAt;
            Status = content.Status;
            BlogMainPhoto = content.BlogMainPhoto;
            BlogBody = content.BlogBody;

            PhotographerId = content.PhotographerId;

            CountLikes = countLikes;
            CountComments = countComments;
            CountFavourites = countFavourites;

            Categories = content.Categories;
            Photos = content.Photos;
        }
    }
}
