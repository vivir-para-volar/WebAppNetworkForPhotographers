using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Categories;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase, ICategoryController
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(DataContext dataContext)
        {
            _categoriesService = new CategoriesService(dataContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoriesService.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoriesService.GetCategoryById(id);

            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDto newCategory)
        {
            Category category;

            try
            {
                category = await _categoriesService.CreateCategory(newCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            try
            {
                return await _categoriesService.UpdateCategory(updatedCategory);
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
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoriesService.DeleteCategory(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
