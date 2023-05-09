using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CategoryDirsService : ICategoryDirsService
    {
        private readonly DataContext _context;

        public CategoryDirsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDir>> GetAllCategoryDirsWithCategories()
        {
            return await _context.CategoryDirs.Include(item => item.Categories).ToListAsync();
        }

        public async Task<CategoryDir?> GetCategoryDirById(int id)
        {
            return await _context.CategoryDirs.Include(item => item.Categories).FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<CategoryDir> CreateCategoryDir(CreateCategoryDirDto categoryDirDto)
        {
            if (await CheckExistenceName(categoryDirDto.Name))
            {
                throw new UniqueFieldException("name", categoryDirDto.Name);
            }

            var categoryDir = new CategoryDir(categoryDirDto);

            await _context.CategoryDirs.AddAsync(categoryDir);
            await _context.SaveChangesAsync();

            return categoryDir;
        }

        public async Task<CategoryDir> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto)
        {
            var categoryDir = (await GetCategoryDirById(categoryDirDto.Id)) ??
                throw new CategoryDirNotFoundException(categoryDirDto.Id);

            if (await CheckExistenceName(categoryDirDto.Name, categoryDir.Id))
            {
                throw new UniqueFieldException("name", categoryDirDto.Name);
            }

            categoryDir.Update(categoryDirDto);

            _context.Entry(categoryDir).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return categoryDir;
        }

        public async Task DeleteCategoryDir(int id)
        {
            var categoryDir = (await GetCategoryDirById(id)) ??
                throw new CategoryDirNotFoundException(id);

            _context.CategoryDirs.Remove(categoryDir);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckExistenceName(string name, int categoryDirId = -1)
        {
            return await _context.CategoryDirs.AnyAsync(item => item.Id != categoryDirId && item.Name == name);
        }
    }
}
