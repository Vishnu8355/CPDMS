
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPDMSEF;

using Microsoft.AspNetCore.Mvc;

namespace CPDMS.Model.ExceptionList
{
    
    public interface IExceptionProvider
    {
        string ErrorList(LogEntry objLogEntry);
        
        UserLoginSession LoginUser(LoginEF ObjloginEF);
    }

}
