using CPDMSEF;

namespace CPDMS.Areas.CMS.Models.Chemical
{
    public interface IChemicalModel
    {
        List<YealryChemicalListEF> GetYearlyChemicalList(YealryChemicalListEF objUnit);
        List<YealryChemicalListEF> GetYearlyPetroChemicalList(YealryChemicalListEF objUnit);
        List<MonthlyChemicalListEF> GetMonthlyChemicalList(MonthlyChemicalListEF objUnit);
        List<MonthlyChemicalListEF> GetMonthlyPetroChemicalList(MonthlyChemicalListEF objUnit);
        MessageEF YearlyReportApprovalByDeptApp(YealryChemicalListEF objchem);
        MessageEF MonthlyReportApprovalByDeptApp(YealryChemicalListEF objchem);
        List<ChemicalImport> ViewImportReport(DashboardRequestEF objOpenModel);

        List<ChemicalExport> ViewExportReport(DashboardRequestEF objOpenModel);
        List<ChemicalExport> ViewpetroExportReport(DashboardRequestEF objOpenModel);
        List<Block1ReportEF> ChemicalsDescription(DashboardRequestEF objOpenModel);
    }
}
