using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProceedToBuyMicroService.Models;
using ProceedToBuyMicroService.Provider;

namespace ProceedToBuyMicroService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProceedToBuyController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));
        private readonly IProceedToBuyProvider _provider;
        public ProceedToBuyController(IProceedToBuyProvider p)
        {
            this._provider = p;
        }



        // POST: api/ProceedToBuy
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart([FromBody] CartModel cart)
        {
            _log4net.Info("Add to Cart method initiated");
            try
            {
                CartDto result = _provider.GetSupply(cart.Product_Id, cart.Customer_Id, cart.Customer_Name, cart.Zipcode, cart.DeliveryDate);
                if (result == null)
                {
                    return BadRequest();
                }
                _log4net.Info("Added to Cart");
                return Ok(result);

            }
            catch
            {
                _log4net.Info("Cannot Add To Cart");
                return StatusCode(500);
            }

        }


        [HttpPost]
        [Route("AddToWishlist")]
        public IActionResult AddToWishList([FromBody] WishlistModel wish)
        {
            _log4net.Info("Add to WishList method initiated");
            try
            {
                _log4net.Info("Add To Wishlist Provider called");

                WishlistSuccess message = _provider.Wish(wish.CustomerId, wish.ProductId);
                if (message == null)
                {
                    return BadRequest();
                }
                return Ok(message);

            }
            catch
            {
                _log4net.Info("Error calling Add Wishlist provider");
                return BadRequest();
            }


        }


    }
}
