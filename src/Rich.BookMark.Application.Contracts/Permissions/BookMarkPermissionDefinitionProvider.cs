using Rich.BookMark.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rich.BookMark.Permissions;

public class BookMarkPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookMarkPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookMarkPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookMarkResource>(name);
    }
}
