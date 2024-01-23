using FSH.WebApi.Domain.ValueObjects.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.ValueObjects;
public class Name : ValueObject, IEquatable<Name>
{
    public const int MaxLength = 50;
    private Name(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException(string.Format("Name shouldn't excced {0}", MaxLength));
        }
        Value = value;
    }
    public string Value { get; }

    public static Result<Name> Create(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<Name>(new Error(
                "Name.Empty",
                "Name is empty." ));
        }
        if (name.Length > MaxLength)
        {
            return Result.Failure<Name>(new Error(
                "Name.TooLong",
                "Name is too long."));
        }
        return new Name(name);
    }

    public bool Equals(Name? other)
    {
        return Value.SequenceEqual(other.Value);
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
