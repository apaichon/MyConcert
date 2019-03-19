using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyConcert.Models;
using Puppy.Utils;
using Puppy.Model.Output;
using Newtonsoft.Json;
using System.Net.Http;


namespace MyConcert.Controllers
{
    public class ConcertController : Controller
    {
        public async Task<IActionResult> Index(string searchString)
        {
            RestApi api = new RestApi("https://localhost:5003/api/concert/searchConcert");
            string searchCondition =@"{title:{'$regex' : '.*$search.*', '$options' : 'i'}}";
            searchCondition = searchCondition.Replace("$search", (!String.IsNullOrEmpty(searchString)?searchString :""));
            var data =  await api
            .SendAsync(HttpMethod.Post, searchCondition) ;
            
            var responseBody = await data.Content.ReadAsStringAsync();
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);
            List<ConcertModel> concertList = JsonConvert.DeserializeObject<List<ConcertModel>>(result.Data);
            
            return View(concertList);
        }


        public async Task<IActionResult> Details(string concertId)
        {
            RestApi api = new RestApi("https://localhost:5003/api/concert?concertId="+concertId);
          
            var data =  await api.GetAsync("");
            
            var responseBody = await data.Content.ReadAsStringAsync();
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);
            List<ConcertModel> concertList = JsonConvert.DeserializeObject<List<ConcertModel>>(result.Data);

            var concert = (from c in concertList where c.Id == concertId select c).FirstOrDefault(x => x.IsActive == true);

            api = new RestApi("https://localhost:5003/api/concertSeats?concertId="+concertId);
            data = await api.GetAsync("");
            responseBody = await data.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Result>(responseBody);
            List<AvailableSeatModel> availableSeats = JsonConvert.DeserializeObject<List<AvailableSeatModel>>(result.Data);
            
            ConcertDetailModel concertDetail = new ConcertDetailModel {
                Concert = concert,
                AvailableSeats = availableSeats
            };

            if (availableSeats.Count > 0)
            {
                HttpContext.Session.Set("ZoneId",Encoding.ASCII.GetBytes(availableSeats[0].ZoneId));
            }
            return View(concertDetail);
        }

        public async Task<IActionResult> LayoutBooking(string zoneId)
        {
            byte[] values;string id="";
            var cookies = HttpContext.Request.Cookies;
            HttpContext.Session.TryGetValue("ZoneId",out values);
            if (values != null )
            {
                id = Encoding.ASCII.GetString(values);
            }
           
            zoneId = (zoneId==null?id:zoneId);
            RestApi api = new RestApi("https://localhost:5003/api/concertSeats/Zone?zoneId="+zoneId);
            var data = await api.GetAsync("");
        
            var responseBody = await data.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(responseBody);
            List<ConcertSeatModel> availableSeats = JsonConvert.DeserializeObject<List<ConcertSeatModel>>(result.Data);
            return View(availableSeats);
        }
        [HttpPost]
        public async Task<IActionResult> Booking(int totalSeats, string seatIds)
        {
           var seats = seatIds;

            if (HttpContext.Request.Cookies["fbLogIn"] == null ){
                return RedirectToAction("LogIn", "Authentication");
            }

            string bookedBy = HttpContext.Request.Cookies["fbLogIn"].ToString();

            //set json for Api
            string jsonBooked =@"
            {
                'seatIds':{0},
                'total':{1},    
                'booking':
                    {
                        '$set':
                            {
                                'bookingStatus': 
                                    {  '_id':'bs02','name':'Sold Out', 
                                        'isActive': true,
                                        'bookedDate':'{2}',
                                        'bookedBy':'{3}'
                                    }
                            }
                    }
                    
            }";
            string bookedDate = DateTime.Now.ToString("YYYY-MM-dd hh:mm:ss");

            jsonBooked = jsonBooked.Replace("{0}", seatIds);
            jsonBooked = jsonBooked.Replace("{1}", totalSeats.ToString());
            jsonBooked = jsonBooked.Replace("{2}", bookedDate);
            jsonBooked = jsonBooked.Replace("{3}", bookedBy);
            

            RestApi api = new RestApi("https://localhost:5003/api/concertSeats/BookingSeats");
            var data = await api.AddAsync(jsonBooked);
            
            var responseBody = await data.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(responseBody);
            return RedirectToAction("MyTicket", "Customer", result);
        }

    


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
