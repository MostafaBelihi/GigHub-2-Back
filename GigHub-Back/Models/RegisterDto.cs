using System;
namespace GigHubBack.Models
{
    public class RegisterDto
    {
        public AppUser User
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
    }
}
