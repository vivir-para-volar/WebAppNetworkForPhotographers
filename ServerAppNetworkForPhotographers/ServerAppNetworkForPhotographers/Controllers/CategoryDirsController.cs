using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryDirsController : ControllerBase
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
        public async Task<ActionResult<CategoryDir?>> GetCategoryDirById(int id)
        {
            return Ok(await _categoryDirsService.GetCategoryDirById(id));
        }

        [HttpGet("CheckCategories/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<bool>> CheckCategories(int id)
        {
            return Ok(await _categoryDirsService.CheckCategories(id));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CategoryDir>> CreateCategoryDir(CreateCategoryDirDto categoryDirDto)
        {
            CategoryDir categoryDir;

            try
            {
                categoryDir = await _categoryDirsService.CreateCategoryDir(categoryDirDto);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new FieldResponse(ex.Field, ex.Message));
            }

            return CreatedAtAction(nameof(GetCategoryDirById), new { id = categoryDir.Id }, categoryDir);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CategoryDir>> UpdateCategoryDir(UpdateCategoryDirDto categoryDirDto)
        {
            try
            {
                return Ok(await _categoryDirsService.UpdateCategoryDir(categoryDirDto));
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
        public async Task<ActionResult> DeleteCategoryDir(int id)
        {
            try
            {
                await _categoryDirsService.DeleteCategoryDir(id);
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
