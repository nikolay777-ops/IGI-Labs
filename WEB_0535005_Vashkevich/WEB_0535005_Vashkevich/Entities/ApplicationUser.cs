using Microsoft.AspNetCore.Identity;

namespace WEB_0535005_Vashkevich.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? Avatar { get; set; }
        public string? Country { get; set; } 
    }
}
