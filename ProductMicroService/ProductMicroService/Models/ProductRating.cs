using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Models
{
    public class ProductRating
    {
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }

    }
}
