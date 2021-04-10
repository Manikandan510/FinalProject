using EcommPortal.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public class ProductProvider : IProductProvider
    {
        public async Task<HttpResponseMessage> GetAllProducts(string token) 
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44304/");
                //client.BaseAddress = new Uri("http://20.37.132.88/");

                var response = await client.GetAsync("api/Product/getAllProducts/");
                return response;
            }
        }
        public async Task<HttpResponseMessage> GetProductById(int id,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44304/");
                //client.BaseAddress = new Uri("http://20.37.132.88/");

                var response = await client.GetAsync("api/Product/searchProductById/" +id);
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetProductByName(string name,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44304/");
                //client.BaseAddress = new Uri("http://20.37.132.88/");
                
               
                var response = await client.GetAsync("api/Product/searchProductByName/" + name);
                return response;
            }
        }

        public async Task<HttpResponseMessage> RateAProduct(ProductViewModel prod,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44304/");
               // client.BaseAddress = new Uri("http://20.37.132.88/");
                var jsonstring = JsonConvert.SerializeObject(prod);
                var obj = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/Product/AddProductRating/", obj);
                return response;
            }
        }
    }
}
