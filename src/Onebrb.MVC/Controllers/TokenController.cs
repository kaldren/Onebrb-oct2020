using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
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
            var tokenOptions = new ApiOptions();
            _configuration.GetSection(ApiOptions.Token).Bind(tokenOptions);

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
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch bearer token.");
            }
        }
    }
}
