using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using BBBBFLIX.Series;
using BBBBFLIX.WatchLists;
using BBBBFLIX.Users;
using BBBBFLIX.Ratings;
using BBBBFLIX.Seasons;
using BBBBFLIX.Episodes;
using BBBBFLIX.Notifications;
using BBBBFLIX.Notificaciones;
using BBBBFLIX.Monitoring;

namespace BBBBFLIX.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BBBBFLIXDbContext :
    AbpDbContext<BBBBFLIXDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Serie> Series {  get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Watchlist> WatchLists { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<APIMonitoring> APIMonitorings { get; set; }

    private readonly ICurrentUserService _currrentUserService;

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext 
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion


    public BBBBFLIXDbContext(DbContextOptions<BBBBFLIXDbContext> options, ICurrentUserService currrentUserService)
        : base(options)
    {
        _currrentUserService = currrentUserService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Include modules to your migration db context */
        builder.Entity<Serie>(b =>
        {
            b.ToTable(BBBBFLIXConsts.DbTablePrefix + "Series",
                BBBBFLIXConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(128);
        //    b.Property(x => x.Classification).IsRequired().HasMaxLength(128);
            b.Property(x => x.ReleasedDate).IsRequired().HasMaxLength(128);
            b.Property(x => x.Duration).IsRequired().HasMaxLength(128);
            b.Property(x => x.Genre).IsRequired().HasMaxLength(128);
            b.Property(x => x.Director).IsRequired().HasMaxLength(128);
            b.Property(x => x.Writer).IsRequired().HasMaxLength(128);
            b.Property(x => x.Actors).IsRequired().HasMaxLength(128);
            b.Property(x => x.Plot).IsRequired().HasMaxLength(300);
            b.Property(x => x.Language).IsRequired().HasMaxLength(128);
            b.Property(x => x.Country).IsRequired().HasMaxLength(128);
            b.Property(x => x.Poster).IsRequired().HasMaxLength(128);
            b.Property(x => x.ImdbRating).IsRequired().HasMaxLength(128);
            b.Property(x => x.ImdbVotes).IsRequired();
            b.Property(x => x.ImdbId).IsRequired().HasMaxLength(128);
            b.Property(x => x.Type).IsRequired().HasMaxLength(128);
            b.Property(x => x.numSeasons).IsRequired();

            b.HasMany(s => s.Seasons)
             .WithOne(t => t.Serie)
             .HasForeignKey(t => t.IdSerie)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
            // Serie -- Ratings
            b.HasMany(s => s.Ratings)
             .WithOne(c => c.Serie)
             .HasForeignKey(c => c.SerieId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        });
        //Season
        builder.Entity<Season>(b =>
        {
            b.ToTable(BBBBFLIXConsts.DbTablePrefix + "Temporadas",
                BBBBFLIXConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Title).IsRequired().HasMaxLength(128);
            b.Property(x => x.ReleasedDate).IsRequired().HasMaxLength(128);
            b.Property(x => x.SeasonNumber).IsRequired();

            // Season -- Serie
            b.HasOne(t => t.Serie)
             .WithMany(s => s.Seasons)
             .HasForeignKey(t => t.IdSerie)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
            // Season -- Episodes
            b.HasMany(t => t.Episodes)
             .WithOne(e => e.Season)
             .HasForeignKey(e => e.IdSeason)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        });
        builder.Entity<Episode>(b =>
        {
            b.ToTable(BBBBFLIXConsts.DbTablePrefix + "Episodios",
                BBBBFLIXConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Title).IsRequired().HasMaxLength(128);
            b.Property(x => x.Director).IsRequired().HasMaxLength(128);
            b.Property(x => x.Writer).IsRequired().HasMaxLength(128);
            b.Property(x => x.Synopsis).IsRequired().HasMaxLength(128);
            b.Property(x => x.Duration).IsRequired().HasMaxLength(128);
            b.Property(x => x.ReleasedTime).IsRequired();
            b.Property(x => x.EpisodeNumber).IsRequired();
            b.HasOne(e => e.Season)
             .WithMany(t => t.Episodes)
             .HasForeignKey(e => e.IdSeason)
             .OnDelete(DeleteBehavior.Cascade);
        });


        builder.Entity<Watchlist>(b =>
        {
            b.ToTable(BBBBFLIXConsts.DbTablePrefix + "Watchlists",
                BBBBFLIXConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.ModificatedDate).IsRequired();
            b.HasMany(ls => ls.Series)
                 .WithOne();
        });

        builder.Entity<Rating>(b =>
        {
            b.ToTable(BBBBFLIXConsts.DbTablePrefix + "Ratings",
                BBBBFLIXConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.RatingNumber).IsRequired();
            b.Property(x => x.Commentary);
            b.Property(x => x.CreatedDate).IsRequired();
            b.Property(x => x.SerieId).IsRequired();
            b.Property(x => x.UserId).IsRequired(); // Configura la propiedad UsuarioId como requerida
        });
        //Notification
        builder.Entity<Notification>(b =>
        {
            b.ToTable(BBBBFLIXConsts.DbTablePrefix + "Notificacion",
                BBBBFLIXConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.UserId).IsRequired();
            b.Property(x => x.Title).IsRequired();
            b.Property(x => x.Message).IsRequired();
            b.Property(x => x.IsRead).IsRequired();
            b.Property(x => x.Type).IsRequired();
            b.Property(x => x.CreatedDate).IsRequired();

        });
    }
}
