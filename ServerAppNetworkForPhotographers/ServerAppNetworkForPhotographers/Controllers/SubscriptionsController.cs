using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Subscriptions;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
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
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
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
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
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
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
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
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Subscribers/{photographerId}")]
        public async Task<ActionResult<List<Photographer>>> GetSubscribers(int photographerId)
        {
            try
            {
                return Ok(await _subscriptionsService.GetSubscribers(photographerId));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Subscriptions/{photographerId}")]
        public async Task<ActionResult<List<Photographer>>> GetSubscriptions(int photographerId)
        {
            try
            {
                return Ok(await _subscriptionsService.GetSubscriptions(photographerId));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
