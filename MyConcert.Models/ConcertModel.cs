using System;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class ConcertModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
        [JsonProperty("location")]
        public LocationModel Location { get; set; }
        [JsonProperty("capacity")]
        public int Capacity { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
        [JsonProperty("status")]
        public BookingStatusModel Status { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("layoutImageUrl")]
        public string LayoutImageUrl { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
         
    }
}