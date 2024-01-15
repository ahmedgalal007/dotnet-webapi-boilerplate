using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Medias.Images;
public class LocalizedVideo : LocalizedMedia
{
    public string? VideoTitle { get; set; } = string.Empty;
}
