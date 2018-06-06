using Microsoft.AspNetCore.Mvc;
using Peppermint.App.Controllers.Account.Requests;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Account
{
    [Route("[controller]")]
    public class AccountController : Controller
    {

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

            Request.Query.TryGetValue("returnUrl", out var returnUrl);
            if(returnUrl.Count == 0)
            {
                returnUrl = "/";
            }

            return Redirect(returnUrl);
        }
    }
}
