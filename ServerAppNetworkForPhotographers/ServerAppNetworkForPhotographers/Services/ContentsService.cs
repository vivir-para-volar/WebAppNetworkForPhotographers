using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.Security.Authentication;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ContentsService
    {
        private readonly DataContext _context;
        private const int _countInPart = 2;

        public ContentsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetContentForListDto>> GetUserContents(int photographerId, string typeContent, string userId, int part)
        {
            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.PhotographerId == photographerId && item.Type == typeContent)
                .OrderByDescending(item => item.CreatedAt)
                .Skip((part - 1) * _countInPart).Take(_countInPart)
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<List<GetContentForListDto>> GetPhotographerContents(int photographerId, string typeContent, string userId, int part)
        {
            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.PhotographerId == photographerId &&
                       item.Status == StatusContent.Open &&
                       item.Type == typeContent)
                .OrderByDescending(item => item.CreatedAt)
                .Skip((part - 1) * _countInPart).Take(_countInPart)
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<List<GetContentForListDto>> GetPhotographerFavouritesContents(int photographerId, string typeContent, string userId, int part)
        {
            var contents = new List<Content>();
            await _context.Favourites
                .Include(item => item.Content)
                .Include(item => item.Content.Photographer)
                .Include(item => item.Content.Categories)
                .Where(item => item.PhotographerId == photographerId &&
                               item.Content.Status == StatusContent.Open &&
                               item.Content.Type == typeContent)
                .OrderByDescending(item => item.Content.CreatedAt)
                .Skip((part - 1) * _countInPart).Take(_countInPart)
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
            if (content.Status == StatusContent.Blocked && content.Photographer.UserId != userId)
            {
                throw new AuthenticationException();
            }

            var getContent = content.ToGetContentDto();

            getContent.CountLikes = await _context.Likes.CountAsync(item => item.ContentId == content.Id);
            getContent.CountComments = await _context.Comments.CountAsync(item => item.ContentId == content.Id);
            getContent.CountFavourites = await _context.Favourites.CountAsync(item => item.ContentId == content.Id);

            await SetUserLikeAndFavourite(getContent, userId);

            return getContent;
        }

        public async Task<List<GetContentForListDto>> SearchContents(SearchDto searchDto, string typeContent, string userId, int part)
        {
            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.Type == typeContent &&
                               item.Status == StatusContent.Open &&
                               EF.Functions.Like(item.Title, $"%{searchDto.SearchData}%"))
                .OrderByDescending(item => item.CreatedAt)
                .Skip((part - 1) * _countInPart).Take(_countInPart)
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<List<GetContentForListDto>> GetNews(NewsDto newsDto, string userId, int part)
        {
            var photographer = (await GetPhotographerById(newsDto.PhotographerId)) ??
                throw new NotFoundException(nameof(Photographer), newsDto.PhotographerId);

            var subscriptions = await GetPhotographerSubscriptionsIds(photographer.Id);

            var contents = await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Where(item => item.Status == StatusContent.Open &&
                       subscriptions.Contains(item.Photographer.Id) &&
                       (newsDto.TypeContent == null ? true : item.Type == newsDto.TypeContent) &&
                       (newsDto.CategoriesIds == null ? true : item.Categories.Any(category => newsDto.CategoriesIds.Contains(category.Id))))
                .OrderByDescending(item => item.CreatedAt)
                .Skip((part - 1) * _countInPart).Take(_countInPart)
                .ToListAsync();

            return await ConvertListContents(contents, userId);
        }

        public async Task<List<GetContentForListDto>> GetOthers(OthersDto othersDto, string userId, int part)
        {
            if (othersDto.TypeSorting == TypeSorting.New)
            {
                var contents = await _context.Contents
                    .Include(item => item.Photographer)
                    .Include(item => item.Categories)
                    .Where(item => item.Likes.Count >= othersDto.CountLikeSorting &&
                           item.Status == StatusContent.Open &&
                           (othersDto.TypeContent == null ? true : item.Type == othersDto.TypeContent) &&
                           (othersDto.CategoriesIds == null ? true : item.Categories.Any(category => othersDto.CategoriesIds.Contains(category.Id))))
                    .OrderByDescending(item => item.CreatedAt)
                    .Skip((part - 1) * _countInPart).Take(_countInPart)
                    .ToListAsync();

                return await ConvertListContents(contents, userId);
            }
            else
            {
                DateTime startDate;
                switch (othersDto.PeriodSorting)
                {
                    case TypeSorting.PeriodAllTime:
                        startDate = DateTime.MinValue;
                        break;
                    case TypeSorting.PeriodDay:
                        startDate = DateTime.Now.AddDays(-1);
                        break;
                    case TypeSorting.PeriodWeek:
                        startDate = DateTime.Now.AddDays(-7);
                        break;
                    case TypeSorting.PeriodMonth:
                        startDate = DateTime.Now.AddMonths(-1);
                        break;
                    case TypeSorting.PeriodYear:
                        startDate = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        startDate = DateTime.MinValue;
                        break;
                }

                var contents = await _context.Contents
                    .Include(item => item.Photographer)
                    .Include(item => item.Categories)
                    .Where(item => item.CreatedAt >= startDate &&
                           item.Status == StatusContent.Open &&
                           (othersDto.TypeContent == null ? true : item.Type == othersDto.TypeContent) &&
                           (othersDto.CategoriesIds == null ? true : item.Categories.Any(category => othersDto.CategoriesIds.Contains(category.Id))))
                    .OrderByDescending(item => item.Likes.Count)
                    .Skip((part - 1) * _countInPart).Take(_countInPart)
                    .ToListAsync();

                return await ConvertListContents(contents, userId);
            }

        }

        public async Task<Content> CreateContent(CreateContentDto contentDto, string typeContent)
        {
            if (!await CheckExistencePhotographer(contentDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), contentDto.PhotographerId);
            }

            var categories = await GetListCategories(contentDto.CategoriesIds);

            var content = new Content(contentDto, typeContent, categories);

            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();

            return content;
        }

        public async Task<Content> UpdateContentBlog(UpdateContentBlogDto contentBlogDto)
        {
            var content = (await GetSimpleContentById(contentBlogDto.Id)) ??
                throw new NotFoundException(nameof(Content), contentBlogDto.Id);

            content.BlogBody = contentBlogDto.BlogBody;

            _context.Entry(content).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return content;
        }

        public async Task<Content> UpdateBlogMainPhoto(int id, IFormFile photo)
        {
            var content = (await GetSimpleContentById(id)) ??
                throw new NotFoundException(nameof(Content), id);

            await content.UpdateBlogMainPhoto(photo);

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

        private async Task<Photographer?> GetPhotographerById(int id)
        {
            return await _context.Photographers.FindAsync(id);
        }

        public async Task<List<int>> GetPhotographerSubscriptionsIds(int photographerId)
        {
            var subscriptions = new List<int>();
            await _context.Subscriptions
                .Include(item => item.Photographer)
                .Where(item => item.SubscriberId == photographerId)
                .ForEachAsync((item) => subscriptions.Add(item.Photographer.Id));

            return subscriptions;
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
                var getContent = content.ToGetContentForListDto();

                getContent.CountLikes = await _context.Likes.CountAsync(item => item.ContentId == content.Id);
                getContent.CountComments = await _context.Comments.CountAsync(item => item.ContentId == content.Id);
                getContent.CountFavourites = await _context.Favourites.CountAsync(item => item.ContentId == content.Id);

                if (content.Type == TypeContent.Post)
                {
                    content.Photos = await _context.Photos.Where(item => item.ContentId == content.Id).ToListAsync();
                }

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
