using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; } 
    }
}
