using AutoMapper;
using Onebrb.Core.Models;
using Onebrb.Core.RequestModels;
using Onebrb.Core.ResponseModels;
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
            CreateMap<UserRequestModel, User>();
            CreateMap<UserRequestModel, UserResponseModel>();
        }
    }
}
