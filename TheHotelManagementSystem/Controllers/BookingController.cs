using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TheHotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult<Booking?>> CreateBooking(Booking booking)
        {
            return await _bookingService.CreateAsync(booking);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
            => Ok(await _bookingService.GetAllAsync());

        [HttpPut]
        public async Task<ActionResult<ReplaceOneResult?>> UpdateBooking(Booking booking)
        {
            return await _bookingService.UpdateAsync(booking);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteResult?>> DeleteBooking(string id)
        {
            return await _bookingService.DeleteAsync(id);
        }
    }
}
