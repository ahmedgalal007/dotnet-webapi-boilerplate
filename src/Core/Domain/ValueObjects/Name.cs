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
    public Name(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException(string.Format("Name shouldn't excced {0}", MaxLength));
        }
        Value = value;
    }
    public string Value { get; }

    // TODO:public static Result<Name> Create(string value)
    // {
       
    // }

    public Boolean Equals(Name? other)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Object> GetAtomicValues()
    {
        yield return Value;
    }
}
