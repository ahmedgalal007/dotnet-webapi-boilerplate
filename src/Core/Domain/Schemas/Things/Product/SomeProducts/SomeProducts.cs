using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class SomeProducts : Product
{
    public override string TypeName { get; protected set; } = nameof(SomeProducts);
}
