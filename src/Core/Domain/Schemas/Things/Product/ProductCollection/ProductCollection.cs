using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class ProductCollection : Product
{
    public override string TypeName { get; protected set; } = nameof(ProductCollection);
}
