﻿using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites
{
    public class ShowFavouritesDto
    {
        public List<GetContentForListDto> Posts { get; set; }
        public List<GetContentForListDto> Blogs { get; set; }

        public ShowFavouritesDto()
        {
            Posts = new List<GetContentForListDto>();
            Blogs = new List<GetContentForListDto>();
        }
    }
}
