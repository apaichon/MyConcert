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
         // GET api/concert
        [HttpGet("{jsonSearch}")]
        public ActionResult<Result> Get(string jsonSearch)
        {
            ConcertBLL concert = new ConcertBLL();
            IMessage message = BusinessMessage.CreateMessage(BusinessLocale.th_TH);
            Result result = concert.Get(jsonSearch, message);
            return result;
        }

        // POST api/searchConcert
        [HttpPost("SearchConcert")]
        public ActionResult<Result> PostSearchConcert([FromBody] string jsonSearch)
        {
            // SetEnvironment();
            ConcertBLL concert = new ConcertBLL();

            IMessage message = BusinessMessage.CreateMessage(BusinessLocale.th_TH);
            Result result = concert.Get(jsonSearch, message);
            return result;
        }

        private void SetEnvironment()
        {
            Environment.SetEnvironmentVariable("ENV_MODE", "DEV",EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("DATABASE_PROVIDER", "mongodb",EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("CONNECTION_DEV", "mongodb://localhost:27017",EnvironmentVariableTarget.Process);
        }
        
    }
}