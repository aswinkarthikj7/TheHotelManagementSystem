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
    public class RoomService : IRoomService
    {
        private readonly IMongoCollection<Room> _roomCollection;
        public RoomService(IOptions<HotelDatabaseSettings> hotelDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(hotelDatabaseSettings.Value.DatabaseName);
            _roomCollection = database.GetCollection<Room>(hotelDatabaseSettings.Value.RoomCollectionName);
        }

        public async Task<Room?> CreateAsync(Room room)
        {
            room.Id = ObjectId.GenerateNewId().ToString();
            await _roomCollection.InsertOneAsync(room);
            return room;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomCollection.Find(c => true).ToListAsync();
        }

        public async Task<Room> GetByIdAsync(string id)
        {
            return await _roomCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Room>> GetByCategoryAsync(string id)
        {
            return await _roomCollection.Find(c => c.CategoryId == id).ToListAsync();
        }

        public async Task<ReplaceOneResult> UpdateAsync(Room room)
        {
            return await _roomCollection.ReplaceOneAsync(s => s.Id == room.Id, room);
        }
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _roomCollection.DeleteOneAsync(s => s.Id == id);
        }
    }
}
