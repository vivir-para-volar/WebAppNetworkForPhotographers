using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintsBaseController : ControllerBase
    {
        private readonly ComplaintsBaseService _complaintsBaseService;

        public ComplaintsBaseController(DataContext dataContext)
        {
            _complaintsBaseService = new ComplaintsBaseService(dataContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<ComplaintBase>>> GetAllComplaintsBase()
        {
            return Ok(await _complaintsBaseService.GetAllComplaintsBase());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintBase?>> GetComplaintBaseById(int id)
        {
            return Ok(await _complaintsBaseService.GetComplaintBaseById(id));
        }

        [HttpGet("CheckComplaints/{id}")]
        public async Task<ActionResult<bool>> CheckComplaints(int id)
        {
            return Ok(await _complaintsBaseService.CheckComplaints(id));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ComplaintBase>> CreateComplaintBase(CreateComplaintBaseDto complaintBaseDto)
        {
            ComplaintBase complaintBase;

            try
            {
                complaintBase = await _complaintsBaseService.CreateComplaintBase(complaintBaseDto);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(new FieldResponse(ex.Field, ex.Message));
            }

            return CreatedAtAction(nameof(GetComplaintBaseById), new { id = complaintBase.Id }, complaintBase);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ComplaintBase>> UpdateComplaintBase(UpdateComplaintBaseDto complaintBaseDto)
        {
            try
            {
                return Ok(await _complaintsBaseService.UpdateComplaintBase(complaintBaseDto));
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
        public async Task<ActionResult> DeleteComplaintBase(int id)
        {
            try
            {
                await _complaintsBaseService.DeleteComplaintBase(id);
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
