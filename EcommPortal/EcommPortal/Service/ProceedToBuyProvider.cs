using EcommPortal.Models;
using EcommPortal.Models.ViewModels;
using EcommPortal.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public class ProceedToBuyProvider:IProceedToBuyProvider
    {
        private readonly ICartRepository _repo;
        public ProceedToBuyProvider(ICartRepository repo)
        {
            this._repo = repo;
        }
        public async Task<HttpResponseMessage> AddToCart(CartView cart,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44325/");
                //client.BaseAddress = new Uri("http://52.189.64.29/");
                var jsonstring = JsonConvert.SerializeObject(cart);
                var obj = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/ProceedToBuy/AddToCart/", obj );
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    CartDto dto = new CartDto()
                    {

                        CustomerId = cart.Customer_Id,
                        CustomerName = cart.Customer_Name,
                        ProductId = cart.Product_Id,
                        Price=cart.Price,
                        Desciption=cart.Description,
                        Zipcode = cart.ZipCode,
                        DeliveryDate = cart.DeliveryDate,

                        vendorobj=new Vendor() 
                        { 
                            VendorId=cart.VendorID,
                            VendorName=cart.VendorName,
                            VendorRating=cart.VendorRating,
                            DeliveryCharge=cart.DeliveryCharge
                        }
                        
                    };
                    await _repo.AddCart(dto);
                }
                return response;
            }
        }
        public async Task<HttpResponseMessage> AddToWishlist(WishlistViewModel wish,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44325/");
                //client.BaseAddress = new Uri("http://52.189.64.29/");
                var jsonstring = JsonConvert.SerializeObject(wish);
                var obj = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/ProceedToBuy/AddToWishList/", obj);
                return response;
            }
        }

    }
}
