using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;

public class DbSchemaAwareMigrationAssembly : MigrationsAssembly
{
    private readonly DbContext _context;
    public DbSchemaAwareMigrationAssembly(
               ICurrentDbContext currentContext,
               IDbContextOptions options,
               IMigrationsIdGenerator idGenerator,
               IDiagnosticsLogger<DbLoggerCategory.Migrations> logger)
         : base(currentContext, options, idGenerator, logger)
    {
        _context = currentContext.Context;
    }
    

    //public override Migration CreateMigration(TypeInfo migrationClass,
    //     string activeProvider)
    //{
    //    if (activeProvider == null)
    //        throw new ArgumentNullException(nameof(activeProvider));

    //    var hasCtorWithSchema = migrationClass
    //            .GetConstructor(new[] { typeof(IDbContextSchema) }) != null;

    //    if (hasCtorWithSchema && _context is IDbContextSchema schema)
    //    {
    //        var instance = (Migration)Activator.CreateInstance(migrationClass.AsType(), schema);
    //        instance.ActiveProvider = activeProvider;
    //        return instance;
    //    }

    //    return base.CreateMigration(migrationClass, activeProvider);
    //}

    public Migration CreateMigration(
                   TypeInfo migrationClass,
                   string activeProvider)
    {
        if (activeProvider == null)
            throw new ArgumentNullException(nameof(activeProvider));
        var hasCtorWithSchema = migrationClass
            .GetConstructor(new[] { typeof(IDynamicDbContextSchema) }) != null;
        if (hasCtorWithSchema && _context is IDynamicDbContextSchema schema)
        {
            var instance = (Migration)Activator.CreateInstance(migrationClass.AsType(), schema);
            instance.ActiveProvider = activeProvider;
            return instance;
        }

        return base.CreateMigration(migrationClass, activeProvider);
    }
}
