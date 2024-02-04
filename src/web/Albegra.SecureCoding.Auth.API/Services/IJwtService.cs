using Albegra.SecureCoding.Auth.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Albegra.SecureCoding.Auth.API.Services
{
    public interface IJwtService
    {
        AccessTokenDto GenerateJwtToken(IdentityUser user);
        Task<string> GenerateRefreshToken(IdentityUser user);
        Task<RefreshTokenDto> IsRefreshTokenValid(string refreshToken);
    }
}
