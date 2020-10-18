using AutoMapper;
using Onebrb.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Onebrb.Core.Models;

namespace Onebrb.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemServiceModel>();
        }
    }
}
