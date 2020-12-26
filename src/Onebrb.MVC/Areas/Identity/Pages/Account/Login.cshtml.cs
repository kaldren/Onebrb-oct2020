using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Onebrb.Core.Models;
using System.Security.Claims;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using Onebrb.MVC.Config;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Onebrb.MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<User> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                // Logged in user
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.UserName);

                    // Set user id claim
                    if (!this.User.Claims.Any(x => x.Value == "Id"))
                    {
                        var userIdClaim = new Claim("Id", user.Id);
                        await _userManager.AddClaimAsync(user, userIdClaim);
                    }

                    // Set security salt claim
                    if (!this.User.Claims.Any(x => x.Value == "SecurityHash"))
                    {
                        var securitySaltClaim = new Claim("SecurityHash", user.SecurityHash);
                        await _userManager.AddClaimAsync(user, securitySaltClaim);
                    }


                    // Getting bearer token from Azure AD once we're successfully logged in
                    var tokenOptions = new ApiOptions();
                    _configuration.GetSection(ApiOptions.Token).Bind(tokenOptions);

                    IConfidentialClientApplication app;

                    app = ConfidentialClientApplicationBuilder.Create(tokenOptions.ClientId)
                        .WithClientSecret(tokenOptions.ClientSecret)
                        .WithAuthority(new Uri(tokenOptions.Authority))
                        .Build();

                    string[] resourceIds = new string[] { tokenOptions.ResourceId };

                    AuthenticationResult authResult = null;

                    try
                    {
                        authResult = await app.AcquireTokenForClient(resourceIds).ExecuteAsync();
                        Response.Cookies.Append("OnebrbApiToken", authResult.AccessToken);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to fetch bearer token. {ex.Message}");
                    }

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
