using AutoMapper;
using Onebrb.Api.Models;
using Onebrb.Api.Models.Item;
using Onebrb.Core.Models;
using Onebrb.Services;
using Onebrb.Services.Models.Item;

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
            CreateMap<EditItemRequestModel, EditItemServiceModel>();
        }
    }
}
