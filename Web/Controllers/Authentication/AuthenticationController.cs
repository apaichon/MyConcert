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

        public IActionResult LogOn(string fbId)
        {
            Response.Cookies.Append("fbLogIn", fbId);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
