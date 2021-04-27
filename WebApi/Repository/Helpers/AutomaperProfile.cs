using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository.ViewModels;

namespace WebApi.Repository.Helpers
{
    public class AutomaperProfile : Profile 
    {
        public AutomaperProfile()
        {
            CreateMap<Todo, TodoViewModel>().ReverseMap();
        }
    }
}
