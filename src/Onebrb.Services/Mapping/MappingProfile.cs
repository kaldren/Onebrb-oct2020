using AutoMapper;
using Onebrb.Core.Models;
using Onebrb.Services.Models.Category;
using Onebrb.Services.Models.Rating;
using Onebrb.Services.Services;

namespace Onebrb.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Item
            CreateMap<Item, ItemServiceModel>();
            CreateMap<ItemServiceModel, Item>();

            // Category
            CreateMap<Category, CategoryServiceModel>();
            CreateMap<CategoryServiceModel, Category>();

            // Rating
            CreateMap<Rating, RatingServiceModel>();
        }
    }
}
