using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICheckInService
    {
        Task<IEnumerable<CheckIn>> GetAllAsync();
        Task<CheckIn> GetByIdAsync(string id);
        Task<CheckIn?> CreateAsync(CheckIn category);
        Task<ReplaceOneResult> UpdateAsync(CheckIn room);
        Task<DeleteResult> DeleteAsync(string id);
    }
}
