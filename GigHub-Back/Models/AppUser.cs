using System;
using Microsoft.AspNetCore.Identity;

namespace GigHubBack.Models
{
    public class AppUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
