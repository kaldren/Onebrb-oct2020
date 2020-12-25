using AutoMapper;
using Onebrb.MVC.Models.Item;
using OnebrbApiClient.Models;

namespace Onebrb.MVC.Mappings
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<ItemServiceModel, ItemViewModel>();
            CreateMap<ItemServiceModel, EditItemViewModel>();
            CreateMap<CreateItemRequestModel, ItemServiceModel>();
        }
    }
}
