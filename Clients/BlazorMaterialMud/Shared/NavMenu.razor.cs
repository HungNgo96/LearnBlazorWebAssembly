using Microsoft.AspNetCore.Components;

namespace BlazorMaterialMud.Client.Shared
{
    public sealed partial class NavMenu
    {
        [Parameter]
        public bool SideBarOpen { get; set; }
        private static void OnMouseLeaveNav(bool isChange)
        {
          
            Console.WriteLine("NavMenu::OnMouseLeaveNav:" + isChange); 
        }
    }
}
