using Microsoft.AspNetCore.Identity;

namespace Domain.Entity
{
    public class User: IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
