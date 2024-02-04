using Albegra.SecureCoding.Auth.API.Data;
using Albegra.SecureCoding.Auth.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Albegra.SecureCoding.Auth.API.Services
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public JwtService(IConfiguration configuration,ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public AccessTokenDto GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
            var expDate = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpirationInMinutes"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }),
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience= _configuration["JwtSettings:Audience"],
                Issuer= _configuration["JwtSettings:Issuer"]
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return new AccessTokenDto
            {
                ExpDate = expDate,
                Token = jwtTokenHandler.WriteToken(token)
            };
             
        }

        public async Task<string> GenerateRefreshToken(IdentityUser user)
        {
            var randomNumber = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
                var base64 = Convert.ToBase64String(randomNumber);
                var existingToken=await _context.RefreshTokens
                    .FirstOrDefaultAsync(x=> x.UserId.Equals(user.Id) && !x.IsRevoked);

                if (existingToken is not null)
                {
                    existingToken.IsRevoked = true;
                    existingToken.Revoked = DateTime.UtcNow; 
                    _context.RefreshTokens.Update(existingToken);
                }
                

                _context.RefreshTokens.Add(new RefreshToken
                {
                    Created = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:RefreshTokenExpirationInDays"])),
                    UserId = user.Id,
                    IsRevoked = false,
                    Token=base64
                });
                await _context.SaveChangesAsync();
                return base64;
            }
        }


        public async Task<RefreshTokenDto> IsRefreshTokenValid(string refreshToken)
        {
            var token=_context.RefreshTokens
                .Include(x=>x.User)
                .FirstOrDefault(x => x.Token.Equals(refreshToken) && !x.IsRevoked);
            if(token is null)
            {
                return new RefreshTokenDto { IsValid=false};
            }

            if (token.ExpiryDate<=DateTime.UtcNow)
            {
                return new RefreshTokenDto { IsValid = false };
            }

            return new RefreshTokenDto { IsValid = true,User=token.User };
        }


    }
}
