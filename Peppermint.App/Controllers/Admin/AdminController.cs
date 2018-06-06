using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize]
    public class AdminController : Controller
    {
        public Task<dynamic> Index()
        {
            return Task.FromResult<dynamic>(new { Admin = true });
        }
    }
}
