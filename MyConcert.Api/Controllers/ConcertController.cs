using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyConcert.BLL;
using Puppy.BLL;
using Puppy.Model.Business;
using Puppy.Model.Output;
using Puppy.Model.Message;

namespace MyConcertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ConcertController : ControllerBase
    {
        public IMessage Message
        {
            get;set;
        }
        public ConcertController()
        {
           this.Message = BusinessMessage.CreateMessage(BusinessLocale.th_TH);
        }
 
        [HttpGet()]
        public ActionResult<Result> GetConcert(string concertId)
        {
            using (ConcertBLL concert = new ConcertBLL())
            {
              // string jsonSerarh = String.Format("{Id:'{0}'}",concertId);
              Result result = concert.Get("{_id:'"+concertId+"'}", this.Message);
              return result;  
            }
        }

        // POST api/concert/searchConcert
        [HttpPost("SearchConcert")]
        public ActionResult<Result> PostSearchConcert([FromBody] dynamic jsonSearch)
        {
            using(ConcertBLL concert = new ConcertBLL())
            {
                IMessage message = BusinessMessage.CreateMessage(BusinessLocale.th_TH);
                Result result = concert.Get(jsonSearch.ToString(), message);
                return result;
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