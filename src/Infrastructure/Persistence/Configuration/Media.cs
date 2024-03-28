using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Domain.Common.Enumerators;
using FSH.WebApi.Domain.Keywords;
using FSH.WebApi.Domain.Medias;
using FSH.WebApi.Domain.Medias.Images;
using FSH.WebApi.Domain.Medias.Videos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;
using Namotion.Reflection;
using System;
using File = FSH.WebApi.Domain.Storage.File;
// using Image = FSH.WebApi.Domain.Medias.Images.Image;

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

//public class MediaLocalizedImageConfig : IEntityTypeConfiguration<Media<LocalizedImage>>
//{
//    public void Configure(EntityTypeBuilder<Media<LocalizedImage>> builder)
//    {
//        builder.IsMultiTenant();
//        builder.ToTable("Media", nameof(SchemaNames.Media));
//        builder.HasDiscriminator(e => e.TypeName)
//        .HasValue<Image>("Image");
//    }
//}
//public class MediaLocalizedVideoConfig : IEntityTypeConfiguration<Media<LocalizedVideo>>
//{
//    public void Configure(EntityTypeBuilder<Media<LocalizedVideo>> builder)
//    {
//        builder.IsMultiTenant();
//        builder.ToTable("Media", nameof(SchemaNames.Media));
//        builder.HasDiscriminator(e => e.TypeName)
//        .HasValue<Video>("Video");
//    }
//}


public class ImageConfig : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("Media", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        // .HasValue<Media<LocalizedImage>>("Image")
            .HasValue<Image>("Image");
        // .HasValue<Media<LocalizedVideo>>("Video")
        // .HasValue<Video>("Video");
        // .HasValue<Media<LocalizedMedia>>("Media");


        //builder
        //    .HasMany(e => e.News)
        //    .WithMany(e => (IEnumerable<Image>)e.Medias)
        //    .UsingEntity(
        //        "NewsMedia",
        //        l => l.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        r => r.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
        //        j => j.ToTable("NewsMedia", nameof(SchemaNames.Media))
        //                    .HasKey("MediaId", "NewsId")
        //    );
        //builder
        //    .HasMany(e => e.Keywords)
        //    .WithMany(e => (IEnumerable<Image>)e.Medias)
        //    .UsingEntity(
        //        "KeywordMedia",
        //        l => l.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
        //        j => j.ToTable("KeywordMedia", nameof(SchemaNames.Media))
        //                    .HasKey("MediaId", "KeywordId")
        //    );

        //builder
        //    .HasMany(e => e.Albums)
        //    .WithMany(e => (IEnumerable<Image>)e.Medias )
        //    .UsingEntity(
        //        "AlbumMedia",
        //        l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
        //        r => r.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        j => j.ToTable("AlbumMedia", nameof(SchemaNames.Media))
        //                    .HasKey("MediaId", "AlbumId")
        //    );

        builder.HasMany(e => e.ImageVersions).WithOne()
               .HasForeignKey(e => e.ImageId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}

public class VideoConfig : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("Media", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        //.HasValue<Media<LocalizedImage>>("Image")
        // .HasValue<Image>("Image")
        //.HasValue<Media<LocalizedVideo>>("Video")
        .HasValue<Video>("Video");
        //.HasValue<Media<LocalizedMedia>>("Media");

        //builder
        //    .HasMany(e => e.News)
        //    .WithMany(e => (IEnumerable<Video>)e.Medias)
        //    .UsingEntity(
        //        "NewsMedia",
        //        l => l.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        r => r.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
        //        j => j.ToTable("NewsMedia", nameof(SchemaNames.Media))
        //                    .HasKey("MediaId", "NewsId")
        //    );
        //builder
        //    .HasMany(e => e.Keywords)
        //    .WithMany(e => (IEnumerable<Video>)e.Medias)
        //    .UsingEntity(
        //        "KeywordMedia",
        //        l => l.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
        //        j => j.ToTable("KeywordMedia", nameof(SchemaNames.Media))
        //                    .HasKey("MediaId", "KeywordId")
        //    );

        //builder
        //    .HasMany(e => e.Albums)
        //    .WithMany(e => e.Medias as IEnumerable<Video>)
        //    .UsingEntity(
        //        "AlbumMedia",
        //        l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
        //        r => r.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        j => j.ToTable("AlbumMedia", nameof(SchemaNames.Media))
        //                    .HasKey("MediaId", "AlbumId")
        //    );

    }
}

