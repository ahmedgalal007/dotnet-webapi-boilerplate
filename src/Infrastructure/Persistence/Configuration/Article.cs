using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namotion.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(Category), nameof(SchemaNames.Article));
        builder.Property(e => e.Slug).HasMaxLength(50);
        builder.Property(e => e.Color).HasMaxLength(10);
    }
}

public class LocalizedCategoryConfig : IEntityTypeConfiguration<LocalizedCategory>
{
    public void Configure(EntityTypeBuilder<LocalizedCategory> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(LocalizedCategory), nameof(SchemaNames.Article));
        builder.Property(e => e.culturCode).HasMaxLength(6);
        builder.Property(e => e.Name).HasMaxLength(150);
        builder.Property(e => e.Description).HasMaxLength(300);
    }
}

public class NewsConfig : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(News), nameof(SchemaNames.Article));
        builder.Property(e => e.slug).HasMaxLength(150);
        builder.Property(e => e.MainImage).HasMaxLength(250);
    }
}

public class LocalizedNewsConfig : IEntityTypeConfiguration<LocalizedNews>
{
    public void Configure(EntityTypeBuilder<LocalizedNews> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(LocalizedNews), nameof(SchemaNames.Article));
        builder.Property(e => e.culturCode).HasMaxLength(6);
        builder.Property(e => e.Title).HasMaxLength(150);
        builder.Property(e => e.Description).HasMaxLength(300);
        builder.Property(e => e.SubTitle).HasMaxLength(60);
        builder.Property(e => e.SEOTitle).HasMaxLength(60);
        builder.Property(e => e.SocialTitle).HasMaxLength(60);
        builder.Property(e => e.Body).HasMaxLength(4000);
    }
}