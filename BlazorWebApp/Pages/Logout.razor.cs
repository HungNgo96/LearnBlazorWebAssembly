using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorWebApp.Pages
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            //NavigationManager.NavigateTo("/");
            NavigationManager.NavigateToLogout("/ll/logout", "/ll/demo");
        }
    }
}
