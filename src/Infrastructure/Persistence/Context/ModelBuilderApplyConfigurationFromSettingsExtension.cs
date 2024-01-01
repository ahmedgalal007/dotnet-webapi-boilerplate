using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FSH.WebApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;

namespace Microsoft.EntityFrameworkCore;

//[DbContext(typeof(BaseDbContext))]
//internal static class ModelBuilderApplyConfigurationFromSettingsExtension 
//{
//    [RequiresUnreferencedCode("This API isn't safe for trimming, since it searches for types in an arbitrary assembly.")]
//    public static ModelBuilder ApplyConfigurationsFromAssembly(
//        this ModelBuilder modelBuilder,
//        Assembly assembly,
//        Func<Type, bool>? predicate = null)
//    {
//        var applyEntityConfigurationMethod = typeof(ModelBuilder)
//            .GetMethods()
//            .Single(
//                e => e.Name == nameof(modelBuilder.ApplyConfiguration)
//                    && e.ContainsGenericParameters
//                    && e.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition()
//                    == typeof(IEntityTypeConfiguration<>));

//        foreach (var type in assembly.GetConstructibleTypes().OrderBy(t => t.FullName))
//        {
//            // Only accept types that contain a parameterless constructor, are not abstract and satisfy a predicate if it was used.
//            if (type.GetConstructor(Type.EmptyTypes) == null
//                || (!predicate?.Invoke(type) ?? false))
//            {
//                continue;
//            }

//            foreach (var @interface in type.GetInterfaces())
//            {
//                if (!@interface.IsGenericType)
//                {
//                    continue;
//                }

//                if (@interface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
//                {
//                    var target = applyEntityConfigurationMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
//                    target.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
//                }
//            }
//        }

//        return modelBuilder;
//    }
//}


public class SettingsModelBuilderDecorator: ModelBuilder
{
    public override ModelBuilder ApplyConfigurationsFromAssembly(Assembly assembly, Func<Type, Boolean>? predicate = null)
    {
        return base.ApplyConfigurationsFromAssembly(assembly, predicate);
    }
}