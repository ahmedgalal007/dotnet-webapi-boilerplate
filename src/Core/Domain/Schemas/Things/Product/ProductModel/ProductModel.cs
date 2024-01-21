using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class ProductModel : Product
{
    public override string TypeName { get; protected set; } = nameof(ProductModel);
}
