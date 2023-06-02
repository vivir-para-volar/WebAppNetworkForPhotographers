using Microsoft.AspNetCore.Mvc;
using UserClientAppNetworkForPhotographers.API.ApiRequests;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;

namespace UserClientAppNetworkForPhotographers.Controllers
{
    public class SubscriptionsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<int>> GetCountSubscribers(int photographerId)
        {
            int count;

            try
            {
                count = await ApiSubscriptions.GetCountSubscribers(photographerId, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, count);
        }

        public async Task<ActionResult<int>> GetCountSubscriptions(int photographerId)
        {
            int count;

            try
            {
                count = await ApiSubscriptions.GetCountSubscriptions(photographerId, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, count);
        }

        public async Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscribers(int photographerId)
        {
            List<GetPhotographerForListDto> photographersForList;

            try
            {
                photographersForList = await ApiSubscriptions.GetSubscribers(photographerId, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, photographersForList);
        }

        public async Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscriptions(int photographerId)
        {
            List<GetPhotographerForListDto> photographersForList;

            try
            {
                photographersForList = await ApiSubscriptions.GetSubscriptions(photographerId, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status200OK, photographersForList);
        }


        public async Task<ActionResult> Check(int photographerId)
        {
            var subscriptionDto = new SubscriptionDto(photographerId, AppUser.GetPhotographerId(HttpContext));

            try
            {
                await ApiSubscriptions.Check(subscriptionDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public async Task<ActionResult> Create(int photographerId)
        {
            var subscriptionDto = new SubscriptionDto(photographerId, AppUser.GetPhotographerId(HttpContext));

            try
            {
                await ApiSubscriptions.Create(subscriptionDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int photographerId)
        {
            var subscriptionDto = new SubscriptionDto(photographerId, AppUser.GetPhotographerId(HttpContext));

            try
            {
                await ApiSubscriptions.Delete(subscriptionDto, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.Status, ex.Message);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
