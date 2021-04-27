using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.ViewModels
{
    public class LoginResultViewModel
    {
        public string UserName { get; set; }
        public string Token { get; set; } 
        public string Role { get; set; }  

    }
}
