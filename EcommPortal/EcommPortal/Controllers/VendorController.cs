using EcommPortal.Models;
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
    public class VendorController : Controller
    {
        private readonly IVendorProvider _vendor;
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ProductController));
        public VendorController(IVendorProvider provider)
        {
            this._vendor = provider;
        }
        public async Task<IActionResult> VendorList()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<Vendor> vend = new List<Vendor>();
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _vendor.VendorList(token);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        vend = JsonConvert.DeserializeObject<List<Vendor>>(JsonContent);
                        return View(vend);
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
    }
}
