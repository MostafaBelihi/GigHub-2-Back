using System.Collections.Generic;
using GigHubBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GigHubBack.Controllers
{
    [Produces("application/json")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private UserManager<AppUser> userManager;

        public AuthController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        [HttpGet("getUsers")]
        public IEnumerable<AppUser> GetUsers()
        {
            return userManager.Users;
        }
    }
}
