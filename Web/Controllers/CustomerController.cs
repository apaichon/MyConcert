using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyConcert.Models;
using Puppy.Utils;
using Puppy.Model.Output;
using MyConcert.Utils;
using Newtonsoft.Json;

namespace MyConcert.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> MyTicket( )
        {
            string bookedBy = HttpContext.Request.Cookies["fbLogIn"].ToString();
            RestApi api = new RestApi("https://localhost:5003/api/concertSeats/TicketByOwner?ownerId="+bookedBy);
            api.SetHeader("Authorization", Cookies.GetToken(Request));
            var data = await api.GetAsync("");
            var responseBody = await data.Content.ReadAsStringAsync();
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);
            List<ConcertSeatModel> list = JsonConvert.DeserializeObject<List<ConcertSeatModel>>(result.Data);
            return View(list);
        }

        public IActionResult MyProfile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
