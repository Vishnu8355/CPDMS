using CPDMSEF;

namespace CPDMS.Areas.CompanyRegistraion.Models.UnitRegistration
{
    public interface ICompanyModel
    {
        MessageEF AddCompanyRegistrationEntry(TRegistrationEF tRegistrationEF);
        MessageEF CompanyRegistrationOTPEntry(RegistrationOTPEF registrationOTPEF);
        List<MasterEF> GetStateList(MasterEF objOpenModel);
        List<MasterEF> GetDistrictDetails(MasterEF objMaster);
        List<MasterEF> GetBlockByDistrictId(MasterEF objMaster);
        MessageEF AddCompanyEntry(CompanyRegistrationEF objUnit);

        List<MasterE> CompanyList(MasterE objOpenModel);
        List<MasterE> UnitList(MasterE objOpenModel);
        MessageEF AddCompanyUnitTagging(CUnitTagging objUnit);
        MessageEF ChangePassword(ChangePasswordEF changePasswordEF);

        List<UnitDetailsEF> GetUnitDetailsList(DashboardRequestEF objOpenModel);
        List<CompanyPerformanceEF> GetCompanyPerformanceList(DashboardRequestEF objOpenModel);
        List<ReportViewEF> ViewAdminReportList(DashboardRequestEF objOpenModel);
        List<CompanyProfileEF> CompnayProfileReport(DashboardRequestEF objOpenModel);

        List<Unitlist> CompanyTaggingList(MasterE objOpenModel);
        MessageEF UCompanyTagging(MasterE objOpenModel);
        MessageEF DashboardMarquee(DashboardRequestEF objOpenModel);
        List<ProfileEF> Profile(DashboardRequestEF objOpenModel);

        List<BestPracticesEF> BestPractisesReports(DashboardRequestEF objOpenModel);
        MessageEF SendEmail(EmailEF emailef);

        List<CapacityGraphEF> GetGraphsDetailsList(DashboardRequestEF objOpenModel);
        List<MonthChemGraphEF> GetMonthGraphsDetailsList(DashboardRequestEF objOpenModel);
        List<MonthInportExportGraphEF> GetMonthImportExportGraphs(DashboardRequestEF objOpenModel);
        List<CompanyRegistrationEF> CompanyEntryGet(MasterEF objOpenModel);
    }
}
