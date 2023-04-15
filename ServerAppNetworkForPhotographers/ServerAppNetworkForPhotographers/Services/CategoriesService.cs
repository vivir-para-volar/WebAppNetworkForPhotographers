using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Categories;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CategoriesService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoriesService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateCategory(CreateCategoryDto newCategory)
        {
            if (!(await CheckExistenceCategoryDir(newCategory.CategoryDirId)))
            {
                throw new KeyNotFoundException("CategoryDir with this id was not found");
            }


            if (await CheckExistenceCategoryInDir(newCategory.Name, newCategory.CategoryDirId))
            {
                throw new InvalidOperationException("Category with this name already exists in this dir");
            }

            var category = new Category(newCategory);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            var category = (await GetCategoryById(updatedCategory.Id)) ??
                throw new KeyNotFoundException("Category with this id was not found");

            if (await CheckExistenceCategoryInDir(updatedCategory.Name, category.CategoryDirId, updatedCategory.Id))
            {
                throw new InvalidOperationException("Category with this name already exists in this dir");
            }

            category.Update(updatedCategory);

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = (await GetCategoryById(id)) ??
                throw new KeyNotFoundException("Category with this id was not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
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
