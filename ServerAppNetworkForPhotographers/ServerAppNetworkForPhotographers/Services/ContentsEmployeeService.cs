using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;

namespace ServerAppNetworkForPhotographers.Services
{
    public class ContentsEmployeeService
    {
        private readonly DataContext _context;

        public ContentsEmployeeService(DataContext context)
        {
            _context = context;
        }

        public async Task<Content> UpdateContentStatus(int id)
        {
            var content = (await GetSimpleContentById(id)) ??
                throw new NotFoundException(nameof(Content), id);

            content.UpdateStatus();

            _context.Entry(content).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return content;
        }

        private async Task<Content?> GetSimpleContentById(int id)
        {
            return await _context.Contents.FindAsync(id);
        }
    }
}
