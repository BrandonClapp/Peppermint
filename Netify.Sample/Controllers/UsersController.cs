﻿using Microsoft.AspNetCore.Mvc;
using Netify.Common.Entities;
using Netify.Common.Services;
using System.Collections.Generic;
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

        [HttpGet("")]
        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return users;
        }

        [HttpGet("{userId}")]
        public async Task<UserEntity> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            return user;
        }
    }
}