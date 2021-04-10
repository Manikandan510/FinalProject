using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyMicroService.Models
{
    public class CartModel
    {
        public int Product_Id { get; set; }
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public int Zipcode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Vendor vendorObj { get; set; }
    }
}
