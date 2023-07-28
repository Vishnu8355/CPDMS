using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDMS.Models.Utility
{
    public interface IHttpWebClients
    {
        string PostRequest(string URI, string parameterValues);
        string GetRequest(string URI, object parameterValues);
        Task<string> AwaitPostRequest(string URI, string parameterValues);
        string PostRequestExternal(string URI, string parameterValues);
    }
}
