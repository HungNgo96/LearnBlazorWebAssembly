using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages
{
    public partial class CustomNotFound
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void NavigateToHome()
        {
            NavigationManager.NavigateTo("/ll");
        }

        protected override void OnInitialized()
        {
            //NavigationManager.NavigateTo("/ll");
        }
    }
}
