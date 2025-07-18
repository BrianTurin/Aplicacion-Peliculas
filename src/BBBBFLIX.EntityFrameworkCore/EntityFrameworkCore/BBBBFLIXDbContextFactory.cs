using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BBBBFLIX.Users;

namespace BBBBFLIX.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BBBBFLIXDbContextFactory : IDesignTimeDbContextFactory<BBBBFLIXDbContext>
{
    public BBBBFLIXDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        BBBBFLIXEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<BBBBFLIXDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new BBBBFLIXDbContext(builder.Options, new GetNewCurrentUserService());
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BBBBFLIX.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
