using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorMaterialMud.Client.Shared
{
    public sealed partial class MainLayout
    {
        private MudTheme _currentTheme = new();
        private bool _sidebarOpen = false;


        private void ToggleTheme(MudTheme changedTheme) => _currentTheme = changedTheme;

        private void ToggleSidebar() => _sidebarOpen = !_sidebarOpen;
    }
}
