using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommPortal.Models.ViewModels
{
    public class WishlistViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
    }
}
