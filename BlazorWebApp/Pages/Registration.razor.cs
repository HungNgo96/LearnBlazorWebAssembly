using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Requests.Auth;

namespace BlazorWebApp.Pages
{
    public partial class Registration : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private UserForRegistrationRequest _userForRegistration = new UserForRegistrationRequest();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }

        public async Task Register()
        {
            ShowRegistrationErrors = false;

            var result = await AuthenticationService.RegisterUser(_userForRegistration, cts.Token);
            if (!result.Succeeded)
            {
                Errors = result.Messages;
                ShowRegistrationErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
