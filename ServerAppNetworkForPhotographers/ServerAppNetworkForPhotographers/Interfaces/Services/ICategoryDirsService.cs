using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICategoryDirsService
    {
        Task<List<CategoryDir>> GetAllCategoryDirsWithCategories();
        Task<CategoryDir?> GetCategoryDirById(int id);
        Task<CategoryDir> CreateCategoryDir(CreateCategoryDirDto newCategoryDir);
        Task<CategoryDir> UpdateCategoryDir(UpdateCategoryDirDto updatedCategoryDir);
        Task DeleteCategoryDir(int id);
    }
}
