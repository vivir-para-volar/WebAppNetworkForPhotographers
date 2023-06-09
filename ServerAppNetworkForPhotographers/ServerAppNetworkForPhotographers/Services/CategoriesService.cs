﻿using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Services
{
    public class CategoriesService
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

        public async Task<bool> CheckContents(int id)
        {
            var category = await _context.Categories.Include(item => item.Contents).FirstOrDefaultAsync(item => item.Id == id);
            return category == null ? false : category.Contents.Count != 0;
        }

        public async Task<GetCategoryDto> CreateCategory(CreateCategoryDto categoryDto)
        {
            if (!(await CheckExistenceCategoryDir(categoryDto.CategoryDirId)))
            {
                throw new NotFoundException(nameof(CategoryDir), categoryDto.CategoryDirId);
            }

            if (await CheckExistenceCategoryInDir(categoryDto.Name, categoryDto.CategoryDirId))
            {
                throw new UniqueFieldException(nameof(categoryDto.Name), categoryDto.Name);
            }

            var category = new Category(categoryDto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return await GetCategoryById(category.Id);
        }

        public async Task<GetCategoryDto> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var category = (await GetSimpleCategoryById(categoryDto.Id)) ??
                throw new NotFoundException(nameof(Category), categoryDto.Id);

            if (!(await CheckExistenceCategoryDir(categoryDto.CategoryDirId)))
            {
                throw new NotFoundException(nameof(CategoryDir), categoryDto.CategoryDirId);
            }

            if (await CheckExistenceCategoryInDir(categoryDto.Name, categoryDto.CategoryDirId, categoryDto.Id))
            {
                throw new UniqueFieldException(nameof(categoryDto.Name), categoryDto.Name);
            }

            category.Update(categoryDto);

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetCategoryById(category.Id);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.Include(item => item.Contents).FirstOrDefaultAsync(item => item.Id == id);

            if (category == null) throw new NotFoundException(nameof(Category), id);
            if (category.Contents.Count != 0) throw new DeleteException(nameof(Content));

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        private async Task<Category?> GetSimpleCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        private async Task<bool> CheckExistenceCategoryDir(int categoryDirId)
        {
            return await _context.CategoryDirs.AnyAsync(item => item.Id == categoryDirId);
        }

        private async Task<bool> CheckExistenceCategoryInDir(string name, int categoryDirId, int categoryId = -1)
        {
            return await _context.Categories
                .AnyAsync(item => item.Id != categoryId && item.CategoryDirId == categoryDirId && item.Name == name);
        }
    }
}
