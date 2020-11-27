using AutoMapper;
using Onebrb.Core.Models;
using Onebrb.Services.Services;

namespace Onebrb.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemServiceModel>();
            CreateMap<ItemServiceModel, Item>();
        }
    }
}
