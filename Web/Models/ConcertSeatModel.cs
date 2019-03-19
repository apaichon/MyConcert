using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyConcert.Models
{
    public class ConcertSeatModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("concertId")]
        public string ConcertId { get; set; }
        
        [JsonProperty("location")]
        public LocationModel Location { get; set; }

        [JsonProperty("zone")]
        public ConcertTicketZoneModel Zone { get; set; }

        [JsonProperty("seatNo")]
        public string SeatNo { get; set;}

        [JsonProperty("bookingStatus")]
        public BookingStatusModel BookingStatus { get; set;}

        /* [JsonProperty("bookedModel")]
        public BookedModel Booked { get; set;}
        */


    }
}