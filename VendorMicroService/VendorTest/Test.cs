using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using VendorMicroService.Controllers;
using VendorMicroService.Models;
using VendorMicroService.Provider;
using VendorMicroService.Repository;

namespace VendorMicroServiceTest
{
    [TestFixture]
    public class Tests
    {
        List<Vendor> vendors = new List<Vendor>();
        List<VendorStock> stock = new List<VendorStock>();
        private Mock<IVendorRepository> _repo;
        private VendorProvider provider;
        private VendorController _controller;
        private Mock<IVendorProvider> _config;

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IVendorRepository>();
            provider = new VendorProvider(_repo.Object);

            _config = new Mock<IVendorProvider>();
            _controller = new VendorController(_config.Object);

        }


        [Test]
        public void GetVendorDetails_Success()
        {
            _config.Setup(p => p.GetDetailsOfVendor(1)).Returns(new List<VendorDto> {new VendorDto()
                {
                VendorId = 1,
                VendorName = "FastTrack",
                DeliveryCharge= 150,
                VendorRating = 4
                } });
            var result = _controller.GetVendorDetails(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }


        [Test]
        public void GetVendorDetails_Fail()
        {
            var result = _controller.GetVendorDetails(0);
            Assert.IsNull(result);
        }
        [Test]
        public void GetVendorSuccess()
        {
            _repo.Setup(x => x.GetVendorDetails()).Returns(new List<VendorDto>());
            _repo.Setup(x => x.GetVendorStocks()).Returns(new List<VendorStockDto>());
            var result = provider.GetDetailsOfVendor(1);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetVendorFail()
        {
            _repo.Setup(x => x.GetVendorDetails()).Returns(new List<VendorDto>());
            _repo.Setup(x => x.GetVendorStocks()).Returns(new List<VendorStockDto>());
            var result = provider.GetDetailsOfVendor(10);
            Assert.IsEmpty(result);
        }
    }
}