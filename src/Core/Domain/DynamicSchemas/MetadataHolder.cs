using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.DynamicSchemas;
public class MetadataHolder
{
    public List<MetadataEntity> Entities { get; set; } = new List<MetadataEntity>();

    public string Version { get; set; }
}
