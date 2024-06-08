using bb_demo_github.Data;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Diagnostics.CodeAnalysis;

namespace bb_demo_github.Components.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }


        [Inject]
        [NotNull]
        private NavigationManager? NavigationManager{ get; set; }

        [Inject]
        [NotNull]
        private AuthorizeService? AuthorizeService { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new() { Text = "Index", Icon = "fa-solid fa-fw fa-table", Url = "/" , Match = NavLinkMatch.All},
                new() { Text = "Counter", Icon = "fa-solid fa-fw fa-table", Url = "/counter" },
                new() { Text = "Table", Icon = "fa-solid fa-fw fa-table", Url = "/table" },
                new() { Text = "User", Icon = "fa-solid fa-fw fa-table", Url = "/users" }
            };

            return menus;
        }

        private async Task<bool> OnAuthorizing(string url)
        {
            var relativeUrl = NavigationManager.ToBaseRelativePath(url);
            bool result = await AuthorizeService.IsAuhorizeMenuAsync(1, relativeUrl);
            //bool result = AuthorizeService.IsAuhorizeMenu(1, relativeUrl);
            return result;
        }
    }
}
