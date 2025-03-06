using Microsoft.AspNetCore.Identity;

namespace ProjectComp1640.Model
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
