using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyConcert.Models
{
    public class AvailableSeatModel
    {
        [JsonProperty("zoneId")]
        public string ZoneId { get; set; }
        [JsonProperty("zone")]

        public string Zone { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("totalAvailable")]
        public int TotalAvailable { get; set;}
        [JsonProperty("totalTickets")]
        public List<ConcertTicketZoneModel> TotalTickets { get; set;}


    }
}