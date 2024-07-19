using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheHotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        [Route("GetAvailabilityOfCategory")]
        public async Task<ActionResult<AvailabilityResponse>> GetAvailabilityOfCategory(string categoryId, DateTime checkinDateTime, DateTime checkoutDateTime)
            => Ok(await _availabilityService.GetAvailabilityAsync(categoryId, checkinDateTime, checkoutDateTime));

        [HttpGet]
        [Route("GetOverallAvailabilityOfCategories")]
        public async Task<ActionResult<AvailabilityResponse>> GetOverallAvailabilityOfCategories(DateTime checkinDateTime, DateTime checkoutDateTime)
            => Ok(await _availabilityService.GetOverallAvailabilityStatusAsync(checkinDateTime, checkoutDateTime));
    }
}
