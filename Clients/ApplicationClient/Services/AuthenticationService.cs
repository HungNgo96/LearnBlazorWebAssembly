using ApplicationClient.AuthProviders;
using ApplicationClient.Interfaces;
using ApplicationClient.Options;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Shared.Helper;
using Shared.Model.Http;
using Shared.Requests.Auth;
using Shared.Responses.Auth;
using Shared.Wrapper;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ApplicationClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UrlOption _urlOptions;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly JsonSerializerOptions _jsonOptions;
        public AuthenticationService(IHttpClientFactory httpClientFactory,
                                     IOptions<UrlOption> urlOptions,
                                     AuthenticationStateProvider authStateProvider,
                                     ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _urlOptions = urlOptions.Value;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }

        public async Task<IResult<AuthResponse>> Login(UserForAuthenticationRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("ProductsAPI");
            (var result, var errorModel) = await CallApi<UserForAuthenticationRequest, BaseResponse<AuthResponse>>
                 .PostAsJsonAsync(request, string.Empty, path: _urlOptions.Accounts.Login
                 , new HttpOption { Client = client }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<AuthResponse>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<AuthResponse>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            await _localStorage.SetItemAsync("authToken", result.Data.Token);
            await _localStorage.SetItemAsync("refreshToken", result.Data.RefreshToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Data.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Data.Token);

            return await Result<AuthResponse>.SuccessAsync(data: result.Data!);
        }

        public async Task Logout()
        {
            var client = _httpClientFactory.CreateClient("ProductsAPI");
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<IResult<RegistrationResponse>> RegisterUser(UserForRegistrationRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("ProductsAPI");
            (var result, var errorModel) = await CallApi<UserForRegistrationRequest, BaseResponse<RegistrationResponse>>
                 .PostAsJsonAsync(request, string.Empty, path: _urlOptions.Accounts.RegisterUser
                 , new HttpOption { Client = client }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<RegistrationResponse>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<RegistrationResponse>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            return await Result<RegistrationResponse>.SuccessAsync(data: result.Data!);
        }

        public async Task<IResult<string>> RefreshToken(CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("ProductsAPI");
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

            (var result, var errorModel) = await CallApi<RefreshTokenRequest, BaseResponse<AuthResponse>>
                .PostAsJsonAsync(new RefreshTokenRequest { Token = token, RefreshToken = refreshToken }, string.Empty, _urlOptions.Token.Refresh,
               new HttpOption() { Client = client }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<string>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<string>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            await _localStorage.SetItemAsync("authToken", result.Data.Token);
            await _localStorage.SetItemAsync("refreshToken", result.Data.RefreshToken);

            return await Result<string>.SuccessAsync(result.Data.Token);
        }
    }
}
