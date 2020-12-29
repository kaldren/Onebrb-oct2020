using AutoMapper;
using Onebrb.MVC.Models.Category;
using Onebrb.MVC.Models.Item;
using Onebrb.MVC.Models.Rating;
using OnebrbApiClient.Models;

namespace Onebrb.MVC.Mappings
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<ItemServiceModel, ItemViewModel>();
            CreateMap<ItemServiceModel, EditItemViewModel>()
                .ForMember(m => m.ItemId, opt => opt.MapFrom(m => m.Id));
            CreateMap<CreateItemRequestModel, ItemServiceModel>();

            CreateMap<CategoryServiceModel, CategoryModel>();

            CreateMap<Rating, RatingModel>();
        }
    }
}
