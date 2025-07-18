using BBBBFLIX.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace BBBBFLIX.Permissions;

public class BBBBFLIXPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BBBBFLIXPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BBBBFLIXPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BBBBFLIXResource>(name);
    }
}
