using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyMicroService.Models
{
    public class VendorDto
    {
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public int vendorRating { get; set; }
        public double deliveryCharge { get; set; }
    }
}
