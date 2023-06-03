using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models.Lists;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ComplaintsService
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
                throw new NotFoundException(nameof(ComplaintBase), complaintDto.ComplaintBaseId);
            }

            var content = (await GetContentById(complaintDto.ContentId)) ??
                throw new NotFoundException(nameof(Content), complaintDto.ContentId);

            var complaint = new Complaint(complaintDto, content.PhotographerId);

            await _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();

            return complaint;
        }

        public async Task<Complaint> UpdateComplaintStatus(int id)
        {
            var complaint = (await GetComplaintById(id)) ??
                throw new NotFoundException(nameof(Complaint), id);

            complaint.UpdateStatus();

            _context.Entry(complaint).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return complaint;
        }

        private async Task<bool> CheckExistenceComplaintBase(int complaintBaseId)
        {
            return await _context.ComplaintsBase.AnyAsync(item => item.Id == complaintBaseId);
        }

        private async Task<Content?> GetContentById(int contentId)
        {
            return await _context.Contents.FindAsync(contentId);
        }
    }
}
