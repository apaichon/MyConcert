using System;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class BookingStatusModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("bookedDate")]
        public string BookedDate { get; set; }

         [JsonProperty("bookedBy")]
        public string BookedBy { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set;}

    }
}