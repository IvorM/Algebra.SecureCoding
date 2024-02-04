using Albegra.SecureCoding.Auth.API.Data;
using Albegra.SecureCoding.Auth.API.Models;
using Albegra.SecureCoding.Auth.API.Services;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Albegra.SecureCoding.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        public AuthenticationController(UserManager<IdentityUser> userManager,IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                return BadRequest("Email or password is incorret");
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!passwordOk)
            {
                return BadRequest("Email or password is incorret");
            }

            var accessToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = await _jwtService.GenerateRefreshToken(user);

            return Ok(new
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken,
                ExpDate= accessToken.ExpDate
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tokenValidation=await _jwtService.IsRefreshTokenValid(refreshTokenRequest.RefreshToken);
            if (!tokenValidation.IsValid)
            {
                return BadRequest("Invalid token");
            }

            var newAccessToken = _jwtService.GenerateJwtToken(tokenValidation.User);
            var newRefreshToken = await _jwtService.GenerateRefreshToken(tokenValidation.User);
          

            return Ok(new
            {
                AccessToken = newAccessToken.Token,
                RefreshToken = newRefreshToken,
                ExpDate = newAccessToken.ExpDate
            });
        }
    }
}
