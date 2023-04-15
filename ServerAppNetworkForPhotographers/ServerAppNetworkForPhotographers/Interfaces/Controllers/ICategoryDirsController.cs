using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.CategoryDirs;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ICategoryDirsController
    {
        Task<ActionResult<List<CategoryDir>>> GetAllCategoryDirs();
        Task<ActionResult<List<Category>>> GetCategoriesInCategoryDir(int id);
        Task<ActionResult<CategoryDir>> GetCategoryDirById(int id);
        Task<ActionResult<CategoryDir>> CreateCategoryDir(CreateCategoryDirDto newCategoryDir);
        Task<ActionResult<CategoryDir>> UpdateCategoryDir(UpdateCategoryDirDto updatedCategoryDir);
        Task<ActionResult> DeleteCategoryDir(int id);
    }
}
