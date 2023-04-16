using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ComplaintsBaseService : IComplaintsBaseService
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

        public async Task<ComplaintBase> CreateComplaintBase(CreateComplaintBaseDto newComplaintBase)
        {
            if (await CheckExistenceName(newComplaintBase.Name))
            {
                throw new InvalidOperationException("ComplaintBase with this name already exists");
            }

            var complaintBase = new ComplaintBase(newComplaintBase);

            await _context.ComplaintsBase.AddAsync(complaintBase);
            await _context.SaveChangesAsync();

            return complaintBase;
        }

        public async Task<ComplaintBase> UpdateComplaintBase(UpdateComplaintBaseDto updatedComplaintBase)
        {
            var complaintBase = (await GetComplaintBaseById(updatedComplaintBase.Id)) ??
                throw new KeyNotFoundException("ComplaintBase with this id was not found");

            if (await CheckExistenceName(updatedComplaintBase.Name, complaintBase.Id))
            {
                throw new InvalidOperationException("ComplaintBase with this name already exists");
            }

            complaintBase.Update(updatedComplaintBase);

            _context.Entry(complaintBase).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return complaintBase;
        }

        public async Task DeleteComplaintBase(int id)
        {
            var complaintBase = (await GetComplaintBaseById(id)) ??
                throw new KeyNotFoundException("ComplaintBase with this id was not found");

            _context.ComplaintsBase.Remove(complaintBase);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckExistenceName(string name, int complaintId = -1)
        {
            return await _context.ComplaintsBase.AnyAsync(item => item.Id != complaintId && item.Name == name);
        }
    }
}
