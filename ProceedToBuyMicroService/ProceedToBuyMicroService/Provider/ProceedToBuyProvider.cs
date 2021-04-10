using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProceedToBuyMicroService.Models;
using ProceedToBuyMicroService.Repository;

namespace ProceedToBuyMicroService.Provider
{
    public class ProceedToBuyProvider : IProceedToBuyProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyProvider));

        private readonly IProceedToBuyRepository proceedToBuyRepository;
        public ProceedToBuyProvider(IProceedToBuyRepository repo)
        {
            proceedToBuyRepository = repo;
        }
        public WishlistSuccess Wish(int custid, int prodid)
        {

            WishlistDto wl = new WishlistDto()
            {
                CustomerId = custid,
                ProductId = prodid,
                Quantity = 1,
                DateAddedToWishlist = DateTime.Now.Date
            };
            WishlistDto wl1 = proceedToBuyRepository.addToWishlist(wl);
            WishlistSuccess msg = new WishlistSuccess();
            if (wl1 != null)
            {
                msg.Message = " Product added to wishlist";
                return msg;
            }

            return null;
        }


        public CartDto GetSupply(int prodid, int custid, string Username, int zipcode, DateTime delidt)
        {

            var client = new HttpClient();


            client.BaseAddress = new Uri("https://localhost:44334/");
            //client.BaseAddress = new Uri("http://40.76.144.8/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Vendor/GetVendorDetails/" + prodid).Result;

            string apiResponse = response.Content.ReadAsStringAsync().Result;
            //var value = response.Content.ReadAsStringAsync().Result;

            List<VendorDto> vendorsdto = JsonConvert.DeserializeObject<List<VendorDto>>(apiResponse);
            if (vendorsdto.Count > 0)
            {
                int max = 0;
                VendorDto taggeddto = vendorsdto.FirstOrDefault();

                foreach (VendorDto v in vendorsdto)
                {
                    if (v.vendorRating >= max)
                    {
                        max = v.vendorRating;
                        taggeddto = v;
                    }
                }
                Vendor taggedvendor = new Vendor()
                {
                    VendorId = taggeddto.vendorId,
                    VendorName = taggeddto.vendorName,
                    Rating = taggeddto.vendorRating,
                    DeliveryCharge = taggeddto.deliveryCharge
                };



                Random unid = new Random();
                CartDto finalcart = new CartDto()
                {
                    CartId = unid.Next(1, 999),
                    CustomerId = custid,
                    CustomerName = Username,
                    ProductId = prodid,
                    Zipcode = zipcode,
                    DeliveryDate = delidt,
                    VendorObj = taggedvendor,
                };
                CartDto ficart = proceedToBuyRepository.addToCart(finalcart);
                return ficart;
            }
            else
            {
                CartDto fcart = new CartDto();
                fcart.ProductId = prodid;
                fcart.CustomerId = custid;
                fcart.Zipcode = zipcode;
                return fcart;
            }
        }
    }
}
