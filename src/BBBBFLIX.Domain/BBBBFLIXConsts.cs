using Volo.Abp.Identity;

namespace BBBBFLIX;

public static class BBBBFLIXConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
    public const string CollectionDefinitionName = "BBBBFLIX collection";
}
