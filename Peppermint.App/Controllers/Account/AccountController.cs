using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Peppermint.App.Controllers.Account.Requests;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Account
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly EntityFactory _entityFactory;

        public AccountController(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            //return Task.FromResult<dynamic>(new { Login = true });
            return await Task.FromResult<IActionResult>(View());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest credentials)
        {
            var temp = await Task.FromResult<dynamic>(new { });

            // validate credentials.
            var user = await AuthenticateUser(credentials.UserName, credentials.Password);

            if (user == null)
                return View();

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

            Request.Query.TryGetValue("returnUrl", out var returnUrl);
            if(returnUrl.Count == 0)
            {
                returnUrl = "/";
            }

            return Redirect(returnUrl);
        }

        // Move to authentication service
        private async Task<User> AuthenticateUser(string username, string password)
        {
            // Assume that checking the database takes 500ms
            await Task.Delay(500);

            if (username.ToLower() == "brandon" && password == "password")
            {
                var user = _entityFactory.Make<User>();
                user.UserName = "Brandon";
                user.Email = "brandon@email.com";
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
