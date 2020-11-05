using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Models;

namespace Onebrb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> PostAsync()
        {
            var user = new User
            {
                UserName = "admin",
                Email = "drenski666@gmail.com",
                FirstName = "Kaloyan",
                LastName = "Drenski"
            };

            var result = await _userManager.CreateAsync(user, "Parola123-");

            return result.Succeeded;
        }
    }
}
