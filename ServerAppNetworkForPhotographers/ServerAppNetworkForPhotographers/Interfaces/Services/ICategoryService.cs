﻿using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<GetCategoryDto?> GetCategoryById(int id);
        Task<Category> CreateCategory(CreateCategoryDto categoryDto);
        Task<Category> UpdateCategory(UpdateCategoryDto categoryDto);
        Task DeleteCategory(int id);
    }
}
