using System;
namespace GigHubBack.Models
{
    // Token carrier
    public class JwtPacket
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
    }
}
