using FSH.WebApi.Domain.DynamicSchemas;
using FSH.WebApi.Infrastructure.Multitenancy;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas.Models;
// using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas.Scaffold;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
public class DynamicDbContext : DbContext
{
    string _version;
    public DynamicDbContext(DbContextOptions<DynamicDbContext> options) : base(options)
    {
        _metaDataEntityList = new List<MetadataEntity>(){
            new MetadataEntity
            {
                Name = "SPField",
                Properties = new()
                {
                    new MetadataEntityProperty { Name = "Id", Type = typeof(Guid).FullName!,  ColumnName = "Id", IsNavigation = false },
                    new MetadataEntityProperty { Name = "Name", Type = typeof(string).FullName!,  ColumnName ="Name", IsNavigation=false }
                },
                SchemaName = "DEntities",
                TableName = "SPField",
                EntityType = typeof(SPFieldBase),
            }
        };
    }

    public List<MetadataEntity> _metaDataEntityList = new List<MetadataEntity>();

    public void AddMetadata(MetadataEntity metadataEntity) => _metaDataEntityList.Add(metadataEntity);
    public void SetMetadata(List<MetadataEntity> metadataEntities) => _metaDataEntityList = metadataEntities;

    public MetadataEntity GetMetadaEntity(Type type) => _metaDataEntityList.FirstOrDefault(p => p.EntityType == type);


    public void SetContextVersion(string version) => _version = version;

    public string GetContextVersion() => _version;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDefaultSchema("DEntities");
        foreach (var metadataEntity in _metaDataEntityList)
        {
            modelBuilder.Entity(metadataEntity.EntityType).ToTable(metadataEntity.TableName, metadataEntity.SchemaName).HasKey("Id");

            foreach (var metaDataEntityProp in metadataEntity.Properties)
            {
                if (!metaDataEntityProp.IsNavigation)
                {
                    var propBuilder = modelBuilder.Entity(metadataEntity.EntityType).Property(metaDataEntityProp.Name);

                    if (!string.IsNullOrEmpty(metaDataEntityProp.ColumnName))
                        propBuilder.HasColumnName(metaDataEntityProp.ColumnName);
                }
            }
        }

        base.OnModelCreating(modelBuilder);
    }

}