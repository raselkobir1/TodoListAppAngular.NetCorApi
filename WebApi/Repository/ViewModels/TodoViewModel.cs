using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.ViewModels
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    }
}
