using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class Vehicle : Product
{
    public override string TypeName { get; protected set; } = nameof(Vehicle);
}
