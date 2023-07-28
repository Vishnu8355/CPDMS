using CPDMS.ActionFilter;
using CPDMS.Areas.UnitRegistration.Models.UnitRegistration;
using CPDMS.Entities;
using CPDMS.Web;
using CPDMSEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using static System.Net.WebRequestMethods;

namespace CPDMS.Areas.UnitRegistration.Controllers
{
    [Area("UnitRegistration")]
    public class UnitRegistrationController : Controller
    {
        private readonly ILogger<UnitRegistrationController> _logger;

        private IUnitModel _objIUnitModel;
        MessageEF objMessageModel = new MessageEF();
        List<UnitRegistrationViewEF> objUnitList = new List<UnitRegistrationViewEF>();
        private readonly IHostEnvironment hostingEnvironment;
        public UnitRegistrationController(IUnitModel objIUnitModel, IHostEnvironment hostingEnvironment, ILogger<UnitRegistrationController> logger)
        {
            _objIUnitModel = objIUnitModel;
            this.hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public IActionResult UnitsEntryAdd(int id = 0)
        {
            //ViewBag.userid = userid;
            //ViewBag.userType = usertype;
            //ViewBag.userroleinfo = "ML";

            MasterEF objUnitModel = new MasterEF();
            try
            {
                objUnitModel.Action = 1;
                var state = _objIUnitModel.GetStateList(objUnitModel);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                var Category = _objIUnitModel.GetCategoryList(objUnitModel);
                ViewBag.ViewCategoryList = Category.ToList().Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryCode.ToString(),

                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                objUnitModel = null;
            }



            if (id != 0)
            {

                objUnitModel.ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);
                ViewBag.Button = "Update";
                return View();
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(objUnitModel);
            }

        }
        [HttpPost]
        public IActionResult UnitsEntryAdd(UnitRegistrationEF objmodel, string submit)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.Entry_By = Convert.ToString(profile.UserId);
                //  objmodel.UserLoginId = 1; //profile.UserLoginId;
                //if (ModelState.IsValid)
                //{
                objMessageModel = _objIUnitModel.AddUnitEntry(objmodel);
                // }
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Saved Sucessfully";
                    TempData.Keep("Messages");
                    return RedirectToAction("UnitsEntryAdd");
                }
                else if(objMessageModel.Satus == "2")
                {
                    TempData["Messages"] = "Data updated Sucessfully";
                    TempData.Keep("Messages");
                    return RedirectToAction("UnitsEntryAdd");
                }
                else
                {
                    TempData["Message"] = "Something went wrong ,please try again!!";
                    TempData.Keep("Message");
                    return RedirectToAction("UnitsEntryAdd");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }


        }
        public JsonResult GetDistrictByStateId(string State_Code)
        {
            try
            {
                MasterEF masterEF = new MasterEF();
                masterEF.State_Code = State_Code;
                List<MasterEF> lstDistrictModel = _objIUnitModel.GetDistrictDetails(masterEF);
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
                List<MasterEF> lstDistrictModel = _objIUnitModel.GetBlockByDistrictId(masterEF);
                return Json(lstDistrictModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        [SkipSessionTask]

        public IActionResult UnitTRegistration()
        {
            return View();
        }
        [SkipSessionTask]

        [HttpPost]
        public IActionResult UnitTRegistration(TRegistrationEF tRegistrationEF)
        {
            try
            {
                if (tRegistrationEF.Password != tRegistrationEF.ConfirmPassword)
                {
                    TempData["Message"] = "Password and Confirm Password not match";
                    TempData.Keep("Message");
                    //return RedirectToAction("UnitTRegistration");
                    return View();
                }
                Random random = new Random();
                //int emailotp = random.Next(100000, 999999);
                int emailotp = 12345;

                tRegistrationEF.EmailOtp = emailotp;
                objMessageModel = _objIUnitModel.AddUnitRegistrationEntry(tRegistrationEF);
                string email = tRegistrationEF.EmailId;
                if (objMessageModel.Satus == "Not EXISTS")
                {


                    EmailEF ef = new EmailEF()
                    {
                        EmailAddress = email,
                        Message = Convert.ToString(emailotp),
                        Subject = "otp verification"
                    };
                    MessageEF emailres = _objIUnitModel.SendEmail(ef);
                    if (emailres.Satus == "1")
                    {
                        TempData["Messages"] = "Otp Send Sucessfully";
                        TempData.Keep("Messages");
                    }
                    else
                    {
                        TempData["Message"] = "Failed to Send Otp";
                        TempData.Keep("Message");

                    }

                    //   TempData["Message"] = "Data Save Sucessfully";
                    // TempData.Keep("Message");
                    return RedirectToAction("UnitTRegistrationOTP", new { Id = objMessageModel.Value });

                }
                else
                {
                    TempData["Message"] = "Data not Saved Sucessfully";
                    TempData.Keep("Message");
                    return RedirectToAction("UnitTRegistration");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }

        }
        [SkipSessionTask]

        public IActionResult UnitTRegistrationOTP(int Id)
        {
            RegistrationOTPEF registrationOTPEF = new RegistrationOTPEF();
            registrationOTPEF.UserId = Id;
            return View(registrationOTPEF);
        }
        [SkipSessionTask]

        [HttpPost]
        public IActionResult UnitTRegistrationOTP(RegistrationOTPEF registrationOTPEF)
        {
            try
            {
                objMessageModel = _objIUnitModel.UnitTRegistrationOTPEntry(registrationOTPEF);
                if (objMessageModel.Value == "1")
                {
                    //TempData["msg"] = "Data Save Sucessfully";
                    //TempData.Keep("msg");
                    //TempData["Messages"] = "Data Saved Sucessfully";
                    //TempData.Keep("Messages");
                     return RedirectToAction("Welcome", "Home", new { area = "LandingPage" });          
                }
                else
                {
                    TempData["Message"] = "OTP Not Match Please Try Again";
                    TempData.Keep("Message");

                    return RedirectToAction("UnitTRegistrationOTP", new { Id = registrationOTPEF.UserId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }

        }

        public IActionResult UnitDashboard()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            DashboardRequestEF objCompanyModel = new DashboardRequestEF();
            try
            {
                objCompanyModel.Action = 3;
                objCompanyModel.ID =profile.UserId;
                objCompanyModel.RoleId = profile.RoleID;

                var UnitDetailsList = _objIUnitModel.GetUnitDetailsList(objCompanyModel);
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
                    var CompanyPerformanceList = _objIUnitModel.GetCompanyPerformanceList(objCompanyModel);
                ViewBag.ViewUnitDetailsLis = UnitDetailsList;
                ViewBag.ViewCompanyPerformanceList = CompanyPerformanceList;
                //marquee
                objCompanyModel.ID = profile.UserId;

                MessageEF marqueedahbord = _objIUnitModel.DashboardMarquee(objCompanyModel);
                if (!(marqueedahbord.Value == "1"))
                {
                    return RedirectToAction("UnitsEntryAdd", "UnitRegistration", new { area = "UnitRegistration" });
                }
                if (marqueedahbord.Value != null || UnitDetailsList.Count == 0)
                    ViewBag.MarqueeMsg = marqueedahbord.Msg;
                else
                    ViewBag.MarqueeMsg = "";
                //graph
                objCompanyModel.Action = 4;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsYearlyChem = _objIUnitModel.GetGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsYearlyChem = GrapDetailsYearlyChem;
                //monthchemgraph
                objCompanyModel.Action = 2;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsMonthChem = _objIUnitModel.GetMonthGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsMonthChem = GrapDetailsMonthChem;
                //end
                //import exportgrap
                objCompanyModel.Action = 2;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var getMonthImportExportGraphs = _objIUnitModel.GetMonthImportExportGraphs(objCompanyModel);
                ViewBag.ViewGraphImportexportmonth = getMonthImportExportGraphs;
                //end
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                Console.WriteLine(ex);
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }
            return View();
        }


        public IActionResult UnitOPTDashboard()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            DashboardRequestEF objCompanyModel = new DashboardRequestEF();
            try
            {
                objCompanyModel.Action = 4;
                objCompanyModel.ID = profile.UserId;
                objCompanyModel.RoleId = profile.RoleID;

                var UnitDetailsList = _objIUnitModel.GetUnitDetailsList(objCompanyModel);
                if(UnitDetailsList== null || UnitDetailsList.Count == 0)
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
                var CompanyPerformanceList = _objIUnitModel.GetCompanyPerformanceList(objCompanyModel);
                ViewBag.ViewUnitDetailsLis = UnitDetailsList;
                ViewBag.ViewCompanyPerformanceList = CompanyPerformanceList;
                //graph
                objCompanyModel.Action = 1;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsYearlyChem = _objIUnitModel.GetGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsYearlyChem = GrapDetailsYearlyChem;
                //monthchemgraph
                objCompanyModel.Action = 1;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsMonthChem = _objIUnitModel.GetMonthGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsMonthChem = GrapDetailsMonthChem;
                //end
                //import exportgrap
                objCompanyModel.Action = 1;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var getMonthImportExportGraphs = _objIUnitModel.GetMonthImportExportGraphs(objCompanyModel);
                ViewBag.ViewGraphImportexportmonth = getMonthImportExportGraphs;
                //end
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }
            return View();
        }
        public IActionResult UnitDPTAPPDashboard()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            DashboardRequestEF objCompanyModel = new DashboardRequestEF();
            try
            {
                objCompanyModel.Action = 1;
                objCompanyModel.ID = profile.UserId;
                objCompanyModel.RoleId = profile.RoleID;

                var UnitDetailsList = _objIUnitModel.GetUnitDetailsList(objCompanyModel);

                var CompanyPerformanceList = _objIUnitModel.GetCompanyPerformanceList(objCompanyModel);
                ViewBag.ViewUnitDetailsLis = UnitDetailsList;
                ViewBag.ViewCompanyPerformanceList = CompanyPerformanceList;
                //graph
                objCompanyModel.Action = 2;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsYearlyChem = _objIUnitModel.GetGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsYearlyChem = GrapDetailsYearlyChem;
                //monthchemgraph
                objCompanyModel.Action = 4;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsMonthChem = _objIUnitModel.GetMonthGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsMonthChem = GrapDetailsMonthChem;
                //end
                //import exportgrap
                objCompanyModel.Action = 4;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var getMonthImportExportGraphs = _objIUnitModel.GetMonthImportExportGraphs(objCompanyModel);
                ViewBag.ViewGraphImportexportmonth = getMonthImportExportGraphs;
                //end
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }

            return View();
        }
        public IActionResult UnitDPTADMDashboard()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            DashboardRequestEF objCompanyModel = new DashboardRequestEF();
            try
            {
                objCompanyModel.Action = 1;
                objCompanyModel.ID = profile.UserId;
                objCompanyModel.RoleId = profile.RoleID;

                var UnitDetailsList = _objIUnitModel.GetUnitDetailsList(objCompanyModel);

                var CompanyPerformanceList = _objIUnitModel.GetCompanyPerformanceList(objCompanyModel);
                ViewBag.ViewUnitDetailsLis = UnitDetailsList;
                ViewBag.ViewCompanyPerformanceList = CompanyPerformanceList;
                //graph
                objCompanyModel.Action = 2;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsYearlyChem = _objIUnitModel.GetGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsYearlyChem = GrapDetailsYearlyChem;
                //monthchemgraph
                objCompanyModel.Action = 4;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var GrapDetailsMonthChem = _objIUnitModel.GetMonthGraphsDetailsList(objCompanyModel);
                ViewBag.ViewGrapDetailsMonthChem = GrapDetailsMonthChem;
                //end
                //import exportgrap
                objCompanyModel.Action = 4;
                objCompanyModel.UserId = Convert.ToString(profile.UserId);
                objCompanyModel.RoleId = profile.RoleID;
                var getMonthImportExportGraphs = _objIUnitModel.GetMonthImportExportGraphs(objCompanyModel);
                ViewBag.ViewGraphImportexportmonth = getMonthImportExportGraphs;
                //end
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
                throw;
            }
            finally
            {

                objCompanyModel = null;
            }
            return View();
        }

        public IActionResult YearlyChemicalAdd(int id = 0)
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            try
            {
                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();

                var substance = _objIUnitModel.GetSubstanceList(objChemicalModel);
                ViewBag.ViewSubstanceList = substance.ToList().Select(c => new SelectListItem
                {
                    Text = c.Substance_Detail_Name,
                    Value = c.Substance_Detail_ID.ToString(),

                }).ToList();

                var Hazard = _objIUnitModel.GetHazardList(objChemicalModel);
                ViewBag.ViewHazardList = Hazard.ToList().Select(c => new SelectListItem
                {
                    Text = c.Hazard_Classification_Name,
                    Value = c.Hazard_Classification_ID.ToString(),

                }).ToList();

                var ResultingHarard = _objIUnitModel.GetResultingHazardList(objChemicalModel);
                ViewBag.ViewResultingHazardList = ResultingHarard.ToList().Select(c => new SelectListItem
                {
                    Text = c.Resulting_Hazard_Name,
                    Value = c.Resulting_Hazard_ID.ToString(),

                }).ToList();

                var MainCategory = _objIUnitModel.GetMainCategoryList(objChemicalModel);
                ViewBag.ViewMainCategoryList = MainCategory.ToList().Select(c => new SelectListItem
                {
                    Text = c.Main_Use_Category_Name,
                    Value = c.Main_Use_Category_ID.ToString(),

                }).ToList();

                var Specification = _objIUnitModel.GetSpecificationList(objChemicalModel);
                ViewBag.ViewSpecificationList = Specification.ToList().Select(c => new SelectListItem
                {
                    Text = c.Specification_Industrial_Professional_Name,
                    Value = c.Specification_Industrial_Professional_ID.ToString(),

                }).ToList();

                var RouteList = _objIUnitModel.GetRouteList(objChemicalModel);
                ViewBag.ViewRouteList = RouteList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Significant_Route_Exposure_Name,
                    Value = c.Significant_Route_Exposure_ID.ToString(),

                }).ToList();

                var EnvironmentalList = _objIUnitModel.GetEnvironmentalList(objChemicalModel);
                ViewBag.ViewEnvironmentalList = EnvironmentalList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Environmental_Exposure_Name,
                    Value = c.Environmental_Exposure_ID.ToString(),

                }).ToList();

                var PatterList = _objIUnitModel.GetPatterList(objChemicalModel);
                ViewBag.ViewPatterList = PatterList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Pattern_Exposure_Name,
                    Value = c.Pattern_Exposure_ID.ToString(),

                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                objChemicalModel = null;
            }



            if (id != 0)
            {
                objChemicalModel.C_Entry_By = Convert.ToString(profile.UserId);

                objChemicalModel.CategoryID = 1;

                objMessageModel = _objIUnitModel.CheckYearlyChemicalEntry(objChemicalModel);
                if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");

                }
                // objChemicalModel.Y_PChemical_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);

                ViewBag.Button = "Submit";
                return View(objChemicalModel);
              
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(objChemicalModel);
            }

        }
        [HttpPost]
        public IActionResult YearlyChemicalAdd(YearlyChemicalEF objmodel, string submit)
        {
            try
            {

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            objmodel.C_Entry_By = Convert.ToString(profile.UserId);

            //  objmodel.UserLoginId = 1; //profile.UserLoginId;
            //if (ModelState.IsValid)
            //{
            IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
            string localIpAddressString = localIpAddress.ToString();
            objmodel.C_IP_Address = localIpAddressString;
            objmodel.CategoryID = 1;
            objMessageModel = _objIUnitModel.AddYearlyChemicalEntry(objmodel);
            // }
            if (objMessageModel.Satus == "1")
            {
                TempData["Messages"] = "Saved successfully.";
                TempData.Keep("Messages");
                //TempData["Message"] = 1;
                return RedirectToAction("YearlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

            }
              else  if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
            {
                TempData["Message"] = "Data not Saved.";
                TempData.Keep("Message");
                //TempData["Message"] = 2;
                return RedirectToAction("YearlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
            }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }

        }

        public JsonResult GetChemicalProductByID(string Chemical_Code)
        {
            try
            {
                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Chemical_Code = Chemical_Code;
                List<ChemicalProductGroup> chemicalProductGroups = _objIUnitModel.GetChemicalProductByID(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        public JsonResult GetResultingHazardByID(int Hazard_Classification_ID)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try

            {
                objChemicalModel.Hazard_Classification_ID = Hazard_Classification_ID;
                List<ResultingHazardSubstance> ResultingHazardSubstance = _objIUnitModel.GetResultingHazardList(objChemicalModel);
               
                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }

        public JsonResult GetPetroChemicalProductByID(string PChemical_Code)
        {
            try
            {
                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Chemical_Code = PChemical_Code;
                List<PetroChemicalProductGroup> chemicalProductGroups = _objIUnitModel.GetPetroChemicalProductByID(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }

        public IActionResult YearlyPetroChemicalAdd(int id = 0)
        {
            YearlyPetroChemicalEF objChemicalModel = new YearlyPetroChemicalEF();
            YearlyChemicalEF objChemical = new YearlyChemicalEF();
            YearlyChemicalEF objChemicalModel1 = new YearlyChemicalEF();
            MonthlyChemicalEF objMonthly=new MonthlyChemicalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {

                var chemicals = _objIUnitModel.GetPetroChemicalList(objChemicalModel1);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.PChemical_Name,
                    Value = c.PChemical_Code.ToString(),

                }).ToList();

                var substance = _objIUnitModel.GetSubstanceList(objChemical);
                ViewBag.ViewSubstanceList = substance.ToList().Select(c => new SelectListItem
                {
                    Text = c.Substance_Detail_Name,
                    Value = c.Substance_Detail_ID.ToString(),

                }).ToList();

                var Hazard = _objIUnitModel.GetHazardList(objChemical);
                ViewBag.ViewHazardList = Hazard.ToList().Select(c => new SelectListItem
                {
                    Text = c.Hazard_Classification_Name,
                    Value = c.Hazard_Classification_ID.ToString(),

                }).ToList();

                var ResultingHarard = _objIUnitModel.GetResultingHazardList(objChemical);
                ViewBag.ViewResultingHazardList = ResultingHarard.ToList().Select(c => new SelectListItem
                {
                    Text = c.Resulting_Hazard_Name,
                    Value = c.Resulting_Hazard_ID.ToString(),

                }).ToList();

                var MainCategory = _objIUnitModel.GetMainCategoryList(objChemical);
                ViewBag.ViewMainCategoryList = MainCategory.ToList().Select(c => new SelectListItem
                {
                    Text = c.Main_Use_Category_Name,
                    Value = c.Main_Use_Category_ID.ToString(),

                }).ToList();

                var Specification = _objIUnitModel.GetSpecificationList(objChemical);
                ViewBag.ViewSpecificationList = Specification.ToList().Select(c => new SelectListItem
                {
                    Text = c.Specification_Industrial_Professional_Name,
                    Value = c.Specification_Industrial_Professional_ID.ToString(),

                }).ToList();

                var RouteList = _objIUnitModel.GetRouteList(objChemical);
                ViewBag.ViewRouteList = RouteList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Significant_Route_Exposure_Name,
                    Value = c.Significant_Route_Exposure_ID.ToString(),

                }).ToList();

                var EnvironmentalList = _objIUnitModel.GetEnvironmentalList(objChemical);
                ViewBag.ViewEnvironmentalList = EnvironmentalList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Environmental_Exposure_Name,
                    Value = c.Environmental_Exposure_ID.ToString(),

                }).ToList();

                var PatterList = _objIUnitModel.GetPatterList(objChemical);
                ViewBag.ViewPatterList = PatterList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Pattern_Exposure_Name,
                    Value = c.Pattern_Exposure_ID.ToString(),

                }).ToList();
              
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                objChemicalModel = null;
            }



            if (id!= 0)
            {
                objChemical.C_Entry_By = Convert.ToString(profile.UserId);

                objChemical.CategoryID = 2;
                
                objMessageModel = _objIUnitModel.CheckYearlyChemicalEntry(objChemical);
              if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                 
                }
               // objChemicalModel.Y_PChemical_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);

                ViewBag.Button = "Submit";
                return View(objChemicalModel);
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(objChemicalModel);
            }

        }
        [HttpPost]
        public IActionResult YearlyPetroChemicalAdd(YearlyChemicalEF objmodel, string submit)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.C_Entry_By = Convert.ToString(profile.UserId);
                //  objmodel.UserLoginId = 1; //profile.UserLoginId;
                //if (ModelState.IsValid)
                //{
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.C_IP_Address = localIpAddressString;
                objmodel.CategoryID = 2;
                objmodel.Chemical_Code = objmodel.PChemical_Code;
                objmodel.Chemical_Product_Code = objmodel.PChemical_Product_Code;
                objMessageModel = _objIUnitModel.AddYearlyChemicalEntry(objmodel);
                // }
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyPetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyPetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("YearlyPetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }


        }

        public JsonResult GetYearlyChemicalEntryByID(string Chemical_Code, string Chemical_Product_Code)
        {
            try
            {
                MonthlyChemicalEF masterEF = new MonthlyChemicalEF();
                masterEF.Chemical_Code = Chemical_Code;
                masterEF.Chemical_Product_Code = Chemical_Product_Code;
                List<YearlyChemicalGroup> chemicalProductGroups = _objIUnitModel.GetYearlyChemicalEntryByID(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return Json(null);
            }

        }

        public IActionResult MonthlyChemicalAdd(int id = 0)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            objChemicalModel.C_Entry_By = profile.UserId.ToString();
            try
            {
                //
                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Action = 26;
                masterEF.C_Entry_By = Convert.ToString(profile.UserId);
                var chemicals = _objIUnitModel.GetChemicalProduct(masterEF);

                //
                //var chemicals = _objIUnitModel.GetChemicalProductByID(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Product_Name,
                    Value = c.Chemical_Product_Code.ToString(),

                }).ToList();

                var finYear = _objIUnitModel.GetFinancialYear(onjMonthModel);
                ViewBag.ViewFinYEarList = finYear.ToList().Select(c => new SelectListItem
                {
                    Text = c.Fyear_Name,
                    Value = c.Fyear_Code.ToString(),

                }).ToList();

                var month = _objIUnitModel.GetMonth(onjMonthModel);
                ViewBag.ViewMonthList = month.ToList().Select(c => new SelectListItem
                {
                    Text = c.Month_Name,
                    Value = c.Month_Code.ToString(),

                }).ToList();
                var PhysicalState = _objIUnitModel.GetPhysicalState(onjMonthModel);
                ViewBag.ViewPhyStateList = PhysicalState.ToList().Select(c => new SelectListItem
                {
                    Text = c.PhysicalStateName,
                    Value = c.PhysicalStateID.ToString(),

                }).ToList();
                var PhysicalUnit = _objIUnitModel.GetPhysicalUnit(onjMonthModel);
                ViewBag.ViewPhyUnitList = PhysicalUnit.ToList().Select(c => new SelectListItem
                {
                    Text = c.UnitName,
                    Value = c.UnitID.ToString(),

                }).ToList();


            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                onjMonthModel = null;
            }



            if (id != 0)
            {
                onjMonthModel.M_Entry_By = Convert.ToInt32(profile.UserId);

                onjMonthModel.CategoryID = 1;
                objMessageModel = _objIUnitModel.CheckMonthlyChemicalEntry(onjMonthModel);
                if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Data already submitted.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                  //  return RedirectToAction("MonthlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
                }
              //  onjMonthModel.Production_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);
                //ViewBag.Button = "Update";
                ViewBag.Button = "Submit";

                return View(onjMonthModel);
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(onjMonthModel);
            }

        }
        [HttpPost]
        public IActionResult MonthlyChemicalAdd(MonthlyChemicalEF objmodel, string submit)
        {
            try
            {          
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            objmodel.M_Entry_By = Convert.ToInt32(profile.UserId);
           // objmodel.UserLoginId = profile.UserLoginId;
            //if (ModelState.IsValid)
                //{
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
            string localIpAddressString = localIpAddress.ToString();
            objmodel.M_IP_Address = localIpAddressString;

            objmodel.CategoryID = 1;
            objMessageModel = _objIUnitModel.AddMonthlyChemicalEntry(objmodel);
            // }
            if (objMessageModel.Satus == "1")
            {
                TempData["Messages"] = "Saved successfully.";
                TempData.Keep("Messages");
                //TempData["Message"] = 1;
                return RedirectToAction("MonthlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Data already submitted.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("MonthlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
                }
            else
            {
                TempData["Message"] = "Data not Saved.";
                TempData.Keep("Message");
                //TempData["Message"] = 2;
                return RedirectToAction("MonthlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
            }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
        }

        public IActionResult MonthlySubstanceChemicalAdd(int id = 0)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlySubstanceChemicalEF objMonthSubsModel = new MonthlySubstanceChemicalEF();
            try
            {
                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();

                var finYear = _objIUnitModel.GetFinancialYear(onjMonthModel);
                ViewBag.ViewFinYEarList = finYear.ToList().Select(c => new SelectListItem
                {
                    Text = c.Fyear_Name,
                    Value = c.Fyear_Code.ToString(),

                }).ToList();

                var month = _objIUnitModel.GetMonth(onjMonthModel);
                ViewBag.ViewMonthList = month.ToList().Select(c => new SelectListItem
                {
                    Text = c.Month_Name,
                    Value = c.Month_Code.ToString(),

                }).ToList();

                var whetherFrom = _objIUnitModel.GetWhetherFrom(objMonthSubsModel);
                ViewBag.ViewWhetherFromList = whetherFrom.ToList().Select(c => new SelectListItem
                {
                    Text = c.Whether_From_Name,
                    Value = c.Whether_From_ID.ToString(),

                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");  
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                onjMonthModel = null;
            }
            if (id != 0)
            {
                objMonthSubsModel.M_C_Substances_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);
                ViewBag.Button = "Update";
                return View();
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(onjMonthModel);
            }

        }
        [HttpPost]
        public IActionResult MonthlySubstanceChemicalAdd(MonthlySubstanceChemicalEF objmodel, string submit)
        {
            try
            { 
            // UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            objmodel.M_C_Entry_By = "1";// profile.UserId;
                                        //  objmodel.UserLoginId = 1; //profile.UserLoginId;
                                        //if (ModelState.IsValid)
                                        //{
            IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
            string localIpAddressString = localIpAddress.ToString();
            objmodel.M_C_IP_Address = localIpAddressString;


            objMessageModel = _objIUnitModel.AddMonthlySubstanceChemicalEntry(objmodel);
            // }
            if (objMessageModel.Satus == "1")
            {
                TempData["Messages"] = "Saved successfully.";
                //TempData["Message"] = 1;
                return RedirectToAction("MonthlySubstanceChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

            }
            else
            {
                TempData["Message"] = "Data not Saved.";
                //TempData["Message"] = 2;
                return RedirectToAction("MonthlySubstanceChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
            }
        }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }

        }

        public JsonResult GetYearlyPetroChemicalEntryByID(string Chemical_Code, string Chemical_Product_Code)
        {
            try
            {
                MonthlyPetroChemicalEF masterEF = new MonthlyPetroChemicalEF();
                masterEF.Chemical_Code = Chemical_Code;
                masterEF.Chemical_Product_Code = Chemical_Product_Code;
                List<YearlyPetroChemicalGroup> chemicalProductGroups = _objIUnitModel.GetYearlyPetroChemicalEntryByID(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return Json(null);
            }

        }

        public IActionResult MonthlyPetroChemicalAdd(int id = 0)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            objChemicalModel.C_Entry_By = profile.UserId.ToString();
            try
            {
                //

                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Action = 27;
                masterEF.C_Entry_By = Convert.ToString(profile.UserId);
                var chemicalProductGroups = _objIUnitModel.GetChemicalPetroProduct(masterEF);
                ViewBag.ViewChemicalGroupList = chemicalProductGroups.ToList().Select(c => new SelectListItem
                {
                    Text = c.PChemical_Product_Name,
                    Value = c.PChemical_Product_Code.ToString(),

                }).ToList();
                //
                //var chemicals = _objIUnitModel.GetChemicalProductByID(objChemicalModel);
              

                var finYear = _objIUnitModel.GetFinancialYear(onjMonthModel);
                ViewBag.ViewFinYEarList = finYear.ToList().Select(c => new SelectListItem
                {
                    Text = c.Fyear_Name,
                    Value = c.Fyear_Code.ToString(),

                }).ToList();

                var month = _objIUnitModel.GetMonth(onjMonthModel);
                ViewBag.ViewMonthList = month.ToList().Select(c => new SelectListItem
                {
                    Text = c.Month_Name,
                    Value = c.Month_Code.ToString(),

                }).ToList();
                var PhysicalState = _objIUnitModel.GetPhysicalState(onjMonthModel);
                ViewBag.ViewPhyStateList = PhysicalState.ToList().Select(c => new SelectListItem
                {
                    Text = c.PhysicalStateName,
                    Value = c.PhysicalStateID.ToString(),

                }).ToList();
                var PhysicalUnit = _objIUnitModel.GetPhysicalUnit(onjMonthModel);
                ViewBag.ViewPhyUnitList = PhysicalUnit.ToList().Select(c => new SelectListItem
                {
                    Text = c.UnitName,
                    Value = c.UnitID.ToString(),

                }).ToList();


            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                onjMonthModel = null;
            }



            if (id != 0)
            {
                onjMonthModel.M_Entry_By = Convert.ToInt32(profile.UserId);

                onjMonthModel.CategoryID = 1;
                objMessageModel = _objIUnitModel.CheckMonthlyChemicalEntry(onjMonthModel);
                if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Data already submitted.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    //  return RedirectToAction("MonthlyChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
                }
                //  onjMonthModel.Production_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);
                //ViewBag.Button = "Update";
                ViewBag.Button = "Submit";

                return View(onjMonthModel);
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(onjMonthModel);
            }
        }
        [HttpPost]
        public IActionResult MonthlyPetroChemicalAdd(MonthlyChemicalEF objmodel, string submit)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.M_Entry_By = Convert.ToInt32(profile.UserId);
                // objmodel.UserLoginId = profile.UserLoginId;
                //if (ModelState.IsValid)
                //{
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.M_IP_Address = localIpAddressString;

                objmodel.CategoryID = 2;
                objMessageModel = _objIUnitModel.AddMonthlyChemicalEntry(objmodel);
                // }
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("MonthlyPetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Data already submitted.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("MonthlyPetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("MonthlyPetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }

        }

        public IActionResult MonthlySubstancePetroChemicalAdd(int id = 0)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlySubstanceChemicalEF objMonthSubsModel = new MonthlySubstanceChemicalEF();
            MonthlySubstancePetroChemicalEF objMonthPetSubsModel = new MonthlySubstancePetroChemicalEF();
            try
            {
                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();

                var finYear = _objIUnitModel.GetFinancialYear(onjMonthModel);
                ViewBag.ViewFinYEarList = finYear.ToList().Select(c => new SelectListItem
                {
                    Text = c.Fyear_Name,
                    Value = c.Fyear_Code.ToString(),

                }).ToList();

                var month = _objIUnitModel.GetMonth(onjMonthModel);
                ViewBag.ViewMonthList = month.ToList().Select(c => new SelectListItem
                {
                    Text = c.Month_Name,
                    Value = c.Month_Code.ToString(),

                }).ToList();

                var whetherFrom = _objIUnitModel.GetWhetherFrom(objMonthSubsModel);
                ViewBag.ViewWhetherFromList = whetherFrom.ToList().Select(c => new SelectListItem
                {
                    Text = c.Whether_From_Name,
                    Value = c.Whether_From_ID.ToString(),

                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                objMonthPetSubsModel = null;
            }
            if (id != 0)
            {
                objMonthPetSubsModel.M_PC_Substances_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);
                ViewBag.Button = "Update";
                return View();
            }
            else
            {
                ViewBag.Button = "Submit";
                return View(objMonthPetSubsModel);
            }

        }
        [HttpPost]
        public IActionResult MonthlySubstancePetroChemicalAdd(MonthlySubstancePetroChemicalEF objmodel, string submit)
        {
            try
            { 
            // UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            objmodel.M_PC_Entry_By = "1";// profile.UserId;
                                         //  objmodel.UserLoginId = 1; //profile.UserLoginId;
                                         //if (ModelState.IsValid)
                                         //{
            IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
            string localIpAddressString = localIpAddress.ToString();
            objmodel.M_PC_IP_Address = localIpAddressString;


            objMessageModel = _objIUnitModel.AddMonthlySubstancePetroChemicalEntry(objmodel);
            // }
            if (objMessageModel.Satus == "1")
            {
                TempData["Messages"] = "Saved successfully.";
                // TempData.Keep("Message");
                // ViewBag.Message= "Saved successfully.";
                //TempData["Message"] = 1;
                return RedirectToAction("MonthlySubstancePetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });

            }
            else
            {
                TempData["Message"] = "Data not Saved.";
                // ViewBag.Message = "Data not Saved.";
                //TempData["Message"] = 2;
                return RedirectToAction("MonthlySubstancePetroChemicalAdd", "UnitRegistration", new { Area = "UnitRegistration" });
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
            try { 
            if (changePasswordEF.NewPassword != changePasswordEF.ConfirmNewPassword)
            {
                TempData["Message"] = "NewPassword and ConfirmNewPassword does not match";
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
            var ChangePassword = _objIUnitModel.ChangePassword(changePasswordEF);
            if (ChangePassword.Value == "1")
            {
                TempData["Message"] = ChangePassword.Msg;
                TempData.Keep("Message");
            }
            else
            {
                TempData["Message"] = ChangePassword.Msg;
                TempData.Keep("Message");
            }
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
        }

        public IActionResult Unit(Unit unit)
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            unit.Entry_By = Convert.ToString(profile.UserId);
            var obj = _objIUnitModel.Unit(unit);
            ViewBag.unitDetails = obj.ToList();
            //if(obj.ND_Name==null)
            //{

            //    TempData["Message"] = "Please complete your registration first!";
            //    TempData.Keep("Message");
            //    return RedirectToAction("UnitsEntryAdd");
            //}
            var firstObj = obj.FirstOrDefault();
            firstObj = null;
            return View(firstObj);
        }
        [HttpPost]
        public IActionResult Unit(Unit units, string? id,string? type)
        {


            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {
                UnitNodal UnitNodal = new UnitNodal();
                //delete id 
               
                if(!(id.IsNullOrEmpty()))
                    {
                    if (type.Equals("delete"))
                    {
                        UnitNodal.Action = 2;
                        //UnitNodal.Entry_By = profile.UserId;
                        UnitNodal.UserId = id;


                        objMessageModel = _objIUnitModel.UnitN(UnitNodal);
                        if (objMessageModel.Satus == "3")
                        {
                            TempData["Messages"] = "deleted Sucessfully !";
                            TempData.Keep("Messages");
                            return RedirectToAction("Unit", new { Id = objMessageModel.Value });
                        }
                        else
                        {
                            TempData["Message"] = "User id already exist !";
                            TempData.Keep("Message");
                            return RedirectToAction("Unit");
                        }

                    }

                }

                //end

            UnitNodal.Action =  1;
            UnitNodal.EmailId = units.ND_Email;
            UnitNodal.MobileNo = units.ND_Mobile_no;
            UnitNodal.NameOfUnit = units.ND_Name;
            UnitNodal.Entry_By = units.Entry_By;
            UnitNodal.Password = units.Password;
            UnitNodal.UserId = units.UserId;
            UnitNodal.AD_DOJ = units.AD_DOJ;
            UnitNodal.Address = units.AD_Address;

           UnitNodal.Entry_By = Convert.ToString(profile.UserId);
            if (units.Password != units.ConfirmPassword)
            {
                TempData["Message"] = "Password And Confirm Password Not Match";
                TempData.Keep("Message");
                return RedirectToAction("Unit");
            }

            //Unit unit=new Unit();          
            
            objMessageModel = _objIUnitModel.UnitN(UnitNodal);
            if (objMessageModel.Satus == "Not EXISTS")
            {
                TempData["Messages"] = "Data Saved Sucessfully !";
                TempData.Keep("Messages");
                return RedirectToAction("Unit", new { Id = objMessageModel.Value });
            }
            else
            {
                TempData["Message"] = "User id already exist !";
                TempData.Keep("Message");
                return RedirectToAction("Unit");
            }
        }
        catch(Exception ex)
            {
                  _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
        }

        public IActionResult UnitsEntryApproval()
        {
            UnitRegistrationEF objmodel = new UnitRegistrationEF();
            try
            {

                objmodel.Action = 1;
                objUnitList = _objIUnitModel.ViewUnitPendingList(objmodel);
                return View(objUnitList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          

            }
            finally
            {
                objUnitList = null;
                objmodel = null;
            }

        }

        [HttpPost]
        public IActionResult UnitApprovalByUnitAdmin(string UnitID, string Remarks)
        {
            try
            {
                UnitRegistrationViewEF objUnit = new UnitRegistrationViewEF();
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                objUnit.Remarks = Remarks;
                objUnit.ID = Convert.ToInt32(UnitID);
                objMessageModel = _objIUnitModel.UnitApprovalByUnitAdmin(objUnit);

                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Unit Approved Successfully.";
                    TempData.Keep("Messages");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApproval");
                }
                else
                {
                    TempData["Message"] = "Something went wrong.";
                    TempData.Keep("Message");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApproval");
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
                return Json(null);
            }

        }

        [HttpPost]
        public IActionResult UnitRejectByUnitAdmin(string UnitID, string Remarks)
        {
            try
            {
                UnitRegistrationViewEF objUnit = new UnitRegistrationViewEF();
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                objUnit.Remarks = Remarks;
                objUnit.ID = Convert.ToInt32(UnitID);
                objMessageModel = _objIUnitModel.UnitRejectByUnitAdmin(objUnit);

                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Unit Rejected Successfully.";
                    TempData.Keep("Messages");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApproval");
                }
                else
                {
                    TempData["Message"] = "Something went wrong.";
                    TempData.Keep("Message");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApproval");
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });                
            }

        }
        public IActionResult UnitsEntryApprovalByDept()
        {
            UnitRegistrationEF objmodel = new UnitRegistrationEF();
            try
            {

                objmodel.Action = 3;
                objUnitList = _objIUnitModel.ViewUnitPendingList(objmodel);
                return View(objUnitList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {
                objUnitList = null;
                objmodel = null;
            }

        }

        [HttpPost]
        public IActionResult UnitApprovalByDeptAdmin(string UnitID, string Remarks)
        {
            try
            {
                UnitRegistrationViewEF objUnit = new UnitRegistrationViewEF();
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                objUnit.Remarks = Remarks;
                objUnit.ID = Convert.ToInt32(UnitID);
                objMessageModel = _objIUnitModel.UnitApprovalByDeptAdmin(objUnit);

                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Unit Approved Successfully.";
                    TempData.Keep("Messages");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApprovalByDept");
                }
                else
                {
                    TempData["Message"] = "Something went wrong.";
                    TempData.Keep("Message");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApprovalByDept");
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }

        }

        [HttpPost]
        public IActionResult UnitRejectByDeptAdmin(string UnitID, string Remarks)
        {
            try
            {
                UnitRegistrationViewEF objUnit = new UnitRegistrationViewEF();
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                objUnit.Remarks = Remarks;
                objUnit.ID = Convert.ToInt32(UnitID);
                objMessageModel = _objIUnitModel.UnitRejectByDeptAdmin(objUnit);

                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Unit Rejected Successfully.";
                    TempData.Keep("Messages");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApprovalByDept");
                }
                else
                {
                    TempData["Message"] = "Something went wrong.";
                    TempData.Keep("Message");
                    // return View(objUnit);
                    return RedirectToAction("UnitsEntryApprovalByDept");
                }

            }
            catch (Exception ex)
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
                    var com = _objIUnitModel.UCompanyTagging(objCompanyModel);
                }
                ////list
                objCompanyModel.Action = 2;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                Unitlist = _objIUnitModel.UnitTaggingList(objCompanyModel);
                ////listend
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 2;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();
                ViewBag.Name_of_Unit = profile.UserName;
                ViewBag.UserName = profile.UserId;

            }

            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
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

            //MasterE objCompanyModel = new MasterE();
            objmodel.Entry_By = Convert.ToString(profile.UserId);
            objmodel.Unit_Code = Convert.ToString(profile.UserId);
            objMessageModel = _objIUnitModel.AddCompanyUnitTagging(objmodel);
            if (objMessageModel.Satus == "0")
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
             catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
        }

        public IActionResult CompnayProfileReport(int? State_Code, int? District_Code)
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
                else if (profile.RoleID == 2|| profile.RoleID==3)
                {

                    objmodel.Action = 2;
                    objmodel.ID = profile.UserId;
                    if ((State_Code == null || State_Code == 0) && (District_Code == null || District_Code == 0))
                    {
                        objmodel.State_Code = 0;
                        objmodel.District_Code = 0;
                    }
                    else
                    {
                        objmodel.State_Code = State_Code;
                        objmodel.District_Code = District_Code;
                    }
                }
                else if (profile.RoleID == 6)
                {
                    //uniadm

                    objmodel.Action = 3;
                    objmodel.ID = profile.UserId;
                    if ((State_Code == null || State_Code == 0) && (District_Code == null || District_Code == 0))
                    {
                        objmodel.State_Code = 0;
                        objmodel.District_Code = 0;
                    }
                    else
                    {
                        objmodel.State_Code = State_Code;
                        objmodel.District_Code = District_Code;
                    }
                }
                objCompanyList = _objIUnitModel.CompnayProfileReport(objmodel);
                string protocol = Request.Scheme;
                string host = Request.Host.Value;

                ViewBag.protocols = protocol;
                ViewBag.Host = host;
                //state data
                MasterEF objCompanyModel = new MasterEF();
                try
                {
                    objCompanyModel.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompanyModel);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Exception:::::: {ex}");
                    return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
                }
                finally
                {

                    objCompanyModel = null;
                }
                return View(objCompanyList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
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
        public IActionResult Report()
        {

            List<ReportViewEF> objCompanyList = new List<ReportViewEF>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {

                if (profile.RoleID == 6)
                {
                    objmodel.Action = 1;
                    objmodel.ID = profile.UserId;
                    objCompanyList = _objIUnitModel.ViewAdminReportList(objmodel);
                    //return View(objCompanyList);
                }
                else if (profile.RoleID == 7)
                {
                    objmodel.Action = 3;
                    objmodel.ID = profile.UserId;
                    objCompanyList = _objIUnitModel.ViewAdminReportList(objmodel);
                    //return View(objCompanyList);
                }  else if (profile.RoleID == 3)
                {
                    objmodel.Action = 4;
                    objmodel.ID = profile.UserId;
                    objCompanyList = _objIUnitModel.ViewAdminReportList(objmodel);
                    //return View(objCompanyList);
                }
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
        public IActionResult ReportDPTAPP(int? State_Code, int? District_Code, int? Company_Code)
        {

            List<ReportViewEF> objCompanyList = new List<ReportViewEF>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {

                if (profile.RoleID == 6)
                {
                    objmodel.Action = 1;

                    objCompanyList = _objIUnitModel.ViewAdminReportList(objmodel);
                    //return View(objCompanyList);
                }
                else if (profile.RoleID == 7)
                {
                    objmodel.Action = 3;

                    objCompanyList = _objIUnitModel.ViewAdminReportList(objmodel);
                    //return View(objCompanyList);
                }
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
        public IActionResult Profile()
        {
            List<UProfileEF> objCompanyList = new List<UProfileEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            UProfileEF objmodel = new UProfileEF();
            try
            {
                
                    objmodel.Action = 3;
                    objmodel.UserID = (int)profile.UserId;
                

                objCompanyList = _objIUnitModel.Profile(objmodel);
                string protocol = Request.Scheme;
                string host = Request.Host.Value;

                ViewBag.Protocol = protocol;
                ViewBag.Host = host;
                ViewBag.NameOfUnitCode = profile.UserName;
                ViewBag.Email = profile.Email_id;  
                
                if (objCompanyList.Count!=0)
                {
                    UProfileEF singleCompany = objCompanyList[0];
                    return View(new List<UProfileEF> { singleCompany });
                }
                else
                {
                    TempData["Message"] = "Please complete your registration first!";
                    TempData.Keep("Message");
                    return RedirectToAction("UnitsEntryAdd");
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
        public IActionResult NProfile()
        {
            List<NProfileEF> objCompanyList = new List<NProfileEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            NProfileEF objmodel = new NProfileEF();
            try
            {
                objmodel.Action = 2;
                if (profile.RoleID==7)
                {
                    objmodel.Action = 4;
                }
                 
                    objmodel.UserID = (int)profile.UserId;
              

                    objCompanyList = _objIUnitModel.NProfile(objmodel);
                    string protocol = Request.Scheme;
                    string host = Request.Host.Value;

                    ViewBag.Protocol = protocol;
                    ViewBag.Host = host;
                    ViewBag.NameOfUnitCode = profile.UserName;
                    ViewBag.Email = profile.Email_id;
                
                if(objCompanyList != null)
                {
                    NProfileEF singleCompany = objCompanyList[0];
                    return View(new List<NProfileEF> { singleCompany });
                }
                else
                {
                    TempData["Message"] = "Profile not found ";
                    return RedirectToAction("UnitOPTDashboard");
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
        public IActionResult BestPractises()
        {
            List<BestPracticesQuEF> objquestionList = new List<BestPracticesQuEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                for (int i = 1; i <= 10; i++) {
                    objmodel.ID = i;
 
                  objquestionList = _objIUnitModel.BestPractisesQa(objmodel);

    ViewData["ViewBestPracticesQu" + i] = objquestionList;

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

                objquestionList = null;
            }
            return View();
        }
        [HttpPost]
        public IActionResult BestPractises(BestPracticesEF objmodel)
        {
            try
            {
                List<BestPracticesQuEF> objquestionList = new List<BestPracticesQuEF>();
                DashboardRequestEF objmodelreq = new DashboardRequestEF();

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);


                objmodel.Action = 1;
                objmodel.Entry_By = Convert.ToString(profile.UserId);
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.IP_Address = localIpAddressString;
                objMessageModel = _objIUnitModel.BestPractices(objmodel);
                //get quetsion

                for (int i = 1; i <= 10; i++)
                {
                    objmodelreq.ID = i;

                    objquestionList = _objIUnitModel.BestPractisesQa(objmodelreq);

                    ViewData["ViewBestPracticesQu" + i] = objquestionList;

                }
//
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Saved Successfully.";
                    TempData.Keep("Messages");
                   
                    return View();
                }
                else
                {
                    TempData["Message"] = "Something went wrong.";
                    TempData.Keep("Message");
                    // return View(objUnit);
                    return View();
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }

            return View();
        }
        [HttpPost]
        public IActionResult GetOptReportDetails(int rowid)
        {
            DashboardRequestEF objmodel = new DashboardRequestEF();
           List<MonthtlySubMainEF>  objresult = new List<MonthtlySubMainEF>();
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);


                objmodel.Action = 3;
                objmodel.ID = rowid;

                objresult = _objIUnitModel.GetOptReportDetails(objmodel);

               

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            return Json(objresult);
        }
        [HttpPost]
        public IActionResult GetYearlyReportDetails(int rowid)
        {
            DashboardRequestEF objmodel = new DashboardRequestEF();
            List<YearlySubMainEF> objresult = new List<YearlySubMainEF>();
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);


                objmodel.Action = 3;
                objmodel.ID = rowid;

                objresult = _objIUnitModel.GetYearlyReportDetails(objmodel);



            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}"); 
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            return Json(objresult);
        }

        public IActionResult ChemicalGroup()
        {
            List<MonthlyChemicalEF> objmodel = new List<MonthlyChemicalEF>();
            MonthlyChemicalEF objreq = new MonthlyChemicalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.ChemicalGroupList(objreq);
               ViewBag.chemicalsList = objmodel;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }
        [HttpPost]
        public IActionResult ChemicalGroup(MonthlyChemicalEF objmodel)
        {
            MonthlyChemicalEF objreq = new MonthlyChemicalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(objmodel.C_Name))
            {
                objmodel.Chemical_Name = objmodel.Chemical_Name;

                // objmodel.Chemical_Code = Convert.ToString(profile.UserId);
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.ChemicalGroup(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    ViewBag.msg = "Data Save Sucessfully";
                }
                else
                {
                    ViewBag.msg = "Data not Save Sucessfully";
                   
                }
            }else
            {
                objmodel.C_Name = objmodel.C_Name;

                objmodel.Chemical_Code = objmodel.Chemical_Code;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.ChemicalGroup(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Update Sucessfully";
                    TempData.Keep("Messages");
                }
                else
                {
                    TempData["Messages"] = "Failed to update";
                    TempData.Keep("Messages");
                }

            }
            objreq.Action = 1;
           var objmodellist = _objIUnitModel.ChemicalGroupList(objreq);
            ViewBag.chemicalsList = objmodellist;

            return View("ChemicalGroup");
        }

        public IActionResult GetChemicalGroup()
      {
            List<MonthlyChemicalEF> objmodel = new List<MonthlyChemicalEF>();
            MonthlyChemicalEF objreq = new MonthlyChemicalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.ChemicalGroupList(objreq);
              return View(objmodel);
            }
          catch (Exception ex)
            {
               _logger.LogCritical($"Exception:::::: {ex}");throw ex;
            }
          finally
            {
                objmodel = null;
                
            }

      }


        public IActionResult BestPractisesQa()
        {
            MasterE objCompanyModel = new MasterE();
            List<BestPracticesQuEF> objquestionList = new List<BestPracticesQuEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                objCompanyModel.Action = 1;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);

                       
                objquestionList = _objIUnitModel.BestPractisesQa(objmodel);

                ViewBag.ViewBestPractices = objquestionList;


                return View();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");  
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
            finally
            {

                objquestionList = null;
            }
            return View();
        }

        public IActionResult DptadmSubgroup(string Chemical_Code, string Chemical_Product_Code, int actionid, int id = 0)
        {
            MasterSubgroup objChemicalModel = new MasterSubgroup();
            List<Subgroup> ShowSubgrouplist = new List<Subgroup>();
            try
            {


                objChemicalModel.Action = 4;
                var chemicals = _objIUnitModel.GetChemical(objChemicalModel);
                ViewBag.ViewChemicalList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                ////list
                objChemicalModel.Action = 1;

                ShowSubgrouplist = _objIUnitModel.Subgroup(objChemicalModel);
                ////listend


            }
            catch (Exception ex)
            {
               
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                objChemicalModel = null;
            }




            ViewBag.ShowSubgrouplist = ShowSubgrouplist;
            ViewBag.Button = "Submit";
            return View(objChemicalModel);


        }
        [HttpPost]
        public IActionResult DptadmSubgroup(MasterSubgroup objmodel, string Chemical_Code, string Chemical_Product_Name, string Chemical_Code1, string submit)
        {
            try
            {
                if (string.IsNullOrEmpty(objmodel.Chemical_Product_Code))
                {
                    objmodel.Action = 2;
                    objmodel.Chemical_Code = Chemical_Code;
                    objmodel.Chemical_Product_Code = "12345";
                    objmodel.Chemical_Product_Name = Chemical_Product_Name;
                    objMessageModel = _objIUnitModel.Addsubgroup(objmodel);
                    if (objMessageModel.Satus == "0")
                    {
                        TempData["Messages"] = "Data Save Sucessfully";
                        TempData.Keep("Messages");                        
                        return RedirectToAction("DPTADMSUBGROUP");
                    }
                    else
                    {
                        TempData["Messages"] = "Data not Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("DPTADMSUBGROUP");
                    }

                }
                else
                {
                    objmodel.Action = 3;
                    objmodel.Chemical_Code = Chemical_Code1;
                    objmodel.Chemical_Product_Code = objmodel.Chemical_Product_Code;
                    objmodel.Chemical_Product_Name = objmodel.NewChemical_Product_Name;
                    objMessageModel = _objIUnitModel.Usubgroup(objmodel);
                    if (objMessageModel.Satus == "1")
                    {
                        TempData["Messages"] = "Data Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("DPTADMSUBGROUP");
                    }
                    else
                    {
                        TempData["Messages"] = "Data not Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("DPTADMSUBGROUP");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
        }
        public IActionResult BestPractisesReports()
        {
            MasterE objCompanyModel = new MasterE();
            List<BestPracticesEF> objCompanyList = new List<BestPracticesEF>();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            DashboardRequestEF objmodel = new DashboardRequestEF();
            try
            {

                objCompanyModel.Action = 6;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);

                var Unit = _objIUnitModel.UnitList(objCompanyModel);

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


                objCompanyList = _objIUnitModel.BestPractisesReports(objmodel);

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

                objCompanyModel.Action = 6;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);

                var Unit = _objIUnitModel.UnitList(objCompanyModel);

                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                    objmodel.Action = 1;
                    objmodel.ID = Unit_Code;
                    //objmodel.ID = 101;
                


                objCompanyList = _objIUnitModel.BestPractisesReports(objmodel);
                if (objCompanyList != null)
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


        public IActionResult HclassifiSub(int actionid, int id = 0)
        {
            MasterHcsn objHCSNModel = new MasterHcsn();
            List<Hcsn> ShowHCSNlist = new List<Hcsn>();
            try
            {                
                ////list
                objHCSNModel.Action = 1;
                ShowHCSNlist = _objIUnitModel.Hcsn(objHCSNModel);
                ////listend


            }
            catch (Exception ex)
            {
            
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                objHCSNModel = null;
            }

            ViewBag.ShowHCSNlist = ShowHCSNlist;
            ViewBag.Button = "Submit";
            return View(objHCSNModel);

        }

        [HttpPost]
        public IActionResult HclassifiSub(MasterHcsn objHCSNModel, string Hazard_Classification_Name, string HazardClassificationID, string HazardClassificationID1,string submit)
        {
            try
            {
                if (string.IsNullOrEmpty(objHCSNModel.Hazard_Classification_ID))
                {
                    objHCSNModel.Action = 2;
                    objHCSNModel.Hazard_Classification_Name = Hazard_Classification_Name;
                    objMessageModel = _objIUnitModel.AddHcsn(objHCSNModel);
                    if (objMessageModel.Satus == "0")
                    {
                        TempData["Messages"] = "Data Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("HclassifiSub");
                    }
                    else
                    {
                        TempData["Messages"] = "Data not Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("HclassifiSub");
                    }

                }
                else
                {
                    objHCSNModel.Action = 3;
                    
                    objHCSNModel.Hazard_Classification_Name = objHCSNModel.NewHazard_Classification_Name;
                    objMessageModel = _objIUnitModel.UHcsn(objHCSNModel);
                    if (objMessageModel.Satus == "1")
                    {
                        TempData["Messages"] = "Data Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("HclassifiSub");
                    }
                    else
                    {
                        TempData["Messages"] = "Data not Save Sucessfully";
                        TempData.Keep("Messages");
                        return RedirectToAction("HclassifiSub");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });          
            }
        }
        public IActionResult ChemicalSubgroupReport()
        {
            MasterSubgroup objChemicalModel = new MasterSubgroup();
            List<Subgroup> ShowSubgrouplist = new List<Subgroup>();
            try
            {
                objChemicalModel.Action = 4;
                var chemicals = _objIUnitModel.GetChemical(objChemicalModel);
                ViewBag.ViewChemicalList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                objChemicalModel.Action = 1;
                 ShowSubgrouplist = _objIUnitModel.Subgroup(objChemicalModel);

                return View(ShowSubgrouplist);
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                ShowSubgrouplist = null;
            }

        }
        [HttpPost]
        public IActionResult ChemicalSubgroupReport(string Chemical_Code, string Chemical_Product_Code)
        {
            MasterSubgroup objChemicalModel = new MasterSubgroup();
            List<Subgroup> ShowSubgrouplist = new List<Subgroup>();
            try
            {
                objChemicalModel.Action = 4;
                var chemicals = _objIUnitModel.GetChemical(objChemicalModel);
                ViewBag.ViewChemicalList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                objChemicalModel.Action = 4;
                objChemicalModel.Chemical_Code = Chemical_Code;
                objChemicalModel.Chemical_Product_Code = Chemical_Product_Code;
                ShowSubgrouplist = _objIUnitModel.Subgroup(objChemicalModel);

                return View(ShowSubgrouplist);
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                ShowSubgrouplist = null;
            }

        }
        public IActionResult ChemicalgroupReport()
        {
            MasterSubgroup objChemicalModel = new MasterSubgroup();
            List<MonthlyChemicalEF> objmodel = new List<MonthlyChemicalEF>();
            MonthlyChemicalEF objreq = new MonthlyChemicalEF();
            try
            {
                objreq.Action = 1;
                objmodel = _objIUnitModel.ChemicalGroupList(objreq);
                ViewBag.chemicalsList = objmodel;
                objChemicalModel.Action = 4;
                var chemicals = _objIUnitModel.GetChemical(objChemicalModel);
                ViewBag.ViewChemicalList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                return View(objmodel);
            }
            catch (Exception ex)
            {
            
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                objmodel = null;
            }

        }
        [HttpPost]
        public IActionResult ChemicalgroupReport(string Chemical_Code)
        {
            MasterSubgroup objChemicalModel = new MasterSubgroup();
            List<MonthlyChemicalEF> objmodel = new List<MonthlyChemicalEF>();
            MonthlyChemicalEF objreq = new MonthlyChemicalEF();
            try
            {
                objChemicalModel.Action = 4;
                var chemicals = _objIUnitModel.GetChemical(objChemicalModel);
                ViewBag.ViewChemicalList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                objreq.Action = 4;
                objreq.Chemical_Code = Chemical_Code;
                objmodel = _objIUnitModel.ChemicalGroupList(objreq);
                ViewBag.chemicalsList = objmodel;
                return View(objmodel);
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                objmodel = null;
            }

        }

        public IActionResult PetroChemical()
        {
            List<MonthlyPetroChemicalEF> objmodel = new List<MonthlyPetroChemicalEF>();
            MonthlyPetroChemicalEF objreq = new MonthlyPetroChemicalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.PetroChemicalList(objreq);
                ViewBag.PetroChemicalList = objmodel;
            }
            catch (Exception ex)
            {
      
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }

        [HttpPost]
        public IActionResult PetroChemical(MonthlyPetroChemicalEF objmodel)
        {
            MonthlyPetroChemicalEF objreq = new MonthlyPetroChemicalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(objmodel.PC_Name))
            {
                objmodel.PChemical_Name = objmodel.PChemical_Name;
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.PetroChemical(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");
                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");
                }
            }
            else
            {
                objmodel.PC_Name = objmodel.PC_Name;

                objmodel.PChemical_Code = objmodel.PChemical_Code;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.PetroChemical(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Update Sucessfully";
                    TempData.Keep("Messages");
                }
                else
                {
                    TempData["Messages"] = "Failed to update";
                    TempData.Keep("Messages");
                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.PetroChemicalList(objreq);
            ViewBag.PetroChemicalList = objmodellist;

            return View("PetroChemical");
        }

        public IActionResult GetPetroChemical()
        {
            List<MonthlyPetroChemicalEF> objmodel = new List<MonthlyPetroChemicalEF>();
            MonthlyPetroChemicalEF objreq = new MonthlyPetroChemicalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.PetroChemicalList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }


        public IActionResult EnvironmentalExposure()
        {
            List<EnvironmentalEF> objmodel = new List<EnvironmentalEF>();
            EnvironmentalEF objreq = new EnvironmentalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.EnvironmentalExposureList(objreq);
                ViewBag.EnvironmentalExposureList = objmodel;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }

        [HttpPost]
        public IActionResult EnvironmentalExposure(EnvironmentalEF objmodel)
        {
            EnvironmentalEF objreq = new EnvironmentalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Environmental_Exposure_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.EnvironmentalExposure(objmodel);
                if (objMessageModel.Satus == "1")
                {

                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");
                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");
                }
            }
            else
            {
                objmodel.Environmental_Exposure_ID = objmodel.Environmental_Exposure_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.EnvironmentalExposure(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.EnvironmentalExposureList(objreq);
            ViewBag.EnvironmentalExposureList = objmodellist;

            return View("EnvironmentalExposure");
        }

        public IActionResult GetEnvironmentalExposure()
        {
            List<EnvironmentalEF> objmodel = new List<EnvironmentalEF>();
            EnvironmentalEF objreq = new EnvironmentalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.EnvironmentalExposureList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }

        public IActionResult MainUseCategory()
        {
            List<MainUseCategoryEF> objmodel = new List<MainUseCategoryEF>();
            MainUseCategoryEF objreq = new MainUseCategoryEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.MainUseCategoryList(objreq);
                ViewBag.MainUseCategoryList = objmodel;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }

        [HttpPost]
        public IActionResult MainUseCategory(MainUseCategoryEF objmodel)
        {
            MainUseCategoryEF objreq = new MainUseCategoryEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Main_Use_Category_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.MainUseCategory(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Main_Use_Category_ID = objmodel.Main_Use_Category_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.MainUseCategory(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.MainUseCategoryList(objreq);
            ViewBag.MainUseCategoryList = objmodellist;

            return View("MainUseCategory");
        }

        public IActionResult GetMainUseCategory()
        {
            List<MainUseCategoryEF> objmodel = new List<MainUseCategoryEF>();
            MainUseCategoryEF objreq = new MainUseCategoryEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.MainUseCategoryList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }

        public IActionResult PatternExposure()
        {

            List<PatternExposureEF> objmodel = new List<PatternExposureEF>();
            PatternExposureEF objreq = new PatternExposureEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.PatternExposureList(objreq);
                ViewBag.PatternExposureList = objmodel;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }

        [HttpPost]
        public IActionResult PatternExposure(PatternExposureEF objmodel)
        {
            PatternExposureEF objreq = new PatternExposureEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Pattern_Exposure_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.PatternExposure(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Pattern_Exposure_ID = objmodel.Pattern_Exposure_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.PatternExposure(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }

            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.PatternExposureList(objreq);
            ViewBag.PatternExposureList = objmodellist;

            return View("PatternExposure");
        }

        public IActionResult GetPatternExposure()
        {
            List<PatternExposureEF> objmodel = new List<PatternExposureEF>();
            PatternExposureEF objreq = new PatternExposureEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.PatternExposureList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }

        public IActionResult ResultingHazardSubstance()
        {
            List<ResultingHazardSubstanceEF> objmodel = new List<ResultingHazardSubstanceEF>();
            ResultingHazardSubstanceEF objreq = new ResultingHazardSubstanceEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.ResultingHazardSubstanceList(objreq);
                ViewBag.ResultingHazardSubstanceList = objmodel;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }

        [HttpPost]
        public IActionResult ResultingHazardSubstance(ResultingHazardSubstanceEF objmodel)
        {
            ResultingHazardSubstanceEF objreq = new ResultingHazardSubstanceEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Resulting_Hazard_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.ResultingHazardSubstance(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Resulting_Hazard_ID = objmodel.Resulting_Hazard_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.ResultingHazardSubstance(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.ResultingHazardSubstanceList(objreq);
            ViewBag.ResultingHazardSubstanceList = objmodellist;

            return View("ResultingHazardSubstance");
        }


        public IActionResult GetResultingHazardSubstance()
        {
            List<ResultingHazardSubstanceEF> objmodel = new List<ResultingHazardSubstanceEF>();
            ResultingHazardSubstanceEF objreq = new ResultingHazardSubstanceEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.ResultingHazardSubstanceList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }


        public IActionResult SignificantRouteExposure()
        {
            List<SignificantRouteExposureEF> objmodel = new List<SignificantRouteExposureEF>();
            SignificantRouteExposureEF objreq = new SignificantRouteExposureEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SignificantRouteExposureList(objreq);
                ViewBag.SignificantRouteExposureList = objmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();

        }

        [HttpPost]
        public IActionResult SignificantRouteExposure(SignificantRouteExposureEF objmodel)
        {
            SignificantRouteExposureEF objreq = new SignificantRouteExposureEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Significant_Route_Exposure_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.SignificantRouteExposure(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Significant_Route_Exposure_ID = objmodel.Significant_Route_Exposure_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.SignificantRouteExposure(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.SignificantRouteExposureList(objreq);
            ViewBag.SignificantRouteExposureList = objmodellist;

            return View("SignificantRouteExposure");
        }

        public IActionResult GetSignificantRouteExposure()
        {
            List<SignificantRouteExposureEF> objmodel = new List<SignificantRouteExposureEF>();
            SignificantRouteExposureEF objreq = new SignificantRouteExposureEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SignificantRouteExposureList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }


        public IActionResult SpecificationIndustrialProfessional()
        {
            List<SpecificationIndustrialProfessionalEF> objmodel = new List<SpecificationIndustrialProfessionalEF>();
            SpecificationIndustrialProfessionalEF objreq = new SpecificationIndustrialProfessionalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SpecificationIndustrialProfessionalList(objreq);
                ViewBag.SpecificationIndustrialProfessionalList = objmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();

        }

        [HttpPost]
        public IActionResult SpecificationIndustrialProfessional(SpecificationIndustrialProfessionalEF objmodel)
        {
            SpecificationIndustrialProfessionalEF objreq = new SpecificationIndustrialProfessionalEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Specification_Industrial_Professional_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.SpecificationIndustrialProfessional(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Specification_Industrial_Professional_ID = objmodel.Specification_Industrial_Professional_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.SpecificationIndustrialProfessional(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Update Sucessfully";
                    TempData.Keep("Messages");
                }
                else
                {
                    TempData["Messages"] = "Failed to update";
                    TempData.Keep("Messages");
                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.SpecificationIndustrialProfessionalList(objreq);
            ViewBag.SpecificationIndustrialProfessionalList = objmodellist;

            return View("SpecificationIndustrialProfessional");
        }

        public IActionResult GetSpecificationIndustrialProfessional()
        {
            List<SpecificationIndustrialProfessionalEF> objmodel = new List<SpecificationIndustrialProfessionalEF>();
            SpecificationIndustrialProfessionalEF objreq = new SpecificationIndustrialProfessionalEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SpecificationIndustrialProfessionalList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }


        public IActionResult SubstanceDetail()
        {
            List<SubstanceDetailEF> objmodel = new List<SubstanceDetailEF>();
            SubstanceDetailEF objreq = new SubstanceDetailEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SubstanceDetailList(objreq);
                ViewBag.SubstanceDetailList = objmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();

        }

        [HttpPost]
        public IActionResult SubstanceDetail(SubstanceDetailEF objmodel)
        {
            SubstanceDetailEF objreq = new SubstanceDetailEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Substance_Detail_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.SubstanceDetail(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Substance_Detail_ID = objmodel.Substance_Detail_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.SubstanceDetail(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.SubstanceDetailList(objreq);
            ViewBag.SubstanceDetailList = objmodellist;

            return View("SubstanceDetail");
        }

        public IActionResult GetSubstanceDetail()
        {
            List<SubstanceDetailEF> objmodel = new List<SubstanceDetailEF>();
            SubstanceDetailEF objreq = new SubstanceDetailEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SubstanceDetailList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }

        public IActionResult SubstancesWhetherFrom()
        {
            List<SubstancesWhetherFromEF> objmodel = new List<SubstancesWhetherFromEF>();
            SubstancesWhetherFromEF objreq = new SubstancesWhetherFromEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SubstancesWhetherFromList(objreq);
                ViewBag.SubstancesWhetherFromList = objmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }
            return View();
        }

        [HttpPost]
        public IActionResult SubstancesWhetherFrom(SubstancesWhetherFromEF objmodel)
        {
            SubstancesWhetherFromEF objreq = new SubstancesWhetherFromEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            if (string.IsNullOrEmpty(Convert.ToString(objmodel.Whether_From_ID)))

            {
                objmodel.Action = 2;
                objMessageModel = _objIUnitModel.SubstancesWhetherFrom(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }
            }
            else
            {
                objmodel.Whether_From_ID = objmodel.Whether_From_ID;
                objmodel.Action = 3;
                objMessageModel = _objIUnitModel.SubstancesWhetherFrom(objmodel);
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Data Save Sucessfully";
                    TempData.Keep("Messages");

                }
                else
                {
                    TempData["Messages"] = "Data not Save Sucessfully";
                    TempData.Keep("Messages");

                }


            }
            objreq.Action = 1;
            var objmodellist = _objIUnitModel.SubstancesWhetherFromList(objreq);
            ViewBag.SubstancesWhetherFromList = objmodellist;

            return View("SubstancesWhetherFrom");
        }

        public IActionResult GetSubstancesWhetherFrom()
        {
            List<SubstancesWhetherFromEF> objmodel = new List<SubstancesWhetherFromEF>();
            SubstancesWhetherFromEF objreq = new SubstancesWhetherFromEF();
            try
            {

                objreq.Action = 1;
                objmodel = _objIUnitModel.SubstancesWhetherFromList(objreq);
                return View(objmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objmodel = null;

            }

        }


        public IActionResult EnvironmentalExposureReport()
        {
            List<EnvironExposReport> objmodel = new List<EnvironExposReport>();
            EnvironExposReport objreq = new EnvironExposReport();
            try
            {
                objreq.Action = 1;
                objmodel = _objIUnitModel.EnvironmExpReportList(objreq);
                ViewBag.EnvironmList = objmodel;
                objreq.Action = 1;
                var chemicals = _objIUnitModel.EnvironmExpReportList(objreq);
                ViewBag.EnvironmList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Environmental_Exposure_Name,
                    Value = c.Environmental_Exposure_ID.ToString(),

                }).ToList();
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                objmodel = null;
            }

        }
        [HttpPost]
        public IActionResult EnvironmentalExposureReport(int Environmental_Exposure_ID)
        {
            List<EnvironExposReport> objmodel = new List<EnvironExposReport>();
            EnvironExposReport objreq = new EnvironExposReport();
            try
            {
                objreq.Action = 4;
                objreq.Environmental_Exposure_ID = Environmental_Exposure_ID;
                objmodel = _objIUnitModel.EnvironmExpReportList(objreq);
                ViewBag.EnvironmList = objmodel;
                objreq.Action = 1;
                var chemicals = _objIUnitModel.EnvironmExpReportList(objreq);
                ViewBag.EnvironmList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Environmental_Exposure_Name,
                    Value = c.Environmental_Exposure_ID.ToString(),

                }).ToList();
                return View(objmodel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }
            finally
            {

                objmodel = null;
            }

        }
        public IActionResult HazardClassificationSubstanceReport()
        {
            MasterHcsn objHCSNModel = new MasterHcsn();
            List<Hcsn> ShowHCSNlist = new List<Hcsn>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                ShowHCSNlist = _objIUnitModel.Hcsn(objHCSNModel);                
                ////listend
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.Hcsn(objHCSNModel);
                ViewBag.ShowHCSNSlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Hazard_Classification_Name,
                    Value = c.Hazard_Classification_ID.ToString(),

                }).ToList();
                ViewBag.ShowHCSNlist = ShowHCSNlist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult HazardClassificationSubstanceReport(int Hazard_Classification_ID)
        {
            MasterHcsn objHCSNModel = new MasterHcsn();
            List<Hcsn> ShowHCSNlist = new List<Hcsn>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Hazard_Classification_ID = Hazard_Classification_ID.ToString();
                ShowHCSNlist = _objIUnitModel.Hcsn(objHCSNModel);
                ViewBag.ShowHCSNlist = ShowHCSNlist;
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.Hcsn(objHCSNModel);
                ViewBag.ShowHCSNSlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Hazard_Classification_Name,
                    Value = c.Hazard_Classification_ID,

                }).ToList();
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        public IActionResult MainUseCategoryReport()
        {
            MainUseCategoryReport objHCSNModel = new MainUseCategoryReport();
            List<MainUseCategoryReport> ShowMainUseCategorylist = new List<MainUseCategoryReport>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                ShowMainUseCategorylist = _objIUnitModel.MainUseCategoryReportList(objHCSNModel);
                ////listend
                 objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.MainUseCategoryReportList(objHCSNModel);
                ViewBag.ShowMainCategorylist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Main_Use_Category_Name,
                    Value = c.Main_Use_Category_ID.ToString(),

                }).ToList();
                ViewBag.ShowMainUseCategorylist = ShowMainUseCategorylist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult MainUseCategoryReport(int Main_Use_Category_ID)
        {
            MainUseCategoryReport objHCSNModel = new MainUseCategoryReport();
            List<MainUseCategoryReport> ShowMainUseCategorylist = new List<MainUseCategoryReport>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Main_Use_Category_ID = Main_Use_Category_ID;
                ShowMainUseCategorylist = _objIUnitModel.MainUseCategoryReportList(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.MainUseCategoryReportList(objHCSNModel);
                ViewBag.ShowMainCategorylist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Main_Use_Category_Name,
                    Value = c.Main_Use_Category_ID.ToString(),

                }).ToList();               
                ViewBag.ShowMainUseCategorylist = ShowMainUseCategorylist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        public IActionResult PatternExposureReport()
        {
            PatternExposureReport objHCSNModel = new PatternExposureReport();
            List<PatternExposureReport> ShowPatternExposurelist = new List<PatternExposureReport>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                ShowPatternExposurelist = _objIUnitModel.PatternExposureReportList(objHCSNModel);
                ////listend
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.PatternExposureReportList(objHCSNModel);
                ViewBag.ShowPatternlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Pattern_Exposure_Name,
                    Value = c.Pattern_Exposure_ID.ToString(),

                }).ToList();
                ViewBag.ShowPatternExposurelist = ShowPatternExposurelist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult PatternExposureReport(int Pattern_Exposure_ID)
        {
            PatternExposureReport objHCSNModel = new PatternExposureReport();
            List<PatternExposureReport> ShowPatternExposurelist = new List<PatternExposureReport>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Pattern_Exposure_ID = Pattern_Exposure_ID;
                ShowPatternExposurelist = _objIUnitModel.PatternExposureReportList(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.PatternExposureReportList(objHCSNModel);
                ViewBag.ShowPatternlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Pattern_Exposure_Name,
                    Value = c.Pattern_Exposure_ID.ToString(),

                }).ToList();
                ViewBag.ShowPatternExposurelist = ShowPatternExposurelist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        public IActionResult ResultingHazardSubstanceReport()
        {
            ResultingHazardSubstanceReport objHCSNModel = new ResultingHazardSubstanceReport();
            List<ResultingHazardSubstanceReport> ResultingHazardSubstancelist = new List<ResultingHazardSubstanceReport>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                ResultingHazardSubstancelist = _objIUnitModel.ResultingHazardSubstanceReportList(objHCSNModel);
                ////listend
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.ResultingHazardSubstanceReportList(objHCSNModel);
                ViewBag.ResultingHazardlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Resulting_Hazard_Name,
                    Value = c.Resulting_Hazard_ID.ToString(),

                }).ToList();
                ViewBag.ResultingHazardSubstancelist = ResultingHazardSubstancelist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult ResultingHazardSubstanceReport(int Resulting_Hazard_ID)
        {
            ResultingHazardSubstanceReport objHCSNModel = new ResultingHazardSubstanceReport();
            List<ResultingHazardSubstanceReport> ResultingHazardSubstancelist = new List<ResultingHazardSubstanceReport>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Resulting_Hazard_ID = Resulting_Hazard_ID;
                ResultingHazardSubstancelist = _objIUnitModel.ResultingHazardSubstanceReportList(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.ResultingHazardSubstanceReportList(objHCSNModel);
                ViewBag.ResultingHazardlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Resulting_Hazard_Name,
                    Value = c.Resulting_Hazard_ID.ToString(),

                }).ToList();
                ViewBag.ResultingHazardSubstancelist = ResultingHazardSubstancelist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }

        public IActionResult SignificantRouteExposureReport()
        {
            SignificantRouteExposureReport objHCSNModel = new SignificantRouteExposureReport();
            List<SignificantRouteExposureReport> SignificantRouteExposurelist = new List<SignificantRouteExposureReport>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                SignificantRouteExposurelist = _objIUnitModel.SignificantRouteExposureReportList(objHCSNModel);
                ////listend
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SignificantRouteExposureReportList(objHCSNModel);
                ViewBag.SignificantRouteExposurelist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Significant_Route_Exposure_Name,
                    Value = c.Significant_Route_Exposure_ID.ToString(),

                }).ToList();
                ViewBag.ResultingHazardSubstancelist = SignificantRouteExposurelist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult SignificantRouteExposureReport(int Significant_Route_Exposure_ID)
        {
            SignificantRouteExposureReport objHCSNModel = new SignificantRouteExposureReport();
            List<SignificantRouteExposureReport> SignificantRouteExposurelist = new List<SignificantRouteExposureReport>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Significant_Route_Exposure_ID = Significant_Route_Exposure_ID;
                SignificantRouteExposurelist = _objIUnitModel.SignificantRouteExposureReportList(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SignificantRouteExposureReportList(objHCSNModel);
                ViewBag.SignificantRouteExposurelist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Significant_Route_Exposure_Name,
                    Value = c.Significant_Route_Exposure_ID.ToString(),

                }).ToList();
                ViewBag.ResultingHazardSubstancelist = SignificantRouteExposurelist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        public IActionResult SpecificationIndustrialProfessionalRe()
        {
            SpecificationIndustrialProfessionalRe objHCSNModel = new SpecificationIndustrialProfessionalRe();
            List<SpecificationIndustrialProfessionalRe> SpecificationIndustrialProfessionallist = new List<SpecificationIndustrialProfessionalRe>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                SpecificationIndustrialProfessionallist = _objIUnitModel.SpecificationIndustrialProfessionallist(objHCSNModel);
                ////listend
                 objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SpecificationIndustrialProfessionallist(objHCSNModel);
                ViewBag.SpecificationIndustriallist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Specification_Industrial_Professional_Name,
                    Value = c.Specification_Industrial_Professional_ID.ToString(),

                }).ToList();
                ViewBag.SpecificationIndustrialProfessionallist = SpecificationIndustrialProfessionallist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult SpecificationIndustrialProfessionalRe(int Specification_Industrial_Professional_ID)
        {
            SpecificationIndustrialProfessionalRe objHCSNModel = new SpecificationIndustrialProfessionalRe();
            List<SpecificationIndustrialProfessionalRe> SpecificationIndustrialProfessionallist = new List<SpecificationIndustrialProfessionalRe>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Specification_Industrial_Professional_ID = Specification_Industrial_Professional_ID;
                SpecificationIndustrialProfessionallist = _objIUnitModel.SpecificationIndustrialProfessionallist(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SpecificationIndustrialProfessionallist(objHCSNModel);
                ViewBag.SpecificationIndustriallist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Specification_Industrial_Professional_Name,
                    Value = c.Specification_Industrial_Professional_ID.ToString(),

                }).ToList();
                ViewBag.SpecificationIndustrialProfessionallist = SpecificationIndustrialProfessionallist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        public IActionResult SubstanceDetailReport()
        {
            SubstanceDetailReport objHCSNModel = new SubstanceDetailReport();
            List<SubstanceDetailReport> SubstanceDetailReportlist = new List<SubstanceDetailReport>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                SubstanceDetailReportlist = _objIUnitModel.SubstanceDetailReportlist(objHCSNModel);
                ////listend
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SubstanceDetailReportlist(objHCSNModel);
                ViewBag.SubstanceDetaillist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Substance_Detail_Name,
                    Value = c.Substance_Detail_ID.ToString(),

                }).ToList();
                ViewBag.SubstanceDetailReportlist = SubstanceDetailReportlist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult SubstanceDetailReport(int Substance_Detail_ID)
        {
            SubstanceDetailReport objHCSNModel = new SubstanceDetailReport();
            List<SubstanceDetailReport> SubstanceDetailReportlist = new List<SubstanceDetailReport>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Substance_Detail_ID = Substance_Detail_ID;
                SubstanceDetailReportlist = _objIUnitModel.SubstanceDetailReportlist(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SubstanceDetailReportlist(objHCSNModel);
                ViewBag.SubstanceDetaillist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Substance_Detail_Name,
                    Value = c.Substance_Detail_ID.ToString(),

                }).ToList();
                ViewBag.SubstanceDetailReportlist = SubstanceDetailReportlist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }
        public IActionResult SubstancesWhetherFromReport()
        {
            SubstancesWhetherFromReport objHCSNModel = new SubstancesWhetherFromReport();
            List<SubstancesWhetherFromReport> SubstancesWhetherFromReportlist = new List<SubstancesWhetherFromReport>();
            try
            {
                ////list
                objHCSNModel.Action = 1;
                SubstancesWhetherFromReportlist = _objIUnitModel.SubstancesWhetherFromReportlist(objHCSNModel);
                ////listend
                ///objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SubstancesWhetherFromReportlist(objHCSNModel);
                ViewBag.SubstancesWhetherFromlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Whether_From_Name,
                    Value = c.Whether_From_ID.ToString(),

                }).ToList();
                ViewBag.SubstancesWhetherFromReportlist = SubstancesWhetherFromReportlist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

        }

        [HttpPost]
        public IActionResult SubstancesWhetherFromReport(int Whether_From_ID)
        {
            SubstancesWhetherFromReport objHCSNModel = new SubstancesWhetherFromReport();
            List<SubstancesWhetherFromReport> SubstancesWhetherFromReportlist = new List<SubstancesWhetherFromReport>();
            try
            {
                objHCSNModel.Action = 4;
                objHCSNModel.Whether_From_ID = Whether_From_ID;
                SubstancesWhetherFromReportlist = _objIUnitModel.SubstancesWhetherFromReportlist(objHCSNModel);
                objHCSNModel.Action = 1;
                var chemicals = _objIUnitModel.SubstancesWhetherFromReportlist(objHCSNModel);
                ViewBag.SubstancesWhetherFromlist = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Whether_From_Name,
                    Value = c.Whether_From_ID.ToString(),

                }).ToList();
                ViewBag.SubstancesWhetherFromReportlist = SubstancesWhetherFromReportlist;
                return View(objHCSNModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                throw ex;
            }

}

        public IActionResult NotificationsManager()
        {
            var notifications = new List<NotificationEF>
    {
        new NotificationEF { Id = 1, Title = "Notification 1", Message = "This is notification 1", Date = DateTime.Now },
        new NotificationEF { Id = 2, Title = "Notification 2", Message = "This is notification 2", Date = DateTime.Now.AddDays(-1) },
        new NotificationEF { Id = 3, Title = "Notification 3", Message = "This is notification 3", Date = DateTime.Now.AddDays(-2) }
    };

            return View(notifications);
            return View();

        }
        [HttpPost]
        public JsonResult CashNoSuggestion(string? inputText, int mode)
        {
            CashNoEF objModel = new CashNoEF();

            try

            {
                objModel.Action = mode;
                objModel.CAshNo = inputText;
                List<CashNoEF> ResultingHazardSubstance = _objIUnitModel.CashNoSuggestion(objModel);

                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }

        //public IActionResult GisDashboard()
        //{

        //    return View();
        //}
        [HttpPost]
        public JsonResult GetChemicalProductByUserId(int Action)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Action = Action;
                masterEF.C_Entry_By = Convert.ToString(profile.UserId);
                List<ChemicalProductGroup> chemicalProductGroups = _objIUnitModel.GetChemicalProductByUserId(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        public JsonResult GetChemicalProduct(int Action)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Action = Action;
                masterEF.C_Entry_By = Convert.ToString(profile.UserId);
                List<ChemicalProductGroup> chemicalProductGroups = _objIUnitModel.GetChemicalProduct(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        public JsonResult GetChemicalPetroProduct(int Action)
        {
            try
            {
                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Action = Action;
                masterEF.C_Entry_By = Convert.ToString(profile.UserId);
                List<PetroChemicalProductGroup> chemicalProductGroups = _objIUnitModel.GetChemicalPetroProduct(masterEF);
                return Json(chemicalProductGroups);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        public IActionResult ChemicalBlockEntry()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            try
            {
                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();

                var substance = _objIUnitModel.GetSubstanceList(objChemicalModel);
                ViewBag.ViewSubstanceList = substance.ToList().Select(c => new SelectListItem
                {
                    Text = c.Substance_Detail_Name,
                    Value = c.Substance_Detail_ID.ToString(),

                }).ToList();

                var Hazard = _objIUnitModel.GetHazardList(objChemicalModel);
                ViewBag.ViewHazardList = Hazard.ToList().Select(c => new SelectListItem
                {
                    Text = c.Hazard_Classification_Name,
                    Value = c.Hazard_Classification_ID.ToString(),

                }).ToList();

                var ResultingHarard = _objIUnitModel.GetResultingHazardList(objChemicalModel);
                ViewBag.ViewResultingHazardList = ResultingHarard.ToList().Select(c => new SelectListItem
                {
                    Text = c.Resulting_Hazard_Name,
                    Value = c.Resulting_Hazard_ID.ToString(),

                }).ToList();

                var MainCategory = _objIUnitModel.GetMainCategoryList(objChemicalModel);
                ViewBag.ViewMainCategoryList = MainCategory.ToList().Select(c => new SelectListItem
                {
                    Text = c.Main_Use_Category_Name,
                    Value = c.Main_Use_Category_ID.ToString(),

                }).ToList();

                var Specification = _objIUnitModel.GetSpecificationList(objChemicalModel);
                ViewBag.ViewSpecificationList = Specification.ToList().Select(c => new SelectListItem
                {
                    Text = c.Specification_Industrial_Professional_Name,
                    Value = c.Specification_Industrial_Professional_ID.ToString(),

                }).ToList();

                var RouteList = _objIUnitModel.GetRouteList(objChemicalModel);
                ViewBag.ViewRouteList = RouteList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Significant_Route_Exposure_Name,
                    Value = c.Significant_Route_Exposure_ID.ToString(),

                }).ToList();

                var EnvironmentalList = _objIUnitModel.GetEnvironmentalList(objChemicalModel);
                ViewBag.ViewEnvironmentalList = EnvironmentalList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Environmental_Exposure_Name,
                    Value = c.Environmental_Exposure_ID.ToString(),

                }).ToList();

                var PatterList = _objIUnitModel.GetPatterList(objChemicalModel);
                ViewBag.ViewPatterList = PatterList.ToList().Select(c => new SelectListItem
                {
                    Text = c.Pattern_Exposure_Name,
                    Value = c.Pattern_Exposure_ID.ToString(),

                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                objChemicalModel = null;
            }



            if (false)
            {
                objChemicalModel.C_Entry_By = Convert.ToString(profile.UserId);

                objChemicalModel.CategoryID = 1;

                objMessageModel = _objIUnitModel.CheckYearlyChemicalEntry(objChemicalModel);
                if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");

                }
                // objChemicalModel.Y_PChemical_ID = id;
                // objmodel = _objIUnitModel.Edit(objmodel);

                ViewBag.Button = "Submit";
                return View(objChemicalModel);

            }
            else
            {
                ViewBag.Button = "Submit";
                return View(objChemicalModel);
            }
       
        }
        public IActionResult Block2()
        {
            ProductCodeChemical productCodeChemical = new ProductCodeChemical();
            AddProductCodeChemical addProductCodeChemical = new AddProductCodeChemical();
            try
            {
               

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                productCodeChemical.Entry_By = Convert.ToString(profile.UserId);

                var chemicals = _objIUnitModel.ProductChemicalList(productCodeChemical);
                ViewBag.ProductCodeChemical = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Product_Name,
                    Value = c.Chemical_Product_Code.ToString(),

                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.Button = "Submit";
            return View(addProductCodeChemical);
        }
        [HttpPost]
        public IActionResult Block2(AddProductCodeChemical addProductCodeChemical)
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            addProductCodeChemical.Entry_By = Convert.ToString(profile.UserId);
            addProductCodeChemical.CategoryID = 1;
            objMessageModel = _objIUnitModel.AddBlock2(addProductCodeChemical);
            if (objMessageModel.Satus == "1")
            {
                TempData["Messages"] = "Saved successfully.";
                TempData.Keep("Messages");
                return RedirectToAction("Block2", "UnitRegistration", new { Area = "UnitRegistration" });

            }
            else
            {
                TempData["Messages"] = "Data Not Saved.";
                TempData.Keep("Messages");
                return RedirectToAction("Block2", "UnitRegistration", new { Area = "UnitRegistration" });
            }
            return View();
        }

        public IActionResult Block2List()
        {
            
            return View();
        }
        public JsonResult BindIUPACHAZARD(string ProductID, int? mode = 1)
          {
            BindIUPACHAZARDChemical bindIUPACHAZARDChemical = new BindIUPACHAZARDChemical();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            GetIUPACHAZARD getIUPACHAZARD = new GetIUPACHAZARD();

            try

            {
                if (mode == 1)
                {
                    bindIUPACHAZARDChemical.Entry_By = Convert.ToString(profile.UserId);
                    bindIUPACHAZARDChemical.Product_ID = ProductID;
                    bindIUPACHAZARDChemical.Action = 1;
                }
                else if (mode == 2)
                {
                    bindIUPACHAZARDChemical.Entry_By = Convert.ToString(profile.UserId);
                    bindIUPACHAZARDChemical.Product_ID = ProductID;
                    bindIUPACHAZARDChemical.Action = 2;
                }
                List<GetIUPACHAZARD> ResultingHazardSubstance = _objIUnitModel.BindIUPACHAZARD(bindIUPACHAZARDChemical);

                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }


        public IActionResult YearlyChemicalExport()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YearlyChemicalExport(yearlyChemicalExport objmodel, string submit)
        {
            try
            {

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.C_Entry_By = Convert.ToString(profile.UserId);                
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.C_IP_Address = localIpAddressString;
                objmodel.CategoryID = 1;
                objMessageModel = _objIUnitModel.ChemicalProductEntry(objmodel);
              
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyChemicalExport", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyChemicalExport", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("YearlyChemicalExport", "UnitRegistration", new { Area = "UnitRegistration" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
        }
        public IActionResult YearlyPetroChemicalExport()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YearlyPetroChemicalExport(yearlyChemicalExport objmodel, string submit)
        {
            try
            {

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.C_Entry_By = Convert.ToString(profile.UserId);
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.C_IP_Address = localIpAddressString;
                objmodel.CategoryID = 1;
                objMessageModel = _objIUnitModel.ChemicalProductEntry(objmodel);

                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyChemicalExport", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("YearlyChemicalExport", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("YearlyChemicalExport", "UnitRegistration", new { Area = "UnitRegistration" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
        }
        public JsonResult GetCemicalbyCategory(int chemcid)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            List<ChemicalGroup> chemicalgroup = new List<ChemicalGroup>();
            List<PetroChemicalGroup> pchemicalgroup = new List<PetroChemicalGroup>();
            try

            {
                if(chemcid == 1)
                    {
                    chemicalgroup = _objIUnitModel.GetChemicalList(objChemicalModel);
                    chemicalgroup = chemicalgroup.ToList().Select(c => new ChemicalGroup
                    {
                        Chemical_Code = c.Chemical_Name,
                        Chemical_Name = c.Chemical_Code.ToString(),

                    }).ToList();

                }
                else if (chemcid==2)
                {
                    pchemicalgroup = _objIUnitModel.GetPetroChemicalList(objChemicalModel);
                    chemicalgroup = pchemicalgroup.ToList().Select(c => new ChemicalGroup
                    {
                        Chemical_Code = c.PChemical_Name,
                        Chemical_Name = c.PChemical_Code.ToString(),

                    }).ToList();

                }

                return Json(chemicalgroup);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        public JsonResult GetChemicalProductBycategoryID(string Chemical_Code, int category)
        {
            try
            {
                List<ChemicalProductGroup> chemicalProductGroups = new List<ChemicalProductGroup>();
                if (category == 1)
                {
                    YearlyChemicalEF masterEF = new YearlyChemicalEF();
                    masterEF.Chemical_Code = Chemical_Code;
                    chemicalProductGroups = _objIUnitModel.GetChemicalProductByID(masterEF);
                    return Json(chemicalProductGroups);
                }else if(category==2)
                {
                    YearlyChemicalEF masterEF = new YearlyChemicalEF();
                    masterEF.Chemical_Code = Chemical_Code;
                    List<PetroChemicalProductGroup> pchemicalProductGroups = _objIUnitModel.GetPetroChemicalProductByID(masterEF);
                    chemicalProductGroups= pchemicalProductGroups.ToList().Select(c => new ChemicalProductGroup
                    {
                        Chemical_Product_Code = c.PChemical_Product_Code,
                        Chemical_Product_Name = c.PChemical_Product_Name.ToString(),

                    }).ToList();
                    return Json(chemicalProductGroups);
                }
                return Json(null);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        [HttpPost]
        public IActionResult ChemicalBlockEntry(Block1EF objmodel, string submit)
        {
            try
            {

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.C_Entry_By = Convert.ToString(profile.UserId);

                //  objmodel.UserLoginId = 1; //profile.UserLoginId;
                //if (ModelState.IsValid)
                //{
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.C_IP_Address = localIpAddressString;
                objMessageModel = _objIUnitModel.ChemicalBlockEntry(objmodel);
                // }
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("ChemicalBlockEntry", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("ChemicalBlockEntry", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("ChemicalBlockEntry", "UnitRegistration", new { Area = "UnitRegistration" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }

        }

        public IActionResult Block3()
        {
            return View();
        }


        public IActionResult Blockmonth()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Bindblock3inpportbind(int productcode,int entryid=0, int mode=0)
        {
            CashNoEF objModel = new CashNoEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);


            try

            {
                if (mode == 1)
                {
                    objModel.Entry_By = Convert.ToString(profile.UserId);
                    objModel.Action = mode;
                    objModel.Id = productcode;
                    objModel.RoleId = profile.RoleID;
                    objModel.CEntry_By = entryid;

                }
                else if (mode==2)
                {
                    objModel.Entry_By = Convert.ToString(profile.UserId);
                    objModel.Action = mode;
                    objModel.Id = productcode;
                    objModel.RoleId = profile.RoleID;
                    objModel.CEntry_By = entryid;

                }
                List<ConstituentsEF> ResultingHazardSubstance = _objIUnitModel.Bindblock3inpportbind(objModel);

                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }

        [HttpPost]
        public IActionResult Block3(Block3EF objmodel, string submit)
        {
            try
            {

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.C_Entry_By = Convert.ToString(profile.UserId);

                //  objmodel.UserLoginId = 1; //profile.UserLoginId;
                //if (ModelState.IsValid)
                //{
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.C_IP_Address = localIpAddressString;
                objmodel.CategoryID = 1;

                objMessageModel = _objIUnitModel.ChemicalBlock3Entry(objmodel);
                // }
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("Block3", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("Block3", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("Block3", "UnitRegistration", new { Area = "UnitRegistration" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }

        }

        public IActionResult Block2Petrochemical()
        {
            ProductCodeChemical productCodeChemical = new ProductCodeChemical();
            AddProductCodeChemical addProductCodeChemical = new AddProductCodeChemical();
            try
            {


                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                productCodeChemical.Entry_By = Convert.ToString(profile.UserId);

             

                //


                YearlyChemicalEF masterEF = new YearlyChemicalEF();
                masterEF.Action = 27;
                masterEF.C_Entry_By = Convert.ToString(profile.UserId);
                var chemicalProductGroups = _objIUnitModel.GetChemicalPetroProduct(masterEF);
                ViewBag.ProductCodeChemical = chemicalProductGroups.ToList().Select(c => new SelectListItem
                {
                    Text = c.PChemical_Product_Name,
                    Value = c.PChemical_Product_Code.ToString(),

                }).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.Button = "Submit";
            return View(addProductCodeChemical);
        }
        [HttpPost]
        public IActionResult Block2Petrochemical(AddProductCodeChemical addProductCodeChemical)
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            addProductCodeChemical.Entry_By = Convert.ToString(profile.UserId);
            addProductCodeChemical.CategoryID = 2;

            objMessageModel = _objIUnitModel.AddBlock2(addProductCodeChemical);
            if (objMessageModel.Satus == "1")
            {
                TempData["Messages"] = "Saved successfully.";
                TempData.Keep("Messages");
                return RedirectToAction("Block2Petrochemical", "UnitRegistration", new { Area = "UnitRegistration" });

            }
            else
            {
                TempData["Messages"] = "Data Not Saved.";
                TempData.Keep("Messages");
                return RedirectToAction("Block2Petrochemical", "UnitRegistration", new { Area = "UnitRegistration" });
            }
            return View();
        }
        public IActionResult PetroPurchaseofChemicals()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PetroPurchaseofChemicals(Block3EF objmodel, string submit)
        {
            try
            {

                UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
                objmodel.C_Entry_By = Convert.ToString(profile.UserId);
                IPAddress localIpAddress = HttpContext.Connection.LocalIpAddress ?? IPAddress.Any;
                string localIpAddressString = localIpAddress.ToString();
                objmodel.C_IP_Address = localIpAddressString;
                objmodel.CategoryID = 2;
                objMessageModel = _objIUnitModel.ChemicalBlock3Entry(objmodel);
                // }
                if (objMessageModel.Satus == "1")
                {
                    TempData["Messages"] = "Saved successfully.";
                    TempData.Keep("Messages");
                    //TempData["Message"] = 1;
                    return RedirectToAction("Block3", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 1;
                    return RedirectToAction("Block3", "UnitRegistration", new { Area = "UnitRegistration" });

                }
                else
                {
                    TempData["Message"] = "Data not Saved.";
                    TempData.Keep("Message");
                    //TempData["Message"] = 2;
                    return RedirectToAction("Block3", "UnitRegistration", new { Area = "UnitRegistration" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }

        }

        public IActionResult ProductReport()
        {
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();
            try
            {
                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();

                var MainCategory = _objIUnitModel.GetMainCategoryList(objChemicalModel);
                ViewBag.ViewMainCategoryList = MainCategory.ToList().Select(c => new SelectListItem
                {
                    Text = c.Main_Use_Category_Name,
                    Value = c.Main_Use_Category_ID.ToString(),

                }).ToList();

               
               
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }
            finally
            {

                objChemicalModel = null;
            }

            if (false)
            {
                objChemicalModel.C_Entry_By = Convert.ToString(profile.UserId);

                objChemicalModel.CategoryID = 1;

                objMessageModel = _objIUnitModel.CheckYearlyChemicalEntry(objChemicalModel);
                if (objMessageModel.Satus == "3")
                {
                    TempData["Message"] = "Entry already submitted successfully.";
                    TempData.Keep("Message");

                }
                

                ViewBag.Button = "Submit";
                return View(objChemicalModel);

            }
            else
            {
                ViewBag.Button = "Submit";
                return View(objChemicalModel);
            }
        }
        public JsonResult GetResultingHazardCategoryByID(int Hazard_Classification_ID)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try

            {
                objChemicalModel.Hazard_Classification_ID = Hazard_Classification_ID;
                List<HazardCategory> ResultingHazardSubstance = _objIUnitModel.GetResultingHazardCategoryList(objChemicalModel);

                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }

        public JsonResult GetResultingHazardSubCategoryByID(int Hazard_Classification_ID)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try

            {
                objChemicalModel.Hazard_Classification_ID = Hazard_Classification_ID;
                List<HazardsubCategory> ResultingHazardSubstance = _objIUnitModel.GetResultingHazardSubCategoryList(objChemicalModel);

                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }
        public IActionResult GisDashboard()
        {
            MasterEF objUnitModel = new MasterEF();
            try
            {
                objUnitModel.Action = 1;
                var state = _objIUnitModel.GetStateList(objUnitModel);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
               }catch(Exception e)
            {

            }
            return View();
        }
       
        public ActionResult GetHazardImage(string imageName)
        {
           
            try
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs/ResultingHazard", imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                    return File(imageBytes, "application/octet-stream");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public JsonResult GetImageByID(int Hazard_Classification_ID,int mode=0)
        {
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try

            {
                if(mode==1)
                {
                    objChemicalModel.Hazard_Classification_ID = Hazard_Classification_ID;
                    objChemicalModel.Action = 1;
                    List<HazardsubCategory> ResultingHazardSubstance = _objIUnitModel.GetImageByID(objChemicalModel);
                    return Json(ResultingHazardSubstance);
                }
                else
                {
                    objChemicalModel.Hazard_Classification_ID = Hazard_Classification_ID;
                    objChemicalModel.Action = 2;
                    List<HazardsubCategory> ResultingHazardSubstance = _objIUnitModel.GetImageByID(objChemicalModel);
                    return Json(ResultingHazardSubstance);
                }

                return Json(null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }

        public ActionResult ContactList()
        {
            try
            {
                ContactEF contact = new ContactEF();
                List<ContactEF> contactEF = new List<ContactEF>();
                contactEF = _objIUnitModel.ContactList(contact);

                return View(contactEF); 
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult FeedackList()
        {
            try
            {
                feedbackEF feedbackEF = new feedbackEF();
                List<feedbackEF> contactEF = new List<feedbackEF>();
                contactEF = _objIUnitModel.feedback(feedbackEF);

                return View(contactEF);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult BindExportbind(int productcode, int entryid = 0, int mode = 0)
        {
            CashNoEF objModel = new CashNoEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);


            try

            {
                if (mode == 1)
                {
                    objModel.Entry_By = Convert.ToString(profile.UserId);
                    objModel.Action = mode;
                    objModel.Id = productcode;
                    objModel.RoleId = profile.RoleID;
                    objModel.CEntry_By = entryid;

                }
                else if (mode == 2)
                {
                    objModel.Entry_By = Convert.ToString(profile.UserId);
                    objModel.Action = mode;
                    objModel.Id = productcode;
                    objModel.RoleId = profile.RoleID;
                    objModel.CEntry_By = entryid;

                }
                List<ConstituentsE> ResultingHazardSubstance = _objIUnitModel.BindExportbind(objModel);

                return Json(ResultingHazardSubstance);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");

                return Json(null);
            }

        }


        [HttpPost]
        public JsonResult MolicularImage(IFormFile MolecularImage)
        {
            var Registration_Certificate_Name = "";
            try
            {
                Registration_Certificate_Name = Guid.NewGuid().ToString() + "_" + "Reg_" + MolecularImage.FileName.Replace(" ", "");
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploadimage");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var filePath = Path.Combine(uploadsFolder, Registration_Certificate_Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    MolecularImage.CopyTo(stream);
                }


                return Json(Registration_Certificate_Name);
            }
            catch (Exception e)
            {
                return Json(Registration_Certificate_Name);
            }
        }

    }
}
