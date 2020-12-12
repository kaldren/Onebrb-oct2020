using AutoMapper;
using Onebrb.Api.Models;
using Onebrb.Core.Models;
using Onebrb.Services;

namespace Onebrb.Api.Mappings
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            // User
            CreateMap<UserRequestModel, User>();
            CreateMap<UserRequestModel, UserResponseModel>();

            // Item
            CreateMap<ItemRequestModel, ItemServiceModel>();
        }
    }
}
