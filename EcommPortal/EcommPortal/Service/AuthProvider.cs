using EcommPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public class AuthProvider : IAuthProvider
    {
        public async Task<HttpResponseMessage> Login(User user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response1 = await httpClient.PostAsync("https://localhost:44394/api/Auth/Login", content1);
               // var response1 = await httpClient.PostAsync("http://13.86.4.222/api/Authentication/Login", content1);
                return response1;
            }
        }


    }
}
