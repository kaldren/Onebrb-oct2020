using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Api.Constants;
using Onebrb.Core.Models;
using Onebrb.Core.RequestModels;
using Onebrb.Core.ResponseModels;

namespace Onebrb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserRequestModel requestModel)
        {
            var user = _mapper.Map<User>(requestModel);

            var result = await _userManager.CreateAsync(user, requestModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(ResponseMessages.BadRequestCouldntCreateAccount);
            }

            var response = _mapper.Map<UserResponseModel>(requestModel);

            return Created($"api/users/{response.UserName}", response);
        }
    }
}
