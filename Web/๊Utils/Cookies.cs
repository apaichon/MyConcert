using System;
using Microsoft.AspNetCore.Http;
using MyConcert.Models;
using Newtonsoft.Json;

namespace MyConcert.Utils
{
    public class Cookies
    {
        public static bool CheckFbLogIn(HttpRequest req)
        {
            return (req.Cookies["fbLogIn"] != null? true: false);
        }

        public static string GetToken(HttpRequest req)
        {
            UserModel model = JsonConvert.DeserializeObject<UserModel> (req.Cookies["token"].ToString());
            return "Bearer " + model.Token;
        }
    }
}