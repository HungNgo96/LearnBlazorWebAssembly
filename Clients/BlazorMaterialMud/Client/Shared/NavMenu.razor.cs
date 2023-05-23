using Microsoft.AspNetCore.Components;

namespace BlazorMaterialMud.Client.Shared
{
    public sealed partial class NavMenu
    {
        [Parameter]
        public bool SideBarOpen { get; set; }
    }
}
