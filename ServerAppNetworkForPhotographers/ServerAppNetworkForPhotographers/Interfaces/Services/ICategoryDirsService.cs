using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ICategoryDirsService
    {
        Task<List<CategoryDir>> GetAllCategoryDirs();
        Task<List<GetCategoryDirDto>> GetAllCategoryDirsWithCategories();
        Task<GetCategoryDirDto?> GetCategoryDirById(int id);
        Task<CategoryDir> CreateCategoryDir(CreateCategoryDirDto categoryDirDto);
        Task<CategoryDir> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto);
        Task DeleteCategoryDir(int id);
    }
}
