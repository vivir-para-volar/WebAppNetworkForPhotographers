using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
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

        public async Task<List<GetContentForListDto>> GetPhotographerContents(int photographerId, string typeContent)
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

            return await ConvertListContents(contents);
        }

        public async Task<List<GetContentForListDto>> GetPhotographerFavouritesContents(int photographerId, string typeContent)
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
                .Where(item => item.PhotographerId == photographerId && item.Content.Type == typeContent)
                .ForEachAsync(item => contents.Add(item.Content));

            return await ConvertListContents(contents);
        }

        public async Task<GetContentDto?> GetContentById(int id)
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

            return await content.ToGetContentDto(countLikes, countComments, countFavourites);
        }

        public async Task<List<GetContentForListDto>> SearchContents(SearchDto searchDto, string typeContent)
        {
            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.Type == typeContent && EF.Functions.Like(item.Title, $"%{searchDto.SearchData}%"))
                .ToListAsync();

            return await ConvertListContents(contents);
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

        private async Task<List<GetContentForListDto>> ConvertListContents(List<Content> contents)
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

                getContents.Add(await content.ToGetContentForListDto(countLikes, countComments, countFavourites));
            }

            return getContents;
        }
    }
}
