using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class GetContentForEmployeeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string? BlogMainPhoto { get; set; }
        public string? BlogBody { get; set; }

        public GetPhotographerForListDto Photographer { get; set; }

        public List<Category> Categories { get; set; }
        public List<Photo> Photos { get; set; }

        public GetContentForEmployeeDto(Content content, GetPhotographerForListDto photographer)
        {
            Id = content.Id;
            Type = content.Type;
            Title = content.Title;
            CreatedAt = content.CreatedAt;
            Status = content.Status;
            BlogMainPhoto = content.BlogMainPhoto;
            BlogBody = content.BlogBody;

            Photographer = photographer;

            Categories = content.Categories;
            Photos = content.Photos;
        }
    }
}
