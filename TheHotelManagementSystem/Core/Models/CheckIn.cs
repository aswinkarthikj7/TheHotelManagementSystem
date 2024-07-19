using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class CheckIn
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string BookingId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RoomId { get; set; }
        public Booking Booking { get; set; }
    }
}
