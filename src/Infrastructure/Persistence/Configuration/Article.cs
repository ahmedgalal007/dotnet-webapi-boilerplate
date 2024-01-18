using System;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Domain.Medias;
using FSH.WebApi.Infrastructure.Persistence.Configuration.CustomConfigurations;
using FSH.WebApi.Infrastructure.SEO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
public class CategoryConfig : EntityTypeConfigurationDependency<Category>
{
    private readonly SEOSettings _seoSettings;

    // public CategoryConfig() { _seoSettings = new SEOSettings(); }
    public CategoryConfig(IConfiguration config)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
    }

    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(Category), nameof(SchemaNames.Article));
        builder.Property(e => e.Slug).HasMaxLength(_seoSettings.NewsSlugMaxLength ?? 50);
        builder.Property(e => e.Color).HasMaxLength(10);
        builder.HasMany(e => e.Childrens).WithOne().HasForeignKey(e => e.ParentId);
        builder.HasMany(e => e.News).WithOne();
    }
}

public class LocalizedCategoryConfig : EntityTypeConfigurationDependency<LocalizedCategory>
{
    private readonly SEOSettings _seoSettings;

    // public LocalizedCategoryConfig() { _seoSettings = new SEOSettings(); }
    public LocalizedCategoryConfig(IConfiguration config)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
    }

    public override void Configure(EntityTypeBuilder<LocalizedCategory> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(LocalizedCategory), nameof(SchemaNames.Article));
        builder.Property(e => e.CulturCode).HasMaxLength(6);
        builder.Property(e => e.Name).HasMaxLength(150);
        builder.Property(e => e.Description).HasMaxLength(300);
    }
}

public class KeywordConfig : EntityTypeConfigurationDependency<Keyword>
{
    private readonly SEOSettings _seoSettings;

    // public KeywordConfig() { _seoSettings = new SEOSettings(); }
    public KeywordConfig(IConfiguration config)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
    }

    public override void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(Keyword), nameof(SchemaNames.Article));
        builder.Property(e => e.Slug).HasMaxLength(_seoSettings.NewsSlugMaxLength ?? 50);
        builder.Property(e => e.Color).HasMaxLength(10);

        builder
            .HasMany(e => e.Medias)
            .WithMany(e => e.Keywords)
            .UsingEntity(
                "KeywordMedia",
                l => l.HasOne(typeof(Media)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media.Id)),
                r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
                j => j.ToTable("KeywordMedia", nameof(SchemaNames.Media))
                            .HasKey("MediaId", "KeywordId"));
        builder
            .HasMany(e => e.News)
            .WithMany(e => e.Keywords)
            .UsingEntity(
                "KeywordNews",
                l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
                r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
                j => j.ToTable("KeywordNews", nameof(SchemaNames.Article))
                            .HasKey("NewsId", "KeywordId"));
        builder
            .HasMany(e => e.Albums)
            .WithMany(e => e.Keywords)
            .UsingEntity(
                "KeywordAlbum",
                l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
                r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
                j => j.ToTable("KeywordAlbum", nameof(SchemaNames.Media))
                            .HasKey("AlbumId", "KeywordId"));
    }
}

public class LocalizedKeywordConfig : EntityTypeConfigurationDependency<LocalizedKeyword>
{
    private readonly SEOSettings _seoSettings;

    // public LocalizedKeywordConfig() { _seoSettings = new SEOSettings(); }
    public LocalizedKeywordConfig(IConfiguration config)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
    }

    public override void Configure(EntityTypeBuilder<LocalizedKeyword> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(LocalizedKeyword), nameof(SchemaNames.Article));
        builder.Property(e => e.CulturCode).HasMaxLength(6);
        builder.Property(e => e.Title).HasMaxLength(150);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.HasIndex(e => e.CulturCode);
    }
}

public class NewsConfig : EntityTypeConfigurationDependency<News>
{
    private readonly SEOSettings _seoSettings;

    // public NewsConfig() { _seoSettings = new SEOSettings(); }
    public NewsConfig(IConfiguration config)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
    }

    public override void Configure(EntityTypeBuilder<News> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(News), nameof(SchemaNames.Article));
        builder.Property(e => e.Slug).HasMaxLength(_seoSettings.SlugOptions.MaximumLength);
        builder.Property(e => e.MainImage).HasMaxLength(250);
        builder
            .HasMany(e => e.Keywords)
            .WithMany(e => e.News)
            .UsingEntity(
                "KeywordNews",
                l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
                r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
                j => j.ToTable("KeywordNews", nameof(SchemaNames.Article))
                            .HasKey("NewsId", "KeywordId"));
        builder
            .HasMany(e => e.Medias)
            .WithMany(e => e.News)
            .UsingEntity(
                "NewsMedia",
                l => l.HasOne(typeof(Media)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media.Id)),
                r => r.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
                j => j.ToTable("NewsMedia", nameof(SchemaNames.Media))
                            .HasKey("MediaId", "NewsId"));
        builder
            .HasMany(e => e.Albums)
            .WithMany(e => e.News)
            .UsingEntity(
                "NewsAlbum",
                l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
                r => r.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
                j => j.ToTable("NewsAlbum", nameof(SchemaNames.Media))
                            .HasKey("AlbumId", "NewsId"));
    }
}

public class LocalizedNewsConfig : EntityTypeConfigurationDependency<LocalizedNews>
{
    private readonly SEOSettings _seoSettings;

    // public LocalizedNewsConfig() { _seoSettings = new SEOSettings(); }
    public LocalizedNewsConfig(IConfiguration config)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
    }

    public override void Configure(EntityTypeBuilder<LocalizedNews> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(LocalizedNews), nameof(SchemaNames.Article));
        builder.Property(e => e.CulturCode).HasMaxLength(6);
        builder.Property(e => e.Title).HasMaxLength(_seoSettings.NewsTitleMaxLength ?? 160);
        builder.Property(e => e.Description).HasMaxLength(300);
        builder.Property(e => e.SubTitle).HasMaxLength(_seoSettings.NewsSubTitleMaxLength ?? 160);
        builder.Property(e => e.SEOTitle).HasMaxLength(_seoSettings.SEOTitleMaxLength ?? 60);
        builder.Property(e => e.SocialTitle).HasMaxLength(_seoSettings.SocialTitleMaxLength ?? 60);
        builder.Property(e => e.Body).HasMaxLength(4000);
    }
}