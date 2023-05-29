using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CommentsService
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

            return Comment.ToListGetCommentDto(comments);
        }

        public async Task<List<GetCommentDto>> GetNewContentComments(int contentId, DateTime startTime, string userId)
        {
            if (!await CheckExistenceContent(contentId))
            {
                throw new NotFoundException(nameof(Content), contentId);
            }

            var comments = await _context.Comments
                .Include(item => item.Photographer)
                .Where(item => item.ContentId == contentId && item.CreatedAt >= startTime && item.Photographer.UserId != userId)
                .OrderBy(item => item.CreatedAt)
                .ToListAsync();

            return Comment.ToListGetCommentDto(comments);
        }

        public async Task<GetCommentDto> CreateComment(CreateCommentDto commentDto)
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

            var getComment = (await GetCommentById(comment.Id)) ??
                throw new NotFoundException(nameof(Comment), comment.Id);

            return getComment;
        }

        public async Task DeleteComment(int id)
        {
            var comment = (await GetSimpleCommentById(id)) ??
                throw new NotFoundException(nameof(Comment), id);

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        private async Task<GetCommentDto?> GetCommentById(int id)
        {
            var comment = await _context.Comments
                .Include(item => item.Photographer)
                .FirstOrDefaultAsync(item => item.Id == id);

            return comment != null ? comment.ToGetCommentDto() : null;
        }

        private async Task<Comment?> GetSimpleCommentById(int id)
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
