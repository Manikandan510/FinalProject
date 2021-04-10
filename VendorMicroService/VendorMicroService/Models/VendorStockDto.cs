using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorMicroService.Models
{
    public class VendorStockDto
    {
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public int StockInHand { get; set; }
        public DateTime ExpectedStockReplenishmentDate { get; set; }
    }
}
