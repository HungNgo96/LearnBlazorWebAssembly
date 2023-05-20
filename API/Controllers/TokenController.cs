using Application.Interfaces.Services;
using Application.Options;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Requests.Auth;
using Shared.Responses.Auth;
using Shared.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace API.Controllers
{
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly JwtOption _options;

        public TokenController(UserManager<User> userManager,
                               ITokenService tokenService,
                               IOptions<JwtOption> options)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _options = options.Value;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if (request is null)
            {
                return BadRequest(await Result<AuthResponse>.FailAsync("Invalid client request", (int)HttpStatusCode.BadRequest));
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var username = principal.Identity.Name;
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest(await Result<AuthResponse>.FailAsync("Invalid client request", (int)HttpStatusCode.BadRequest));

            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            user.RefreshToken = _tokenService.GenerateRefreshToken();

            await _userManager.UpdateAsync(user);

            return Ok(await Result<AuthResponse>.SuccessAsync(data: new()
            {
                ExpiryInMinutes = int.Parse(_options.ExpiryInMinutes),
                Token = token,
                RefreshToken = user.RefreshToken,
            }, (int)HttpStatusCode.BadRequest));
        }
    }
}