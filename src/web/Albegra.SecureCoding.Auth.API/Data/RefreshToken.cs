using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Albegra.SecureCoding.Auth.API.Data
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserId { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public IdentityUser User { get; set; }
    }
}
