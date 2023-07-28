using CPDMS.Areas.CMS.Models.Chemical;
using CPDMS.Areas.UnitRegistration.Models.UnitRegistration;
using CPDMS.Web;
using CPDMSEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPDMS.Areas.CMS.Controllers
{
    [Area("CMS")]
    public class ReportsController : Controller
    {

        private readonly ILogger<ReportsController> _logger;

        private IChemicalModel _objIChemicalModel;
        private IUnitModel _objIUnitModel;

        MessageEF objMessageModel = new MessageEF();
        List<YealryChemicalListEF> objChemicalList = new List<YealryChemicalListEF>();
        List<MonthlyChemicalListEF> objMonthChemicalList = new List<MonthlyChemicalListEF>();
        private readonly IHostEnvironment hostingEnvironment;
        public ReportsController(IChemicalModel objIChemicalModel, IHostEnvironment hostingEnvironment, IUnitModel objIUnitModel, ILogger<ReportsController> logger)
        {
            _objIChemicalModel = objIChemicalModel;
            _objIUnitModel = objIUnitModel;
            this.hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

      
        public IActionResult ChemicalImport()
        {

            List<ChemicalImport> objlist = new List<ChemicalImport>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {

               
                    objmodel.Action = 1;
                if (profile.RoleID == 2 || profile.RoleID == 3)
                {
                    objmodel.Action = 3;
                }
               else if(profile.RoleID == 1)
                {
                    objmodel.Action = 5;
                }
                else if (profile.RoleID == 6)
                {
                    objmodel.Action = 7;
                }
                objmodel.ID = profile.UserId;
                    objlist = _objIChemicalModel.ViewImportReport(objmodel);
                    //return View(objCompanyList);
                
             
                return View(objlist);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return View();
            }
            finally
            {

                objlist = null;
            }

        }

        public IActionResult ChemicalsDescription()
        {

            List<Block1ReportEF> objlist = new List<Block1ReportEF>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {


                objmodel.Action = 1;
                if(profile.RoleID==6)
                {
                    objmodel.Action = 3;
                }  
                if(profile.RoleID==2||profile.RoleID == 3)
                {
                    objmodel.Action = 6;
                }
                else if(profile.RoleID==1)
                {
                    objmodel.Action = 8;
                }
                objmodel.ID = profile.UserId;
                objlist = _objIChemicalModel.ChemicalsDescription(objmodel);
                //return View(objCompanyList);


                return View(objlist);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return View();
            }
            finally
            {

                objlist = null;
            }

        }
          public IActionResult ChemicalExport()

          {
            List<ChemicalExport> objlist = new List<ChemicalExport>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {


                {
                    objmodel.Action = 1;
                    if (profile.RoleID == 2 || profile.RoleID == 3)
                    {
                        objmodel.Action = 3;
                    }
                    else if (profile.RoleID == 1)
                    {
                        objmodel.Action = 5;
                    }
                    else if (profile.RoleID == 6)
                    {
                        objmodel.Action = 7;
                    }
                    objmodel.ID = profile.UserId;
                      objlist = _objIChemicalModel.ViewExportReport(objmodel);

                }

                return View(objlist);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return View();
            }
            finally
            {

                objlist = null;
            }
        }

        public IActionResult PetroChemicalExport()

        {
            List<ChemicalExport> objlist = new List<ChemicalExport>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            try
            {

                {
                    objmodel.Action = 2;
                    if (profile.RoleID == 2 || profile.RoleID == 3)
                    {
                        objmodel.Action = 4;
                    }
                    else if (profile.RoleID == 1)
                    {
                        objmodel.Action = 6;
                    }
                    else if (profile.RoleID == 6)
                    {
                        objmodel.Action = 8;
                    }
                    objmodel.ID = profile.UserId;
                    objlist = _objIChemicalModel.ViewpetroExportReport(objmodel);

                }

                return View(objlist);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return View();
            }
            finally
            {

                objlist = null;
            }
        }

        public IActionResult PetroChemicalImport()
        {

            List<ChemicalImport> objlist = new List<ChemicalImport>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {


                objmodel.Action = 2;
                if (profile.RoleID == 2 || profile.RoleID == 3)
                {
                    objmodel.Action = 4;
                }
                else if(profile.RoleID == 1)
                {
                    objmodel.Action = 6;
                }
                else if (profile.RoleID == 6)
                {
                    objmodel.Action = 8;
                }
                objmodel.ID = profile.UserId;
                objlist = _objIChemicalModel.ViewImportReport(objmodel);
                //return View(objCompanyList);


                return View(objlist);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return View();
            }
            finally
            {

                objlist = null;
            }

        }
        public IActionResult PetroChemicalsDescription()
        {

            List<Block1ReportEF> objlist = new List<Block1ReportEF>();
            DashboardRequestEF objmodel = new DashboardRequestEF();
            UserLoginSession profile = HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);

            try
            {


                objmodel.Action = 2;
                if (profile.RoleID == 6)
                {
                    objmodel.Action = 4;
                }
				if (profile.RoleID == 2 || profile.RoleID == 3)
				{
					objmodel.Action = 7;
				}
                else if (profile.RoleID == 1)
                {
                    objmodel.Action = 9;
                }
                objmodel.ID = profile.UserId;
                objlist = _objIChemicalModel.ChemicalsDescription(objmodel);
                //return View(objCompanyList);


                return View(objlist);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception:::::: {ex}");
                return View();
            }
            finally
            {

                objlist = null;
            }

        }

    }

}