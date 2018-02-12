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

        // + Dependency Injection (DI)
        public AuthController(
            UserManager<AppUser> userManager, 
            AppIdentityDbContext context
        )
        {
            _userManager = userManager;
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
            // Add new user
            var result = await _userManager.CreateAsync(regsiter.User, regsiter.Password);

            if (result.Succeeded)
            {
                // Save
                await _context.SaveChangesAsync();
                return CreateJwtPacket(regsiter.User, false);
            }
            else
            {
                return CreateJwtPacket(regsiter.User, true);
            }
        }

        [HttpPost("login")]
        public async Task<JwtPacket> Login([FromBody]LoginDto login)
        {
            // + Check existence of the user using UserManager class
            AppUser user = await _userManager.FindByNameAsync(login.UserName);

            // + There is no need for SignInManager as of the reference PDF-1. 
            // + We will just use the check and authenticate following JWT method in V-4
            if (user != null)
                return CreateJwtPacket(user, false);
            else
                return CreateJwtPacket(user, true);
        }

        private JwtPacket CreateJwtPacket(AppUser user, bool isError)
        {
            var packet = new JwtPacket();

            packet.IsError = isError;

            if (!isError)
            {
                // Create token
                var jwt = new JwtSecurityToken();       // create token
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);     // encode token

                packet.Token = encodedJwt;
                packet.FirstName = user.FirstName;
            }

            return packet;
        }
    }
}
