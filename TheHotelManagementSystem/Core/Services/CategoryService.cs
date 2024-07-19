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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        public CategoryService(IOptions<HotelDatabaseSettings> hotelDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(hotelDatabaseSettings.Value.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(hotelDatabaseSettings.Value.CategoryCollectionName);
        }

        public async Task<Category?> CreateAsync(Category category)
        {
            category.Id = ObjectId.GenerateNewId().ToString();
            await _categoryCollection.InsertOneAsync(category);
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryCollection.Find(c => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, Category category)
        {
            return await _categoryCollection.ReplaceOneAsync(s => s.Id == category.Id, category);
        }
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _categoryCollection.DeleteOneAsync(s => s.Id == id);
        }
    }
}
