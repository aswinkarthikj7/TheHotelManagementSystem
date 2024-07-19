using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TheHotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckInController : Controller
    {
        private readonly ICheckInService _checkInService;
        private readonly IAvailabilityService _availabilityService;
        private readonly IBookingService _bookingService;
        public CheckInController(ICheckInService checkInService, IAvailabilityService availabilityService, IBookingService bookingService)
        {
            _checkInService = checkInService;
            _availabilityService = availabilityService;
            _bookingService = bookingService;
        }   

        [HttpPost]
        public async Task<ActionResult<CheckIn?>> CreateCheckInAsync(CheckIn checkIn)
        {
            if (!await IsAvailabilityConfirmed(checkIn))
            {
                return StatusCode(500, "Selected Room is not available. Please try checkIn of different category room.");
            }

            return await _checkInService.CreateAsync(checkIn);
        }

        [HttpGet]
        public async Task<ActionResult<AvailabilityResponse>> GetAllCheckIns()
            => Ok(await _checkInService.GetAllAsync());

        [HttpGet]
        [Route("GetCheckInById")]
        public async Task<ActionResult<AvailabilityResponse>> GetAllCheckInById(string id)
            => Ok(await _checkInService.GetByIdAsync(id));

        [HttpPut]
        public async Task<ActionResult<ReplaceOneResult?>> UpdateCheckIn(CheckIn checkIn)
        {
            if (!await IsAvailabilityConfirmed(checkIn))
            {
                return StatusCode(500, "Selected Room is not available. Please try checkIn of different category room.");
            }

            return await _checkInService.UpdateAsync(checkIn);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteResult?>> DeleteCheckIn(string id)
        {
            
            return await _checkInService.DeleteAsync(id);
        }

        private async Task<bool> IsAvailabilityConfirmed(CheckIn checkIn)
        {
            var booking = await _bookingService.GetByIdAsync(checkIn.BookingId);

            var availabilityResponse = await _availabilityService.GetAvailabilityAsync(booking.CategoryId, booking.CheckinDateTime, booking.CheckoutDateTime);

            if (availabilityResponse.NumberOfAvailableRooms <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
