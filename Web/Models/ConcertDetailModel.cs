using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyConcert.Models
{
    public class ConcertDetailModel
    {
       public ConcertModel Concert {get;set;}
       public List<AvailableSeatModel> AvailableSeats {get;set;}
       
         
    }
}