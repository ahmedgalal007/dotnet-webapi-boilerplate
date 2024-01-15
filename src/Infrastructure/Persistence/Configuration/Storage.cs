using FSH.WebApi.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Storage;
using System.Reflection.Emit;
using File = FSH.WebApi.Domain.Storage.File;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class AdapterConfig : IEntityTypeConfiguration<Adapter>
{
    public void Configure(EntityTypeBuilder<Adapter> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(Adapter), nameof(SchemaNames.Storage));
    }
}
public class FileConfig : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(File), nameof(SchemaNames.Storage));
        builder.Property(b => b.Name).HasMaxLength(120);
        builder.Property(b => b.Extention).HasMaxLength(10);
    }
}

public class FolderConfig : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(nameof(Folder), nameof(SchemaNames.Storage));
        builder.HasMany(e => e.Childrens).WithOne().HasForeignKey(e => e.ParentId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(e => e.Files).WithOne().HasForeignKey(e => e.FolderId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(b => b.Name).HasMaxLength(60);
        builder.Property(b => b.Path).HasMaxLength(250);
        builder.Property(b => b.Directory).HasMaxLength(250);

    }
}




