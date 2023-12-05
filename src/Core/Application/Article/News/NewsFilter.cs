using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News;
public class NewsFilterDto: BaseFilterDto
{
    public string? CultureCode { get; set; }
    public bool LoadLocals { get; set; }
    public bool SearchTitle { get; set; }
    public bool SearchDescription { get; set; }
    public bool SearchBody { get; set; }
}
