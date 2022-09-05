
using Microsoft.AspNetCore.Identity;

namespace JobAdder.Users.API.Models
{
    public class User : IdentityUser
    {
        public Guid Token { get; set; }
        public string AuthPasswordHash { get; set; }
    }
}
