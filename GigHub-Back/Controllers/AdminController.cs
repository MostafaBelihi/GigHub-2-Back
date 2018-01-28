using System.Collections.Generic;
using GigHubBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GigHubBack.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        [HttpGet]
        public IEnumerable<AppUser> Get()
        {
            return userManager.Users;
        }
    }
}
