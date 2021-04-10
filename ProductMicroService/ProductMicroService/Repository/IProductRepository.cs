using ProductMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Repository
{
    public interface IProductRepository
    {
        public List<ProductDto> GetAllProducts();
        public List<ProductDto> SearchProductById();
        public List<ProductDto> SearchProductByName();
        public bool AddProductRating(ProductRating model);
    }
}
