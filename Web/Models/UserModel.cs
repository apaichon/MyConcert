using System;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class UserModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("registeredDate")]
        public string RegisteredDate { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set;}

    }
}