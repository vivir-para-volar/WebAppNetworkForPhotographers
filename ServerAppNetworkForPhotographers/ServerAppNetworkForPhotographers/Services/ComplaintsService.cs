using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models.Lists;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ComplaintsService : IComplaintsService
    {
        private readonly DataContext _context;

        public ComplaintsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Complaint>> GetAllComplaintsOpen()
        {
            return await _context.Complaints
                .Include(item => item.ComplaintBase)
                .Where(item => item.Status == StatusComplaint.Open)
                .ToListAsync();
        }

        public async Task<Complaint?> GetComplaintById(int id)
        {
            return await _context.Complaints.Include(item => item.ComplaintBase).FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Complaint> CreateComplaint(CreateComplaintDto complaintDto)
        {
            if (!await CheckExistenceComplaintBase(complaintDto.ComplaintBaseId))
            {
                throw new ComplaintBaseNotFoundException(complaintDto.ComplaintBaseId);
            }

            if (!await CheckExistenceContent(complaintDto.ContentId))
            {
                throw new ContentNotFoundException(complaintDto.ContentId);
            }

            var complaint = new Complaint(complaintDto);

            await _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();

            return complaint;
        }

        public async Task<Complaint> UpdateComplaintStatus(int id)
        {
            var complaint = (await GetComplaintById(id)) ??
                throw new ComplaintNotFoundException(id);

            complaint.UpdateStatus();

            _context.Entry(complaint).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return complaint;
        }

        private async Task<bool> CheckExistenceComplaintBase(int complaintBaseId)
        {
            return await _context.ComplaintsBase.AnyAsync(item => item.Id == complaintBaseId);
        }

        private async Task<bool> CheckExistenceContent(int contentId)
        {
            return await _context.Contents.AnyAsync(item => item.Id == contentId);
        }
    }
}
