using CPDMS.ActionFilter;
using CPDMS.Areas.LandingPage.Models.Login;
using CPDMS.Entities;
using CPDMS.Models;

using CPDMS.Web;
using CPDMSEF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

namespace CPDMS.Areas.LandingPage.Controllers
{
	[Area("LandingPage")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ILoginModel _objILoginModel;
        UserLoginSession objUserLoginSession;
        private string strLclSalt;
        public readonly IOptions<KeyList> _objKeyList;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        //  SMSService _smsService;

        public HomeController( ILoginModel ObjILoginModel, IOptions<KeyList> objKeyList, IConfiguration configuration, HttpClient httpClient)
        {
            _objILoginModel = ObjILoginModel;
            _objKeyList = objKeyList;
            _configuration = configuration;
            _httpClient = httpClient;
          //  _smsService = sMSService;
        }
        [SkipSessionTask]
        public IActionResult LandingPage()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 1;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");


            DashboardEF dashboardEF = new DashboardEF();
            dashboardEF = _objILoginModel.Dashboard(dashboardEF);

            var chemicals = (dashboardEF.Chemicals.ToString("000"));
            var chemicalsdigits = chemicals.ToString().Select(t => int.Parse(t.ToString())).ToArray();
            TempData["chemicalsfirst"] = chemicalsdigits[0];
            TempData["chemicalssecond"] = chemicalsdigits[1];
            TempData["chemicalsthird"] = chemicalsdigits[2];
            TempData.Keep("chemicalsfirst");
            TempData.Keep("chemicalssecond");
            TempData.Keep("chemicalsthird");

            var PeroChemicals = (dashboardEF.PeroChemicals.ToString("000"));
            var PeroChemicalsdigits = PeroChemicals.ToString().Select(t => int.Parse(t.ToString())).ToArray();
            TempData["PeroChemicalsfirst"] = PeroChemicalsdigits[0];
            TempData["PeroChemicalssecond"] = PeroChemicalsdigits[1];
            TempData["PeroChemicalsthird"] = PeroChemicalsdigits[2];
            TempData.Keep("PeroChemicalsfirst");
            TempData.Keep("PeroChemicalssecond");
            TempData.Keep("PeroChemicalsthird");

            var TotalUnits = (dashboardEF.TotalUnits.ToString("000"));
            var TotalUnitsdigits = TotalUnits.ToString().Select(t => int.Parse(t.ToString())).ToArray();
            TempData["TotalUnitsfirst"] = TotalUnitsdigits[0];
            TempData["TotalUnitssecond"] = TotalUnitsdigits[1];
            TempData["TotalUnitsthird"] = TotalUnitsdigits[2];
            TempData.Keep("TotalUnitsfirst");
            TempData.Keep("TotalUnitssecond");
            TempData.Keep("TotalUnitsthird");


            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [SkipSessionTask]
        public IActionResult Login()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [SkipSessionTask]
        public async Task<IActionResult> Login(LoginEF loginEF)
        {
            #region Validation
            if (string.IsNullOrEmpty(loginEF.UserName) && string.IsNullOrEmpty(loginEF.Password))
            {
                TempData["Message"] = "Please enter userId and password";
                TempData.Keep("Message");
                //return RedirectToAction("Login");
                return View();
            }
            else if (loginEF.Source == "Select")
            {
                TempData["Message"] = "Please select role";
                TempData.Keep("Message");
                //return RedirectToAction("Login");
                return View();
            }
            //if (string.IsNullOrEmpty(loginEF.UserName))
            //{
            //    ModelState.AddModelError("UserName", "User Name is Required");
            //}
            //if (string.IsNullOrEmpty(loginEF.Password))
            //{
            //    ModelState.AddModelError("Password", "Password is Required");
            //}

            #endregion
            //if (ModelState.IsValid)
            //{
            objUserLoginSession = new UserLoginSession();
            objUserLoginSession = _objILoginModel.LoginUser(loginEF);
            string pwdString1 = "";

            if (objUserLoginSession != null)
            {

                if (objUserLoginSession.UserId > 0)
                {
                    string strLclPwd1 = objUserLoginSession.EncryptPassword;
                    string pwd2 = ComputeSha256Hash(strLclPwd1) + strLclSalt;
                    pwdString1 = ComputeSha256Hash(pwd2).ToUpper();

                    //if (strLclPwd1 == (objUserLoginSession.EncryptPassword))
                    //{
                    //    if (objUserLoginSession.IsPasswordChange == 1)
                    //    {

                    List<MenuEF> Listmenu = new List<MenuEF>();
                    menuonput objmenu = new menuonput();
                    objmenu.UserID = Convert.ToInt32(objUserLoginSession.UserId);
                    objmenu.MMenuId = null;
                    //Listmenu = _objILoginModel.MenuList(objmenu);
                    // objUserLoginSession.Listmenu = Listmenu;
                    //HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession);
                    HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession);
                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email, objUserLoginSession.Email_id ),
                                    new Claim(ClaimTypes.Name, objUserLoginSession.UserName.ToString()),
                                   // new Claim(ClaimTypes.GivenName, objUserLoginSession. RoleDesc),
                                   new Claim(ClaimTypes.NameIdentifier, objUserLoginSession.UserId.ToString()),
                                    new Claim(ClaimTypes.Role , objUserLoginSession.Role.ToString()),
                                    new Claim(ClaimTypes.PrimarySid , objUserLoginSession.UserId.ToString())
                                   // new Claim(ClaimTypes.UserData , objUserLoginSession.UserName.ToString())

                                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };
                    await HttpContext.SignInAsync("Identity.Application", claimsPrincipal, authProperties);
                    string ss = User.Identity.Name;

                    if (objUserLoginSession.Role == "UNIADM")
                    {
                        return RedirectToAction("UnitDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                    else if (objUserLoginSession.Role == "COMADM")
                    {
                        return RedirectToAction("CompanyDashboard", "CompanyRegistration", new { Area = "CompanyRegistraion" });
                    }
                    else if (objUserLoginSession.Role == "UNIOPT")
                    {
                        return RedirectToAction("UnitOPTDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                    else if (objUserLoginSession.Role == "DPTAPP")
                    {
                        return RedirectToAction("UnitDPTAPPDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                    else if (objUserLoginSession.Role == "DPTADM")
                    {
                        return RedirectToAction("UnitDPTADMDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                }
                else
                {
                    // HttpContext.Session.SetInt32("UserId", Convert.ToInt32(objUserLoginSession.UserId ?? 0));
                    // HttpContext.Session.SetString("CurPassword", objUserLoginSession.Password ?? 0);
                    TempData["Message"] = "Invalid user name and password";
                    TempData.Keep("Message");
                    //return RedirectToAction("Login", "Home");
                    return View();
                }
                //    }
                //}
                //else
                //{
                //    TempData["msg"] = "Invalid user name password";
                //}
            }
            else
            {
                TempData["Message"] = "Invalid user name and password";
                //TempData.Keep("Message");
                return View();
            }

            // }
            return View(loginEF);
        }
        [SkipSessionTask]
        public IActionResult LoginV1()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [SkipSessionTask]
        public async Task<IActionResult> LoginV1(LoginEF loginEF)
        {
            #region Validation
            if (string.IsNullOrEmpty(loginEF.UserName) && string.IsNullOrEmpty(loginEF.Password))
            {
                TempData["Message"] = "Please enter userId and password";
                TempData.Keep("Message");
                //return RedirectToAction("Login");
                return View();
            }
            else if (loginEF.Source == "Select")
            {
                TempData["Message"] = "Please select role";
                TempData.Keep("Message");
                //return RedirectToAction("Login");
                return View();
            }
            //if (string.IsNullOrEmpty(loginEF.UserName))
            //{
            //    ModelState.AddModelError("UserName", "User Name is Required");
            //}
            //if (string.IsNullOrEmpty(loginEF.Password))
            //{
            //    ModelState.AddModelError("Password", "Password is Required");
            //}

            #endregion
            //if (ModelState.IsValid)
            //{
            objUserLoginSession = new UserLoginSession();
            objUserLoginSession = _objILoginModel.LoginUser(loginEF);
            string pwdString1 = "";

            if (objUserLoginSession != null)
            {

                if (objUserLoginSession.UserId > 0)
                {
                    string strLclPwd1 = objUserLoginSession.EncryptPassword;
                    string pwd2 = ComputeSha256Hash(strLclPwd1) + strLclSalt;
                    pwdString1 = ComputeSha256Hash(pwd2).ToUpper();

                    //if (strLclPwd1 == (objUserLoginSession.EncryptPassword))
                    //{
                    //    if (objUserLoginSession.IsPasswordChange == 1)
                    //    {

                    List<MenuEF> Listmenu = new List<MenuEF>();
                    menuonput objmenu = new menuonput();
                    objmenu.UserID = Convert.ToInt32(objUserLoginSession.UserId);
                    objmenu.MMenuId = null;
                    //Listmenu = _objILoginModel.MenuList(objmenu);
                    // objUserLoginSession.Listmenu = Listmenu;
                    //HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession);
                    HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession);
                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email, objUserLoginSession.Email_id ),
                                    new Claim(ClaimTypes.Name, objUserLoginSession.UserName.ToString()),
                                   // new Claim(ClaimTypes.GivenName, objUserLoginSession. RoleDesc),
                                   new Claim(ClaimTypes.NameIdentifier, objUserLoginSession.UserId.ToString()),
                                    new Claim(ClaimTypes.Role , objUserLoginSession.Role.ToString()),
                                    new Claim(ClaimTypes.PrimarySid , objUserLoginSession.UserId.ToString())
                                   // new Claim(ClaimTypes.UserData , objUserLoginSession.UserName.ToString())

                                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };
                    await HttpContext.SignInAsync("Identity.Application", claimsPrincipal, authProperties);
                    string ss = User.Identity.Name;

                    if (objUserLoginSession.Role == "UNIADM")
                    {
                        return RedirectToAction("UnitDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                    else if (objUserLoginSession.Role == "COMADM")
                    {
                        return RedirectToAction("CompanyDashboard", "CompanyRegistration", new { Area = "CompanyRegistraion" });
                    }
                    else if (objUserLoginSession.Role == "UNIOPT")
                    {
                        return RedirectToAction("UnitOPTDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                    else if (objUserLoginSession.Role == "DPTAPP")
                    {
                        return RedirectToAction("UnitDPTAPPDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                    else if (objUserLoginSession.Role == "DPTADM")
                    {
                        return RedirectToAction("UnitDPTADMDashboard", "UnitRegistration", new { Area = "UnitRegistration" });
                    }
                }
                else
                {
                    // HttpContext.Session.SetInt32("UserId", Convert.ToInt32(objUserLoginSession.UserId ?? 0));
                    // HttpContext.Session.SetString("CurPassword", objUserLoginSession.Password ?? 0);
                    TempData["Message"] = "Invalid user name and password";
                    TempData.Keep("Message");
                    //return RedirectToAction("Login", "Home");
                    return View();
                }
                //    }
                //}
                //else
                //{
                //    TempData["msg"] = "Invalid user name password";
                //}
            }
            else
            {
                TempData["Message"] = "Invalid user name and password";
                //TempData.Keep("Message");
                return View();
            }

            // }
            return View(loginEF);
        }
        #region password encript 
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i <= bytes.Length - 1; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }
        //Added by sanjay kumar on 10-09-2021 for Invalid login
        [HttpGet]
        public JsonResult GenerateRandom()
        {
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder CSRFRandNum = new StringBuilder();
            for (int i = 0; i < 13; i++)
                CSRFRandNum.Append(combination[random.Next(combination.Length)]);
            TempData["UNIQUERANDOM"] = CSRFRandNum.ToString();
            string randNo = CSRFRandNum.ToString();
            return Json(randNo);
        }
        //End
        #endregion
        public async Task<RedirectResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("Identity.Application");
            await HttpContext.SignOutAsync("Identity.Application");
            HttpContext.Session.Clear();
            string LogoutPath = _configuration.GetSection("Logout").GetValue<string>("LogoutPath");
            return Redirect(LogoutPath);
            //return Redirect(_objKeyList.Value.LoginUrl);
        }
        [SkipSessionTask]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [SkipSessionTask]
        public IActionResult ForgotPassword(LoginEF tRegistrationEF)
        {
           
            MessageEF objMessageEF = new MessageEF();
            objMessageEF = _objILoginModel.ForgotPassword(tRegistrationEF);
            if (objMessageEF.Satus == "Not EXISTS")
            {
                ViewBag.msg = "Data Save Sucessfully";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.msg = "Data not Save Sucessfully";
                return RedirectToAction("Login");
            }
        }
        public IActionResult test()
        {
            return View();
        }
        [SkipSessionTask]

        public IActionResult ForgotPasswordV1()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [SkipSessionTask]
        public  IActionResult ForgotPasswordV1(LoginEF objLogin)
        {
            MessageEF objMessageEF = new MessageEF();
            SMSEF objsms = new SMSEF();
            objsms.recipientPhoneNumber = "8877530313";

            bool status = sendSMS("8877530313");
            if (objLogin.NewPassword == objLogin.ConfirmPassword)
            {
                objMessageEF = _objILoginModel.ForgotPassword(objLogin);
            }

         
           if (objMessageEF.Satus == "1")
            {
                TempData["Message"] = "Password changed successfully.";
                TempData.Keep("Message");
                return View();
            }
            else if (objMessageEF.Satus == "2")
            {
                TempData["Messages"] = "Entered Wrong OTP.";
                TempData.Keep("Messages");
                return View();
            }
            else if (objMessageEF.Satus == "0")
            {
                TempData["Messages"] = "User does not exist";
                TempData.Keep("Messages");
                return View();
            }
            {
                TempData["Messages"] = "Wrong Credential.";
                TempData.Keep("Messages");
                return View();
            }
        }
        public bool sendSMS(string mobile)
        {
            try
            {
                string URL = _configuration.GetSection("SMS").GetValue<string>("URL");
                string UserName = _configuration.GetSection("SMS").GetValue<string>("AuthID");
                string password = _configuration.GetSection("SMS").GetValue<string>("Password");
                string baseURL = URL + "username=" + UserName + "and pin=" + password + "and";
                string replyTo = _configuration.GetSection("SMS").GetValue<string>("SenderID");
                string recipient = mobile;
                string messageBody = "Test message sent via SMS platform using C#";

                // URL encode message body
                //  messageBody = Server.UrlEncode(messageBody);

                // Construct URL
                string URI = baseURL;
                URI += "signature=" + replyTo;
                URI += "andmnumber=" + recipient;
                URI += "andmessage=" + messageBody;

                var response =  _httpClient.GetAsync(URI);
                return true;
                //if (response.)
                //{
                //    // SMS sent successfully
                //    return true;
                //}
                //else
                //{
                //    // SMS sending failed
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during SMS sending
                Console.WriteLine($"Error sending SMS: {ex.Message}");
                return false;
            }
        }
        public IActionResult ResetPassword(string OTP, string NewPassword, string CofirmPassword, string source)
        {
            MessageEF objMessageEF = new MessageEF();
            LoginEF objLogin = new LoginEF();
            objLogin.NewPassword = NewPassword;
            objLogin.ConfirmPassword = CofirmPassword;
            objLogin.OTP = OTP;
            objLogin.Source = source;
            if (objLogin.NewPassword == objLogin.ConfirmPassword)
            {
                objMessageEF = _objILoginModel.ForgotPassword(objLogin);
            }
            if (objMessageEF.Satus == "1")
            {
                TempData["Message"] = "Password changed successfully.";
                return RedirectToAction("Login");
            }
            else if (objMessageEF.Satus == "2")
            {
                TempData["Messages"] = "Entered Wrong OTP.";
                TempData.Keep("Messages");
                return RedirectToAction("ForgotPasswordV1");
            }
            else if (objMessageEF.Satus == "0")
            {
                TempData["Messages"] = "User does not exist";
                TempData.Keep("Messages");
                return RedirectToAction("ForgotPasswordV1");
            }
            {
                TempData["Messages"] = "Wrong Credential.";
                TempData.Keep("Messages");
                return RedirectToAction("ForgotPasswordV1");
            }
        }
        [SkipSessionTask]
        public string SendOTP(string MobileEmail)
        {
            MessageEF objMessageEF = new MessageEF();
            TRegistrationEF tRegistrationEF = new();
            tRegistrationEF.EmailId = MobileEmail;
            objMessageEF = _objILoginModel.SendOTP(tRegistrationEF);
            return objMessageEF.Msg;
            //if (objMessageEF.Satus == "1")
            //{
            //    TempData["Msg"] = objMessageEF.Msg;
            //    return RedirectToAction("ForgotPasswordV1");
            //}
            //else
            //{
            //    TempData["Msg"] = objMessageEF.Msg;
            //    return RedirectToAction("ForgotPasswordV1");
            //}
        }
        public IActionResult MChemical()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            MonthlyChemicalEF  onjMonthModel = new MonthlyChemicalEF();
          var  onjMonth = _objILoginModel.ChemicalsList(onjMonthModel);

            return View(onjMonth);
        }

        [SkipSessionTask]
        public IActionResult about()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult Chemicals()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult PetroChemicals()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");


            return View();
        }
        [SkipSessionTask]
        public IActionResult Team()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult Data()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

        

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            var finYear = _objILoginModel.GetFinancialYear(onjMonthModel);
            ViewBag.ViewFinYEarList = finYear.ToList().Select(c => new SelectListItem
            {
                Text = c.Fyear_Name,
                Value = c.Fyear_Code.ToString(),

            }).ToList();

            var month = _objILoginModel.GetMonth(onjMonthModel);
            ViewBag.ViewMonthList = month.ToList().Select(c => new SelectListItem
            {
                Text = c.Month_Name,
                Value = c.Month_Code.ToString(),

            }).ToList();
            return View();
        }
        [SkipSessionTask]
        [HttpPost]
        public IActionResult Data(DataEF Data)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            var finYear = _objILoginModel.GetFinancialYear(onjMonthModel);
            ViewBag.ViewFinYEarList = finYear.ToList().Select(c => new SelectListItem
            {
                Text = c.Fyear_Name,
                Value = c.Fyear_Code.ToString(),

            }).ToList();

            var month = _objILoginModel.GetMonth(onjMonthModel);
            ViewBag.ViewMonthList = month.ToList().Select(c => new SelectListItem
            {
                Text = c.Month_Name,
                Value = c.Month_Code.ToString(),

            }).ToList();
            TempData["Message"] = "No Data Found";
            TempData.Keep("Message");
            return RedirectToAction("Data");
        }
         [SkipSessionTask]
        public IActionResult Dashboard()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");


            DashboardEF dashboardEF = new DashboardEF();

            dashboardEF = _objILoginModel.Dashboard(dashboardEF);
            TempData["Chemicals"] = dashboardEF.Chemicals;
            TempData["PeroChemicals"] = dashboardEF.PeroChemicals;
            TempData["Export"] = dashboardEF.Export;
            TempData["Import"] = dashboardEF.Import;
            TempData.Keep("Chemicals");
            TempData.Keep("PeroChemicals");
            TempData.Keep("Export");
            TempData.Keep("Import");
            return View();
        }
        [SkipSessionTask]
        public IActionResult VIDEO()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult PDF()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }

        [SkipSessionTask]
        public IActionResult Contact()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }

        [SkipSessionTask]
        [HttpPost]
        public IActionResult Contact(ContactEF contacts)
        {
            ContactEF contactEF = new ContactEF();
            contactEF = _objILoginModel.Contact(contacts);
            if(contactEF.status == 0)
            {
                TempData["Message"] = "Save successfully.";
                TempData.Keep("Message");
            }
            else
            {
                TempData["Messages"] = "Not Save successfully.";
                TempData.Keep("Messages");
            }

            return RedirectToAction("Contact");
        }
        [SkipSessionTask]
        public IActionResult Disclaimer()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }


