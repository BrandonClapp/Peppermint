using Microsoft.AspNetCore.Mvc;
using Netify.Common.Data;
using Netify.Common.Models;
using Netify.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netify.Sample.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<User> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            return user;
        }
    }
}
