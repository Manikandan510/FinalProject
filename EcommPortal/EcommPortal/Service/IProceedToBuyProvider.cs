using EcommPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public interface IProceedToBuyProvider
    {
        public Task<HttpResponseMessage> AddToCart(CartView cart,string token);
        public Task<HttpResponseMessage> AddToWishlist(WishlistViewModel wish,string token);
    }
}
