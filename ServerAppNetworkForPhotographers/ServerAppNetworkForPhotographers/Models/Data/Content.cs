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

        public async Task ConvertBlogMainPhoto()
        {
            if (BlogMainPhoto != null)
            {
                BlogMainPhoto = await FileInteraction.GetBase64BlogMainPhoto(BlogMainPhoto);
            }
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

        public async Task<GetContentForListDto> ToGetContentForListDto(int countLikes, int countComments, int countFavourites)
        {
            if (Type == TypeContent.Blog)
            {
                await ConvertBlogMainPhoto();
            }
            else
            {
                foreach (var photo in Photos)
                {
                    await photo.ConvertContentPhoto();
                }
            }

            var photographer = await Photographer.ToGetPhotographerForListDto();
            return new GetContentForListDto(this, countLikes, countComments, countFavourites, photographer);
        }

        public async Task<GetContentDto> ToGetContentDto(int countLikes, int countComments, int countFavourites)
        {
            if (Type == TypeContent.Blog)
            {
                await ConvertBlogMainPhoto();
            }

            var photos = new List<GetPhotoDto>();
            foreach (var photo in Photos)
            {
                var photoName = photo.PhotoContent;
                await photo.ConvertContentPhoto();

                photos.Add(new GetPhotoDto(photo.Id, photoName, photo.PhotoContent, photo.ContentId));   
            }

            var photographer = await Photographer.ToGetPhotographerForListDto();
            return new GetContentDto(this, countLikes, countComments, countFavourites, photographer, photos);
        }

        public void UpdateStatus()
        {
            if (Status == StatusContent.Open)
            {
                Status = StatusContent.Blocked;
            }
            else
            {
                Status = StatusContent.Open;
            }
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
