using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorMicroService.Models;

namespace VendorMicroService.Repository
{
    public interface IVendorRepository
    {
        public List<VendorDto> GetVendorDetails();
        public List<VendorStockDto> GetVendorStocks();


    }
}
