using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
public class DynamicDbCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, Boolean designTime)
    {
        return new DynamicDbCacheKey(context);
    }

}

public class DynamicDbCacheKey : ModelCacheKey
{
    readonly string _contextVersion;
    public DynamicDbCacheKey(DbContext context)
        : base(context)
    {
        _contextVersion = (context as DynamicDbContext)?.GetContextVersion();
    }

    protected override bool Equals(ModelCacheKey other)
     => base.Equals(other)
        && (other as DynamicDbCacheKey)?._contextVersion == _contextVersion;

    public override int GetHashCode() => base.GetHashCode();
}