        [SkipSessionTask]
        public IActionResult TermsConditions()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult Privacy()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult Websitepolicy()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult Faq()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult feedback()
        {

            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");


            return View();
        }
        [SkipSessionTask]
        [HttpPost]
        public IActionResult feedback(feedbackEF feedback)
        {
            feedbackEF feedbacks = new feedbackEF();
            feedback.EntryDate = DateTime.Now;
            feedbacks = _objILoginModel.feedback(feedback);
            if (feedbacks.Result == 0)
            {
                TempData["Message"] = "Save successfully.";
                TempData.Keep("Message");
            }
            else
            {
                TempData["Messages"] = "Not Save successfully.";
                TempData.Keep("Messages");
            }

            return RedirectToAction("feedback");
        }
        [SkipSessionTask]
        public IActionResult dram()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult Welcome()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult ForgotUserName()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [SkipSessionTask]
        public IActionResult ForgotUserName(LoginEF objLogin)
        {
            MessageEF objMessageEF = new MessageEF();
            SMSEF objsms = new SMSEF();
            objsms.recipientPhoneNumber = "8877530313";

            bool status = sendSMS("8877530313");
            if (true)
            {
                objMessageEF = _objILoginModel.ForgotUserName(objLogin);
            }


            if (objMessageEF.Satus == "1")
            {
                EmailEF ef = new EmailEF()
                {
                    EmailAddress = objMessageEF.Value,
                    Message = $"User id {objMessageEF.Msg}",
                    Subject = "User id"
                };
                MessageEF emailres = _objILoginModel.SendEmail(ef);

                TempData["Message"] = "User name sent on email successfully.";
                TempData.Keep("Message");
                return View();
            }
            else if (objMessageEF.Satus == "2")
            {
                TempData["Messages"] = "Entered Wrong OTP.";
                TempData.Keep("Messages");
                return View();
            }
            else if (objMessageEF.Satus == "0")
            {
                TempData["Messages"] = "User does not exist";
                TempData.Keep("Messages");
                return View();
            }
            {
                TempData["Messages"] = "Wrong Credential.";
                TempData.Keep("Messages");
                return View();
            }
        }
      
