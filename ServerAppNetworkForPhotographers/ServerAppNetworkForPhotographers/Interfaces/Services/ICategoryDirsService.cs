using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICategoryDirsService
    {
        Task<List<CategoryDir>> GetAllCategoryDirsWithCategories();
        Task<CategoryDir?> GetCategoryDirById(int id);
        Task<CategoryDir> CreateCategoryDir(CreateCategoryDirDto categoryDirDto);
        Task<CategoryDir> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto);
        Task DeleteCategoryDir(int id);
    }
}
