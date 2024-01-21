using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class ProductGroup : Product
{
    public override string TypeName { get; protected set; } = nameof(ProductGroup);
}
