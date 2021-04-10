using ProductMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Provider
{
    public interface IProvider
    {
        public List<ProductDto> GetAllProduct();
        public ProductDto SearchProductById(int Product_Id);
        public ProductDto SearchProductByName(string Product_Name);
        public RatingStatus AddProductRating(ProductRating model);
    }
}
