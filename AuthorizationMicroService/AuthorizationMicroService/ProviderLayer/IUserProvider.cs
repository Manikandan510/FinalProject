using AuthorizationMicroService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.ProviderLayer
{
    public interface IUserProvider
    {
        public string LoginProvider(UserDetails user);
        public string GenerateJSONWebToken(UserDetails userInfo);
        public int GetUserId(UserDetails user);
    }
}
