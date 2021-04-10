using AuthorizationMicroService.Models;
using AuthorizationMicroService.RepoLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroService.ProviderLayer
{
    public class UserProvider : IUserProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserProvider));
        private readonly IUserRepo userRepo;
        private IConfiguration _config;
        public UserProvider(IUserRepo userRepo, IConfiguration config)
        {
            this.userRepo = userRepo;
            this._config = config;
        }
        public int GetUserId(UserDetails user)
        {
            try
            {
                UserDetails _user = userRepo.GetUserDetails(user);
                if (_user == null)
                {
                    return 0;
                }
                else
                {
                    return (_user.Userid);
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Exception occured while authenticating user/generating token as " + e.Message);
            }
            return 0;

        }

        public string LoginProvider(UserDetails user)
        {
            try
            {
                UserDetails _user = userRepo.GetUserDetails(user);
                if (_user == null)
                {
                    return null;
                }
                else
                {
                    return GenerateJSONWebToken(user);
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Exception occured while authenticating user/generating token as " + e.Message);
            }
            return null;
        }

        public string GenerateJSONWebToken(UserDetails userInfo)
        {
            _log4net.Info("Token Generation initiated");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
