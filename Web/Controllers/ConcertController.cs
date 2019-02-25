using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MvcConcert.Models;
using Puppy.Utils;
using Puppy.Model.Output;
using Newtonsoft.Json;
using System.Net.Http;

namespace MvcConcert.Controllers
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

        public IActionResult Details(string concertId)
        {
            ViewData["ConcertId"] = concertId;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
