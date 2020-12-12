using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Onebrb.MVC.Config;

namespace Onebrb.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> GetBearerToken()
        {
            var tokenOptions = new TokenOptions();
            _configuration.GetSection(TokenOptions.Token).Bind(tokenOptions);

            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(tokenOptions.ClientId)
                .WithClientSecret(tokenOptions.ClientSecret)
                .WithAuthority(new Uri(tokenOptions.Authority))
                .Build();

            string[] resourceIds = new string[] { tokenOptions.ResourceId };

            AuthenticationResult result = null;

            try
            {
                result = await app.AcquireTokenForClient(resourceIds).ExecuteAsync();
                return result.AccessToken;
            }
            catch (Exception)
            {
                throw new Exception("Failed to fetch bearer token.");
            }
        }
    }
}
