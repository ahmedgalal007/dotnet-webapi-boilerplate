using FSH.WebApi.Shared.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Common.Contracts;

public abstract class AuditableLocalizedEntity : AuditableLocalizedEntity<DefaultIdType>
{

}
public abstract class AuditableLocalizedEntity<T> : AuditableEntity<T>
{
    public string CulturCode { get; protected set; }

    public bool Enabled { get; protected set; }
           
    public bool IsDefault { get; protected set; }

    public abstract AuditableLocalizedEntity<T> Create(string cultureCode,bool enabled = false, bool isDefault = false);

}
