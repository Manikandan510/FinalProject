using ProductMicroService.Models;
using ProductMicroService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Provider
{
    public class ProductProvider : IProvider
    {
        private readonly IProductRepository _prodRepo;
        RatingStatus rating = new RatingStatus();
        ProductRepository itemsProvider = new ProductRepository();

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductProvider));
        public ProductProvider(IProductRepository prodRepository)
        {
            this._prodRepo = prodRepository;
        }
        public List<ProductDto> GetAllProduct()
        {
            try
            {
                _log4net.Info("Product details have been successfully recieved.");

                List<ProductDto> p = _prodRepo.GetAllProducts();
                return p;

            }
            catch (Exception e)
            {
                _log4net.Error("Error in getting product details");
                throw e;
            }
        }
        public ProductDto SearchProductById(int prod_id)
        {
            try
            {
                _log4net.Info("Product details have been successfully recieved.");

                List<ProductDto> p = _prodRepo.SearchProductById();
                ProductDto finalprod = p.FirstOrDefault(x => x.Id == prod_id);
                return finalprod;

            }
            catch (Exception e)
            {
                _log4net.Error("Error in getting product details");
                throw e;
            }
        }
        public ProductDto SearchProductByName(string prod_name)
        {
            try
            {
                _log4net.Info("Product details have been successfully recieved.");
                List<ProductDto> p = _prodRepo.SearchProductByName();
                ProductDto finalprod = p.FirstOrDefault(x => x.Name == prod_name);
                return finalprod;

            }
            catch (Exception e)
            {
                _log4net.Error("Error in getting product details");
                throw e;
            }
        }
        public RatingStatus AddProductRating(ProductRating model)
        {
            try
            {
                _log4net.Info("Rating has been successfully assigned to the product.");
                bool response = _prodRepo.AddProductRating(model);
                if (response == true)
                {
                    rating.Message = "Rating added Sucessfully to the Product";
                }
                else
                {
                    return null;
                }
                return rating;

            }
            catch (Exception e)
            {
                _log4net.Error("Error in getting product details");
                throw e;
            }
        }

    }
}
