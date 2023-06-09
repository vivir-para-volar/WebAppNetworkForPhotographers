﻿using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Comments;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class GetContentDto
    {
        public int? AppUserId { get; set; }

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

        public List<Category> Categories { get; set; }
        public List<GetPhotoDto> Photos { get; set; }

        public List<GetCommentDto>? Comments { get; set; }

        public bool IsLike { get; set; }
        public bool IsFavourite { get; set; }

    }
}
