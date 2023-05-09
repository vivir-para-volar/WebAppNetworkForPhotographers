using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CategoriesService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoriesService(DataContext context)
        {
            _context = context;
        }

        public async Task<GetCategoryDto?> GetCategoryById(int id)
        {
            var category = await _context.Categories.Include(item => item.CategoryDir).FirstOrDefaultAsync(item => item.Id == id);
            return category != null ? category.ToGetCategoryDto() : null;
        }

        public async Task<Category> CreateCategory(CreateCategoryDto categoryDto)
        {
            if (!(await CheckExistenceCategoryDir(categoryDto.CategoryDirId)))
            {
                throw new CategoryDirNotFoundException(categoryDto.CategoryDirId);
            }

            if (await CheckExistenceCategoryInDir(categoryDto.Name, categoryDto.CategoryDirId))
            {
                throw new UniqueFieldException("name (in this dir)", categoryDto.Name);
            }

            var category = new Category(categoryDto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var category = (await GetSimpleCategoryById(categoryDto.Id)) ??
                throw new CategoryNotFoundException(categoryDto.Id);

            if (!(await CheckExistenceCategoryDir(categoryDto.CategoryDirId)))
            {
                throw new CategoryDirNotFoundException(categoryDto.CategoryDirId);
            }

            if (await CheckExistenceCategoryInDir(categoryDto.Name, categoryDto.CategoryDirId, categoryDto.Id))
            {
                throw new UniqueFieldException("name (in this dir)", categoryDto.Name);
            }

            category.Update(categoryDto);

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = (await GetSimpleCategoryById(id)) ??
                throw new CategoryNotFoundException(id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetSimpleCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(item => item.Id == id);
        }

        private async Task<bool> CheckExistenceCategoryDir(int categoryId)
        {
            return await _context.CategoryDirs.AnyAsync(item => item.Id == categoryId);
        }

        private async Task<bool> CheckExistenceCategoryInDir(string name, int categoryDirId, int categoryId = -1)
        {
            return await _context.Categories
                .AnyAsync(item => item.Id != categoryId && item.CategoryDirId == categoryDirId && item.Name == name);
        }
    }
}
