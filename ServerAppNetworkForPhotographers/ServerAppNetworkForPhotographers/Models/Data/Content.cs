using ServerAppNetworkForPhotographers.Files;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
{
    public class Content
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string? BlogMainPhoto { get; set; }
        public string? BlogBody { get; set; }

        public int PhotographerId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public List<Category> Categories { get; set; }

        [JsonIgnore]
        public List<Like> Likes { get; set; }

        [JsonIgnore]
        public List<Comment> Comments { get; set; }

        [JsonIgnore]
        public List<Favourite> Favourites { get; set; }

        [JsonIgnore]
        public List<Photo> Photos { get; set; }

        [JsonIgnore]
        public List<Complaint> Complaints { get; set; }


        public Content()
        {
            InitLists();
        }

        public Content(CreateContentPostDto contentPostDto, List<Category> categories)
        {
            InitLists();

            Type = TypeContent.Post;
            CreatedAt = DateTime.Now;
            Status = StatusContent.Open;

            Title = contentPostDto.Title;
            PhotographerId = contentPostDto.PhotographerId;

            Categories = categories;
        }

        public Content(CreateContentBlogDto contentBlogDto, List<Category> categories)
        {
            InitLists();

            Type = TypeContent.Blog;
            CreatedAt = DateTime.Now;
            Status = StatusContent.Open;

            Title = contentBlogDto.Title;
            BlogBody = contentBlogDto.BlogBody;
            PhotographerId = contentBlogDto.PhotographerId;

            Categories = categories;
        }

        public async Task UpdateBlogMainPhoto(IFormFile photo)
        {
            if (Type != TypeContent.Blog)
            {
                throw new InvalidOperationException($"This {nameof(Content)} is not a {TypeContent.Blog}");
            }

            DeleteBlogMainPhoto();
            BlogMainPhoto = await FileInteraction.SaveBlogMainPhoto(photo);
        }

        public void DeleteBlogMainPhoto()
        {
            if (BlogMainPhoto != null)
            {
                FileInteraction.DeleteBlogMainPhoto(BlogMainPhoto);
            }

            BlogMainPhoto = null;
        }

        public GetContentForListDto ToGetContentForListDto()
        {
            var photographer = Photographer.ToGetPhotographerForListDto();
            return new GetContentForListDto(this, photographer);
        }

        public GetContentDto ToGetContentDto()
        {
            var photographer = Photographer.ToGetPhotographerForListDto();
            return new GetContentDto(this, photographer);
        }

        public GetContentForEmployeeDto ToGetContentForEmployeeDto()
        {
            var photographer = Photographer.ToGetPhotographerForListDto();
            return new GetContentForEmployeeDto(this, photographer);
        }

        public void UpdateStatus()
        {
            Status = StatusContent.Blocked;
        }

        private void InitLists()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Photos = new List<Photo>();
            Categories = new List<Category>();
            Complaints = new List<Complaint>();
            Favourites = new List<Favourite>();
        }
    }
}
