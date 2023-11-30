using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Storage;
public class Adapter : AuditableEntity
{
    public string Name { get; set; }
}
