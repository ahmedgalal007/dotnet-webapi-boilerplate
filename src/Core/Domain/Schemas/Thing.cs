using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas;
public abstract class Thing
{
    public virtual string TypeName { get; set; } = nameof(Thing);
    public string? AdditionalType { get; private set; }
    public string? AlternateName { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string? DisambiguatingDescription { get; private set; }
    public string? Identifier { get; private set; }
    public string? Image { get; private set; }
    public string? MainEntityOfPage { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? PotentialAction { get; private set; }
    public string? SameAs { get; private set; }
    public string? SubjectOf { get; private set; }
    public string? Url { get; private set; }
}
