using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProceedToBuyMicroService.Models;

namespace ProceedToBuyMicroService.Provider
{
    public interface IProceedToBuyProvider
    {
        public CartDto GetSupply(int prodid, int cutsid, string Username, int zipcode, DateTime delidt);
        
        public WishlistSuccess Wish(int custid, int prodid);
    }
}

