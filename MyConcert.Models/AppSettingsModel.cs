using System;
using Puppy.Model;
using Newtonsoft.Json;

namespace MyConcert.Models
{
    public class AppSettingsModel
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}