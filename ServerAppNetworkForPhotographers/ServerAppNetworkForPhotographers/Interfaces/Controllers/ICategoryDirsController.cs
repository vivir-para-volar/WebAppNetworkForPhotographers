using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ICategoryDirsController
    {
        Task<ActionResult<List<CategoryDir>>> GetAllCategoryDirsWithCategories();
        Task<ActionResult<CategoryDir>> GetCategoryDirById(int id);
        Task<ActionResult<CategoryDir>> CreateCategoryDir(CreateCategoryDirDto newCategoryDir);
        Task<ActionResult<CategoryDir>> UpdateCategoryDir(UpdateCategoryDirDto updatedCategoryDir);
        Task<ActionResult> DeleteCategoryDir(int id);
    }
}
