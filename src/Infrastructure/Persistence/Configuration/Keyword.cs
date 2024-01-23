//using FSH.WebApi.Domain.Keywords;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
//internal class KeywordConfig : IEntityTypeConfiguration<Keyword>
//{
//    public void Configure(EntityTypeBuilder<Keyword> builder)
//    {

//    }
//}

//internal class LocalizedKeywordConfig : IEntityTypeConfiguration<LocalizedKeyword>
//{
//    public void Configure(EntityTypeBuilder<LocalizedKeyword> builder)
//    {
//        builder.Property(e => e.culturCode).HasMaxLength(3);
//        builder.HasIndex(e => e.culturCode);
//    }
//}


using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Infrastructure.Persistence.Configuration.CustomConfigurations;
using FSH.WebApi.Infrastructure.Persistence.Configuration;
using FSH.WebApi.Infrastructure.SEO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FSH.WebApi.Domain.Medias;
using FSH.WebApi.Domain.Article;

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
