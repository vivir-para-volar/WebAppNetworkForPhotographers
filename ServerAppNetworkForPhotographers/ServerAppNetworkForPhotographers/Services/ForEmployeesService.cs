using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Lists;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ForEmployeesService
    {
        private readonly DataContext _context;

        public ForEmployeesService(DataContext context)
        {
            _context = context;
        }

        public async Task<GetPhotographerForListDto?> GetPhotographerById(int id)
        {
            return (await _context.Photographers.FindAsync(id))?.ToGetPhotographerForListDto();
        }

        public async Task<GetContentForEmployeeDto?> GetContentById(int id)
        {
            return (await _context.Contents
                .Include(item => item.Photographer)
                .Include(item => item.Categories)
                .Include(item => item.Photos)
                .FirstOrDefaultAsync(item => item.Id == id))?.ToGetContentForEmployeeDto();
        }

        public async Task UpdateContentStatus(int contentId)
        {
            var content = await _context.Contents
                .Include(item => item.Complaints)
                .FirstOrDefaultAsync(item => item.Id == contentId);

            if (content == null) throw new NotFoundException(nameof(Content), contentId);
            if (content.Status != StatusPhotographer.Open) return;

            content.UpdateStatus();
            _context.Entry(content).State = EntityState.Modified;

            foreach (var complaint in content.Complaints)
            {
                if (complaint.Status != StatusComplaint.Open) continue;

                complaint.UpdateStatus();
                _context.Entry(complaint).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhotographerStatus(int photographerId)
        {
            var photographer = await _context.Photographers
                .Include(item => item.Contents)
                .Include(item => item.Complaints)
                .FirstOrDefaultAsync(item => item.Id == photographerId);

            if (photographer == null) throw new NotFoundException(nameof(Photographer), photographerId);
            if (photographer.Status != StatusPhotographer.Open) return;

            photographer.UpdateStatus();
            _context.Entry(photographer).State = EntityState.Modified;

            foreach (var content in photographer.Contents)
            {
                if (content.Status != StatusContent.Open) continue;

                content.UpdateStatus();
                _context.Entry(content).State = EntityState.Modified;
            }

            foreach (var complaint in photographer.Complaints)
            {
                if (complaint.Status != StatusComplaint.Open) continue;

                complaint.UpdateStatus();
                _context.Entry(complaint).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
