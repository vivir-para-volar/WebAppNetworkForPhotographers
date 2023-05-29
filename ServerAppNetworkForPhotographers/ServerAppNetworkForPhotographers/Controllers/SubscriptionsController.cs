using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class SubscriptionsController : ControllerBase
    {
        private readonly SubscriptionsService _subscriptionsService;

        public SubscriptionsController(DataContext dataContext)
        {
            _subscriptionsService = new SubscriptionsService(dataContext);
        }

        [HttpPost("Check")]
        public async Task<ActionResult<bool>> CheckSubscription(SubscriptionDto subscriptionDto)
        {
            return Ok(await _subscriptionsService.CheckSubscription(subscriptionDto));
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> CreateSubscription(SubscriptionDto subscriptionDto)
        {
            Subscription subscription;

            try
            {
                subscription = await _subscriptionsService.CreateSubscription(subscriptionDto);
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

            return CreatedAtAction(null, subscription);
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

        [HttpGet("CountSubscribers/{photographerId}")]
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

        [HttpGet("CountSubscriptions/{photographerId}")]
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

        [HttpGet("Subscribers/{photographerId}")]
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

        [HttpGet("Subscriptions/{photographerId}")]
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
