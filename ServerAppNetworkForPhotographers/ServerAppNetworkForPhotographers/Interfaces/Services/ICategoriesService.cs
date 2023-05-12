using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICategoriesService
    {
        Task<GetCategoryDto?> GetCategoryById(int id);
        Task<Category> CreateCategory(CreateCategoryDto categoryDto);
        Task<Category> UpdateCategory(UpdateCategoryDto categoryDto);
        Task DeleteCategory(int id);
    }
}
