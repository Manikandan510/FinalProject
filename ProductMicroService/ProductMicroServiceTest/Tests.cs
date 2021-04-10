using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductMicroService.Controllers;
using ProductMicroService.Models;
using ProductMicroService.Provider;
using ProductMicroService.Repository;
using System.Collections.Generic;

namespace ProductMicroServiceTest
{
    [TestFixture]
    public class Tests
    {
        private ProductController _controller;
        private Mock<IProvider> _config;
        private ProductProvider provider;
        private Mock<IProductRepository> _repo;
        private List<ProductDto> dto;



        [SetUp]
        public void Setup()
        {
            _config = new Mock<IProvider>();
            _controller = new ProductController(_config.Object);
            _repo = new Mock<IProductRepository>();
            provider = new ProductProvider(_repo.Object);
            dto = new List<ProductDto>()
            {
                new ProductDto(){Id=1,Price=2500,Name="Watch",Description="Watch is a high-performance sport watch for the modern man",Image_name="Watch.png",Rating=4},
                new ProductDto(){Id=2,Price=4000,Name="Bluetooth",Description="Unthinkably connected with people in the modern world",Image_name="Bluetooth.png",Rating=4},
                new ProductDto(){Id=3,Price=3000,Name="PowerBank",Description="Power up! Power down! Use the power to charge all of your fun",Image_name="PowerBank.png",Rating=5},
                new ProductDto(){Id=4,Price=2000,Name="Shoes",Description="Refresh your style with a new pair of men's shoes",Image_name="Shoes.png",Rating=5},
                new ProductDto(){Id=5,Price=5000,Name="HomeTheatre",Description="Experience the original sound",Image_name="HomeTheatre.png",Rating=3 }


            };
        }

        [Test]
        public void GetProductDetailsById_Success()
        {
            _config.Setup(p => p.SearchProductById(1)).Returns(new ProductDto
            {
                Id = 1,
                Price = 1000,
                Name = "Watch",
                Description = "Watch is a high-performance sport watch for the modern man",
                Image_name = "Watch.png",
                Rating = 4
            });
            var result = _controller.SearchProductById(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void GetProductDetailsByName_Success()
        {
            _config.Setup(p => p.SearchProductByName("Bluetooth")).Returns(new ProductDto
            {
                Id = 2,
                Price = 5000,
                Name = "Bluetooth",
                Description = "Unthinkably connected with people in the modern world",
                Image_name = "Bluetooth.png",
                Rating = 4
            });
            var result = _controller.SearchProductByName("Bluetooth");
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
       
        [Test]
        public void AddRatingToProduct_Success()
        {
            ProductRating rating = new ProductRating { Id = 1, Rating = 3 };
            _config.Setup(p => p.AddProductRating(rating)).Returns(new RatingStatus { Message = "Rating added Successfully to the Product" });
            var result = _controller.AddProductRating(rating);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void GetProductByIdRepo_Sucess()
        {
            _repo.Setup(x => x.SearchProductById()).Returns(dto);
            var response = provider.SearchProductById(1);
            Assert.IsNotNull(response);
        }
        [Test]
        public void GetProductByIdRepo_Fail()
        {
            _repo.Setup(x => x.SearchProductById()).Returns(dto);
            var response = provider.SearchProductById(999);
            Assert.IsNull(response);
        }
        [Test]
        public void GetProductByNameRepo_Sucess()
        {
            _repo.Setup(x => x.SearchProductByName()).Returns(dto);
            var response = provider.SearchProductByName("Watch");
            Assert.IsNotNull(response);
        }
        [Test]
        public void GetProductByNameRepo_Fail()
        {
            _repo.Setup(x => x.SearchProductByName()).Returns(dto);
            var response = provider.SearchProductByName("Cars");
            Assert.IsNull(response);
        }
    }
}