﻿using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ICategoriesController
    {
        Task<ActionResult<GetCategoryDto?>> GetCategoryById(int id);
        Task<ActionResult<Category>> CreateCategory(CreateCategoryDto categoryDto);
        Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDto categoryDto);
        Task<ActionResult> DeleteCategory(int id);
    }
}
