using System;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class BookedModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("bookedDate")]
        public DateTime BookedDate { get; set; }

        [JsonProperty("bookedBy")]
        public UserModel BookedBy {get;set;}

    }
}