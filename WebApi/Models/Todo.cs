﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string status { get; set; } 
    }
}
