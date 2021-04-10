using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorMicroService.Models;

namespace VendorMicroService.Provider
{
    public interface IVendorProvider
    {
        public List<VendorDto> VendorList();
        public List<VendorDto> GetDetailsOfVendor(int ProductId);
    }
}
