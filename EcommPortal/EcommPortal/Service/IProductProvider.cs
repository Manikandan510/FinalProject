using EcommPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public interface IProductProvider
    {
        Task<HttpResponseMessage> GetAllProducts(string token);
        Task<HttpResponseMessage> GetProductById(int id,string token);
        Task<HttpResponseMessage> GetProductByName(string name,string token);
        Task<HttpResponseMessage> RateAProduct(ProductViewModel prod,string token);
    }
}
