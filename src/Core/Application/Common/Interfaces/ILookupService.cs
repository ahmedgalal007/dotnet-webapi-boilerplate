using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.Interfaces;
public interface ILookupService
{
    // Task<List<KeyValuePair<TID, string>>> Search<TID>(string entityName, string query, bool queryGetAll = false, TID? parentId = default);
    Task<List<KeyValuePair<Guid, string>>> Search(string entityName, string query, bool queryGetAll = false, string lang = "ar-EG", Guid? parentId = default);

}
