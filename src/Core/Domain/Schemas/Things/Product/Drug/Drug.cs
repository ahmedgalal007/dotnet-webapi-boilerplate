using System;

namespace FSH.WebApi.Domain.Schemas.Things.Products;
public class Drug : Product
{
    public override string TypeName { get; protected set; } = nameof(Drug);
}
