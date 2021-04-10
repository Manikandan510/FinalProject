using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EcommPortal.Models.ViewModels
{
    public class CartView
    {
        public int Product_Id { get; set; }
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string ProductName { get; set; }
        public string imagename { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        [Range(100000, 999999, ErrorMessage = "Please Enter A Valid Zipcode")]
        public int ZipCode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public int VendorRating { get; set; }
        public int DeliveryCharge { get; set; }
    }
}
