using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorMicroService.Models
{
    public class VendorDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public double DeliveryCharge { get; set; }
        public int VendorRating { get; set; }

    }
}
