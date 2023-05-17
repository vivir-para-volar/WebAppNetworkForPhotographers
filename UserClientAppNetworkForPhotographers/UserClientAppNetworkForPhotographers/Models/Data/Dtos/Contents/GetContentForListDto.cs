using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class GetContentForListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string? BlogMainPhoto { get; set; }
        public string? BlogBody { get; set; }

        public int CountLikes { get; set; }
        public int CountComments { get; set; }
        public int CountFavourites { get; set; }

        public GetPhotographerForListDto Photographer { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
