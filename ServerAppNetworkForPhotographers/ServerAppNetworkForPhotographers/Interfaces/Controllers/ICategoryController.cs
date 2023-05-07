using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ICategoryController
    {
        Task<ActionResult<List<Category>>> GetAllCategories();
        Task<ActionResult<Category>> GetCategoryById(int id);
        Task<ActionResult<Category>> CreateCategory(CreateCategoryDto newCategory);
        Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDto updatedCategory);
        Task<ActionResult> DeleteCategory(int id);
    }
}