        [SkipSessionTask]
        public IActionResult InterestingFacts()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        
       [SkipSessionTask]
        public IActionResult Appreciations()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }

        [SkipSessionTask]
        public IActionResult Analyticalstudy()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
      
        [SkipSessionTask]
        public IActionResult ChemicalsClassification()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]
        public IActionResult HazardousClassification()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        
        [SkipSessionTask]
        public IActionResult ManufacturingUnits()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }
        [SkipSessionTask]

        [HttpGet]
        public IActionResult DownloadDoc(string fileName)
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");


            //fileName = "UNGHSbrief.pdf";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs/HazardPdf", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return View("DocNotFound");
            }
            return PhysicalFile(filePath, "application/octet-stream", fileName);
        }
        
             [SkipSessionTask]
        public IActionResult Sitemap()
        {
            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }


        [SkipSessionTask]
        public IActionResult KnowledgeDirectory()
        {

            VisitcountEF visitcountEF = new VisitcountEF();
            visitcountEF.Visiters = 2;

            visitcountEF = _objILoginModel.visitcount(visitcountEF);
            TempData["visitcountEF"] = visitcountEF.VisitersCounts;
            TempData.Keep("visitcountEF");

            return View();
        }  
        
     
        [SkipSessionTask]
        public IActionResult Search(string searchQuery)
        {
            List<SearchResults> results = new List<SearchResults>();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Areas/LandingPage/Views/Home");

            var cshtmlFiles = Directory.GetFiles(filePath, "*.cshtml", SearchOption.AllDirectories);

            foreach (var file in cshtmlFiles)
            {
                var fileContent = ExtractTextContent(System.IO.File.ReadAllText(file));
               // ExtractTextContent(string htmlContent)
                var sentences = ExtractSentencesContainingSearchQuery(fileContent, searchQuery);

                foreach (var sentence in sentences)
                {
                    // Add the filename, sentence, and file content to the results
                    results.Add(new SearchResults { FileName = Path.ChangeExtension(Path.GetFileName(file), null), Title = sentence, Content = fileContent });
                }
            }
            return View(results);
           // return results;
        }

        // The method to extract sentences containing the search query from the file content
        private List<string> ExtractSentencesContainingSearchQuery(string fileContent, string searchQuery)
        {
            List<string> sentences = new List<string>();

            // Split the file content into sentences (you may need to adjust this logic based on your file's structure)
            string[] delimiterChars = { ".", "!", "?" };
            string[] allSentences = fileContent.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // Check each sentence to see if it contains the search query
            foreach (var sentence in allSentences)
            {
                if (sentence.Contains(searchQuery))
                {
                    sentences.Add(sentence.Trim());
                }
            }

            return sentences;
        }
       
        private string ExtractTextContent(string htmlContent)
        {
            htmlContent = Regex.Replace(htmlContent, @"<script[^>]*>[\s\S]*?</script>", string.Empty, RegexOptions.IgnoreCase);

            // Remove style tags and their contents
            htmlContent = Regex.Replace(htmlContent, @"<style[^>]*>[\s\S]*?</style>", string.Empty, RegexOptions.IgnoreCase);

            // Remove all HTML tags
            htmlContent = Regex.Replace(htmlContent, @"<[^>]+>", string.Empty);

            // Decode HTML entities, e.g., &nbsp;, &amp;, etc.
            htmlContent = DecodeHtmlEntities(htmlContent);

            // Remove extra whitespaces and trim
            htmlContent = Regex.Replace(htmlContent, @"\s+", " ").Trim();

            // Remove code blocks (e.g., <code>...</code>, <pre>...</pre>, etc.)
            htmlContent = Regex.Replace(htmlContent, @"<code[^>]*>[\s\S]*?</code>", string.Empty, RegexOptions.IgnoreCase);
            htmlContent = Regex.Replace(htmlContent, @"<pre[^>]*>[\s\S]*?</pre>", string.Empty, RegexOptions.IgnoreCase);
            // Ignore content between @{} blocks (e.g., Razor code blocks)
            htmlContent = Regex.Replace(htmlContent, @"@{[\s\S]*?}", string.Empty);
            // Remove lines containing @model followed by space and a word
            htmlContent = Regex.Replace(htmlContent, @"@model\s+\w+", string.Empty);
            htmlContent = Regex.Replace(htmlContent, @"\b\w+EF\b", string.Empty);

            return htmlContent;
        }
        private string DecodeHtmlEntities(string htmlContent)
        {
            return Regex.Replace(htmlContent, @"&[^\s]*;", match =>
            {
                var entity = match.Value;
                return entity switch
                {
                    "&nbsp;" => " ",
                    "&amp;" => "&",
                    "&lt;" => "<",
                    "&gt;" => ">",
                    // Add more cases for other HTML entities you want to handle
                    _ => entity,
                };
            });
        }
    
}
}
