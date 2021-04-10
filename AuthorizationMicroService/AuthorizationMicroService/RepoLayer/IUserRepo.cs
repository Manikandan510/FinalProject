using AuthorizationMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.RepoLayer
{
    public interface IUserRepo
    {
        UserDetails GetUserDetails(UserDetails user);
    }
}
