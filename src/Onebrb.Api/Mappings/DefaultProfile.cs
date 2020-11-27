using AutoMapper;
using Onebrb.Core.Models;
using Onebrb.Core.RequestModels;
using Onebrb.Core.ResponseModels;
using Onebrb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
