using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyConcert.BLL;
using Puppy.BLL;
using Puppy.Model.Data;
using Puppy.Model.Business;
using Puppy.Model.Output;
using Puppy.Model.Message;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MyConcertApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ConcertSeatsController : ControllerBase
    {
        public IMessage Message
        {
            get;set;
        }
        public ConcertSeatsController()
        {
           this.Message = BusinessMessage.CreateMessage(BusinessLocale.th_TH);
        }
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult<Result> GetAvailableSeats(string concertId)
        {
            using (ConcertSeatsBLL concert = new ConcertSeatsBLL())
            {       
              Result result = concert.GetAvailableSeats(concertId, this.Message);
              return result;  
            }
        }

        [HttpGet("Zone")]
        public ActionResult<Result> GetSeatsByZoneId(string zoneId)
        {
            using (ConcertSeatsBLL concert = new ConcertSeatsBLL())
            {       
              Result result = concert.GetSeatsByZoneId(zoneId, this.Message);
              return result;  
            }
        }

        [HttpGet("AvailableSeats")]
        public ActionResult<Result> GetAvailableSeatsByZoneId(string zoneId)
        {
            using (ConcertSeatsBLL concert = new ConcertSeatsBLL())
            {       
              Result result = concert.GetAvailableSeatsByZoneId(zoneId, this.Message);
              return result;  
            }
        }

        [HttpGet("TicketByOwner")]
        public ActionResult<Result> GetTicketByOwner(string ownerId)
        {
            using (ConcertSeatsBLL concert = new ConcertSeatsBLL())
            {       
              Result result = concert.GetTicketByOwner(ownerId, this.Message);
              return result;  
            }
        }

        [HttpPost("BookingSeats")]
        public async Task<ActionResult<Result>> BookingSeats()
        {
            string json =""; 
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {  
               json = await reader.ReadToEndAsync();
            }

            dynamic product = new JObject();
            product.seatIds = "";
            product.total = 0;
            product.booking ="";;

            var condition = JsonConvert.DeserializeObject<JObject>(json);
            using (ConcertSeatsBLL concert = new ConcertSeatsBLL())
            {   
              return concert.BookingSeat(Convert.ToInt32(condition.Property("total").Value), condition.Property("seatIds").Value.ToString(), condition.Property("booking").Value.ToString(), this.Message);
            }
        }



       
        private void SetEnvironment()
        {
            Environment.SetEnvironmentVariable("ENV_MODE", "DEV",EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("DATABASE_PROVIDER", "mongodb",EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("CONNECTION_DEV", "mongodb://localhost:27017",EnvironmentVariableTarget.Process);
        }
        
    }
}