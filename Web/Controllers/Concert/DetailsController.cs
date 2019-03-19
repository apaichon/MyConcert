using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MyConcert.Models;
using Puppy.Utils;
using Puppy.Model.Output;
using Newtonsoft.Json;
using System.Net.Http;

namespace MyConcert.Controllers
{
    public class DetailsController : Controller
    {
      
        public async Task<IActionResult> Details(int concertId)
        {
            ViewData["ConcertId"] = concertId;
            RestApi api = new RestApi("https://localhost:5003/api/concertSeats?concertId="+concertId);
            var body = await api.GetAsync("");
            Result result = JsonConvert.DeserializeObject<Result>(body.Content.ToString());
            List<AvailableSeatModel> availableSeats = JsonConvert.DeserializeObject<List<AvailableSeatModel>>(result.Data);

            // Get Available Tickets
            return View(availableSeats);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
