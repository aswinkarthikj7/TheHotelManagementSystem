using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(string id);
        Task<Category?> CreateAsync(Category category);
        Task<ReplaceOneResult> UpdateAsync(string id, Category category);
        Task<DeleteResult> DeleteAsync(string id);
    }
}
