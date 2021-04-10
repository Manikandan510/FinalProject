using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image_name { get; set; }

        public int Rating { get; set; }
    }
}
