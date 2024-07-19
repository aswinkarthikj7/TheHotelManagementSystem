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
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingCollection;
        public BookingService(IOptions<HotelDatabaseSettings> hotelDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(hotelDatabaseSettings.Value.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(hotelDatabaseSettings.Value.BookingCollectionName);
        }

        public async Task<Booking?> CreateAsync(Booking booking)
        {
            booking.Id = ObjectId.GenerateNewId().ToString();
            await _bookingCollection.InsertOneAsync(booking);
            return booking;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _bookingCollection.Find(c => true).ToListAsync();
        }
        public async Task<ReplaceOneResult> UpdateAsync(Booking booking)
        {
            return await _bookingCollection.ReplaceOneAsync(s => s.Id == booking.Id, booking);
        }
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _bookingCollection.DeleteOneAsync(s => s.Id == id);
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            return await _bookingCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
