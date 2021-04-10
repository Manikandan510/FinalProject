using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProceedToBuyMicroService.Controllers;
using ProceedToBuyMicroService.Models;
using ProceedToBuyMicroService.Provider;
using ProceedToBuyMicroService.Repository; 
using System;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ProceedToBuyTests
{
    [TestFixture]
    public class Tests
    {
        private Mock<ProceedToBuyRepository> _repo;
        WishlistDto dto = new WishlistDto();

        private ProceedToBuyController _controller;
        private Mock<IProceedToBuyProvider> _config;
        private ProceedToBuyProvider provider;


        [SetUp]
        public void Setup()
        {
            _config = new Mock<IProceedToBuyProvider>();
            _controller = new ProceedToBuyController(_config.Object);
            _repo = new Mock<ProceedToBuyRepository>();
            provider = new ProceedToBuyProvider(_repo.Object);

        }


        [Test]
        public void AddToWishList_Fail()
        {
            //   WishlistModel entity = new WishlistModel;
            var result = _controller.AddToWishList(null) as StatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);

        }


        [Test]
        public void AddToCart_FailAsync()
        {
            var result = _controller.AddToCart(null) as StatusCodeResult;
            Assert.AreEqual(500, result.StatusCode);
        }
        [Test]
        public void WishListRepoSuccess()
        {
            //_repo.Setup(x => x.addToWishlist(It.IsAny<WishlistDto>())).Returns(dto);
            WishlistDto dt = new WishlistDto()
            {
                ProductId = 3,
                CustomerId = 1,
                Quantity = 1,
                DateAddedToWishlist = DateTime.Now
            };
            var result = provider.Wish(dt.CustomerId, dt.ProductId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void WishListRepoFail()
        {

            WishlistDto dt = new WishlistDto()
            {
                ProductId = 0,
                CustomerId = 0,
                Quantity = 1,
                DateAddedToWishlist = DateTime.Now
            };
            var result = provider.Wish(dt.CustomerId, dt.ProductId);
            Assert.IsNull(result);
        }
        [Test]
        public void AddToWishList_Success()
        {
            WishlistModel entity = new WishlistModel { CustomerId = 1, ProductId = 3 };

            _config.Setup(p => p.Wish(1, 3)).Returns(new WishlistSuccess { Message = "Product added to wishlist" });
            var result = _controller.AddToWishList(entity);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }
        [Test]
        public void WishListRepoProductIdFail()
        {

            WishlistDto dt = new WishlistDto()
            {
                ProductId = 0,
                CustomerId = 1,
                Quantity = 1,
                DateAddedToWishlist = DateTime.Now
            };
            var result = provider.Wish(dt.CustomerId, dt.ProductId);
            Assert.IsNull(result);
        }
        [Test]
        public void WishListRepoCustomerIdFail()
        {

            WishlistDto dt = new WishlistDto()
            {
                ProductId = 1,
                CustomerId = 0,
                Quantity = 1,
                DateAddedToWishlist = DateTime.Now
            };
            var result = provider.Wish(dt.CustomerId, dt.ProductId);
            Assert.IsNull(result);
        }


    }
}