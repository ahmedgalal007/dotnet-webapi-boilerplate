using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.CreativeWork;
public abstract class Article : CreativeWork
{
    public override string TypeName { get; set; } = nameof(Article);

}
