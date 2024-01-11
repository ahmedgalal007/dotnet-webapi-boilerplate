using System;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
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
        builder.Property(e => e.culturCode).HasMaxLength(6);
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
        builder.Property(e => e.culturCode).HasMaxLength(6);
        builder.Property(e => e.Title).HasMaxLength(150);
        builder.HasIndex(e => e.culturCode);
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
        builder.Property(e => e.culturCode).HasMaxLength(6);
        builder.Property(e => e.Title).HasMaxLength(_seoSettings.NewsTitleMaxLength ?? 160);
        builder.Property(e => e.Description).HasMaxLength(300);
        builder.Property(e => e.SubTitle).HasMaxLength(_seoSettings.NewsSubTitleMaxLength ?? 160);
        builder.Property(e => e.SEOTitle).HasMaxLength(_seoSettings.SEOTitleMaxLength ?? 60);
        builder.Property(e => e.SocialTitle).HasMaxLength(_seoSettings.SocialTitleMaxLength ?? 60);
        builder.Property(e => e.Body).HasMaxLength(4000);
    }
}