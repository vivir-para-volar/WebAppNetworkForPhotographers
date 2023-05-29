using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Services
{
    public class LikesService
    {
        private readonly DataContext _context;

        public LikesService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetPhotographerForListDto>> GetAllContentLikes(int contentId)
        {
            if (!await CheckExistenceContent(contentId))
            {
                throw new NotFoundException(nameof(Content), contentId);
            }

            var photographers = new List<Photographer>();
            await _context.Likes
                .Include(item => item.Photographer)
                .Where(item => item.ContentId == contentId)
                .ForEachAsync((item) => photographers.Add(item.Photographer));

            return Photographer.ToListGetPhotographerForListDto(photographers);
        }

        public async Task<Like> CreateLike(LikeDto likeDto)
        {
            if (!await CheckExistencePhotographer(likeDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), likeDto.PhotographerId);
            }
            if (!await CheckExistenceContent(likeDto.ContentId))
            {
                throw new NotFoundException(nameof(Content), likeDto.ContentId);
            }

            if ((await GetLike(likeDto)) != null)
            {
                throw new UniqueModelException(nameof(Like));
            }

            var like = new Like(likeDto);

            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();

            return like;
        }

        public async Task DeleteLike(LikeDto likeDto)
        {
            var like = (await GetLike(likeDto)) ??
                throw new NotFoundException(likeDto);

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        private async Task<Like?> GetLike(LikeDto likeDto)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(item => item.PhotographerId == likeDto.PhotographerId && item.ContentId == likeDto.ContentId);
        }

        private async Task<bool> CheckExistencePhotographer(int photographerId)
        {
            return await _context.Photographers.AnyAsync(item => item.Id == photographerId);
        }

        private async Task<bool> CheckExistenceContent(int contentId)
        {
            return await _context.Contents.AnyAsync(item => item.Id == contentId);
        }
    }
}
