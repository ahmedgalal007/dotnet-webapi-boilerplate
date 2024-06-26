﻿namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

internal static class SchemaNames
{
    // TODO: figure out how to capitalize these only for Oracle
    public static string Auditing = nameof(Auditing); // "AUDITING";
    public static string Catalog = nameof(Catalog); // "CATALOG";
    public static string Identity = nameof(Identity); // "IDENTITY";
    public static string MultiTenancy = nameof(MultiTenancy); // "MULTITENANCY";
    public static string Article = nameof(Article); // "ARTICLE";
    public static string Storage = nameof(Storage); // "ARTICLE";
    public static string Media = nameof(Media);
    public static string Schema = nameof(Schema);
}