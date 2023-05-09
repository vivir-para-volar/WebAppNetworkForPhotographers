using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.CategoryDirs;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ICategoryDirsController
    {
        Task<ActionResult<List<CategoryDir>>> GetAllCategoryDirsWithCategories();
        Task<ActionResult<CategoryDir?>> GetCategoryDirById(int id);
        Task<ActionResult<CategoryDir>> CreateCategoryDir(CreateCategoryDirDto categoryDirDto);
        Task<ActionResult<CategoryDir>> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto);
        Task<ActionResult> DeleteCategoryDir(int id);
    }
}
