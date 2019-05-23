using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MyConcert.Models;
using Puppy.Utils;
using Puppy.Model;
using Puppy.Model.Output;
using Newtonsoft.Json;
using System.Net.Http;

namespace MyConcert.Controllers
{
    public class AuthenticationController : Controller
    {
       
        public IActionResult AuthenReply()
        {
            
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public async Task<IActionResult> LogOn(string fbId,string fbName)
        {
            Response.Cookies.Append("fbLogIn", fbId);
            Response.Cookies.Append("fbName", fbName);
            // log in with JWT
            UserModel model = new UserModel {
                UserName = fbId,
                Password ="test"
            };
            
            RestApi api = new RestApi ("https://localhost:5003/api/user/authenticate");
            var json = JsonConvert.SerializeObject(model); 
            string token = await api.GetOneAsync(HttpMethod.Post,json );
            Response.Cookies.Append("token", token);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("fbLogIn");
            Response.Cookies.Delete ("fbName");
            Response.Cookies.Delete ("token");
    
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
