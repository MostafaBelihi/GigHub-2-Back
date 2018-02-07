using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
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
        private UserManager<AppUser> _userManager;
        private AppIdentityDbContext _context;

        public AuthController(UserManager<AppUser> usrMgr, AppIdentityDbContext context)
        {
            _userManager = usrMgr;
            _context = context;
        }

        [HttpGet("getUsers")]
        public IEnumerable<AppUser> GetUsers()
        {
            return _userManager.Users;
        }

        [HttpPost("register")]
        public async Task<JwtPacket> Register([FromBody]RegisterDto regsiter)
        {
            // Prepare response object
            var packet = new JwtPacket();

            // Add new user
            var result = await _userManager.CreateAsync(regsiter.User, regsiter.Password);

            if (result.Succeeded)
            {
                // Save
                await _context.SaveChangesAsync();

                // Create token
                var jwt = new JwtSecurityToken();       // create token
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);     // encode token

                packet.Token = encodedJwt;
                packet.FirstName = regsiter.User.FirstName;
                packet.IsError = false;
            }
            else
            {
                packet.IsError = true;
            }

            return packet;
        }
    }
}
