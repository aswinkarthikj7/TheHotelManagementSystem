using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IMongoCollection<Booking> _bookingCollection;
        private readonly ICategoryService _categoryService;
        private readonly IRoomService _roomService;
        public AvailabilityService(IOptions<HotelDatabaseSettings> hotelDatabaseSettings, IMongoClient client, ICategoryService categoryService, IRoomService roomService)
        {
            var database = client.GetDatabase(hotelDatabaseSettings.Value.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(hotelDatabaseSettings.Value.BookingCollectionName);
            _roomService = roomService;
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<AvailabilityResponse>> GetOverallAvailabilityStatusAsync(DateTime checkinDateTime, DateTime checkoutDateTime)
        {
            var checkinDate = DateTime.Parse($"{checkinDateTime.ToString("yyyy-MM-dd")} 00:00:00.000");
            var checkoutDate = DateTime.Parse($"{checkoutDateTime.ToString("yyyy-MM-dd")} 23:59:59.000");
            var availabilityResponseByCategory = new List<AvailabilityResponse>();

            var categories = await _categoryService.GetAllAsync();

            foreach(var category in categories)
            {
                var availabilityResponse = await GetAvailabilityAsync(category.Id, checkinDateTime, checkoutDate);

                availabilityResponseByCategory.Add(new AvailabilityResponse { CategoryName = category.Name, NumberOfAvailableRooms = availabilityResponse.NumberOfAvailableRooms });
            }

            return availabilityResponseByCategory;
        }

        public async Task<AvailabilityResponse> GetAvailabilityAsync(string categoryId, DateTime checkinDateTime, DateTime checkoutDateTime)
        {
            var checkinDate = DateTime.Parse($"{checkinDateTime.ToString("yyyy-MM-dd")} 00:00:00.000");
            var checkoutDate = DateTime.Parse($"{checkoutDateTime.ToString("yyyy-MM-dd")} 23:59:59.000");

            var bookingsOftheCategory = await _bookingCollection.Find
                (f => f.CategoryId == categoryId 
                && checkinDate < f.CheckinDateTime
                && checkoutDate > f.CheckoutDateTime).ToListAsync();

            var roomsInTheCategory = await _roomService.GetByCategoryAsync(categoryId);

            var availabilityCount = roomsInTheCategory.Count() - bookingsOftheCategory.Count();

            return new AvailabilityResponse
            {
                NumberOfAvailableRooms = availabilityCount > 0 ? availabilityCount : 0
            };
        }
    }
}
