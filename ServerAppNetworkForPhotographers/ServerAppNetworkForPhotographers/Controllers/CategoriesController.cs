using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Categories;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
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

        [HttpGet("CheckContents/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<bool>> CheckContents(int id)
        {
            return Ok(await _categoriesService.CheckContents(id));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GetCategoryDto>> CreateCategory(CreateCategoryDto categoryDto)
        {
            GetCategoryDto category;

            try
            {
                category = await _categoriesService.CreateCategory(categoryDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new FieldResponse(ex.Field, ex.Message));
            }

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GetCategoryDto>> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            try
            {
                return Ok(await _categoriesService.UpdateCategory(categoryDto));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new FieldResponse(ex.Field, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoriesService.DeleteCategory(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (DeleteException ex)
            {
                return Conflict(new ConflictResponse(ex.Message));
            }

            return NoContent();
        }
    }
}
