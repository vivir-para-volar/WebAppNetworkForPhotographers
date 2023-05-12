﻿using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Dtos.Content;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IContentsServices
    {
        Task<GetContentDto?> GetContentById(int id);
        Task<Content> CreateContentPost(CreateContentPostDto contentPostDto);
        Task<Content> CreateContentBlog(CreateContentBlogDto contentBlogDto);
        Task<string> UpdateBlogMainPhoto(int id, IFormFile photo);
        Task DeleteContent(int id);
    }
}
