using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Models
{
    public class UserDetails
    {
        [Key]
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
