

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPDMSEF;
using CPDMS.Models.Utility;

namespace CPDMS.Model.ExceptionList
{
    public class ExceptionProvider:IExceptionProvider
    {
        public readonly IHttpWebClients _objIHttpWebClients;
        public ExceptionProvider(IHttpWebClients objIHttpWebClients)
        {

            _objIHttpWebClients = objIHttpWebClients;
        }
        public string ErrorList(LogEntry objLogEntry)
        {
            try
            {
                string s = JsonConvert.DeserializeObject<string>(_objIHttpWebClients.PostRequest("ExceptionData/AddException", JsonConvert.SerializeObject(objLogEntry)));
                return s;
            }
            catch (System.Exception ex)
            {

                //throw ex;
                return "";
            }
        }

        public UserLoginSession LoginUser(LoginEF ObjloginEF)
        {
            UserLoginSession objUserLoginSession = new UserLoginSession();
            try
            {

                objUserLoginSession = JsonConvert.DeserializeObject<UserLoginSession>(_objIHttpWebClients.PostRequest("ExceptionData/getuserDetail", JsonConvert.SerializeObject(ObjloginEF)));
                return objUserLoginSession;
            }
            catch (Exception ex)
            {

                //throw ex;
                return objUserLoginSession;
            }
            finally
            {
                objUserLoginSession = null;
            }
        }
    }
}
