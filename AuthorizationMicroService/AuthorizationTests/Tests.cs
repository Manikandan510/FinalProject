using AuthorizationMicroService.Controllers;
using AuthorizationMicroService.Models;
using AuthorizationMicroService.ProviderLayer;
using AuthorizationMicroService.RepoLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizationTests
{
    public class Tests
    {
        private AuthController _controller;
        private Mock<IUserProvider> _ProviderMock;
        private Mock<IConfiguration> _config;
        private Mock<IUserRepo> _repository;
        private IUserProvider _provider;
        private string token;
        private UserDetails user1;
        List<UserDetails> users;
        [SetUp]
        public void Setup()
        {
            users = new List<UserDetails>() {
            new UserDetails{ Userid = 1, Username = "abc",Password = "abc@123"},
            new UserDetails{ Userid = 2, Username = "xyz",Password = "xyz@123"},
            new UserDetails{ Userid = 1, Username = "pqr",Password = "pqr@123"} };
            _ProviderMock = new Mock<IUserProvider>();
            _controller = new AuthController(_ProviderMock.Object);
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c["Jwt:Key"]).Returns("ThisismySecretKey");
            _repository = new Mock<IUserRepo>();
            _provider = new UserProvider(_repository.Object, _config.Object);
            _ProviderMock.Setup(x => x.LoginProvider(It.IsAny<UserDetails>())).Returns(token);
            _repository.Setup(r => r.GetUserDetails(It.IsAny<UserDetails>())).Returns(user1);
        }


        [Test]
        public void ValidUserFail()
        {
            UserDetails user = new UserDetails()
            {
                Username = "abc",
                Password = "abc@123",
            };
            var response = _controller.Login(user) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }

        [Test]
        public void InvalidUserName()
        {
            UserDetails user = new UserDetails()
            {
                Username = "xyz",
                Password = "xyz@123",
            };
            var response = _controller.Login(user) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }

        [Test]
        public void InvalidPassword()
        {
            UserDetails user = new UserDetails()
            {
                Username = "abc",
                Password = "@123",
            };
            var response = _controller.Login(user) as StatusCodeResult;
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}