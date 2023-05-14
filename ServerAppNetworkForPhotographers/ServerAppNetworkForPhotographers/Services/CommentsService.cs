using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly DataContext _context;

        public CommentsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetCommentDto>> GetAllContentComments(int contentId)
        {
            if (!await CheckExistenceContent(contentId))
            {
                throw new NotFoundException(nameof(Content), contentId);
            }

            var comments = await _context.Comments
                .Include(item => item.Photographer)
                .Where(item => item.ContentId == contentId)
                .OrderBy(item => item.CreatedAt)
                .ToListAsync();

            return await Comment.ToListGetCommentDto(comments);
        }

        public async Task<Comment> CreateComment(CreateCommentDto commentDto)
        {
            if (!await CheckExistencePhotographer(commentDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), commentDto.PhotographerId);
            }
            if (!await CheckExistenceContent(commentDto.ContentId))
            {
                throw new NotFoundException(nameof(Content), commentDto.ContentId);
            }

            var comment = new Comment(commentDto);

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task DeleteComment(int id)
        {
            var comment = (await GetComment(id)) ??
                throw new NotFoundException(nameof(Comment), id);

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        private async Task<Comment?> GetComment(int id)
        {
            return await _context.Comments.FindAsync(id);
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
