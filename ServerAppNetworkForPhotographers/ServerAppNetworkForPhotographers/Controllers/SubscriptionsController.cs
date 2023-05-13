using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase, ISubscriptionsController
    {
        private readonly SubscriptionsService _subscriptionsService;

        public SubscriptionsController(DataContext dataContext)
        {
            _subscriptionsService = new SubscriptionsService(dataContext);
        }

        [HttpPost]
        [Route("Check")]
        public async Task<ActionResult<bool>> CheckSubscription(SubscriptionDto subscriptionDto)
        {
            return Ok(await _subscriptionsService.CheckSubscription(subscriptionDto));
        }

        [HttpPost]
        public async Task<ActionResult> CreateSubscription(SubscriptionDto subscriptionDto)
        {
            try
            {
                await _subscriptionsService.CreateSubscription(subscriptionDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new BadResponse(ex.Message));
            }
            catch (UniqueModelException ex)
            {
                return Conflict(new ConflictResponse(ex.Message));
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSubscription(SubscriptionDto subscriptionDto)
        {
            try
            {
                await _subscriptionsService.DeleteSubscription(subscriptionDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }

        [HttpGet]
        [Route("CountSubscribers/{photographerId}")]
        public async Task<ActionResult<int>> GetCountSubscribers(int photographerId)
        {
            try
            {
                return Ok(await _subscriptionsService.GetCountSubscribers(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("CountSubscriptions/{photographerId}")]
        public async Task<ActionResult<int>> GetCountSubscriptions(int photographerId)
        {
            try
            {
                return Ok(await _subscriptionsService.GetCountSubscriptions(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("Subscribers/{photographerId}")]
        public async Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscribers(int photographerId)
        {
            try
            {
                return Ok(await _subscriptionsService.GetSubscribers(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("Subscriptions/{photographerId}")]
        public async Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscriptions(int photographerId)
        {
            try
            {
                return Ok(await _subscriptionsService.GetSubscriptions(photographerId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
