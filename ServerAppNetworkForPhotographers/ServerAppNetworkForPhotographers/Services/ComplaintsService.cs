using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ComplaintsService : IComplaintsService
    {
        private readonly DataContext _context;

        public ComplaintsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Complaint>> GetAllComplaints()
        {
            return await _context.Complaints.ToListAsync();
        }

        public async Task<Complaint?> GetComplaintById(int id)
        {
            return await _context.Complaints.FindAsync(id);
        }

        public async Task<Complaint> CreateComplaint(CreateComplaintDto newComplaint)
        {
            if (await CheckExistenceText(newComplaint.Text))
            {
                throw new InvalidOperationException("Complaint with this text already exists");
            }

            var complaint = new Complaint(newComplaint);

            await _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();

            return complaint;
        }

        public async Task<Complaint> UpdateComplaint(UpdateComplaintDto updatedComplaint)
        {
            var complaint = (await GetComplaintById(updatedComplaint.Id)) ??
                throw new KeyNotFoundException("Complaint with this id was not found");

            if (await CheckExistenceText(updatedComplaint.Text, complaint.Id))
            {
                throw new InvalidOperationException("Complaint with this text already exists");
            }

            complaint.Update(updatedComplaint);

            _context.Entry(complaint).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return complaint;
        }

        public async Task DeleteComplaint(int id)
        {
            var complaint = (await GetComplaintById(id)) ??
                throw new KeyNotFoundException("Complaint with this id was not found");

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckExistenceText(string text, int complaintId = -1)
        {
            return await _context.Complaints.AnyAsync(item => item.Id != complaintId && item.Text == text);
        }
    }
}
