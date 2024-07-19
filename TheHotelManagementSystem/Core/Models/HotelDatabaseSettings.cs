using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HotelDatabaseSettings
    {
        public string CategoryCollectionName { get; set; } = null!;
        public string RoomCollectionName { get; set; } = null!;
        public string BookingCollectionName { get; set; } = null!;
        public string CheckInCollectionName { get; set; } = null!;

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }
}
