using AutoMapper;
using Onebrb.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Onebrb.Core.Models;

namespace Onebrb.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemServiceModel>();
        }
    }
}
