using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public class VendorProvider : IVendorProvider
    {
        public async Task<HttpResponseMessage> VendorList(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44334/");
                //client.BaseAddress = new Uri("http://20.37.132.88/");

                var response = await client.GetAsync("api/Vendor/VendorList/");
                return response;
            }
        }
    }
}
