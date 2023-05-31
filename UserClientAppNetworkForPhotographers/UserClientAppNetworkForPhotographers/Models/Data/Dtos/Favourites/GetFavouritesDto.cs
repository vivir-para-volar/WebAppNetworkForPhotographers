using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites
{
    public class GetFavouritesDto
    {
        public List<GetContentForListDto> Posts { get; set; }
        public List<GetContentForListDto> Blogs { get; set; }

        public GetFavouritesDto() 
        {
            Posts = new List<GetContentForListDto>();
            Blogs = new List<GetContentForListDto>();
        }
    }
}
