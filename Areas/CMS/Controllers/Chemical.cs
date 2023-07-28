
using CPDMS.Areas.CMS.Models.Chemical;
using CPDMS.Areas.UnitRegistration.Models.UnitRegistration;
using CPDMS.Web;
using CPDMSEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPDMS.Areas.CMS.Controllers
{
    [Area("CMS")]
    public class ChemicalController : Controller
    {
        private readonly ILogger<ChemicalController> _logger;

        private IChemicalModel _objIChemicalModel;
        private IUnitModel _objIUnitModel;

        MessageEF objMessageModel = new MessageEF();
        List<YealryChemicalListEF> objChemicalList = new List<YealryChemicalListEF>();
        List<MonthlyChemicalListEF> objMonthChemicalList = new List<MonthlyChemicalListEF>();
        private readonly IHostEnvironment hostingEnvironment;
        public ChemicalController(IChemicalModel objIChemicalModel, IHostEnvironment hostingEnvironment, IUnitModel objIUnitModel, ILogger<ChemicalController> logger)
        {
            _objIChemicalModel = objIChemicalModel;
            _objIUnitModel = objIUnitModel;
            this.hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        public IActionResult YearlyChemicalList(string mode)
        {

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {
                if(mode=="Approval" || (HttpContext.Session.GetString("mode") == "Approval"))
                {
                    ViewBag.mode = "Approval";
                }
                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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
               

                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
                
               
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                
              //  objmodel.Action = 1;//for unitopt
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 5;
                    objmodel.Userid = profile.UserId;
                } else if (profile.RoleID == 2 || profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 6;
                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 4;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 1;
                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyChemicalList(objmodel);
                return View(objChemicalList);
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
        public IActionResult YearlyPetroChemicalList(string mode)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();

            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                if (mode == "Approval" || (HttpContext.Session.GetString("mode")== "Approval"))
                {
                    ViewBag.mode = "Approval";
                }
                HttpContext.Session.Remove("mode");


                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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
               
                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
               
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                
             //   objmodel.Action = 2;
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 7;
                    objmodel.Userid = profile.UserId;

                }
                else if (profile.RoleID == 2|| profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 8;
                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 5;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 2;
                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyPetroChemicalList(objmodel);
                return View(objChemicalList);
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
        public IActionResult MonthlyChemicalList(string mode)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                if (mode == "Approval" || (HttpContext.Session.GetString("mode") == "Approval"))
                {
                    ViewBag.mode = "Approval";
                }
                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

               // objmodel.Action = 1;//for unitopt
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 5;
                }
                else if (profile.RoleID == 2||profile.RoleID==3) //for dptadm
                {
                    objmodel.Action = 6;
                }
                else if (profile.RoleID == 6 ) //for unitadm
                {
                    objmodel.Action = 1;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 4;
                }
                objmodel.Userid = profile.UserId;
                //  objmodel.Action = 1;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult MonthlyPetroChemicalList(string mode)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            MasterE objCompanyModel = new MasterE();

            MasterEF objCompany = new MasterEF();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {
                if (mode == "Approval" || (HttpContext.Session.GetString("mode") == "Approval"))
                {
                    ViewBag.mode = "Approval";
                }
                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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
                
                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
                
          
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                

               // objmodel.Action = 2;
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 7;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 8;
                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 2;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 5;
                }
                objmodel.Userid = profile.UserId;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyPetroChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult YearlyChemicalList(DashboardRequestEF objreq)
        {
            if ((HttpContext.Session.GetString("mode") == "Approval"))
            {
                ViewBag.mode = "Approval";
            }
            HttpContext.Session.Remove("mode");

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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
               
                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
                
                
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                
                objmodel.Action = 1;//for unitopt
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 5;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)//dptadm
                {
                    objmodel.Action = 6;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;


                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 1;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 4;
                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyChemicalList(objmodel);
                return View(objChemicalList);
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

        public IActionResult YearlyPetroChemicalList(DashboardRequestEF objreq)
        {
            if ((HttpContext.Session.GetString("mode") == "Approval"))
            {
                ViewBag.mode = "Approval";
            }
            HttpContext.Session.Remove("mode");

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();

            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                //cemical group

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
             
                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
                
              
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                
                objmodel.Action = 2;
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 7;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)
                {
                    objmodel.Action = 8;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;

                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 2;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 5;
                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyPetroChemicalList(objmodel);
                return View(objChemicalList);
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
        public IActionResult MonthlyChemicalList(DashboardRequestEF objreq)
        {

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();
            try
            {
                if((HttpContext.Session.GetString("mode") == "Approval"))
                {
                    ViewBag.mode = "Approval";
                }
                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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
              
                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
               
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                
                objmodel.Action = 1;//for unitopt
               
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 5;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)//dptadm
                {
                    objmodel.Action = 6;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                   
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;


                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 1;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 4;
                }

                objmodel.Userid = profile.UserId;
                //  objmodel.Action = 1;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult MonthlyPetroChemicalList(DashboardRequestEF objreq)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            MasterE objCompanyModel = new MasterE();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {
                if((HttpContext.Session.GetString("mode") == "Approval"))
                 {
                    ViewBag.mode = "Approval";
                }

                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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
               
                    objCompanyModel.Action = 5;
                    objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                    var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                    ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Name_of_Unit,
                        Value = c.UserName.ToString(),

                    }).ToList();
               
                    objCompany.Action = 1;
                    var state = _objIUnitModel.GetStateList(objCompany);
                    ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                    {
                        Text = c.State_Name,
                        Value = c.State_Code.ToString(),

                    }).ToList();
                    objCompanyModel.Action = 3;
                    var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                    ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Company_Name,
                        Value = c.Company_Code.ToString(),

                    }).ToList();
                

                objmodel.Action = 2;
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 7;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)
                {
                    objmodel.Action = 8;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;

                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;

                }
                else if (profile.RoleID == 6) //for unitadm
                {
                    objmodel.Action = 1;
                }
                else if (profile.RoleID == 7) //for unitopt
                {
                    objmodel.Action = 4;
                }
                objmodel.Userid = profile.UserId;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyPetroChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult YearlyReportApprovalByDeptApp(string ChemicalsId, string Remarks,string type,string chemtype)
        {
            YealryChemicalListEF objUnit = new YealryChemicalListEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try

            {
                if (chemtype == "Yearlychemicals")
                {
                    HttpContext.Session.SetString("mode", "Approval");
                    if (type == "Approve")
                    {
                       
                        objUnit.Action = 1;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.YearlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Approved Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("YearlyChemicalListApproval");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("YearlyChemicalListApproval");
                        }
                    }
                    else
                    {

                        objUnit.Action = 2;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.YearlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Rejected Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("YearlyChemicalListApproval");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("YearlyChemicalListApproval");
                        }
                    }
                }
                else if (chemtype == "yearlyPetrochemicals")
                {

                    if (type == "Approve")
                    {
                        objUnit.Action = 3;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.YearlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Approved Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("YearlyPetroChemicalListApproval");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("YearlyPetroChemicalListApproval");
                        }
                    }
                    else
                    {
                        objUnit.Action = 4;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.YearlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Rejected Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("YearlyPetroChemicalListApproval");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("YearlyPetroChemicalListApproval");
                        }
                    }
                }else
                {
                    return RedirectToAction("YearlyChemicalList");

                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }

        }
        [HttpPost]
        public IActionResult MonthlyReportApprovalByDeptApp(string ChemicalsId, string Remarks, string type, string chemtype)
        {
            YealryChemicalListEF objUnit = new YealryChemicalListEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try

            {
                if (chemtype == "Monthlychemicals")
                {
                    HttpContext.Session.SetString("mode", "Approval");
                    if (type == "Approve")
                    {

                        objUnit.Action = 1;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.MonthlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Approved Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyChemicalListApproval");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyChemicalListApproval");
                        }
                    }
                    else
                    {

                        objUnit.Action = 2;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.MonthlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Rejected Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyChemicalListApproval");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyChemicalListApproval");
                        }
                    }
                }
                else if (chemtype == "MonthlyPetrochemicals")
                {

                    if (type == "Approve")
                    {
                        objUnit.Action = 3;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.MonthlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Approved Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyPetroChemicalList");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyPetroChemicalList");
                        }
                    }
                    else
                    {
                        objUnit.Action = 4;

                        objUnit.ApprovedBy = Convert.ToInt32(profile.UserId);


                        objUnit.Remarks = Remarks;
                        objUnit.Chemical_Product_Code = Convert.ToInt32(ChemicalsId);
                        objMessageModel = _objIChemicalModel.MonthlyReportApprovalByDeptApp(objUnit);

                        if (objMessageModel.Satus == "1")
                        {
                            TempData["Messages"] = "Report Rejected Successfully.";
                            TempData.Keep("Messages");
                            // return View(objUnit);
                            return RedirectToAction("YearlyChemicalList");
                        }
                        else
                        {
                            TempData["Message"] = "Something went wrong.";
                            TempData.Keep("Message");
                            // return View(objUnit);
                            return RedirectToAction("MonthlyPetroChemicalList");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("MonthlyPetroChemicalList");

                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return RedirectToAction("Login", "Home", new { area = "LandingPage" });
            }

        }
        //approval page 

        public IActionResult YearlyChemicalListApproval(string mode)
        {

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {
               
                    ViewBag.mode = "Approval";
                
               // HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

                objmodel.Action = 1;//for unitopt
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 5;
                    objmodel.Userid = profile.UserId;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 6;
                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyChemicalList(objmodel);
                return View(objChemicalList);
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
        public IActionResult YearlyPetroChemicalListApproval(string mode)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();

            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
               
                    ViewBag.mode = "Approval";


                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();

                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

                objmodel.Action = 2;
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 7;
                    objmodel.Userid = profile.UserId;

                }
                else if (profile.RoleID == 2 || profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 8;
                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyPetroChemicalList(objmodel);
                return View(objChemicalList);
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
        public IActionResult MonthlyChemicalListApproval(string mode)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                    ViewBag.mode = "Approval";
               
                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

                objmodel.Action = 1;//for unitopt
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 5;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 6;
                }

                objmodel.Userid = profile.UserId;
                //  objmodel.Action = 1;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult MonthlyPetroChemicalListApproval(string mode)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            MasterE objCompanyModel = new MasterE();

            MasterEF objCompany = new MasterEF();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {
              
                    ViewBag.mode = "Approval";
               

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();


                objmodel.Action = 2;
                if (profile.RoleID == 1)//for comadm
                {
                    objmodel.Action = 7;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3) //for dptadm
                {
                    objmodel.Action = 8;
                }
                objmodel.Userid = profile.UserId;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyPetroChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult YearlyChemicalListApproval(DashboardRequestEF objreq)
        {
            
                ViewBag.mode = "Approval";
           

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

                objmodel.Action = 1;//for unitopt
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 5;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2)//dptadm
                {
                    objmodel.Action = 6;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;


                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyChemicalList(objmodel);
                return View(objChemicalList);
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

        public IActionResult YearlyPetroChemicalListApproval(DashboardRequestEF objreq)
        {
           
                ViewBag.mode = "Approval";
         

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            YealryChemicalListEF objmodel = new YealryChemicalListEF();
            MasterE objCompanyModel = new MasterE();

            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            try
            {
                //cemical group

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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();


                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

                objmodel.Action = 2;
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 7;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)
                {
                    objmodel.Action = 8;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;

                }
                objmodel.Userid = profile.UserId;
                objChemicalList = _objIChemicalModel.GetYearlyPetroChemicalList(objmodel);
                return View(objChemicalList);
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
        public IActionResult MonthlyChemicalListApproval(DashboardRequestEF objreq)
        {

            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            MasterE objCompanyModel = new MasterE();
            MasterEF objCompany = new MasterEF();
            try
            {
                if ((HttpContext.Session.GetString("mode") == "Approval"))
                {
                    ViewBag.mode = "Approval";
                }
                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();

                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();

                objmodel.Action = 1;//for unitopt

                if (profile.RoleID == 1)
                {
                    objmodel.Action = 5;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)//dptadm
                {
                    objmodel.Action = 6;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;

                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;


                }

                objmodel.Userid = profile.UserId;
                //  objmodel.Action = 1;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyChemicalList(objmodel);
                return View(objMonthChemicalList);
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
        public IActionResult MonthlyPetroChemicalListApproval (DashboardRequestEF objreq)
        {
            MonthlyChemicalEF onjMonthModel = new MonthlyChemicalEF();
            MonthlyChemicalListEF objmodel = new MonthlyChemicalListEF();
            MasterE objCompanyModel = new MasterE();
            YearlyChemicalEF objChemicalModel = new YearlyChemicalEF();

            MasterEF objCompany = new MasterEF();

            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {
                if ((HttpContext.Session.GetString("mode") == "Approval"))
                {
                    ViewBag.mode = "Approval";
                }

                HttpContext.Session.Remove("mode");

                //cemical group

                var chemicals = _objIUnitModel.GetChemicalList(objChemicalModel);
                ViewBag.ViewChemicalGroupList = chemicals.ToList().Select(c => new SelectListItem
                {
                    Text = c.Chemical_Name,
                    Value = c.Chemical_Code.ToString(),

                }).ToList();
                //
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

                objCompanyModel.Action = 5;
                objCompanyModel.Entry_By = Convert.ToString(profile.UserId);
                var Unit = _objIUnitModel.UnitListT(objCompanyModel);
                ViewBag.ViewUnitList = Unit.ToList().Select(c => new SelectListItem
                {
                    Text = c.Name_of_Unit,
                    Value = c.UserName.ToString(),

                }).ToList();

                objCompany.Action = 1;
                var state = _objIUnitModel.GetStateList(objCompany);
                ViewBag.ViewStateList = state.ToList().Select(c => new SelectListItem
                {
                    Text = c.State_Name,
                    Value = c.State_Code.ToString(),

                }).ToList();
                objCompanyModel.Action = 3;
                var Company = _objIUnitModel.CompanyListT(objCompanyModel);
                ViewBag.ViewCompanyList = Company.ToList().Select(c => new SelectListItem
                {
                    Text = c.Company_Name,
                    Value = c.Company_Code.ToString(),

                }).ToList();


                objmodel.Action = 2;
                if (profile.RoleID == 1)
                {
                    objmodel.Action = 7;
                    objmodel.UnitId = objreq.ID;
                    objmodel.Userid = profile.UserId;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;
                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;
                }
                else if (profile.RoleID == 2 || profile.RoleID == 3)
                {
                    objmodel.Action = 8;
                    objmodel.CompnayCode = objreq.ID;
                    objmodel.StateCode = objreq.State_Code;
                    objmodel.DistictCode = objreq.District_Code;

                    objmodel.Block_Code = objreq.Block_Code;
                    objmodel.finyear = objreq.finyear;
                    objmodel.chemcode = objreq.chemcode;
                    objmodel.Chemical_Product_Code = objreq.Chemical_Product_Code;

                }
                objmodel.Userid = profile.UserId;
                objMonthChemicalList = _objIChemicalModel.GetMonthlyPetroChemicalList(objmodel);
                return View(objMonthChemicalList);
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



    }

}
