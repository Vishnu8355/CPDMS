// ***********************************************************************
//  Class Name               : SessionActionFilter
//  Desciption               : Session Action Class
//  Created By               : sanjay
//  Created On               : 31 August 2021
// ***********************************************************************
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CPDMS.Web;
using CPDMS.Model.ExceptionList;
using CPDMSEF;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CPDMS.ActionFilter
{
    public class SessionActionFilter : IActionFilter
    {
        IExceptionProvider _objIExceptionProvider;
        IOptions<KeyList> _objKeyList;
        private readonly IConfiguration _configuration;
        double seconds = 0;
        int secondsInt = 0;
        public SessionActionFilter(IExceptionProvider objIExceptionProvider, IOptions<KeyList> objKeyList, IConfiguration configuration)
        {
            _objIExceptionProvider = objIExceptionProvider;
            _objKeyList = objKeyList;
            this._configuration = configuration;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            // Check if the attribute exists on the action method
            if (controllerActionDescriptor.MethodInfo?.GetCustomAttributes(inherit: true)?.Any(a => a.GetType().Equals(typeof(SkipSessionTaskAttribute))) ?? false)
            {
            }
            else if (claimsIdentity.IsAuthenticated )
            {
                UserLoginSession objUserLoginSession = context.HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                if (objUserLoginSession == null)
                {
                    LoginEF ObjloginEF = new LoginEF();
                    ObjloginEF.UserID = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                    ObjloginEF.UserName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    UserLoginSession objUserLoginSession1 = _objIExceptionProvider.LoginUser(ObjloginEF);
                    if (objUserLoginSession1.UserName == null)
                    {
                        context.HttpContext.Session.Clear();
                        context.Result = new RedirectResult(_configuration["Logout:LogoutPath"]);
                        //    List<MenuEF> Listmenu = new List<MenuEF>();
                        //    //objUserLoginSession1.UserLoginId = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
                        //    //menuonput objmenu = new menuonput();
                        //    //objmenu.UserID = Convert.ToInt32(objUserLoginSession1.UserId);
                        //    //objmenu.MineralId = Convert.ToInt32(objUserLoginSession1.MineralId);
                        //    //objmenu.MineralName = objUserLoginSession1.MineralName;
                        //    //Listmenu = _objIExceptionProvider.MenuList(objmenu);
                        //    objUserLoginSession1.Listmenu = Listmenu;
                    }
                    context.HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession1);

                }
                else if (Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value) != objUserLoginSession.UserId)
                {
                    LoginEF ObjloginEF = new LoginEF();
                    ObjloginEF.UserID = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                    ObjloginEF.UserName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
                    UserLoginSession objUserLoginSession1 = _objIExceptionProvider.LoginUser(ObjloginEF);
                    if (objUserLoginSession1.UserName != null)
                    {
                        List<MenuEF> Listmenu = new List<MenuEF>();
                        //objUserLoginSession1.UserLoginId = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
                        //menuonput objmenu = new menuonput();
                        //objmenu.UserID = Convert.ToInt32(objUserLoginSession1.UserId);
                        //objmenu.MineralId = Convert.ToInt32(objUserLoginSession1.MineralId);
                        //objmenu.MineralName = objUserLoginSession1.MineralName;
                        //Listmenu = _objIExceptionProvider.MenuList(objmenu);
                        objUserLoginSession1.Listmenu = Listmenu;
                    }
                    context.HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession1);

                }
            }
            else
            {
                context.HttpContext.Session.Clear();

                // context.Result = new RedirectResult(_objKeyList.Value.LoginUrl);
                 context.Result = new RedirectResult(_configuration["Logout:LogoutPath"]);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //this method will be executed after an action method has executed 
            //var session = context.HttpContext.Session;
            //if (!session.IsAvailable || !session.TryGetValue("Identity.Application", out _))
            //{
            //    // Session expired or not available, redirect to login page
            //    context.Result = new RedirectResult(_configuration["Logout:LogoutPath"]);
            //}
            //else
            //{
            //    base.OnActionExecuting(context);
            //}
        }
    }
    public class SkipSessionTaskAttribute : Attribute { }
}
