using CPDMSEF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CPDMS.Areas.UnitRegistration.Models.UnitRegistration
{
    public interface IUnitModel
    {
        MessageEF AddUnitEntry(UnitRegistrationEF objUnit);
        List<MasterEF> GetStateList(MasterEF objOpenModel);
        List<MasterEF> GetCategoryList(MasterEF objOpenModel);
        List<MasterEF> GetDistrictDetails(MasterEF objMaster);
        List<MasterEF> GetBlockByDistrictId(MasterEF objMaster);
        MessageEF AddUnitRegistrationEntry(TRegistrationEF tRegistrationEF);
        MessageEF UnitTRegistrationOTPEntry(RegistrationOTPEF registrationOTPEF);

        List<ChemicalGroup> GetChemicalList(YearlyChemicalEF objOpenModel);
        List<ChemicalSubstance> GetSubstanceList(YearlyChemicalEF objOpenModel);
        List<ChemicalProductGroup> GetChemicalProductByID(YearlyChemicalEF objOpenModel);
        List<PetroChemicalProductGroup> GetPetroChemicalProductByID(YearlyChemicalEF objOpenModel);

        List<HazardClassificationSubstance> GetHazardList(YearlyChemicalEF objOpenModel);
        List<ResultingHazardSubstance> GetResultingHazardList(YearlyChemicalEF objOpenModel);
        List<MainUseCategory> GetMainCategoryList(YearlyChemicalEF objOpenModel);
        List<Specification> GetSpecificationList(YearlyChemicalEF objOpenModel);
        List<RouteExposure> GetRouteList(YearlyChemicalEF objOpenModel);
        List<EnvironmentalExposure> GetEnvironmentalList(YearlyChemicalEF objOpenModel);
        List<PatternExposure> GetPatterList(YearlyChemicalEF objOpenModel);
        MessageEF AddYearlyChemicalEntry(YearlyChemicalEF objUnit);
        MessageEF AddYearlyPetroChemicalEntry(YearlyPetroChemicalEF objUnit);


        List<YearlyChemicalGroup> GetYearlyChemicalEntryByID(MonthlyChemicalEF objOpenModel);
        List<FinYear> GetFinancialYear(MonthlyChemicalEF objOpenModel);
        List<Month> GetMonth(MonthlyChemicalEF objOpenModel);
        MessageEF AddMonthlyChemicalEntry(MonthlyChemicalEF objUnit);
        List<WhetherFrom> GetWhetherFrom(MonthlySubstanceChemicalEF objOpenModel);
        MessageEF AddMonthlySubstanceChemicalEntry(MonthlySubstanceChemicalEF objUnit);
        List<YearlyPetroChemicalGroup> GetYearlyPetroChemicalEntryByID(MonthlyPetroChemicalEF objOpenModel);
        MessageEF AddMonthlyPetroChemicalEntry(MonthlyPetroChemicalEF objUnit);
        MessageEF AddMonthlySubstancePetroChemicalEntry(MonthlySubstancePetroChemicalEF objUnit);

        MessageEF ChangePassword(ChangePasswordEF changePasswordEF);
        List<Unit> Unit(Unit unit);
        MessageEF UnitN(UnitNodal unitT);

        List<UnitRegistrationViewEF> ViewUnitPendingList(UnitRegistrationEF objUnit);
        MessageEF UnitApprovalByUnitAdmin(UnitRegistrationViewEF tRegistrationEF);
        MessageEF UnitRejectByUnitAdmin(UnitRegistrationViewEF tRegistrationEF);
        MessageEF UnitApprovalByDeptAdmin(UnitRegistrationViewEF tRegistrationEF);
        MessageEF UnitRejectByDeptAdmin(UnitRegistrationViewEF tRegistrationEF);

        List<MasterE> CompanyList(MasterE objOpenModel);
        List<MasterE> UnitList(MasterE objOpenModel);
        MessageEF AddCompanyUnitTagging(CUnitTagging objUnit);

        List<UnitDetailsEF> GetUnitDetailsList(DashboardRequestEF objOpenModel);
        List<CompanyPerformanceEF> GetCompanyPerformanceList(DashboardRequestEF objOpenModel);

        List<CompanyProfileEF> CompnayProfileReport(DashboardRequestEF objOpenModel);

        List<MasterE> CompanyListT(MasterE objOpenModel);
        List<MasterE> UnitListT(MasterE objOpenModel);   
        List<Unitlist> UnitTaggingList(MasterE objOpenModel);
        MessageEF UCompanyTagging(MasterE objOpenModel);

        List<ReportViewEF> ViewAdminReportList(DashboardRequestEF objOpenModel);
        List<PhysicalState> GetPhysicalState(MonthlyChemicalEF objOpenModel);
        List<PhysicalUnit> GetPhysicalUnit(MonthlyChemicalEF objOpenModel);
        MessageEF BestPractices(BestPracticesEF tRegistrationEF);

        List<UProfileEF> Profile(UProfileEF objOpenModel);
        List<NProfileEF> NProfile(NProfileEF objOpenModel);

        MessageEF SendEmail(EmailEF emailef);
        MessageEF DashboardMarquee(DashboardRequestEF objOpenModel);


        List<PetroChemicalGroup> GetPetroChemicalList(YearlyChemicalEF objOpenModel);
        List<MonthtlySubMainEF> GetOptReportDetails(DashboardRequestEF objOpenModel);
        List<YearlySubMainEF> GetYearlyReportDetails(DashboardRequestEF objOpenModel);

        MessageEF ChemicalGroup(MonthlyChemicalEF objmodel);
        List<MonthlyChemicalEF> ChemicalGroupList(MonthlyChemicalEF objmodel);
        List<CapacityGraphEF> GetGraphsDetailsList(DashboardRequestEF objOpenModel);
        List<MonthChemGraphEF> GetMonthGraphsDetailsList(DashboardRequestEF objOpenModel);
        List<BestPracticesQuEF> BestPractisesQa(DashboardRequestEF objOpenModel);
        List<MasterSubgroup> GetChemical(MasterSubgroup objOpenModel);
        List<Subgroup> Subgroup(MasterSubgroup objOpenModel);
        MessageEF Usubgroup(MasterSubgroup objOpenModel);
        MessageEF Addsubgroup(MasterSubgroup objUnit);

        List<Hcsn> Hcsn(MasterHcsn objOpenModel);
        MessageEF AddHcsn(MasterHcsn objUnit);
        MessageEF UHcsn(MasterHcsn objOpenModel);

        List<BestPracticesEF> BestPractisesReports(DashboardRequestEF objOpenModel);
        List<MonthInportExportGraphEF> GetMonthImportExportGraphs(DashboardRequestEF objOpenModel);
        List<EnvironExposReport> EnvironmExpReportList(EnvironExposReport objmodel);
        List<MainUseCategoryReport> MainUseCategoryReportList(MainUseCategoryReport objmodel);
        List<PatternExposureReport> PatternExposureReportList(PatternExposureReport objmodel);
        List<ResultingHazardSubstanceReport> ResultingHazardSubstanceReportList(ResultingHazardSubstanceReport objmodel);
        
      List<SignificantRouteExposureReport> SignificantRouteExposureReportList(SignificantRouteExposureReport objmodel);
        
  List<SpecificationIndustrialProfessionalRe> SpecificationIndustrialProfessionallist(SpecificationIndustrialProfessionalRe objmodel);

        List<SubstanceDetailReport> SubstanceDetailReportlist(SubstanceDetailReport objmodel);
        List<SubstancesWhetherFromReport> SubstancesWhetherFromReportlist(SubstancesWhetherFromReport objmodel);


         MessageEF PetroChemical(MonthlyPetroChemicalEF objmodel);
        List<MonthlyPetroChemicalEF> PetroChemicalList(MonthlyPetroChemicalEF objmodel);

        MessageEF EnvironmentalExposure(EnvironmentalEF objmodel);
        List<EnvironmentalEF> EnvironmentalExposureList(EnvironmentalEF objmodel);

        MessageEF MainUseCategory(MainUseCategoryEF objmodel);
        List<MainUseCategoryEF> MainUseCategoryList(MainUseCategoryEF objmodel);

        MessageEF PatternExposure(PatternExposureEF objmodel);
        List<PatternExposureEF> PatternExposureList(PatternExposureEF objmodel);

        MessageEF ResultingHazardSubstance(ResultingHazardSubstanceEF objmodel);
        List<ResultingHazardSubstanceEF> ResultingHazardSubstanceList(ResultingHazardSubstanceEF objmodel);


        MessageEF SignificantRouteExposure(SignificantRouteExposureEF objmodel);
        List<SignificantRouteExposureEF> SignificantRouteExposureList(SignificantRouteExposureEF objmodel);


        MessageEF SpecificationIndustrialProfessional(SpecificationIndustrialProfessionalEF objmodel);
        List<SpecificationIndustrialProfessionalEF> SpecificationIndustrialProfessionalList(SpecificationIndustrialProfessionalEF objmodel);

        MessageEF SubstanceDetail(SubstanceDetailEF objmodel);
        List<SubstanceDetailEF> SubstanceDetailList(SubstanceDetailEF objmodel);

        MessageEF SubstancesWhetherFrom(SubstancesWhetherFromEF objmodel);
        List<SubstancesWhetherFromEF> SubstancesWhetherFromList(SubstancesWhetherFromEF objmodel);

        List<CashNoEF> CashNoSuggestion(CashNoEF objOpenModel);
        MessageEF CheckMonthlyChemicalEntry(MonthlyChemicalEF objUnit);
        MessageEF CheckYearlyChemicalEntry(YearlyChemicalEF objUnit);
        List<ChemicalProductGroup> GetChemicalProductByUserId(YearlyChemicalEF objOpenModel);
        List<ChemicalProductGroup> GetChemicalProduct(YearlyChemicalEF objOpenModel);
        MessageEF ChemicalBlockEntry(Block1EF objUnit);


        List<ProductCodeChemical> ProductChemicalList(ProductCodeChemical objOpenModel);

        List<GetIUPACHAZARD> BindIUPACHAZARD(BindIUPACHAZARDChemical objUnit);

        MessageEF AddBlock2(AddProductCodeChemical objUnit);

        List<PetroChemicalProductGroup> GetChemicalPetroProduct(YearlyChemicalEF objOpenModel);
        MessageEF ChemicalProductEntry(yearlyChemicalExport objUnit);
        List<ConstituentsEF> Bindblock3inpportbind(CashNoEF objOpenModel);

        MessageEF ChemicalBlock3Entry(Block3EF objUnit);
        MessageEF ChemicalPetroProductEntry(yearlyChemicalExport objUnit);
        List<HazardCategory> GetResultingHazardCategoryList(YearlyChemicalEF objOpenModel);
        List<HazardsubCategory> GetResultingHazardSubCategoryList(YearlyChemicalEF objOpenModel);
        List<HazardsubCategory> GetImageByID(YearlyChemicalEF objOpenModel);

        List<ContactEF> ContactList(ContactEF contactEF);
        List<feedbackEF> feedback(feedbackEF feedback);
        
        List<ConstituentsE> BindExportbind(CashNoEF objOpenModel);

    }
}
