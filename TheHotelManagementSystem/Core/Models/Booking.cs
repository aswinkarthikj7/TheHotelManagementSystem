using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string GuestName { get; set; }
        public int GuestAge { get; set; }
        public string GuestAddress { get; set; }
        public string GuestIdProofName { get; set; }
        public string GuestIdProofNumber { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public DateTime CheckoutDateTime { get; set; }
        public string? SpecialRequest { get; set; }
    }
}
