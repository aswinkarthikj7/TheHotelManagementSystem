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
    public class CheckInService : ICheckInService
    {
        private readonly IMongoCollection<CheckIn> _checkInCollection;
        private readonly IBookingService _bookingService;
        public CheckInService(IOptions<HotelDatabaseSettings> hotelDatabaseSettings, IMongoClient client, IBookingService bookingService)
        {
            var database = client.GetDatabase(hotelDatabaseSettings.Value.DatabaseName);
            _checkInCollection = database.GetCollection<CheckIn>(hotelDatabaseSettings.Value.CheckInCollectionName);
            _bookingService = bookingService;
        }
        public async Task<CheckIn?> CreateAsync(CheckIn checkIn)
        {
            checkIn.Id = ObjectId.GenerateNewId().ToString();
            await _checkInCollection.InsertOneAsync(checkIn);
            return checkIn;
        }
        public async Task<IEnumerable<CheckIn>> GetAllAsync()
        {
            var checkInList = await _checkInCollection.Find(c => true).ToListAsync();   
            
            await UpdateBookingDetailsAsync(checkInList);

            return checkInList;
        }
        public async Task<CheckIn> GetByIdAsync(string id)
        {
            var checkIn = await _checkInCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

            await UpdateBookingDetailsAsync(new List<CheckIn> { checkIn });

            return checkIn;
        }
        public async Task<ReplaceOneResult> UpdateAsync(CheckIn checkIn)
        {
            return await _checkInCollection.ReplaceOneAsync(s => s.Id == checkIn.Id, checkIn);
        }
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _checkInCollection.DeleteOneAsync(s => s.Id == id);
        }
        private async Task UpdateBookingDetailsAsync(IEnumerable<CheckIn> checkInList)
        {
            foreach (var checkIn in checkInList)
            {
                checkIn.Booking = await _bookingService.GetByIdAsync(checkIn.BookingId);
            }
        }
    }
}
