using System;
using Microsoft.AspNetCore.Http;
using MyConcert.Models;
using Newtonsoft.Json;

namespace MyConcert.Utils
{
    public class Headers
    {
        public static void PrepareToken(HttpRequest req)
        {
            if (req.Cookies["token"] !=null)
            {
                UserModel model = JsonConvert.DeserializeObject<UserModel>(req.Cookies["token"].ToString());
                req.Headers.Add("Authorize", model.Token);
            }
        }
    }
}