using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryDirsController : ControllerBase, ICategoryDirsController
    {
        private readonly CategoryDirsService _categoryDirsService;

        public CategoryDirsController(DataContext dataContext)
        {
            _categoryDirsService = new CategoryDirsService(dataContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDir>>> GetAllCategoryDirs()
        {
            return Ok(await _categoryDirsService.GetAllCategoryDirs());
        }

        [HttpGet("WithCategories")]
        public async Task<ActionResult<List<GetCategoryDirDto>>> GetAllCategoryDirsWithCategories()
        {
            return Ok(await _categoryDirsService.GetAllCategoryDirsWithCategories());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDirDto?>> GetCategoryDirById(int id)
        {
            return Ok(await _categoryDirsService.GetCategoryDirById(id));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDir>> CreateCategoryDir(CreateCategoryDirDto categoryDirDto)
        {
            CategoryDir categoryDir;

            try
            {
                categoryDir = await _categoryDirsService.CreateCategoryDir(categoryDirDto);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }

            return CreatedAtAction(nameof(GetCategoryDirById), new { id = categoryDir.Id }, categoryDir);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDir>> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto)
        {
            try
            {
                return Ok(await _categoryDirsService.UpdateCategoryDir(categoryDirDto));
            }
            catch (CategoryDirNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryDir(int id)
        {
            try
            {
                await _categoryDirsService.DeleteCategoryDir(id);
            }
            catch (CategoryDirNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DeleteException ex)
            {
                return Conflict(ex.Message);
            }

            return NoContent();
        }
    }
}
