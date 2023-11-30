using Azure.Core.Pipeline;
using Finbuckle.MultiTenant;
using FSH.WebApi.Application.Common.Events;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Storage;
// using FSH.WebApi.Domain.Common.Localizations;
using FSH.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using File = FSH.WebApi.Domain.Storage.File;

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
    public DbSet<Folder> Folders => Set<Folder>();
    public DbSet<File> Files => Set<Domain.Storage.File>();
    // public DbSet<Culture> Cultures => Set<Culture>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
        // modelBuilder.Entity<Domain.Common.Localizations.Culture>().ToTable("Culture", tableBuilder => { tableBuilder.Property(x => x.Code).HasColumnName("Code"); }).HasKey(x => x.Code);
        // modelBuilder.Entity<Domain.Common.Localizations.Localization>().HasOne(x => x.Culture).WithOne().HasForeignKey<Domain.Common.Localizations.Localization>(x => x.CultureCode);
        // modelBuilder.Entity<LocalizationSet>().HasMany(x => x.Localizations).WithOne(x => x.LocalizationSet).HasForeignKey(x => x.LocalizationSetId).OnDelete(DeleteBehavior.Cascade);
        // modelBuilder.Entity<LocalizationSet>().HasMany(x => x.Localizations).WithOne(x => x.LocalizationSet).HasForeignKey(x => x.LocalizationSetId).OnDelete(DeleteBehavior.Cascade);
        //modelBuilder.Entity<News>().HasOne(x => x.Title).WithOne().HasForeignKey<News>(x => x.TitleId).OnDelete(DeleteBehavior.NoAction).;
        //modelBuilder.Entity<News>().HasOne(x => x.SEOTitle).WithOne().HasForeignKey<News>(x => x.SEOTitleId).OnDelete(DeleteBehavior.NoAction);
        //modelBuilder.Entity<News>().HasOne(x => x.SocialTitle).WithOne().HasForeignKey<News>(x => x.SocialTitleId).OnDelete(DeleteBehavior.NoAction);
        //modelBuilder.Entity<News>().HasOne(x => x.SubTitle).WithOne().HasForeignKey<News>(x => x.SubTitleId).OnDelete(DeleteBehavior.NoAction);
        //modelBuilder.Entity<News>().HasOne(x => x.Description).WithOne().HasForeignKey<News>(x => x.DescriptionId).OnDelete(DeleteBehavior.NoAction);
        //modelBuilder.Entity<News>().HasOne(x => x.Body).WithOne().HasForeignKey<News>(x => x.BodyId).OnDelete(DeleteBehavior.NoAction);

    }
}