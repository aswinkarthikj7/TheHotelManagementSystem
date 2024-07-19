using Core.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAvailabilityService
    {
        Task<AvailabilityResponse> GetAvailabilityAsync(string categoryId, DateTime checkinDateTime, DateTime checkoutDateTime);
        Task<IEnumerable<AvailabilityResponse>> GetOverallAvailabilityStatusAsync(DateTime checkinDateTime, DateTime checkoutDateTime);
    }

}
