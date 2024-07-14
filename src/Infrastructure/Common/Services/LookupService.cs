//using ClosedXML;
//using Fluid.Parser;
//using FSH.WebApi.Application.Common.Interfaces;
//using FSH.WebApi.Domain.Article;
//using FSH.WebApi.Domain.Common.Contracts;
//using FSH.WebApi.Domain.Keywords;
//using FSH.WebApi.Infrastructure.Common.Extensions;
//using FSH.WebApi.Infrastructure.Identity;
//using FSH.WebApi.Infrastructure.Persistence.Context;
//using Microsoft.EntityFrameworkCore;
//using Namotion.Reflection;
//using SQLitePCL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Dynamic.Core;
//using System.Reflection;

//namespace FSH.WebApi.Infrastructure.Common.Services;
//public class LookupService : ILookupService
//{
//    private ApplicationDbContext _context { get; }
//    public LookupService(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public static List<Lookup> Lookups = new()
//    {
//        new() { EName = "Category", EType = typeof(Category), TitleFieldName = "Name" },
//        new() { EName = "Keyword", EType = typeof(Keyword)},
//        new() { EName = "UserName", EType = typeof(ApplicationUser), TitleFieldName = "UserName"},
//        new() { EName = "UserEmail", EType = typeof(ApplicationUser), TitleFieldName = "Email"},
//        new() { EName = "Role", EType = typeof(ApplicationRole)},
//        new() { EName = "RoleClaims", EType = typeof(ApplicationRoleClaim)}
//    };

//    public async Task<List<KeyValuePair<TID, string>>> Search<TID>(string entityName, string query, bool queryGetAll = false, TID? parentId = default)
//    {
//        List<KeyValuePair<TID, string>> result = new List<KeyValuePair<TID, string>>();

//        if (!Lookups.Any(e => e.EName == entityName)) return result;
//        Lookup lookup = Lookups.Find(e => e.EName == entityName)!;

//        IQueryable qry = _context.Query(lookup.EType).AsQueryable()!;
//        var NavigationsProperty = GetNavigationProperties(_context, lookup.EType);

//        //MethodInfo IncludeMethod = qry.GetType().GetMethod("Include");
//        //if (IncludeMethod != null)
//        //{
//        //    IncludeMethod.Invoke(qry, new object[] { "locals" });
//        //}

//        if (!qry.HasProperty(lookup.IDFieldName)) throw new Exception(string.Format("The Entity '[{0}]' doesn't have a Key property with name '{1}' ", lookup.EName, lookup.IDFieldName));
//        if (!qry.HasProperty(lookup.TitleFieldName)) throw new Exception(string.Format("The Entity '[{0}]' doesn't have a Title property with name '{1}' ", lookup.EName, lookup.TitleFieldName));
//        // var qry = _context.Query(lookup.EName);

//        if (string.IsNullOrEmpty(query) && !queryGetAll)
//        {
//            return result;
//        }
//        else
//        {
//            var clause = $"{lookup.TitleFieldName} like '%{query}%' ";
//            qry = qry.Where(clause).AsQueryable();
//        }

//        return await ParseResult<TID>(qry, lookup);
//    }

//    private List<PropertyInfo> GetNavigationProperties(ApplicationDbContext _gesFormaContext, Type entityType)
//    {
//        List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
//        _gesFormaContext.Model.GetEntityTypes().Select(x => x.GetNavigations()).ToList().ForEach(entityTypes =>
//        {
//            entityTypes.ToList().ForEach(property =>
//            {
//                propertyInfos.AddRange(entityType.GetProperties().Where(x => x.PropertyType == property.PropertyInfo.PropertyType).ToList());
//            });

//        });
//        return propertyInfos;
//    }

//    //public virtual IQueryable Query(string entityName, bool eager = false)
//    //{
//    //    //  var query = _context.Set<T>().AsQueryable();
//    //    var query = _context.Query(entityName).AsQueryable()!;
//    //    if (eager)
//    //    {
//    //        var navigations = _context.Model.FindEntityType(query.ElementType)
//    //            .GetDerivedTypesInclusive()
//    //            .SelectMany(type => type.GetNavigations())
//    //            .Distinct();

//    //        foreach (var property in navigations)
//    //            query = query.Include(property.Name);
//    //    }
//    //    return query;
//    //}

//    //public virtual T Get(Guid itemId, bool eager = false)
//    //{
//    //    return Query(eager).SingleOrDefault(i => i.EntityId == itemId);
//    //}
//    private static async Task<List<KeyValuePair<TID, string>>> ParseResult<TID>(IQueryable qry, Lookup lookup)
//    {
//        List<KeyValuePair<TID, string>> result = new List<KeyValuePair<TID, string>>();
//        foreach (dynamic row in await qry.ToDynamicListAsync())
//        {
//            var key = row[lookup.IDFieldName];
//            var value = row[lookup.TitleFieldName];
//            if (key != null && value != null) result.Add(new KeyValuePair<TID, string>(key, value));
//        }

//        return result;
//    }


//    public static bool TryGetPropertyValue<TType, TObj>(TObj obj, string propertyName, out TType? value)
//    {
//        value = default;
//        if (obj is null)
//        {
//            Console.WriteLine("Object is null.");
//            return false;
//        }
//        PropertyInfo? propertyInfo = typeof(TObj).GetProperty(propertyName);
//        if (propertyInfo is null)
//        {
//            Console.WriteLine($"Property '{propertyName}' not found.");
//            return false;
//        }
//        object? propertyValue = propertyInfo.GetValue(obj);
//        if (propertyValue is null && Nullable.GetUnderlyingType(typeof(TType)) is not null)
//        {
//            return true;
//        }
//        if (propertyValue is not TType typedValue)
//        {
//            Console.WriteLine($"Property '{propertyName}' is of type {propertyInfo.PropertyType}, got {typeof(TType)}.");
//            return false;
//        }
//        value = typedValue;
//        return true;
//    }

//}

//public class Lookup
//{
//    internal string EName { get; set; } = string.Empty;
//    internal Type EType { get; set; } = typeof(object);
//    internal string IDFieldName { get; set; } = "Id";
//    internal string TitleFieldName { get; set; } = "Title";
//}

//public static class ObjectGetPropertyValueExtension
//{
//    public static Object GetPropValue(this Object obj, String name)
//    {
//        foreach (String part in name.Split('.'))
//        {
//            if (obj == null) { return null; }

//            Type type = obj.GetType();
//            PropertyInfo info = type.GetProperty(part);
//            if (info == null) { return null; }

//            obj = info.GetValue(obj, null);
//        }
//        return obj;
//    }

//    public static T GetPropValue<T>(this Object obj, String name)
//    {
//        Object retval = GetPropValue(obj, name);
//        if (retval == null) { return default(T); }

//        // throws InvalidCastException if types are incompatible
//        return (T)retval;
//    }
//}
