using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Core.Dtos
{
    public class AvailabilityResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CategoryName { get; set; }
        public int NumberOfAvailableRooms { get; set; }
    }
}
