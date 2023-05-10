using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Dtos.Content;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ContentService : IContentServices
    {
        private readonly DataContext _context;

        public ContentService(DataContext context)
        {
            _context = context;
        }

        public async Task<GetContentDto?> GetContentById(int id)
        {
            var content = await _context.Contents
                .Include(item => item.Photos)
                .Include(item => item.Categories)
                .Include(item => item.Likes)
                .Include(item => item.Comments)
                .Include(item => item.Favourites)
                .FirstOrDefaultAsync(item => item.Id == id);

            return content != null ? await content.ToGetContentDto() : null;
        }

        public async Task<Content> CreateContentPost(CreateContentPostDto contentPostDto)
        {
            if (!await CheckExistencePhotographer(contentPostDto.PhotographerId))
            {
                throw new PhotographerNotFoundException(contentPostDto.PhotographerId);
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
                throw new PhotographerNotFoundException(contentBlogDto.PhotographerId);
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
                throw new ContentNotFoundException(id);

            await content.UpdateBlogMainPhoto(photo);

            _context.Entry(content).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return content.BlogMainPhoto;
        }

        public async Task DeleteContent(int id)
        {
            var content = (await GetSimpleContentById(id)) ??
                throw new ContentNotFoundException(id);

            content.DeleteBlogMainPhoto();

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
                if (category == null) throw new CategoryNotFoundException(categoryId);

                categories.Add(category);
            }

            return categories;
        }
    }
}
