using AuthorizationMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.RepoLayer
{
    public class UserRepo : IUserRepo
    {
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(UserRepo));
        private static List<UserDetails> user;
        public UserRepo()
        {
            user = new List<UserDetails>()
            {
                new UserDetails{Userid=1, Username="user1", Password="user1@123"},
                new UserDetails{Userid=2, Username="user2", Password="user2@123"},
                new UserDetails{Userid=3, Username="user3", Password="user3@123"},
                new UserDetails{Userid=4, Username="user4", Password="user3@123"},
                new UserDetails{Userid=5, Username="user5", Password="user3@123"},
                new UserDetails{Userid=6, Username="user6", Password="user3@123"},
            };
        }

        public UserDetails GetUserDetails(UserDetails valuser)
        {
            try
            {
                UserDetails vuser = user.FirstOrDefault(c => c.Username == valuser.Username && c.Password == valuser.Password);
                return vuser;
            }
            catch (Exception e)
            {
                _logger.Error("Error in getting user details as " + e.Message);
                return null;
            }
        }
    }
}
