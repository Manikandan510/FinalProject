using EcommPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommPortal.Service
{
    public interface IAuthProvider
    {
        public Task<HttpResponseMessage> Login(User user);
    }
}
