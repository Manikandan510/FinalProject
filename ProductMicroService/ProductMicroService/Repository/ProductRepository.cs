using ProductMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Repository
{
    public class ProductRepository : IProductRepository
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductRepository));
        public ProductDto productdto;
        public static List<Product> items = new List<Product>()
        {
            new Product(){Id=1,Price=25000,Name="iWatch",Description="The new Apple Watch Series 6 and the new Apple Watch SE.",Image_name="iWatch.png",Rating=4},
            new Product(){Id=2,Price=4000,Name="NeckBand",Description="Boult Audio Probass Curve Bluetooth Wireless Neckband With Mic (Black).",Image_name="Neckband.png",Rating=4},
            new Product(){Id=3,Price=3000,Name="PowerBank",Description="Power up! Power down! Use the power to charge all of your fun.",Image_name="PowerBank.png",Rating=5},
            new Product(){Id=4,Price=2000,Name="Fitnessband",Description="The perfect workout companion, Mi Smart Band 6 provides 14 days of battery life4 and a handy magnetic port for quick clip-on and clip-off",Image_name="FitBand.png",Rating=5},
            new Product(){Id=5,Price=5000,Name="HomeTheatre",Description="Break out the popcorn and get an immersive experience with our all-in-one 5.1 home theatre.",Image_name="HomeTheatre.png",Rating=3},
            new Product(){Id=6,Price=45000,Name="PS5",Description="The PlayStation 5 (PS5) is a home video game console developed by Sony Interactive Entertainment.",Image_name="PS5.png",Rating=4},
            new Product(){Id=7,Price=40000,Name="Xbox",Description=" the Xbox series S, the smallest, sleekest Xbox console ever. ",Image_name="Xbox.png",Rating=3},
            new Product(){Id=8,Price=2500,Name="Echo",Description="Amazon Echo is a hands-free speaker you control with your voice. Echo connects to Alexa Voice Service to play music, make calls, answer questions, provide information, news, sports scores, weather, and more - instantly.",Image_name="Echo.png",Rating=4},
            new Product(){Id=9,Price=37000,Name="GoPro",Description="GoPro - World's Most Versatile Action Camera",Image_name="GoPro.png",Rating=4},
            new Product(){Id=10,Price=6000,Name="SSD",Description="Get High Performance SSDs at Crucial! Compare Products. View Solutions.",Image_name="SSD.png",Rating=4},

        };
        public List<ProductDto> GetAllProducts()
        {
            try
            {
                _log4net.Info("Product details  have been successfully retrieved");
                List<ProductDto> productsdto = new List<ProductDto>();

                foreach (Product p in items)
                {
                    ProductDto productnewdto = new ProductDto()
                    {
                        Id = p.Id,
                        Price = p.Price,
                        Name = p.Name,
                        Description = p.Description,
                        Image_name = p.Image_name,
                        Rating = p.Rating
                    };
                    productsdto.Add(productnewdto);
                }
                return productsdto;
            }
            catch (Exception e)
            {
                _log4net.Error("Error " + e.Message);
                return null;
            }


        }
        public List<ProductDto> SearchProductById()
        {
            try
            {
                _log4net.Info("Product details  have been successfully retrieved");
                List<ProductDto> productsdto = new List<ProductDto>();

                foreach (Product p in items)
                {
                    ProductDto productnewdto = new ProductDto()
                    {
                        Id = p.Id,
                        Price = p.Price,
                        Name = p.Name,
                        Description = p.Description,
                        Image_name = p.Image_name,
                        Rating = p.Rating
                    };
                    productsdto.Add(productnewdto);
                }
                return productsdto;
            }
            catch (Exception e)
            {
                _log4net.Error("Error " + e.Message);
                return null;
            }



        }
        public List<ProductDto> SearchProductByName()
        {
            try
            {
                _log4net.Info("Product details  have been successfully retrieved");
                List<ProductDto> productsdto = new List<ProductDto>();

                foreach (Product p in items)
                {
                    ProductDto productnewdto = new ProductDto()
                    {
                        Id = p.Id,
                        Price = p.Price,
                        Name = p.Name,
                        Description = p.Description,
                        Image_name = p.Image_name,
                        Rating = p.Rating


                    };
                    productsdto.Add(productnewdto);
                }
                return productsdto;

            }
            catch (Exception e)
            {
                _log4net.Error("Error " + e.Message);
                return null;
            }



        }
        public bool AddProductRating(ProductRating model)
        {
            try
            {
                _log4net.Info("Getting product details for product id " + model.Id);
                Product p = items.FirstOrDefault(x => x.Id == model.Id);
                p.Rating = model.Rating;
                return true;
            }
            catch (Exception e)
            {
                _log4net.Info("No product found with the given product id " + e.Message);
                return false;
            }


        }

    }
}


