using UserClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos
{
    public class ShowHomeDto
    {
        public List<GetContentForListDto> News { get; set; }
        public List<GetContentForListDto> Others { get; set; }

        public List<GetCategoryDirDto> CategoryDirs { get; set; }
        public int? ChooseCategory { get; set; }

        public ShowHomeDto()
        {
            News = new List<GetContentForListDto>();
            Others = new List<GetContentForListDto>();

            CategoryDirs = new List<GetCategoryDirDto>();
        }
    }
}
