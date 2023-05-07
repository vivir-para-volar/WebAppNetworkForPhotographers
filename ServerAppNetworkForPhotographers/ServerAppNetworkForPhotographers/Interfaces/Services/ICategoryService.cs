using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<Category> CreateCategory(CreateCategoryDto newCategory);
        Task<Category> UpdateCategory(UpdateCategoryDto updatedCategory);
        Task DeleteCategory(int id);
    }
}
