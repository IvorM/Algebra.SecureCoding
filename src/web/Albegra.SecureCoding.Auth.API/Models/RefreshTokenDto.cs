using Microsoft.AspNetCore.Identity;

namespace Albegra.SecureCoding.Auth.API.Models
{
    public class RefreshTokenDto
    {
        public bool IsValid { get; set; }
        public IdentityUser? User { get; set; }
    }
}
