using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class Product : Thing
{
    public override string TypeName { get; protected set; } = nameof(Product);
}


// TODO DietarySupplement
// TODO Drug
// TODO IndividualProduct
// TODO ProductCollection
// TODO ProductGroup
// TODO ProductModel
// TODO SomeProducts
// TODO Vehicle