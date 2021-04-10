using EcommPortal.Models;
using EcommPortal.Models.ViewModels;
using EcommPortal.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommPortal.Controllers
{
    public class ProceedToBuyController : Controller
    {
        private readonly IProceedToBuyProvider _provider;
        private readonly IVendorProvider _vendor;
        private readonly IProductProvider _productProvider;
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));
        public ProceedToBuyController(IProceedToBuyProvider provider,IVendorProvider vendorProvider,IProductProvider productProvider)
        {
            this._provider = provider;
            this._vendor = vendorProvider;
            this._productProvider = productProvider;

        }
        [HttpGet]
        public async Task<IActionResult> AddToCart(int id) 
        {
            Product p = new Product();
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            } 
            string today = DateTime.Now.AddDays(1).ToString("yyyy'-'MM'-'dd");
            ViewBag.Min = today;
            var response = await _productProvider.GetProductById(id, HttpContext.Session.GetString("token"));
            CartView cart = new CartView();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var JsonContent = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Product>(JsonContent);
                cart.ProductName = p.Name;
                cart.Description = p.Description;
                cart.Price = p.Price;
                cart.imagename = p.Image_Name;
            }
            cart.Product_Id = id;
            cart.Customer_Id = Convert.ToInt32(HttpContext.Session.GetInt32("custId"));
            cart.Customer_Name = Convert.ToString(HttpContext.Session.GetString("Username"));
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(CartView cart)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cart);
                }
                Cart cartobj = new Cart();

                try
                {
                    _logger.Info("Adding Products to cart");
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.AddToCart(cart, token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsoncontent = await response.Content.ReadAsStringAsync();
                        cartobj = JsonConvert.DeserializeObject<Cart>(jsoncontent);
                        if (cartobj.CartId != 0)
                        {
                            cartobj.CustomerName = Convert.ToString(HttpContext.Session.GetString("Username"));
                            cartobj.CustomerId = cart.Customer_Id;
                            cartobj.Description = cart.Description;
                            cartobj.Price = cart.Price;
                            _logger.Info("Cart Added");
                            return View("CartAdded", cartobj);
                        }
                        else
                        {
                            _logger.Error("Error in Adding Cart ");
                            return View("CartNotAdded", cartobj);

                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        ModelState.AddModelError("", "Error while  adding to cart");
                        return View();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Having server issue while adding to cart");
                        return View();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("", "Invalid model states");
                        return View();
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception Occured as : " + e.Message);
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult CartAdded()
        {
            _logger.Info("Products added to cart");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            Cart cartobj = new Cart();
            return View(cartobj);
        }
        [HttpGet]
        public IActionResult CartNotAdded()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            Cart cartobj = new Cart();
            return View(cartobj);
        }

        [HttpGet]
        public IActionResult AddToWishlist(int id)
        {
            _logger.Info("Adding Products to WishList");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            WishlistViewModel wish = new WishlistViewModel();
            wish.CustomerId = Convert.ToInt32(HttpContext.Session.GetInt32("custId"));
            wish.CustomerName = Convert.ToString(HttpContext.Session.GetString("Username"));
            wish.ProductId = id;
            return View(wish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWishlist(WishlistViewModel wish)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(wish);
                }
                WishlistStatusViewModel status = new WishlistStatusViewModel();
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.AddToWishlist(wish, token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsoncontent = await response.Content.ReadAsStringAsync();
                        status = JsonConvert.DeserializeObject<WishlistStatusViewModel>(jsoncontent);
                        return View("WishlistStatus", status);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Having server issue while adding to wishlist");
                        return View();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("", "Invalid model states");
                        return View();
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception Occured as : " + e.Message);
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult WishlistStatus()
        {
            _logger.Info("Sucessfull status for adding products");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            WishlistStatusViewModel status = new WishlistStatusViewModel();
            return View(status);
        }
    }
}
