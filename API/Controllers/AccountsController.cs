using Application.Interfaces.Services;
using Application.Options;
using Application.Ultilities;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOption _options;
        private readonly ILogger<AccountsController> _logger;
        private readonly LazyInstanceUtils<ITokenService> _tokenService;

        public AccountsController(UserManager<User> userManager,
                                  IOptions<JwtOption> options,
                                  IServiceProvider serviceProvider,
                                  ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _options = options.Value;
            _tokenService = new LazyInstanceUtils<ITokenService>(serviceProvider);
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationRequest userForRegistration)
        {
            var resultWrap = new Result<RegistrationResponse>();

            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest(resultWrap);
            var user = new User { UserName = userForRegistration.Email, Email = userForRegistration.Email };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                resultWrap.Data.IsSuccessfulRegistration = false;
                resultWrap.Data.Errors = errors;
                return BadRequest(resultWrap);
            }
            resultWrap.Succeeded = true;
            resultWrap.Messages = new List<string>() { "Tạo tài khoản thành công" };

            return Ok(resultWrap);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized(await Result<AuthResponse>.FailAsync(message: "Invalid Authentication", statusCode: (int)HttpStatusCode.Unauthorized));

            var signingCredentials = _tokenService.Value.GetSigningCredentials();
            var claims = await _tokenService.Value.GetClaims(user);
            var tokenOptions = _tokenService.Value.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            user.RefreshToken = _tokenService.Value.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(await Result<AuthResponse>.SuccessAsync(data: new()
            {
                Token = token,
                ExpiryInMinutes = int.Parse(_options.ExpiryInMinutes),
                RefreshToken = user.RefreshToken
            }, message: "Authentication", statusCode: (int)HttpStatusCode.OK));
        }
    }
}