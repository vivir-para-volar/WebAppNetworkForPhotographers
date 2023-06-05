using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ComplaintsBaseService
    {
        private readonly DataContext _context;

        public ComplaintsBaseService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ComplaintBase>> GetAllComplaintsBase()
        {
            return await _context.ComplaintsBase.ToListAsync();
        }

        public async Task<ComplaintBase?> GetComplaintBaseById(int id)
        {
            return await _context.ComplaintsBase.FindAsync(id);
        }

        public async Task<bool> CheckComplaints(int id)
        {
            return await _context.Complaints.AnyAsync(item => item.ComplaintBaseId == id);
        }

        public async Task<ComplaintBase> CreateComplaintBase(CreateComplaintBaseDto complaintBaseDto)
        {
            if (await CheckExistenceName(complaintBaseDto.Name))
            {
                throw new UniqueFieldException(nameof(complaintBaseDto.Name), complaintBaseDto.Name);
            }

            var complaintBase = new ComplaintBase(complaintBaseDto);

            await _context.ComplaintsBase.AddAsync(complaintBase);
            await _context.SaveChangesAsync();

            return complaintBase;
        }

        public async Task<ComplaintBase> UpdateComplaintBase(UpdateComplaintBaseDto complaintBaseDto)
        {
            var complaintBase = (await GetComplaintBaseById(complaintBaseDto.Id)) ??
                throw new NotFoundException(nameof(ComplaintBase), complaintBaseDto.Id);

            if (await CheckExistenceName(complaintBaseDto.Name, complaintBase.Id))
            {
                throw new UniqueFieldException(nameof(complaintBaseDto.Name), complaintBaseDto.Name);
            }

            complaintBase.Update(complaintBaseDto);

            _context.Entry(complaintBase).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return complaintBase;
        }

        public async Task DeleteComplaintBase(int id)
        {
            var complaintBase = (await GetComplaintBaseById(id)) ??
                throw new NotFoundException(nameof(ComplaintBase), id);

            if (await CheckComplaints(id))
                throw new DeleteException(nameof(Complaint));

            _context.ComplaintsBase.Remove(complaintBase);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckExistenceName(string name, int complaintId = -1)
        {
            return await _context.ComplaintsBase.AnyAsync(item => item.Id != complaintId && item.Name == name);
        }
    }
}
