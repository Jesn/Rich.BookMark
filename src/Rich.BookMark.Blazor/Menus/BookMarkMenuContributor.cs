// using System.Threading.Tasks;
// using Rich.BookMark.Localization;
// using Rich.BookMark.MultiTenancy;
// using Volo.Abp.Identity.Blazor;
// using Volo.Abp.SettingManagement.Blazor.Menus;
// using Volo.Abp.TenantManagement.Blazor.Navigation;
// using Volo.Abp.UI.Navigation;
//
// namespace Rich.BookMark.Blazor.Menus;
//
// public class BookMarkMenuContributor : IMenuContributor
// {
//     public async Task ConfigureMenuAsync(MenuConfigurationContext context)
//     {
//         if (context.Menu.Name == StandardMenus.Main)
//         {
//             await ConfigureMainMenuAsync(context);
//         }
//     }
//
//     private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
//     {
//         var administration = context.Menu.GetAdministration();
//         var l = context.GetLocalizer<BookMarkResource>();
//
//         context.Menu.Items.Insert(
//             0,
//             new ApplicationMenuItem(
//                 BookMarkMenus.Home,
//                 l["Menu:Home"],
//                 "/",
//                 icon: "fas fa-home",
//                 order: 0
//             )
//         );
//
//         if (MultiTenancyConsts.IsEnabled)
//         {
//             administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
//         }
//         else
//         {
//             administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
//         }
//
//         administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
//         administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);
//
//         return Task.CompletedTask;
//     }
// }
