using CPDMS.ActionFilter;
using CPDMS.Areas.CompanyRegistraion.Models.UnitRegistration;
using CPDMS.Areas.UnitRegistration.Models.UnitRegistration;
using CPDMS.Web;
using CPDMSEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace CPDMS.Areas.CompanyRegistraion.Controllers
{
    [Area("CompanyRegistraion")]
    public class CompanyRegistrationController : Controller
    {
        private readonly ILogger<CompanyRegistrationController> _logger;

        private ICompanyModel _objICompanyModel;
        MessageEF objMessageModel = new MessageEF();
        private readonly IHostEnvironment hostingEnvironment;
        public CompanyRegistrationController(ICompanyModel objICompanyModel, IHostEnvironment hostingEnvironment, ILogger<CompanyRegistrationController> logger)
        {
            _objICompanyModel = objICompanyModel;
            this.hostingEnvironment = hostingEnvironment;
            _logger = logger;

        }
        [SkipSessionTask]

        public IActionResult CompanyTRegistration()
        {
            return View();
        }
        [SkipSessionTask]

        [HttpPost]

        public IActionResult CompanyTRegistration(TRegistrationEF tRegistrationEF)
        {
            try 
            {
                if (tRegistrationEF.Password != tRegistrationEF.ConfirmPassword)
                {
                    TempData["Message"] = "Password and Confirm Password not match";
                    TempData.Keep("Message");
                    //return RedirectToAction("CompanyTRegistration");
                    return View();
                }
                else if (tRegistrationEF.IsTerms == null)
                {
                    TempData["Message"] = "Please accept terms and condition.";
                    TempData.Keep("Message");
                    //return RedirectToAction("CompanyTRegistration");
                    return View();
                }
                Random random = new Random();
                // int emailotp = random.Next(100000, 999999);
                int emailotp = 12345;

                tRegistrationEF.EmailOtp = emailotp;
                string email = tRegistrationEF.EmailId;
                objMessageModel = _objICompanyModel.AddCompanyRegistrationEntry(tRegistrationEF);
                if (objMessageModel.Satus == "Not EXISTS")
                {
                    EmailEF ef = new EmailEF()
                    {
                        EmailAddress = email,
                        Message = Convert.ToString(emailotp),
                        Subject = "OTP verification"
                    };
                    MessageEF emailres = _objICompanyModel.SendEmail(ef);
                    if (emailres.Satus == "1")
                    {
                        TempData["Messages"] = "OTP Send Sucessfully";
                        TempData.Keep("Messages");
                    }
                    else
                    {
                        TempData["Message"] = "Failed to Send OTP";
                        TempData.Keep("Message");

                    }

                    ViewBag.msg = "Data Save Sucessfully";
                    return RedirectToAction("CompanyTRegistrationOTP", new { Id = objMessageModel.Value });
                }
                else
                {
                    ViewBag.msg = "Data not Save Sucessfully";
                    return RedirectToAction("CompanyTRegistration");
                }

            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
               // return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
        }
        [SkipSessionTask]

        public IActionResult CompanyTRegistrationOTP(int Id)
        {
            RegistrationOTPEF registrationOTPEF = new RegistrationOTPEF();
            registrationOTPEF.UserId = Id;
            return View(registrationOTPEF);
        }
        [SkipSessionTask]

        [HttpPost]

        public IActionResult CompanyTRegistrationOTP(RegistrationOTPEF registrationOTPEF)
        {
            try
            {
                // registrationOTPEF.EmailOTP =12345 ;
                objMessageModel = _objICompanyModel.CompanyRegistrationOTPEntry(registrationOTPEF);
                if (objMessageModel.Value == "1")
                {
                    //TempData["msg"] = "Data Save Sucessfully";
                    //TempData.Keep("msg");
                    //TempData["Messages"] = "Data Saved Sucessfully";
                    //TempData.Keep("Messages");
                    return RedirectToAction("Welcome", "Home", new { Area = "LandingPage" });
                }
                else
                {
                    TempData["Message"] = "OTP Not Match Please Try Again";
                    TempData.Keep("Message");
                    //TempData["msg"] = "OTP Not Match Please Try Again";
                    //TempData.Keep("msg");

                    return RedirectToAction("CompanyTRegistrationOTP", new { Id = registrationOTPEF.UserId });
                }
            }

            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
        }

        public IActionResult CompanyDashboard()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            DashboardRequestEF objCompanyModel = new DashboardRequestEF();
            try
            {
                _logger.LogInformation("compnay dashboard method called!!!");
               
                objCompanyModel.Action = 2;
                objCompanyModel.ID = profile.UserId;
                objCompanyModel.RoleId = profile.RoleID;
                var UnitDetailsList = _objICompanyModel.GetUnitDetailsList(objCompanyModel);
                _logger.LogInformation($"unit details{UnitDetailsList}");

                if (UnitDetailsList == null || UnitDetailsList.Count == 0)
                {
                    UnitDetailsList = new List<UnitDetailsEF>
                        {
                            new UnitDetailsEF
                            {
                                UnitCount = "0",
                                Productions = "0",
                                Chemicals = "0",
                                PetroChemical = "0"
                            }
                        };
                }
                    //capacitytarget graph
                    objCompanyModel.Action = 3;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsYearlyChem = _objICompanyModel.GetGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsYearlyChem = GrapDetailsYearlyChem;
                //end
                //monthchemgraph
                objCompanyModel.Action = 3;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsMonthChem = _objICompanyModel.GetMonthGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsMonthChem = GrapDetailsMonthChem;
                //end
                var CompanyPerformanceList = _objICompanyModel.GetCompanyPerformanceList(objCompanyModel);
                ViewBag.ViewUnitDetailsLis = UnitDetailsList;
                ViewBag.ViewCompanyPerformanceList = CompanyPerformanceList;
                //marquee
                objCompanyModel.ID = profile.UserId;

                MessageEF marqueedahbord = _objICompanyModel.DashboardMarquee(objCompanyModel);
                if(!(marqueedahbord.Value=="1"))
                {
                    return RedirectToAction("CompanyEntryAdd", "CompanyRegistration", new { area = "CompanyRegistraion" });
                }
                if (marqueedahbord.Value!=null)
                ViewBag.MarqueeMsg = marqueedahbord.Msg;
                else
                ViewBag.MarqueeMsg ="";
                //import exportgrap
                objCompanyModel.Action = 3;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var getMonthImportExportGraphs = _objICompanyModel.GetMonthImportExportGraphs(objCompanyModel);
                ViewBag.ViewGraphImportexportmonth = getMonthImportExportGraphs;
                //end

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in COMADM dashboard:::::: {ex}");
                Console.WriteLine(ex);
                 return RedirectToAction("Login", "Home", new { area = "LandingPage" });
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }

            return View();
        }
        public IActionResult CompanyEntryAdd(int id = 0)
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            MasterEF objCompanyModel = new MasterEF();
            CompanyRegistrationEF compnayef = new CompanyRegistrationEF();
            try
            {
                objCompanyModel.Action = 1;
                var state = _objICompanyModel.GetStateList(objCompanyModel);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();

                if (profile.RoleID == 1)
                    {                  
                    objCompanyModel.Action = 2;
                    objCompanyModel.ID = profile.UserId;
                    }


               //  var  objCompanyList = _objICompanyModel.CompanyEntryGet(objCompanyModel);
                  
                compnayef = _objICompanyModel.CompanyEntryGet(objCompanyModel).FirstOrDefault();
               
                if(compnayef!=null)
                {
                    ViewBag.ViewStateList1= compnayef.State_Code;
                    ViewBag.ViewStateList2= compnayef.RState_Code;
                    ViewBag.ViewStateList2 = compnayef.District_Code;
                }
               // compnayef.stateList = new SelectList(state.ToList(), "State_Code", "State_Name", compnayef.State_Code);


                // ViewBag.ViewCompanyList = compnayef.FirstOrDefault(); 
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                Console.WriteLine(ex);
                 return RedirectToAction("Login", "Home", new { area = "LandingPage" });
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }



            if (id != 0)
            {

                objCompanyModel.ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);
                ViewBag.Button = "Update";
                return View();
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(compnayef);
            }

        }
        public JsonResult GetDistrictByStateId(string State_Code)
        {
            try
            {
                MasterEF masterEF = new MasterEF();
                masterEF.State_Code = State_Code;
                List<MasterEF> lstDistrictModel = _objICompanyModel.GetDistrictDetails(masterEF);
                return Json(lstDistrictModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return Json(null);
            }

        }
        public JsonResult GetBlockByDistrictId(string State_Code, string District_Code)
        {
            try
            {
                MasterEF masterEF = new MasterEF();
                masterEF.State_Code = State_Code;
                masterEF.District_Code = District_Code;
                List<MasterEF> lstDistrictModel = _objICompanyModel.GetBlockByDistrictId(masterEF);
                return Json(lstDistrictModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return Json(null);
            }

        }
        //private UserLoginSession DeserializeUserSession(byte[] sessionData)
        //{
        //    if (sessionData == null || sessionData.Length == 0)
        //    {
        //        return null;
        //    }

        //    string json = Encoding.UTF8.GetString(sessionData);
        //    UserLoginSession userSession = JsonConvert.DeserializeObject<UserLoginSession>(json);
        //    return userSession;
        //}
        [HttpPost]
        public IActionResult CompanyEntryAdd(CompanyRegistrationEF objmodel, string submit)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.Entry_By = Convert.ToString(profile.UserId);

                objMessageModel = _objICompanyModel.AddCompanyEntry(objmodel);

                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Saved Sucessfully";
                    TempData.Keep("Messages");
                    return RedirectToAction("CompanyEntryAdd");
                }
                else
                {
                    TempData["Message"] = "Data not Save Sucessfully";
                    TempData.Keep("Message");
                    return RedirectToAction("CompanyEntryAdd");
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
           
        }

        public IActionResult CompanyUnitTagging(int Cid, int actionid, int id = 0)
        {
            MasterE objCompanyModel = new MasterE();
            List<Unitlist> Unitlist = new List<Unitlist>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {
                //delete
                if (actionid == 1)
                {
                    objCompanyModel.Action = 3;
                    objCompanyModel.ID = Cid;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var com = _objICompanyModel.UCompanyTagging(objCompanyModel);
                }
                ////list
                objCompanyModel.Action = 1;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                Unitlist = _objICompanyModel.CompanyTaggingList(objCompanyModel);
                ////listend
                objCompanyModel.Action = 4;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Company = _objICompanyModel.CompanyList(objCompanyModel);
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();
                ViewBag.CompanyName = profile.UserName;
                ViewBag.CompanyCode = profile.UserId;
                objCompanyModel.Action = 1;
                var Unit = _objICompanyModel.UnitList(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


            }

            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                Console.WriteLine(ex);
                 return RedirectToAction("Login", "Home", new { area = "LandingPage" });
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }



            if (id != 0)
            {

                objCompanyModel.ID = id;
                ViewBag.Button = "Update";
                return View();
            }
            else
            {
                ViewBag.Unitlist = Unitlist;

                ViewBag.Button = "Submit";
                return View(objCompanyModel);
            }

        }
        [HttpPost]
        public IActionResult CompanyUnitTagging(CUnitTagging objmodel, string submit)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.Entry_By = Convert.ToString(profile.UserId);
                objmodel.Company_Code = Convert.ToString(profile.UserId);
                objMessageModel = _objICompanyModel.AddCompanyUnitTagging(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    ViewBag.msg = "Data Save Sucessfully";
                    return RedirectToAction("CompanyUnitTagging");
                }
                else
                {
                    ViewBag.msg = "Data not Save Sucessfully";
                    return RedirectToAction("CompanyUnitTagging");
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
        }

        public IActionResult ChangePassword()
        {         
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordEF changePasswordEF)
        {
            try
            {
                if (changePasswordEF.NewPassword != changePasswordEF.ConfirmNewPassword)
                {
                    TempData["Message"] = "New Password and Confirm New Password does not match";
                    TempData.Keep("Message");
                    return RedirectToAction("ChangePassword");
                }
                if (changePasswordEF.NewPassword == null && changePasswordEF.ConfirmNewPassword == null && changePasswordEF.CurrentPassword == null)
                {
                    TempData["Message"] = "All fields are mandatory";
                    TempData.Keep("Message");
                    return RedirectToAction("ChangePassword");
                }
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                changePasswordEF.UserId = Convert.ToString(profile.UserName);
                var ChangePassword = _objICompanyModel.ChangePassword(changePasswordEF);
                if (ChangePassword.Value == "1")
                {
                    TempData["Messages"] = ChangePassword.Msg;
                    TempData.Keep("Message");
                }
                else
                {
                    TempData["Message"] = ChangePassword.Msg;
                    TempData.Keep("Message");
                }
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
           catch(Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
        }

        public IActionResult CreateGroup()
        {
            return View();

        }

        public IActionResult CreateSubGroup()
        {
            return View();
        }

        public IActionResult CreateTwoDigit()
        {
            return View();
        }

        public IActionResult CreateFourDigit()
        {
            return View();
        }

        public IActionResult CreateSixDigit()
        {
            return View();
        }

        public IActionResult CreateEightDigit()
        {
            return View();
        }

        public IActionResult ListOfCompany()
        {
            return View();
        }

        public IActionResult ListOfGroup()
        {
            return View();
        }

        public IActionResult ListOfProduct()
        {
            return View();
        }
        public IActionResult Report()
        {

            List<ReportViewEF> objCompanyList = new List<ReportViewEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                objmodel.Action = 2;
                objmodel.ID = profile.UserId;
                objCompanyList = _objICompanyModel.ViewAdminReportList(objmodel);
                return View(objCompanyList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                objCompanyList = null;
            }

        }

        public IActionResult CompnayProfileReport()
        {

            List<CompanyProfileEF> objCompanyList = new List<CompanyProfileEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                if (profile.RoleID == 1)
                {
                    objmodel.Action = 1;
                    objmodel.ID = profile.UserId;
                }
                else if (profile.RoleID == 2)
                {
                    objmodel.Action = 2;
                    objmodel.ID = profile.UserId;
                }

                objCompanyList = _objICompanyModel.CompnayProfileReport(objmodel);
                string protocol = Request.Scheme;
                string host = Request.Host.Value;

                ViewBag.Protocol = protocol;
                ViewBag.Host = host;
                return View(objCompanyList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                objCompanyList = null;
            }
            return View();
        }
        public IActionResult DownloadDoc(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return View("DocNotFound");
            }
            return PhysicalFile(filePath, "application/octet-stream", fileName);
        }
        public IActionResult DocNotFound()
        {
            return View();
        }

        public IActionResult Profile()
        {

            List<ProfileEF> objCompanyList = new List<ProfileEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                if (profile.RoleID == 1)
                {
                    objmodel.Action = 1;
                    objmodel.ID = profile.UserId;
                }


                objCompanyList = _objICompanyModel.Profile(objmodel);
                string protocol = Request.Scheme;
                string host = Request.Host.Value;

                ViewBag.Protocol = protocol;
                ViewBag.Host = host;
                ViewBag.CompnayName = profile.UserName;
                ViewBag.Email = profile.Email_id;                
                if (objCompanyList.Count != 0)
                {
                    ProfileEF singleCompany = objCompanyList[0];
                    return View(new List<ProfileEF> { singleCompany });
                }
                else
                {
                    TempData["Message"] = "Please complete your registration first!";
                    TempData.Keep("Message");                  
                    return RedirectToAction("CompanyEntryAdd");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                objCompanyList = null;
            }
            return View();
        }

       public IActionResult DashboardMarquee()
            {
                JsonObject obj = new JsonObject();
                obj.Add("i", "hi");

                return Json(obj);
            }
       public IActionResult BestPractisesReports()
            {
                MasterE objCompanyModel = new MasterE();
                List<BestPracticesEF> objCompanyList = new List<BestPracticesEF>();
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

                DashboardRequestEF objmodel = new DashboardRequestEF();
                try
                {

                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);

                    var Unit = _objICompanyModel.UnitList(objCompanyModel);

                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();


                    if (profile.RoleID == 1)
                    {
                        objmodel.Action = 1;
                        objmodel.ID = profile.UserId;
                    }


                    objCompanyList = _objICompanyModel.BestPractisesReports(objmodel);

                    ViewBag.ViewBestPractices = objCompanyList;


                    return View();
                }
                catch (Exception ex)
                {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
                finally
                {

                    objCompanyList = null;
                }
                return View();
            }
        

        [HttpPost]
        public IActionResult BestPractisesReports(int Unit_Code)
        {
            MasterE objCompanyModel = new MasterE();
            List<BestPracticesEF> objCompanyList = new List<BestPracticesEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                objCompanyModel.Action = 5;
               objCompanyModel.Entry_By = Convert.ToString(profile.UserId);

                var Unit = _objICompanyModel.UnitList(objCompanyModel);

                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                if (profile.RoleID == 1)
                {
                    objmodel.Action = 1;
                    objmodel.ID = Unit_Code;
                    //objmodel.ID = 101;
                }


                objCompanyList = _objICompanyModel.BestPractisesReports(objmodel);
                if(objCompanyList!=null)
                {
                    ViewBag.ViewBestPractices = objCompanyList;

                }
                else
                {
                    ViewBag.ViewBestPractices = null;
                }


                return View();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                objCompanyList = null;
            }
            return View();
        }
        public IActionResult GishDashboard()
        {
            return View();
        }
        public IActionResult TestGis()
        {
            MasterEF objCompanyModel = new MasterEF();
           
                objCompanyModel.Action = 1;
                var state = _objICompanyModel.GetStateList(objCompanyModel);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                return View();
        }
    }
}
