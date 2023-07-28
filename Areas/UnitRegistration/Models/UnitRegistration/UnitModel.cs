using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using CPDMS.Models.Utility;
using CPDMSEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;

namespace CPDMS.Areas.UnitRegistration.Models.UnitRegistration
{
    public class UnitModel : IUnitModel
    {
        public readonly IHttpWebClients _objIHttpWebClients;
        public UnitModel(IHttpWebClients objHttpWebClients)
        {

            _objIHttpWebClients = objHttpWebClients;
        }
        public MessageEF AddUnitEntry(UnitRegistrationEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddUnitEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MasterEF> GetStateList(MasterEF objOpenModel)
        {
            try
            {
                List<MasterEF> objListState = new List<MasterEF>();
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetStateList", JsonConvert.SerializeObject(objOpenModel)));
                return objListState;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MasterEF> GetDistrictDetails(MasterEF objOpenModel)
        {
            try
            {
                List<MasterEF> objListState = new List<MasterEF>();
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetDistrictDetails", JsonConvert.SerializeObject(objOpenModel)));
                return objListState;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MasterEF> GetBlockByDistrictId(MasterEF objOpenModel)
        {
            try
            {
                List<MasterEF> objListState = new List<MasterEF>();
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetBlockByDistrictId", JsonConvert.SerializeObject(objOpenModel)));
                return objListState;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddUnitRegistrationEntry(TRegistrationEF tRegistrationEF)
        {
            MessageEF objMessageEF = new MessageEF();
            try
            {

                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitTRegistration", JsonConvert.SerializeObject(tRegistrationEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                return objMessageEF;
            }
        }
        public MessageEF UnitTRegistrationOTPEntry(RegistrationOTPEF registrationOTPEF)
        {
            MessageEF objMessageEF = new MessageEF();
            try
            {

                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitTRegistrationOTP", JsonConvert.SerializeObject(registrationOTPEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                return objMessageEF;
            }
        }

        public List<ChemicalGroup> GetChemicalList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<ChemicalGroup> objListChemical = new List<ChemicalGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ChemicalSubstance> GetSubstanceList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<ChemicalSubstance> objListChemical = new List<ChemicalSubstance>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalSubstance>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetSubstanceList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ChemicalProductGroup> GetChemicalProductByID(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<ChemicalProductGroup> objListChemical = new List<ChemicalProductGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalProductGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalProductByID", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PetroChemicalProductGroup> GetPetroChemicalProductByID(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<PetroChemicalProductGroup> objListChemical = new List<PetroChemicalProductGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<PetroChemicalProductGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPetroChemicalProductByID", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<HazardClassificationSubstance> GetHazardList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<HazardClassificationSubstance> objListChemical = new List<HazardClassificationSubstance>();
                objListChemical = JsonConvert.DeserializeObject<List<HazardClassificationSubstance>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetHazardList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ResultingHazardSubstance> GetResultingHazardList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<ResultingHazardSubstance> objListChemical = new List<ResultingHazardSubstance>();
                objListChemical = JsonConvert.DeserializeObject<List<ResultingHazardSubstance>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetResultingHazardList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MainUseCategory> GetMainCategoryList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<MainUseCategory> objListChemical = new List<MainUseCategory>();
                objListChemical = JsonConvert.DeserializeObject<List<MainUseCategory>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetMainCategoryList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Specification> GetSpecificationList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<Specification> objListChemical = new List<Specification>();
                objListChemical = JsonConvert.DeserializeObject<List<Specification>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetSpecificationList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<RouteExposure> GetRouteList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<RouteExposure> objListChemical = new List<RouteExposure>();
                objListChemical = JsonConvert.DeserializeObject<List<RouteExposure>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetRouteList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EnvironmentalExposure> GetEnvironmentalList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<EnvironmentalExposure> objListChemical = new List<EnvironmentalExposure>();
                objListChemical = JsonConvert.DeserializeObject<List<EnvironmentalExposure>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetEnvironmentalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PatternExposure> GetPatterList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<PatternExposure> objListChemical = new List<PatternExposure>();
                objListChemical = JsonConvert.DeserializeObject<List<PatternExposure>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPatterList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddYearlyChemicalEntry(YearlyChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddYearlyChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddYearlyPetroChemicalEntry(YearlyPetroChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddYearlyPetroChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<YearlyChemicalGroup> GetYearlyChemicalEntryByID(MonthlyChemicalEF objOpenModel)
        {
            try
            {
                List<YearlyChemicalGroup> objListChemical = new List<YearlyChemicalGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<YearlyChemicalGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetYearlyChemicalEntryByID", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<FinYear> GetFinancialYear(MonthlyChemicalEF objOpenModel)
        {
            try
            {
                List<FinYear> objListChemical = new List<FinYear>();
                objListChemical = JsonConvert.DeserializeObject<List<FinYear>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetFinancialYear", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Month> GetMonth(MonthlyChemicalEF objOpenModel)
        {
            try
            {
                List<Month> objListChemical = new List<Month>();
                objListChemical = JsonConvert.DeserializeObject<List<Month>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetMonth", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddMonthlyChemicalEntry(MonthlyChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddMonthlyChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<WhetherFrom> GetWhetherFrom(MonthlySubstanceChemicalEF objOpenModel)
        {
            try
            {
                List<WhetherFrom> objListChemical = new List<WhetherFrom>();
                objListChemical = JsonConvert.DeserializeObject<List<WhetherFrom>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetWhetherFrom", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddMonthlySubstanceChemicalEntry(MonthlySubstanceChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddMonthlySubstanceChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<YearlyPetroChemicalGroup> GetYearlyPetroChemicalEntryByID(MonthlyPetroChemicalEF objOpenModel)
        {
            try
            {
                List<YearlyPetroChemicalGroup> objListChemical = new List<YearlyPetroChemicalGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<YearlyPetroChemicalGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetYearlyPetroChemicalEntryByID", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddMonthlyPetroChemicalEntry(MonthlyPetroChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddMonthlyPetroChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddMonthlySubstancePetroChemicalEntry(MonthlySubstancePetroChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddMonthlySubstancePetroChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF ChangePassword(ChangePasswordEF changePasswordEF)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/ChangePassword", JsonConvert.SerializeObject(changePasswordEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Unit> Unit(Unit unit)
        {
            try
            {
                List<Unit> units = new List<Unit>();

                MessageEF objMessageEF = new MessageEF();
                units = JsonConvert.DeserializeObject<List<Unit>>(_objIHttpWebClients.PostRequest("UnitRegistration/Unit", JsonConvert.SerializeObject(unit)));
                return units;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF UnitN(UnitNodal unitT)
        {
            try
            {
                Unit units = new Unit();
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitNodal", JsonConvert.SerializeObject(unitT)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<UnitRegistrationViewEF> ViewUnitPendingList(UnitRegistrationEF objOpenModel)
        {
            try
            {
                List<UnitRegistrationViewEF> objListChemical = new List<UnitRegistrationViewEF>();
                objListChemical = JsonConvert.DeserializeObject<List<UnitRegistrationViewEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/ViewUnitPendingList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF UnitApprovalByUnitAdmin(UnitRegistrationViewEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitApprovalByUnitAdmin", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF UnitRejectByUnitAdmin(UnitRegistrationViewEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitRejectByUnitAdmin", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF UnitApprovalByDeptAdmin(UnitRegistrationViewEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitApprovalByDeptAdmin", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF UnitRejectByDeptAdmin(UnitRegistrationViewEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitRejectByDeptAdmin", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MasterEF> GetCategoryList(MasterEF objOpenModel)
        {
            try
            {
                List<MasterEF> objListState = new List<MasterEF>();
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetCategoryList", JsonConvert.SerializeObject(objOpenModel)));
                return objListState;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MasterE> CompanyList(MasterE objOpenModel)
        {
            try
            {
                List<MasterE> objListCompany = new List<MasterE>();
                objListCompany = JsonConvert.DeserializeObject<List<MasterE>>(_objIHttpWebClients.PostRequest("CompanyRegistration/CompanyList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MasterE> UnitList(MasterE objOpenModel)
        {
            try
            {
                List<MasterE> objListCompany = new List<MasterE>();
                objListCompany = JsonConvert.DeserializeObject<List<MasterE>>(_objIHttpWebClients.PostRequest("CompanyRegistration/UnitList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddCompanyUnitTagging(CUnitTagging objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddCompanyUnitTagging", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UnitDetailsEF> GetUnitDetailsList(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<UnitDetailsEF> objListCompany = new List<UnitDetailsEF>();
                objListCompany = JsonConvert.DeserializeObject<List<UnitDetailsEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetUnitDetailsList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CompanyPerformanceEF> GetCompanyPerformanceList(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<CompanyPerformanceEF> objListCompany = new List<CompanyPerformanceEF>();
                objListCompany = JsonConvert.DeserializeObject<List<CompanyPerformanceEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetCompanyPerformanceList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CompanyProfileEF> CompnayProfileReport(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<CompanyProfileEF> objListChemical = new List<CompanyProfileEF>();
                objListChemical = JsonConvert.DeserializeObject<List<CompanyProfileEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/CompnayProfileReport", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MasterE> CompanyListT(MasterE objOpenModel)
        {
            try
            {
                List<MasterE> objListCompany = new List<MasterE>();
                objListCompany = JsonConvert.DeserializeObject<List<MasterE>>(_objIHttpWebClients.PostRequest("UnitRegistration/CompanyListT", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MasterE> UnitListT(MasterE objOpenModel)
        {
            try
            {
                List<MasterE> objListCompany = new List<MasterE>();
                objListCompany = JsonConvert.DeserializeObject<List<MasterE>>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitListT", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Unitlist> UnitTaggingList(MasterE objOpenModel)
        {
            try
            {
                List<Unitlist> objListCompany = new List<Unitlist>();
                objListCompany = JsonConvert.DeserializeObject<List<Unitlist>>(_objIHttpWebClients.PostRequest("UnitRegistration/UnitTaggingList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF UCompanyTagging(MasterE objOpenModel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UCompanyTagging", JsonConvert.SerializeObject(objOpenModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ReportViewEF> ViewAdminReportList(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<ReportViewEF> objListChemical = new List<ReportViewEF>();
                objListChemical = JsonConvert.DeserializeObject<List<ReportViewEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/ViewAdminReportList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PhysicalState> GetPhysicalState(MonthlyChemicalEF objOpenModel)
        {
            try
            {
                List<PhysicalState> objListChemical = new List<PhysicalState>();
                objListChemical = JsonConvert.DeserializeObject<List<PhysicalState>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPhysicalState", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF BestPractices(BestPracticesEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/BestPractices", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PhysicalUnit> GetPhysicalUnit(MonthlyChemicalEF objOpenModel)
        {
            try
            {
                List<PhysicalUnit> objListChemical = new List<PhysicalUnit>();
                objListChemical = JsonConvert.DeserializeObject<List<PhysicalUnit>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPhysicalUnit", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PetroChemicalGroup> GetPetroChemicalList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<PetroChemicalGroup> objListChemical = new List<PetroChemicalGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<PetroChemicalGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPetroChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF SendEmail(EmailEF emailef)
        {
            MessageEF msg = new MessageEF();

            try
            {

                string sender = "cpdms1.gadigital@gmail.com";
                string password = "fyevkzamylisdlea";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(sender);
                message.To.Add(emailef.EmailAddress);
                message.Subject = emailef.Subject;
                message.Body = emailef.Message;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(sender, password);
                client.EnableSsl = true;
                client.Send(message);
                msg.Satus = "1";
                return msg;
            }
            catch (Exception ex)
            {
                msg.Satus = "0";
                return msg;
            }
        }
        public MessageEF DashboardMarquee(DashboardRequestEF objOpenModel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/DashboardMarquee", JsonConvert.SerializeObject(objOpenModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<UProfileEF> Profile(UProfileEF objOpenModel)
        {
            try
            {
                List<UProfileEF> objListChemical = new List<UProfileEF>();
                objListChemical = JsonConvert.DeserializeObject<List<UProfileEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/Profile", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<NProfileEF> NProfile(NProfileEF objOpenModel)
        {
            try
            {
                List<NProfileEF> objListChemical = new List<NProfileEF>();
                objListChemical = JsonConvert.DeserializeObject<List<NProfileEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/NProfile", JsonConvert.SerializeObject(objOpenModel)));

                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MonthtlySubMainEF> GetOptReportDetails(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<MonthtlySubMainEF> objsubresult = new List<MonthtlySubMainEF>();
                objsubresult = JsonConvert.DeserializeObject<List<MonthtlySubMainEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetOptReportDetails", JsonConvert.SerializeObject(objOpenModel)));

                return objsubresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<YearlySubMainEF> GetYearlyReportDetails(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<YearlySubMainEF> objsubresult = new List<YearlySubMainEF>();
                objsubresult = JsonConvert.DeserializeObject<List<YearlySubMainEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetYearlyReportDetails", JsonConvert.SerializeObject(objOpenModel)));

                return objsubresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public MessageEF ChemicalGroup(MonthlyChemicalEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/ChemicalGroup", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MonthlyChemicalEF> ChemicalGroupList(MonthlyChemicalEF objmodel)
        {
            try
            {
                List<MonthlyChemicalEF> ojbChemicalList = new List<MonthlyChemicalEF>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<MonthlyChemicalEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalGroup", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<CapacityGraphEF> GetGraphsDetailsList(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<CapacityGraphEF> objListCompany = new List<CapacityGraphEF>();
                objListCompany = JsonConvert.DeserializeObject<List<CapacityGraphEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetGraphsDetailsList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MonthChemGraphEF> GetMonthGraphsDetailsList(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<MonthChemGraphEF> objListCompany = new List<MonthChemGraphEF>();
                objListCompany = JsonConvert.DeserializeObject<List<MonthChemGraphEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetMonthGraphsDetailsList", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BestPracticesQuEF> BestPractisesQa(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<BestPracticesQuEF> objList = new List<BestPracticesQuEF>();
                objList = JsonConvert.DeserializeObject<List<BestPracticesQuEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/BestPractisesQa", JsonConvert.SerializeObject(objOpenModel)));
                return objList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MasterSubgroup> GetChemical(MasterSubgroup objOpenModels)
        {
            try
            {
                List<MasterSubgroup> objListChemicals = new List<MasterSubgroup>();
                objListChemicals = JsonConvert.DeserializeObject<List<MasterSubgroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalslist", JsonConvert.SerializeObject(objOpenModels)));

                return objListChemicals;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Subgroup> Subgroup(MasterSubgroup objOpenModel)
        {
            try
            {
                List<Subgroup> objListCompany = new List<Subgroup>();
                objListCompany = JsonConvert.DeserializeObject<List<Subgroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/Subgroup", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF Usubgroup(MasterSubgroup objOpenModel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/Usubgroup", JsonConvert.SerializeObject(objOpenModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF Addsubgroup(MasterSubgroup objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddSubgroup", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Hcsn> Hcsn(MasterHcsn objOpenModel)
        {
            try
            {
                List<Hcsn> objListCompany = new List<Hcsn>();
                objListCompany = JsonConvert.DeserializeObject<List<Hcsn>>(_objIHttpWebClients.PostRequest("UnitRegistration/Hcsn", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BestPracticesEF> BestPractisesReports(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<BestPracticesEF> objList = new List<BestPracticesEF>();
                objList = JsonConvert.DeserializeObject<List<BestPracticesEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/BestPractisesReports", JsonConvert.SerializeObject(objOpenModel)));
                return objList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF UHcsn(MasterHcsn objOpenModel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/UHcsn", JsonConvert.SerializeObject(objOpenModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MonthInportExportGraphEF> GetMonthImportExportGraphs(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<MonthInportExportGraphEF> objListCompany = new List<MonthInportExportGraphEF>();
                objListCompany = JsonConvert.DeserializeObject<List<MonthInportExportGraphEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetMonthImportExportGraphs", JsonConvert.SerializeObject(objOpenModel)));
                return objListCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddHcsn(MasterHcsn objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddHcsn", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EnvironExposReport> EnvironmExpReportList(EnvironExposReport objmodel)
        {
            try
            {
                List<EnvironExposReport> ojbChemicalList = new List<EnvironExposReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<EnvironExposReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/EnvironExposReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<MainUseCategoryReport> MainUseCategoryReportList(MainUseCategoryReport objmodel)
        {
            try
            {
                List<MainUseCategoryReport> ojbChemicalList = new List<MainUseCategoryReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<MainUseCategoryReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/MainUseCategoryReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<PatternExposureReport> PatternExposureReportList(PatternExposureReport objmodel)
        {
            try
            {
                List<PatternExposureReport> ojbChemicalList = new List<PatternExposureReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<PatternExposureReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/PatternExposureReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ResultingHazardSubstanceReport> ResultingHazardSubstanceReportList(ResultingHazardSubstanceReport objmodel)
        {
            try
            {
                List<ResultingHazardSubstanceReport> ojbChemicalList = new List<ResultingHazardSubstanceReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<ResultingHazardSubstanceReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/ResultingHazardSubstanceReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public MessageEF PetroChemical(MonthlyPetroChemicalEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/PetroChemical", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<MonthlyPetroChemicalEF> PetroChemicalList(MonthlyPetroChemicalEF objmodel)
        {
            try
            {
                List<MonthlyPetroChemicalEF> ojbPetroChemicalList = new List<MonthlyPetroChemicalEF>();
                ojbPetroChemicalList = JsonConvert.DeserializeObject<List<MonthlyPetroChemicalEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPetroChemical", JsonConvert.SerializeObject(objmodel)));
                return ojbPetroChemicalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF EnvironmentalExposure(EnvironmentalEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/EnvironmentalExposure", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EnvironmentalEF> EnvironmentalExposureList(EnvironmentalEF objmodel)
        {
            try
            {
                List<EnvironmentalEF> ojbEnvironmentalExposureList = new List<EnvironmentalEF>();
                ojbEnvironmentalExposureList = JsonConvert.DeserializeObject<List<EnvironmentalEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetEnvironmentalExposure", JsonConvert.SerializeObject(objmodel)));
                return ojbEnvironmentalExposureList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public MessageEF MainUseCategory(MainUseCategoryEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/MainUseCategory", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MainUseCategoryEF> MainUseCategoryList(MainUseCategoryEF objmodel)
        {
            try
            {
                List<MainUseCategoryEF> ojbMainUseCategoryList = new List<MainUseCategoryEF>();
                ojbMainUseCategoryList = JsonConvert.DeserializeObject<List<MainUseCategoryEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetMainUseCategory", JsonConvert.SerializeObject(objmodel)));
                return ojbMainUseCategoryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public MessageEF PatternExposure(PatternExposureEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/PatternExposure", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PatternExposureEF> PatternExposureList(PatternExposureEF objmodel)
        {
            try
            {
                List<PatternExposureEF> ojbPatternExposureList = new List<PatternExposureEF>();
                ojbPatternExposureList = JsonConvert.DeserializeObject<List<PatternExposureEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetPatternExposure", JsonConvert.SerializeObject(objmodel)));
                return ojbPatternExposureList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF ResultingHazardSubstance(ResultingHazardSubstanceEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/ResultingHazardSubstance", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ResultingHazardSubstanceEF> ResultingHazardSubstanceList(ResultingHazardSubstanceEF objmodel)
        {
            try
            {
                List<ResultingHazardSubstanceEF> ojbResultingHazardSubstanceList = new List<ResultingHazardSubstanceEF>();
                ojbResultingHazardSubstanceList = JsonConvert.DeserializeObject<List<ResultingHazardSubstanceEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetResultingHazardSubstance", JsonConvert.SerializeObject(objmodel)));
                return ojbResultingHazardSubstanceList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<SignificantRouteExposureReport> SignificantRouteExposureReportList(SignificantRouteExposureReport objmodel)
        {
            try
            {
                List<SignificantRouteExposureReport> ojbChemicalList = new List<SignificantRouteExposureReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<SignificantRouteExposureReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/SignificantRouteExposureReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public List<SpecificationIndustrialProfessionalRe> SpecificationIndustrialProfessionallist(SpecificationIndustrialProfessionalRe objmodel)
        {
            try
            {
                List<SpecificationIndustrialProfessionalRe> ojbChemicalList = new List<SpecificationIndustrialProfessionalRe>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<SpecificationIndustrialProfessionalRe>>(_objIHttpWebClients.PostRequest("UnitRegistration/SpecificationIndustrialProfessionalRe", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SubstanceDetailReport> SubstanceDetailReportlist(SubstanceDetailReport objmodel)
        {
            try
            {
                List<SubstanceDetailReport> ojbChemicalList = new List<SubstanceDetailReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<SubstanceDetailReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/SubstanceDetailReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public List<SubstancesWhetherFromReport> SubstancesWhetherFromReportlist(SubstancesWhetherFromReport objmodel)
        {
            try
            {
                List<SubstancesWhetherFromReport> ojbChemicalList = new List<SubstancesWhetherFromReport>();
                ojbChemicalList = JsonConvert.DeserializeObject<List<SubstancesWhetherFromReport>>(_objIHttpWebClients.PostRequest("UnitRegistration/SubstancesWhetherFromReport", JsonConvert.SerializeObject(objmodel)));
                return ojbChemicalList;
            }
            catch (Exception e)
            {
                return null;
            }

        }



        public MessageEF SignificantRouteExposure(SignificantRouteExposureEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/SignificantRouteExposure", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SignificantRouteExposureEF> SignificantRouteExposureList(SignificantRouteExposureEF objmodel)
        {
            try
            {
                List<SignificantRouteExposureEF> ojbSignificantRouteExposureList = new List<SignificantRouteExposureEF>();
                ojbSignificantRouteExposureList = JsonConvert.DeserializeObject<List<SignificantRouteExposureEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetSignificantRouteExposure", JsonConvert.SerializeObject(objmodel)));
                return ojbSignificantRouteExposureList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF SpecificationIndustrialProfessional(SpecificationIndustrialProfessionalEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/SpecificationIndustrialProfessional", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecificationIndustrialProfessionalEF> SpecificationIndustrialProfessionalList(SpecificationIndustrialProfessionalEF objmodel)
        {
            try
            {
                List<SpecificationIndustrialProfessionalEF> ojbSpecificationIndustrialProfessionalList = new List<SpecificationIndustrialProfessionalEF>();
                ojbSpecificationIndustrialProfessionalList = JsonConvert.DeserializeObject<List<SpecificationIndustrialProfessionalEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetSpecificationIndustrialProfessional", JsonConvert.SerializeObject(objmodel)));
                return ojbSpecificationIndustrialProfessionalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF SubstanceDetail(SubstanceDetailEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/SubstanceDetail", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubstanceDetailEF> SubstanceDetailList(SubstanceDetailEF objmodel)
        {
            try
            {
                List<SubstanceDetailEF> ojbSubstanceDetailList = new List<SubstanceDetailEF>();
                ojbSubstanceDetailList = JsonConvert.DeserializeObject<List<SubstanceDetailEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetSubstanceDetail", JsonConvert.SerializeObject(objmodel)));
                return ojbSubstanceDetailList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public MessageEF SubstancesWhetherFrom(SubstancesWhetherFromEF objmodel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/SubstancesWhetherFrom", JsonConvert.SerializeObject(objmodel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubstancesWhetherFromEF> SubstancesWhetherFromList(SubstancesWhetherFromEF objmodel)
        {
            try
            {
                List<SubstancesWhetherFromEF> ojbSubstancesWhetherFromList = new List<SubstancesWhetherFromEF>();
                ojbSubstancesWhetherFromList = JsonConvert.DeserializeObject<List<SubstancesWhetherFromEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetSubstancesWhetherFrom", JsonConvert.SerializeObject(objmodel)));
                return ojbSubstancesWhetherFromList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CashNoEF> CashNoSuggestion(CashNoEF objOpenModel)
        {
            try
            {
                List<CashNoEF> objListcash = new List<CashNoEF>();
                objListcash = JsonConvert.DeserializeObject<List<CashNoEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/CashNoSuggestion", JsonConvert.SerializeObject(objOpenModel)));
                return objListcash;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF CheckMonthlyChemicalEntry(MonthlyChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/CheckMonthlyChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF CheckYearlyChemicalEntry(YearlyChemicalEF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/CheckYearlyChemicalEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ChemicalProductGroup> GetChemicalProductByUserId(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<ChemicalProductGroup> objListChemical = new List<ChemicalProductGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalProductGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalProductByUserId", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF ChemicalBlockEntry(Block1EF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/ChemicalBlockEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ChemicalProductGroup> GetChemicalProduct(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<ChemicalProductGroup> objListChemical = new List<ChemicalProductGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalProductGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalProduct", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public  List<ConstituentsEF> Bindblock3inpportbind(CashNoEF objOpenModel)
        {
            List<ConstituentsEF> CashNoList = new List<ConstituentsEF>();
            try
            {
                List<ConstituentsEF> objListChemical = new List<ConstituentsEF>();
                objListChemical = JsonConvert.DeserializeObject<List<ConstituentsEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/Bindblock3import", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
       
        public List<PetroChemicalProductGroup> GetChemicalPetroProduct(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<PetroChemicalProductGroup> objListChemical = new List<PetroChemicalProductGroup>();
                objListChemical = JsonConvert.DeserializeObject<List<PetroChemicalProductGroup>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetChemicalPetroProduct", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF ChemicalProductEntry(yearlyChemicalExport objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/ChemicalProductEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
         public List<ProductCodeChemical> ProductChemicalList(ProductCodeChemical objOpenModel)
        {
            try
            {
                List<ProductCodeChemical> objListChemical = new List<ProductCodeChemical>();
                objListChemical = JsonConvert.DeserializeObject<List<ProductCodeChemical>>(_objIHttpWebClients.PostRequest("UnitRegistration/ProductChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<GetIUPACHAZARD> BindIUPACHAZARD(BindIUPACHAZARDChemical objUnit)
        {
            try
            {
                List <GetIUPACHAZARD> objGetIUPACHAZARD = new List<GetIUPACHAZARD>();
                objGetIUPACHAZARD = JsonConvert.DeserializeObject <List<GetIUPACHAZARD>>(_objIHttpWebClients.PostRequest("UnitRegistration/BindIUPACHAZARD", JsonConvert.SerializeObject(objUnit)));
                return objGetIUPACHAZARD;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MessageEF AddBlock2(AddProductCodeChemical objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/AddBlock2", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
          public MessageEF ChemicalBlock3Entry(Block3EF objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/ChemicalBlock3Entry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF ChemicalPetroProductEntry(yearlyChemicalExport objUnit)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("UnitRegistration/ChemicalPetroProductEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<HazardCategory> GetResultingHazardCategoryList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<HazardCategory> objListChemical = new List<HazardCategory>();
                objListChemical = JsonConvert.DeserializeObject<List<HazardCategory>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetResultingHazardCategoryList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<HazardsubCategory> GetResultingHazardSubCategoryList(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<HazardsubCategory> objListChemical = new List<HazardsubCategory>();
                objListChemical = JsonConvert.DeserializeObject<List<HazardsubCategory>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetResultingHazardSubCategoryList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<HazardsubCategory> GetImageByID(YearlyChemicalEF objOpenModel)
        {
            try
            {
                List<HazardsubCategory> objListChemical = new List<HazardsubCategory>();
                objListChemical = JsonConvert.DeserializeObject<List<HazardsubCategory>>(_objIHttpWebClients.PostRequest("UnitRegistration/GetImageByID", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ContactEF> ContactList(ContactEF contactE)
        {
            try
            {
                List<ContactEF> contactEF = new List<ContactEF>();
                contactEF = JsonConvert.DeserializeObject<List<ContactEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/ContactList", JsonConvert.SerializeObject(contactE)));
                return contactEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<feedbackEF> feedback(feedbackEF feedback)
        {
            try
            {
                List<feedbackEF> feedbackEF = new List<feedbackEF>();
                feedbackEF = JsonConvert.DeserializeObject<List<feedbackEF>>(_objIHttpWebClients.PostRequest("UnitRegistration/feedback", JsonConvert.SerializeObject(feedback)));
                return feedbackEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ConstituentsE> BindExportbind(CashNoEF objOpenModel)
        {
            List<ConstituentsE> CashNoList = new List<ConstituentsE>();
            try
            {
                List<ConstituentsE> objListChemical = new List<ConstituentsE>();
                objListChemical = JsonConvert.DeserializeObject<List<ConstituentsE>>(_objIHttpWebClients.PostRequest("UnitRegistration/BindExportbind", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}