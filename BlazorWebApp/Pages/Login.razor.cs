using ApplicationClient.Extensions;
using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Requests.Auth;

namespace BlazorWebApp.Pages
{
    public sealed partial class Login : IDisposable
    {
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private UserForAuthenticationRequest _userForAuthentication = new UserForAuthenticationRequest();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_userForAuthentication, _tokenSource.Token);

            if (!result.Succeeded)
            {
                Error = result.Messages.First();
                ShowAuthError = true;
            }
            else
            {
                var url = NavigationManager.QueryString("returnUrl");
                if (!string.IsNullOrEmpty(url))
                {
                    NavigationManager.NavigateTo(url);
                }
                else
                {
                    NavigationManager.NavigateTo("/");
                }
            }
        }
    }
}