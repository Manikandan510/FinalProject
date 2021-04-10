using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommPortal.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public string Description{ get; set; }
        public int ZipCode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int totalprice { get; set; }
        public Vendor vendorobj { get; set; }

    }
}
