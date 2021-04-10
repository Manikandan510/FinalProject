using EcommPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommPortal.Repository
{
    public interface ICartRepository
    {
        Task<int> AddCart(CartDto dto);
    }
}
