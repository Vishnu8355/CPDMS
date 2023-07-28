using CPDMS.Models.Utility;
using CPDMSEF;
using Newtonsoft.Json;

namespace CPDMS.Areas.CMS.Models.Chemical
{
    public class ChemicalModel : IChemicalModel
    {
        public readonly IHttpWebClients _objIHttpWebClients;
        public ChemicalModel(IHttpWebClients objHttpWebClients)
        {

            _objIHttpWebClients = objHttpWebClients;
        }
        public List<YealryChemicalListEF> GetYearlyChemicalList(YealryChemicalListEF objOpenModel)
        {
            try
            {
                List<YealryChemicalListEF> objListChemical = new List<YealryChemicalListEF>();
                objListChemical = JsonConvert.DeserializeObject<List<YealryChemicalListEF>>(_objIHttpWebClients.PostRequest("Chemical/GetYearlyChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<YealryChemicalListEF> GetYearlyPetroChemicalList(YealryChemicalListEF objOpenModel)
        {
            try
            {
                List<YealryChemicalListEF> objListChemical = new List<YealryChemicalListEF>();
                objListChemical = JsonConvert.DeserializeObject<List<YealryChemicalListEF>>(_objIHttpWebClients.PostRequest("Chemical/GetYearlyPetroChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MonthlyChemicalListEF> GetMonthlyChemicalList(MonthlyChemicalListEF objOpenModel)
        {
            try
            {
                List<MonthlyChemicalListEF> objListChemical = new List<MonthlyChemicalListEF>();
                objListChemical = JsonConvert.DeserializeObject<List<MonthlyChemicalListEF>>(_objIHttpWebClients.PostRequest("Chemical/GetMonthlyChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<MonthlyChemicalListEF> GetMonthlyPetroChemicalList(MonthlyChemicalListEF objOpenModel)
        {
            try
            {
                List<MonthlyChemicalListEF> objListChemical = new List<MonthlyChemicalListEF>();
                objListChemical = JsonConvert.DeserializeObject<List<MonthlyChemicalListEF>>(_objIHttpWebClients.PostRequest("Chemical/GetMonthlyPetroChemicalList", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF YearlyReportApprovalByDeptApp(YealryChemicalListEF objchem)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Chemical/YearlyReportApprovalByDeptApp", JsonConvert.SerializeObject(objchem)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF MonthlyReportApprovalByDeptApp(YealryChemicalListEF objchem)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Chemical/MonthlyReportApprovalByDeptApp", JsonConvert.SerializeObject(objchem)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ChemicalImport> ViewImportReport(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<ChemicalImport> objListChemical = new List<ChemicalImport>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalImport>>(_objIHttpWebClients.PostRequest("Chemical/ViewImportReport", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<ChemicalExport> ViewExportReport(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<ChemicalExport> objListChemical = new List<ChemicalExport>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalExport>>(_objIHttpWebClients.PostRequest("Chemical/ViewExportReport", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<ChemicalExport> ViewpetroExportReport(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<ChemicalExport> objListChemical = new List<ChemicalExport>();
                objListChemical = JsonConvert.DeserializeObject<List<ChemicalExport>>(_objIHttpWebClients.PostRequest("Chemical/ViewpetroExportReport", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Block1ReportEF> ChemicalsDescription(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<Block1ReportEF> objListChemical = new List<Block1ReportEF>();
                objListChemical = JsonConvert.DeserializeObject<List<Block1ReportEF>>(_objIHttpWebClients.PostRequest("Chemical/ChemicalsDescription", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}

