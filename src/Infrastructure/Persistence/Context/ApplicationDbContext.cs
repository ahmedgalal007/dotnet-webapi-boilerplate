using Finbuckle.MultiTenant;
using FSH.WebApi.Application.Common.Events;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Common.Localizations;
using FSH.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FSH.WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<News> News => Set<News>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
        modelBuilder.HasDefaultSchema(SchemaNames.Article);

        modelBuilder.Entity<Domain.Common.Localizations.Culture>().HasKey(x => x.Code);
        modelBuilder.Entity<Domain.Common.Localizations.Localization>().HasOne(x => x.Culture).WithOne().HasForeignKey<Domain.Common.Localizations.Localization>(x => x.CultureCode);
        modelBuilder.Entity<LocalizationSet>().HasMany(x => x.Localizations).WithOne(x => x.LocalizationSet).HasForeignKey(x => x.LocalizationSetId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<News>().HasOne(x => x.Title).WithMany().HasForeignKey(x => x.TitleId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<News>().HasOne(x => x.SEOTitle).WithMany().HasForeignKey(x => x.SEOTitleId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<News>().HasOne(x => x.SocialTitle).WithMany().HasForeignKey(x => x.SocialTitleId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<News>().HasOne(x => x.SubTitle).WithMany().HasForeignKey(x => x.SubTitleId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<News>().HasOne(x => x.Description).WithMany().HasForeignKey(x => x.DescriptionId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<News>().HasOne(x => x.Body).WithMany().HasForeignKey(x => x.BodyId).OnDelete(DeleteBehavior.NoAction);


    }
}