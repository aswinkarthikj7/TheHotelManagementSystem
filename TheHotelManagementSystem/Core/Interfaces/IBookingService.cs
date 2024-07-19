using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(string id);
        Task<Booking?> CreateAsync(Booking category);
        Task<ReplaceOneResult> UpdateAsync(Booking booking);
        Task<DeleteResult> DeleteAsync(string id);
    }
}
