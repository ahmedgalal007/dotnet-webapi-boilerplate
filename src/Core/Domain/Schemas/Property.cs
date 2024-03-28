using FSH.WebApi.Domain.Schemas.Things;
using FSH.WebApi.Domain.Schemas.Things.CreativeWorks.Articles;
using FSH.WebApi.Domain.ValueObjects;
using FSH.WebApi.Domain.ValueObjects.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Properties;
public interface IProperty { Guid Id { get; set; } }
public class Property : ValueObject
{
    public const int MaxLength = 50;
    protected Property(IProperty value)
    {
        // if (!CheckValueIsAllowed(value))
        // {
        //     throw new ArgumentException($"The value Type <{value.GetType().Name}> should be one of {string.Join(',',  AcceptedTypes.Select(e => e.GetType().Name))}");
        // }

        Ref = value;
        IsRef = true;
    }

    protected Property(string value)
    {
        if(!string.IsNullOrWhiteSpace(value)) Value = value;
    }

    public string Value { get; private set; }
    public bool IsRef { get; private set; } = false;
    public IProperty Ref { get; private set; }

    public static Result<Property> Create(IProperty name)
    {
        if (name.GetType().IsAssignableFrom(typeof(Thing)))
        {
            return Result.Failure<Property>(new Error(
                "Property.NotAllowed",
                "Propert value must be <simple> or driven from <Thing> class ."));
        }

        return new Property(name);
    }
    public static Result<Property> Create(string name)
    {
        return new Property(name);
    }

    public virtual bool Equals(Property? other)
    {
        if (IsRef) return Ref.Id == other.Ref.Id;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is IProperty property && property.Equals(this);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return IsRef ? Ref.Id.ToString() : Value;
    }
}
