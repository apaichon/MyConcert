using System;
using Puppy.Model;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class UserModel:IModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("firstName")]
        public string FirstName {get;set;}
        [JsonProperty("lastName")]
        public string LastName {get;set;}
           
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("token")]
        public string Token {get;set;}
        [JsonProperty("registeredDate")]
        public string RegisteredDate { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set;}
    }
}
