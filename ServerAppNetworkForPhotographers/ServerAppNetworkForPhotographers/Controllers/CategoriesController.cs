using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase, ICategoriesController
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(DataContext dataContext)
        {
            _categoriesService = new CategoriesService(dataContext);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto?>> GetCategoryById(int id)
        {
            return Ok(await _categoriesService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDto categoryDto)
        {
            Category category;

            try
            {
                category = await _categoriesService.CreateCategory(categoryDto);
            }
            catch (CategoryDirNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            try
            {
                return Ok(await _categoriesService.UpdateCategory(categoryDto));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
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
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoriesService.DeleteCategory(id);
            }
            catch (CategoryNotFoundException ex)
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
