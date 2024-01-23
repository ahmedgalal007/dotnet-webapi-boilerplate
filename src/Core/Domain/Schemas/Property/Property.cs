using FSH.WebApi.Domain.Schemas.Things;
using FSH.WebApi.Domain.ValueObjects;
using FSH.WebApi.Domain.ValueObjects.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Property;
internal class Property<T1> : ValueObject
    where T1 : class
{
    

    public const int MaxLength = 50;
    private Property(T1 value)
    {
        if (value.GetType() != typeof(string) && !typeof(Thing).IsAssignableFrom(value.GetType()) )
        {
            throw new ArgumentException(string.Format("Name shouldn't excced {0}", MaxLength));
        }
        Value = value;
    }
    public T1 Value { get; }

    public static Result<Property<T1>> Create(T1 name)
    {
        if (name.GetType().IsAssignableFrom(typeof(Thing)))
        {
            return Result.Failure<Property<T1>>(new Error(
                "Property.NotAllowed",
                "Propert value must be <simple> or driven from <Thing> class ."));
        }

        return new Property<T1>(name);
    }

    public bool Equals(Property<T1>? other)
    {
        // return Value.SequenceEqual(other.Value);
        return other.Value.GetType() == typeof(T1)
            && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Name name && name.Equals(this);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
