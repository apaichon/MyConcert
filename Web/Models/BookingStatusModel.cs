using System;
using Newtonsoft.Json;

namespace MvcConcert.Models
{
    public class BookingStatusModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set;}

    }
}