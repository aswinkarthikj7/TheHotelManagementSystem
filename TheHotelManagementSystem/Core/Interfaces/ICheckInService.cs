using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(string id);
        Task<IEnumerable<Room>> GetByCategoryAsync(string id);
        Task<Room?> CreateAsync(Room category);
        Task<ReplaceOneResult> UpdateAsync(Room room);
        Task<DeleteResult> DeleteAsync(string id);
    }
}
