using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class DietarySupplement : Product
{
    public override string TypeName { get; protected set; } = nameof(DietarySupplement);
}
