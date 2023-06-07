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

        public async Task<List<GetPhotographerCountComplaints>> GetCountPhotographersComplaintsOpen()
        {
            var getPhotographers = new List<GetPhotographerCountComplaints>();

            var photographers = await _context.Photographers
                .Include(item => item.Complaints)
                .Where(item => item.Status == StatusPhotographer.Open && item.Complaints.Count > 0)
                .ToListAsync();

            photographers.ForEach(photographer =>
            {
                int count = photographer.Complaints.Where(item => item.Status == StatusComplaint.Open).Count();

                if (count > 0)
                {
                    getPhotographers.Add(new GetPhotographerCountComplaints(photographer.Id, photographer.Username, count));
                }
            });


            return getPhotographers;
        }

        public async Task<List<GetContentWithCountComplaints>> GetPhotographerContentsWithCountComplaints(int photographerId)
        {
            var getContents = new List<GetContentWithCountComplaints>();

            var contents = await _context.Contents
                .Include(item => item.Complaints)
                .Where(item => item.PhotographerId == photographerId && item.Status == StatusContent.Open && item.Complaints.Count > 0)
                .ToListAsync();

            contents.ForEach(content =>
            {
                int count = content.Complaints.Where(item => item.Status == StatusComplaint.Open).Count();

                if (count > 0)
                {
                    getContents.Add(new GetContentWithCountComplaints(content.Id, content.Type, count));
                }
            });

            return getContents;
        }

        public async Task<List<Complaint>> GetComplaintsOpenForContent(int contentId)
        {
            return await _context.Complaints
                .Include(item => item.ComplaintBase)
                .Where(item => item.ContentId == contentId && item.Status == StatusComplaint.Open)
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

        public async Task UpdateComplaintStatus(int id)
        {
            var complaint = (await GetComplaintById(id)) ??
                throw new NotFoundException(nameof(Complaint), id);

            if (complaint.Status != StatusComplaint.Open) return;

            complaint.UpdateStatus();

            _context.Entry(complaint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAllComplaintsStatusForContent(int contentId)
        {
            var complaints = await GetComplaintsOpenForContent(contentId);

            foreach (var complaint in complaints)
            {
                if (complaint.Status != StatusComplaint.Open) continue;

                complaint.UpdateStatus();
                _context.Entry(complaint).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
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
