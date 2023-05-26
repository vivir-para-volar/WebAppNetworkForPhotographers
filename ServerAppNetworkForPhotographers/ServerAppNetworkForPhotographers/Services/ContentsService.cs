﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using ServerAppNetworkForPhotographers.Models.Lists;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ContentsService : IContentsServices
    {
        private readonly DataContext _context;

        public ContentsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetContentForListDto>> GetUserContents(int photographerId, string typeContent, string userId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.PhotographerId == photographerId && item.Type == typeContent)
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<List<GetContentForListDto>> GetPhotographerContents(int photographerId, string typeContent, string userId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.PhotographerId == photographerId &&
                       item.Status == StatusContent.Open &&
                       item.Type == typeContent)
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<List<GetContentForListDto>> GetPhotographerFavouritesContents(int photographerId, string typeContent, string userId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            var contents = new List<Content>();
            await _context.Favourites
                .Include(item => item.Content)
                .Include(item => item.Content.Photographer)
                .Include(item => item.Content.Categories)
                .Where(item => item.PhotographerId == photographerId &&
                               item.Content.Status == StatusContent.Open &&
                               item.Content.Type == typeContent)
                .ForEachAsync(item => contents.Add(item.Content));

            return await ConvertListContents(contents, userId);
        }

        public async Task<GetContentDto?> GetContentById(int id, string userId)
        {
            var content = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Include(item => item.Photos)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (content == null) return null;

            var countLikes = await _context.Likes.CountAsync(item => item.ContentId == content.Id);
            var countComments = await _context.Comments.CountAsync(item => item.ContentId == content.Id);
            var countFavourites = await _context.Favourites.CountAsync(item => item.ContentId == content.Id);

            var getContent = await content.ToGetContentDto(countLikes, countComments, countFavourites);
            await SetUserLikeAndFavourite(getContent, userId);

            return getContent;
        }

        public async Task<List<GetContentForListDto>> SearchContents(SearchDto searchDto, string typeContent, string userId)
        {
            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.Type == typeContent &&
                               item.Status == StatusContent.Open &&
                               EF.Functions.Like(item.Title, $"%{searchDto.SearchData}%"))
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<Content> CreateContentPost(CreateContentPostDto contentPostDto)
        {
            if (!await CheckExistencePhotographer(contentPostDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), contentPostDto.PhotographerId);
            }

            var categories = await GetListCategories(contentPostDto.CategoriesIds);

            var content = new Content(contentPostDto, categories);

            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();

            return content;
        }

        public async Task<Content> CreateContentBlog(CreateContentBlogDto contentBlogDto)
        {
            if (!await CheckExistencePhotographer(contentBlogDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), contentBlogDto.PhotographerId);
            }

            var categories = await GetListCategories(contentBlogDto.CategoriesIds);

            var content = new Content(contentBlogDto, categories);

            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();

            return content;
        }

        public async Task<string> UpdateBlogMainPhoto(int id, IFormFile photo)
        {
            var content = (await GetSimpleContentById(id)) ??
                throw new NotFoundException(nameof(Content), id);

            await content.UpdateBlogMainPhoto(photo);

            _context.Entry(content).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return content.BlogMainPhoto;
        }

        public async Task<Content> UpdateContentStatus(int id)
        {
            var content = (await GetSimpleContentById(id)) ??
                throw new NotFoundException(nameof(Content), id);

            content.UpdateStatus();

            _context.Entry(content).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return content;
        }

        public async Task DeleteContent(int id)
        {
            var content = (await GetSimpleContentById(id)) ??
               throw new NotFoundException(nameof(Content), id);

            content.DeleteBlogMainPhoto();
            Photo.DeleteAllByContentId(content.Id);

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
        }

        private async Task<Content?> GetSimpleContentById(int id)
        {
            return await _context.Contents.FindAsync(id);
        }

        private async Task<bool> CheckExistencePhotographer(int photographerId)
        {
            return await _context.Photographers.AnyAsync(item => item.Id == photographerId);
        }

        private async Task<List<Category>> GetListCategories(List<int> categoriesIds)
        {
            var categories = new List<Category>();

            foreach (var categoryId in categoriesIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null) throw new NotFoundException(nameof(Category), categoryId);

                categories.Add(category);
            }

            return categories;
        }

        private async Task<List<GetContentForListDto>> ConvertListContents(List<Content> contents, string userId)
        {
            var getContents = new List<GetContentForListDto>();

            foreach (var content in contents)
            {
                var countLikes = await _context.Likes.CountAsync(item => item.ContentId == content.Id);
                var countComments = await _context.Comments.CountAsync(item => item.ContentId == content.Id);
                var countFavourites = await _context.Favourites.CountAsync(item => item.ContentId == content.Id);

                if (content.Type == TypeContent.Post)
                {
                    content.Photos = await _context.Photos.Where(item => item.ContentId == content.Id).ToListAsync();
                }

                var getContent = await content.ToGetContentForListDto(countLikes, countComments, countFavourites);
                await SetUserLikeAndFavourite(getContent, userId);

                getContents.Add(getContent);
            }

            return getContents;
        }

        private async Task SetUserLikeAndFavourite(GetContentDto getContent, string userId)
        {
            var photographer = await GetPhotographerByUserId(userId);

            getContent.IsLike = await CheckLike(photographer.Id, getContent.Id);
            getContent.IsFavourite = await CheckFavourite(photographer.Id, getContent.Id);
        }

        private async Task SetUserLikeAndFavourite(GetContentForListDto getContent, string userId)
        {
            var photographer = await GetPhotographerByUserId(userId);

            getContent.IsLike = await CheckLike(photographer.Id, getContent.Id);
            getContent.IsFavourite = await CheckFavourite(photographer.Id, getContent.Id);
        }

        private async Task<Photographer> GetPhotographerByUserId(string userId)
        {
            return await _context.Photographers.FirstOrDefaultAsync(item => item.UserId == userId);
        }

        private async Task<bool> CheckLike(int photographerId, int contentId)
        {
            return await _context.Likes.AnyAsync(item => item.PhotographerId == photographerId && item.ContentId == contentId);
        }

        private async Task<bool> CheckFavourite(int photographerId, int contentId)
        {
            return await _context.Favourites.AnyAsync(item => item.PhotographerId == photographerId && item.ContentId == contentId);
        }
    }
}
