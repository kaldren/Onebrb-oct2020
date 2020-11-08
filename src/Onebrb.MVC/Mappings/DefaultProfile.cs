using AutoMapper;
using Onebrb.MVC.Models.Item;
using Onebrb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Mappings
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<ItemServiceModel, ItemViewModel>();
        }
    }
}
