using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.SEO;
public interface ISeoUtilitiesService : ITransientService
{
    //public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    //where T : class;

    public string Slugify(string text, bool isLtr = false);
}
