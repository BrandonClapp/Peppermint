﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Peppermint.App.Controllers.Account.Requests;
using Peppermint.App.ViewModels.Account;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Account
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly EntityFactory _entityFactory;
        private readonly Peppermint.Core.Services.AuthenticationService _authentication;
        private LoginViewModel _loginView;

        public AccountController(LoginViewModel loginView, EntityFactory entityFactory, 
            Peppermint.Core.Services.AuthenticationService authentication)
        {
            _entityFactory = entityFactory;
            _authentication = authentication;
            _loginView = loginView;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var vm = await _loginView.Build();
            return View(vm);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest credentials)
        {
            var temp = await Task.FromResult<dynamic>(new { });

            if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Password))
                throw new ArgumentNullException();

            // validate credentials.
            var user = await _authentication.AuthenticateUser(credentials.UserName, credentials.Password);

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

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("loggedout");
        }

        [HttpGet("loggedout")]
        public async Task<IActionResult> LoggedOut()
        {
            var vm = await Task.Run(() => { return _loginView.Build(); });
            return View("SignedOut", vm);
        }
    }
}
