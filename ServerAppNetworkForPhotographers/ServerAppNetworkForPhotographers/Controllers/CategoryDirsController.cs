using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.CategoryDirs;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
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
            var categoryDirs = await _categoryDirsService.GetAllCategoryDirs();

            return Ok(categoryDirs);
        }

        [HttpGet]
        [Route("{id}/categories")]
        public async Task<ActionResult<List<Category>>> GetCategoriesInCategoryDir(int id)
        {
            List<Category> categories;

            try
            {
                categories = await _categoryDirsService.GetCategoriesInCategoryDir(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDir>> GetCategoryDirById(int id)
        {
            var categoryDir = await _categoryDirsService.GetCategoryDirById(id);

            return categoryDir == null ? NotFound() : Ok(categoryDir);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDir>> CreateCategoryDir(CreateCategoryDirDto newCategoryDir)
        {
            CategoryDir categoryDir;

            try
            {
                categoryDir = await _categoryDirsService.CreateCategoryDir(newCategoryDir);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetCategoryDirById), new { id = categoryDir.Id }, categoryDir);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDir>> UpdateCategoryDir(UpdateCategoryDirDto updatedCategoryDir)
        {
            try
            {
                return await _categoryDirsService.UpdateCategoryDir(updatedCategoryDir);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryDir(int id)
        {
            try
            {
                await _categoryDirsService.DeleteCategoryDir(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
