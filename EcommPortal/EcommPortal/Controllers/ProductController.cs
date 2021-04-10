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
    public class ProductController : Controller
    {
        private readonly IProductProvider _provider;
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ProductController));
        public ProductController(IProductProvider provider)
        {
            this._provider = provider;
        }
        public async Task<IActionResult> Home()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<Product> prod = new List<Product>();
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetAllProducts(token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        prod = JsonConvert.DeserializeObject<List<Product>>(JsonContent);
                        return View(prod);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoProduct");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return RedirectToAction("NoProduct");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No record found in DB ";
                        return RedirectToAction("NoProduct");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View();
            }
        }
         
        public IActionResult GetId()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View();
        }
        public IActionResult GetName()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View();
        }
        public IActionResult NoProduct()
        {
            _logger.Info(" There is No such Product To display");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(int Id)
        {
            _logger.Info("Products with id-" + Id + " are getting displayed ");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                Product prod = new Product();
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetProductById(Id, token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        prod = JsonConvert.DeserializeObject<Product>(JsonContent);
                        return View(prod);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoProduct");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return RedirectToAction("NoProduct");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No record found in DB for ID :" + Id;
                        return RedirectToAction("NoProduct");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(prod);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByName(string Name)
        {
            _logger.Info("Products with name-" + Name + " are getting displayed ");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                Product prod = new Product();
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetProductByName(Name, token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        prod = JsonConvert.DeserializeObject<Product>(JsonContent);
                        return View(prod);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoProduct");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return RedirectToAction("NoProduct");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No record found in DB for Name :" + Name;
                        return RedirectToAction("NoProduct");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(prod);
            }
        }

        [HttpGet]
        public IActionResult RateAProduct(int id)
        {
            _logger.Info(" Trying to Adding Rating to a product");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            ProductViewModel model = new ProductViewModel();
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateAProduct(ProductViewModel model)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                RatingStatusViewModel status = new RatingStatusViewModel();
                try
                {
                    _logger.Info("Adding Rating to the product");
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.RateAProduct(model, token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsoncontent = await response.Content.ReadAsStringAsync();
                        status = JsonConvert.DeserializeObject<RatingStatusViewModel>(jsoncontent);
                        return View("RatingAdded", status);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Having server issue while adding rating");
                        return View(model);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("", "Invalid model states");
                        return View(model);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception Occured as : " + e.Message);
                }
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult RatingAdded()
        {
            _logger.Info("Sucessfully aded Rating to the product");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            RatingStatusViewModel model = new RatingStatusViewModel();
            return View(model);
        }
    }
}
