using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Onebrb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> PostAsync()
        {
            var user = new IdentityUser
            {
                UserName = "admin",
                Email = "drenski666@gmail.com",
            };

            var result = await _userManager.CreateAsync(user, "Parola123-");

            return result.Succeeded;
        }
    }
}