public class MediaConfig : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("Media", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
            .HasValue(typeof(Image), "Image")
            .HasValue(typeof(Video), "Video")
            .HasValue<Media>("Media");
        //        builder
        //            .HasMany(e => e.News)
        //            .WithMany(e => e.Medias)
        //            .UsingEntity(
        //                "NewsMedia",
        //                l => l.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //                r => r.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
        //                j => j.ToTable("NewsMedia", nameof(SchemaNames.Media))
        //                            .HasKey("MediaId", "NewsId")
        //            );
        //        builder
        //            .HasMany(e => e.Keywords)
        //            .WithMany(e => e.Medias)
        //            .UsingEntity(
        //                "KeywordMedia",
        //                l => l.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //                r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
        //                j => j.ToTable("KeywordMedia", nameof(SchemaNames.Media))
        //                            .HasKey("MediaId", "KeywordId")
        //            );
        builder
            .HasMany(e => e.Albums)
            .WithMany(e => e.Medias)
            .UsingEntity(
                "AlbumMedia",
                l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
                r => r.HasOne(typeof(Media)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media.Id)),
                j => j.ToTable("AlbumMedia", nameof(SchemaNames.Media))
                            .HasKey("MediaId", "AlbumId")
            );
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
        //.HasValue<LocalizedVideo>("LocalizedVideo");

        // builder.HasOne<LocalizedMedia>().WithOne();
    }
}
public class LocalizedVideoConfig : IEntityTypeConfiguration<LocalizedImage>
{
    public void Configure(EntityTypeBuilder<LocalizedImage> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
        builder.HasDiscriminator(e => e.TypeName)
        //.HasValue<LocalizedImage>("LocalizedImage");
        .HasValue<LocalizedImage>("LocalizedVideo");

        // builder.HasOne<LocalizedMedia>().WithOne();
    }
}
public class LocalizedMediaConfig : IEntityTypeConfiguration<LocalizedMedia>
{
    public void Configure(EntityTypeBuilder<LocalizedMedia> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
        //builder.HasDiscriminator(e => e.TypeName)
        //.HasValue<LocalizedImage>("LocalizedImage")
        //.HasValue<LocalizedVideo>("LocalizedVideo");
    }
}

//public class ImageConfig : IEntityTypeConfiguration<Image>
//{
//    public void Configure(EntityTypeBuilder<Image> builder)
//    {
//        // builder.IsMultiTenant();
//        builder.ToTable("Media", nameof(SchemaNames.Media));
//        builder.HasDiscriminator(e => e.TypeName)
//            .HasValue<Image>("Image");
// 
//        builder.HasMany(e => e.ImageVersions).WithOne()
//            .HasForeignKey(e => e.ImageId)
//            .OnDelete(DeleteBehavior.Cascade);
//    }
//}
//public class LocalizedImageConfig : IEntityTypeConfiguration<LocalizedImage>
//{
//    public void Configure(EntityTypeBuilder<LocalizedImage> builder)
//    {
//        // builder.IsMultiTenant();
//        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
//        builder.HasDiscriminator(e => e.TypeName)
//        .HasValue<LocalizedImage>("LocalizedImage");
//    }
//}


//public class VideoConfig : IEntityTypeConfiguration<Video>
//{
//    public void Configure(EntityTypeBuilder<Video> builder)
//    {
//        // builder.IsMultiTenant();
//        builder.ToTable("Media", nameof(SchemaNames.Media));
//        builder.HasDiscriminator(e => e.TypeName)
//        .HasValue<Video>("Video");
//    }
//}
//public class LocalizedVideoConfig : IEntityTypeConfiguration<LocalizedVideo>
//{
//    public void Configure(EntityTypeBuilder<LocalizedVideo> builder)
//    {
//        // builder.IsMultiTenant();
//        builder.ToTable("LocalizedMedia", nameof(SchemaNames.Media));
//        builder.HasDiscriminator(e => e.TypeName)
//        .HasValue<LocalizedVideo>("LocalizedVideo");
//    }
//}

public class ImageVersionConfig : IEntityTypeConfiguration<ImageVersion>
{
    public void Configure(EntityTypeBuilder<ImageVersion> builder)
    {
        
        // builder.IsMultiTenant();
        builder.ToTable("ImageVersion", nameof(SchemaNames.Media));
        builder.Property(e => e.EnumImageSize).HasConversion(v => v.Value, v => EnumImageSizes.FromValue(v));
        builder.Property(e => e.EnumImageType).HasConversion(v => v.Value, v => EnumImageTypes.FromValue(v));
        builder.HasOne<Image>().WithMany().HasForeignKey(e => e.ImageId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class AlbumConfig : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        // builder.IsMultiTenant();
        builder.ToTable("Album", nameof(SchemaNames.Media));
        builder.Property(e => e.Title).HasMaxLength(250);

        //builder
        //    .HasMany(e => e.News)
        //    .WithMany(e => e.Albums)
        //    .UsingEntity(
        //        "NewsAlbum",
        //        l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
        //        r => r.HasOne(typeof(News)).WithMany().HasForeignKey("NewsId").HasPrincipalKey(nameof(News.Id)),
        //        j => j.ToTable("NewsAlbum", nameof(SchemaNames.Media))
        //                    .HasKey("AlbumId", "NewsId"));

        //builder
        //    .HasMany(e => e.Keywords)
        //    .WithMany(e => e.Albums)
        //    .UsingEntity(
        //        "KeywordAlbum",
        //        l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
        //        r => r.HasOne(typeof(Keyword)).WithMany().HasForeignKey("KeywordId").HasPrincipalKey(nameof(Keyword.Id)),
        //        j => j.ToTable("KeywordAlbum", nameof(SchemaNames.Media))
        //                    .HasKey("AlbumId", "KeywordId"));

        //builder
        //    .HasMany(e => e.Medias)
        //    .WithMany(e => e.Albums)
        //    .UsingEntity(
        //        "AlbumMedia",
        //        l => l.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumId").HasPrincipalKey(nameof(Album.Id)),
        //        r => r.HasOne(typeof(Media<LocalizedMedia>)).WithMany().HasForeignKey("MediaId").HasPrincipalKey(nameof(Media<LocalizedMedia>.Id)),
        //        j => j.ToTable("AlbumMedia", nameof(SchemaNames.Media))
        //                    .HasKey("AlbumId", "MediaId"));
    }
}