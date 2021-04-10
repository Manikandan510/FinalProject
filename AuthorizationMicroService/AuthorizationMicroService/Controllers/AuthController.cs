using AuthorizationMicroService.Models;
using AuthorizationMicroService.ProviderLayer;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly IUserProvider _userProvider;

        public AuthController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDetails login)
        {
            try
            {
                _log4net.Info("Authentication initiated for " + login.Username);
                IActionResult response = Unauthorized();
                string tokenString = _userProvider.LoginProvider(login);
                int uid = _userProvider.GetUserId(login);
                if (tokenString == null)
                {
                    _log4net.Error("Login failed for " + login.Username);
                    return NotFound();
                }
                else
                {
                    return Ok(new { token = tokenString, User_Id = uid });
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Error in login for user " + login.Username + " as " + e.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
