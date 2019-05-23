using System;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class ConcertTicketZoneModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("concertId")]
        public string ConcertId { get; set; }
        [JsonProperty("zone")]
        public string Zone { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("totalTickets")]
        public int TotalTickets { get; set; }
         [JsonProperty("cols")]
        public int Columns { get; set; }
         [JsonProperty("rows")]
        public int Rows { get; set; }
      
    }
}