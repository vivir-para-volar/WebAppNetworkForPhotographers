﻿using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase, IComplaintsController
    {
        private readonly ComplaintsService _complaintsService;

        public ComplaintsController(DataContext dataContext)
        {
            _complaintsService = new ComplaintsService(dataContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<Complaint>>> GetAllComplaintsOpen()
        {
            return Ok(await _complaintsService.GetAllComplaintsOpen());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Complaint?>> GetComplaintById(int id)
        {
            return Ok(await _complaintsService.GetComplaintById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Complaint>> CreateComplaint(CreateComplaintDto complaintDto)
        {
            Complaint complaint;

            try
            {
                complaint = await _complaintsService.CreateComplaint(complaintDto);
            }
            catch (ComplaintBaseNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (ContentNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return CreatedAtAction(nameof(GetComplaintById), new { id = complaint.Id }, complaint);
        }

        [HttpPut("Status/{id}")]
        public async Task<ActionResult<Complaint>> UpdateComplaintStatus(int id)
        {
            try
            {
                return Ok(await _complaintsService.UpdateComplaintStatus(id));
            }
            catch (ComplaintNotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
