using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationClient.Services
{
    public class RefreshTokenService
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly IAuthenticationService _authService;
        public RefreshTokenService(AuthenticationStateProvider authProvider, IAuthenticationService authService)
        {
            _authProvider = authProvider;
            _authService = authService;
        }
        public async Task<string> TryRefreshToken(CancellationToken cancellationToken)
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp")).Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;

            if (diff.TotalMinutes <= 2)
            {
                var result =  await _authService.RefreshToken(cancellationToken);

                if (result.Succeeded)
                {
                    return result.Data;
                }
            }
                
            return string.Empty;
        }
    }
}
