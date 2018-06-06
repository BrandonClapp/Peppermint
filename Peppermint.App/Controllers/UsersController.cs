using Microsoft.AspNetCore.Mvc;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<dynamic>> GetUsers()
        {
            var users = await _userService.GetUsers();
            var userDetails = new List<dynamic>();

            foreach(var user in users)
            {
                userDetails.Add(new {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Groups = await user.GetGroups()
                });
            }

            return userDetails;
        }

        [HttpGet("{userId}")]
        public async Task<dynamic> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);

            if (user == null)
                return null;

            return new
            {
                user.Id,
                user.UserName,
                user.Email,
                Groups = await user.GetGroups()
            };

        }
    }
}
