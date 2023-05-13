using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CategoryDirsService : ICategoryDirsService
    {
        private readonly DataContext _context;

        public CategoryDirsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDir>> GetAllCategoryDirs()
        {
            return await _context.CategoryDirs.ToListAsync();
        }

        public async Task<List<GetCategoryDirDto>> GetAllCategoryDirsWithCategories()
        {
            var CategoryDirs = new List<GetCategoryDirDto>();

            await _context.CategoryDirs
                .Include(item => item.Categories)
                .ForEachAsync(item => CategoryDirs.Add(item.ToGetCategoryDirDto()));

            return CategoryDirs;
        }

        public async Task<GetCategoryDirDto?> GetCategoryDirById(int id)
        {
            var categoryDir = await _context.CategoryDirs
                .Include(item => item.Categories)
                .FirstOrDefaultAsync(item => item.Id == id);

            return categoryDir != null ? categoryDir.ToGetCategoryDirDto() : null;
        }

        public async Task<CategoryDir> CreateCategoryDir(CreateCategoryDirDto categoryDirDto)
        {
            if (await CheckExistenceName(categoryDirDto.Name))
            {
                throw new UniqueFieldException(nameof(categoryDirDto.Name), categoryDirDto.Name);
            }

            var categoryDir = new CategoryDir(categoryDirDto);

            await _context.CategoryDirs.AddAsync(categoryDir);
            await _context.SaveChangesAsync();

            return categoryDir;
        }

        public async Task<CategoryDir> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto)
        {
            var categoryDir = (await GetSimpleCategoryDirById(categoryDirDto.Id)) ??
                throw new NotFoundException(nameof(CategoryDir), categoryDirDto.Id);

            if (await CheckExistenceName(categoryDirDto.Name, categoryDir.Id))
            {
                throw new UniqueFieldException(nameof(categoryDirDto.Name), categoryDirDto.Name);
            }

            categoryDir.Update(categoryDirDto);

            _context.Entry(categoryDir).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return categoryDir;
        }

        public async Task DeleteCategoryDir(int id)
        {
            var categoryDir = (await GetSimpleCategoryDirById(id)) ??
                throw new NotFoundException(nameof(CategoryDir), id);

            if (await _context.Categories.AnyAsync(item => item.CategoryDirId == id))
                throw new DeleteException(nameof(Category));

            _context.CategoryDirs.Remove(categoryDir);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryDir?> GetSimpleCategoryDirById(int id)
        {
            return await _context.CategoryDirs.FindAsync(id);
        }

        private async Task<bool> CheckExistenceName(string name, int categoryDirId = -1)
        {
            return await _context.CategoryDirs.AnyAsync(item => item.Id != categoryDirId && item.Name == name);
        }
    }
}
