using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Domain.Common.Enumerators;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Domain.Medias;
using FSH.WebApi.Domain.Medias.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;
using Namotion.Reflection;
using System;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

// public class MediaConfig<T> : IEntityTypeConfiguration<MediaBaseEntity<T>>
//     where T : ILocalizableEntity, new()
// {
//     public void Configure(EntityTypeBuilder<MediaBaseEntity<T>> builder)
//     {
//         builder.IsMultiTenant();
//         builder.ToTable("Media", nameof(SchemaNames.Media));
//         builder.HasDiscriminator(e => e.TypeName)
//         .HasValue<Image>("Image");
//     }
// }

public class MediaConfig : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable("Media", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        .HasValue<Image>("Image")
        .HasValue<Video>("Video");

        builder
            .HasMany(e => e.Keywords)
            .WithMany(e => e.Medias)
            .UsingEntity(
                "KeywordsMedia",
                l => l.HasOne(typeof(Media)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media.Id)),
                r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
                j => j.ToTable("KeywordsMedia", nameof(SchemaNames.Media))
                            .HasKey("MediaId", "KeywordId")
            );
    }
}

public class LocalizedMediaConfig : IEntityTypeConfiguration<LocalizedMedia>
{
    public void Configure(EntityTypeBuilder<LocalizedMedia> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        .HasValue<LocalizedImage>("LocalizedImage")
        .HasValue<LocalizedVideo>("LocalizedVideo");
    }
}

public class ImageConfig : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("Media", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
            .HasValue<Image>("Image");

        builder.HasMany(e => e.ImageVersions).WithOne()
            .HasForeignKey(e => e.ImageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
public class LocalizedImageConfig : IEntityTypeConfiguration<LocalizedImage>
{
    public void Configure(EntityTypeBuilder<LocalizedImage> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        .HasValue<LocalizedImage>("LocalizedImage");
    }
}


public class VideoConfig : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("Media", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        .HasValue<Video>("Video");
    }
}
public class LocalizedVideoConfig : IEntityTypeConfiguration<LocalizedVideo>
{
    public void Configure(EntityTypeBuilder<LocalizedVideo> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        .HasValue<LocalizedVideo>("LocalizedVideo");
    }
}

public class ImageVersionConfig : IEntityTypeConfiguration<ImageVersion>
{
    public void Configure(EntityTypeBuilder<ImageVersion> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("ImageVersion", nameof(SchemaNames.Media));
        builder.Property(e => e.EnumImageSize).HasConversion(v => v.Value, v => EnumImageSizes.FromValue(v));
        builder.HasOne<Image>().WithMany().HasForeignKey(e => e.ImageId).OnDelete(DeleteBehavior.Cascade);
    }
}

