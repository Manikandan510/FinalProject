using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public interface IVendorProvider
    {
        Task<HttpResponseMessage> VendorList(string token);
    }
}
