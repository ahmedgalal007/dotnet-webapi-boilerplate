using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;

public class Program
{
    public static void RuntimeModel(string[] args)
    {
        var options = new DbContextOptionsBuilder<DynamicDbContext>()
            .UseSqlite("DynamicDbContext")
            .Options;

        using (var context = new DynamicDbContext(options))
        {
            // Add the Product entity type dynamically
            var modelBuilder = new ModelBuilder(new ConventionSet());

            // var runtimeModel = context.Model.GetRelationalModel();
            var runtimeModel = context.Model.GetRelationalModel();


            // Define the new entity type
            // var productEntityType = context.Model.AddEntityType(productEntityType);
            var productEntityType = modelBuilder.Model.AddEntityType(typeof(Product));

            // Define properties for the Product entity
            productEntityType.AddProperty("Id", typeof(int));
            productEntityType.AddProperty("Name", typeof(string));

            // Add the new entity to the DbContext
            // context.Model.AddEntityType(productEntityType);
            modelBuilder.Model.AddEntityType(typeof(Product));

        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

