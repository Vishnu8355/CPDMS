using CPDMS.Models.Utility;
using CPDMSEF;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace CPDMS.Areas.CompanyRegistraion.Models.UnitRegistration
{
    public class CompanyModel: ICompanyModel
    {
        public readonly IHttpWebClients _objIHttpWebClients;
        public CompanyModel(IHttpWebClients objHttpWebClients)
        {

            _objIHttpWebClients = objHttpWebClients;
        }

        public MessageEF AddCompanyRegistrationEntry(TRegistrationEF tRegistrationEF)
        {
            MessageEF objMessageEF = new MessageEF();
            try
            {

                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/CompanyTRegistration", JsonConvert.SerializeObject(tRegistrationEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                return objMessageEF;
            }
        }

        public MessageEF CompanyRegistrationOTPEntry(RegistrationOTPEF registrationOTPEF)
        {
            MessageEF objMessageEF = new MessageEF();
            try
            {

                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/CompanyRegistrationOTP", JsonConvert.SerializeObject(registrationOTPEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {
                return objMessageEF;
            }
        }

        public List<MasterEF> GetStateList(MasterEF objOpenModel)
        {
            try
            {
                List<MasterEF> objListState = new List<MasterEF>();
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetStateList", JsonConvert.SerializeObject(objOpenModel)));
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
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetDistrictDetails", JsonConvert.SerializeObject(objOpenModel)));
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
                objListState = JsonConvert.DeserializeObject<List<MasterEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/GetBlockByDistrictId", JsonConvert.SerializeObject(objOpenModel)));
                return objListState;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public MessageEF AddCompanyEntry(CompanyRegistrationEF objUnit)
        {
            try
            {
                var Registration_Certificate_Name = "";
                var Pollution_Clearance_Certificate_Name = "";
                var GST_Certificate_Name = "";
                var Pan_Certificate_Name = "";
                //save file data  to local storage
                if (objUnit.Registration_Certificate != null && objUnit.Registration_Certificate.Length > 0)

                {
                    Registration_Certificate_Name = Guid.NewGuid().ToString() + "_" + "Reg_" + objUnit.Registration_Certificate.FileName.Replace(" ", "");

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, Registration_Certificate_Name);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        objUnit.Registration_Certificate.CopyTo(stream);
                    }
                }
                if (objUnit.Pollution_Clearance_Certificate != null && objUnit.Pollution_Clearance_Certificate.Length > 0)

                {
                    Pollution_Clearance_Certificate_Name = Guid.NewGuid().ToString() + "_" + "Pol_" + objUnit.Pollution_Clearance_Certificate.FileName.Replace(" ", "");

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs");



                    var filePath = Path.Combine(uploadsFolder, Pollution_Clearance_Certificate_Name);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        objUnit.Pollution_Clearance_Certificate.CopyTo(stream);
                    }
                }
                if (objUnit.GST_Certificate != null && objUnit.GST_Certificate.Length > 0)

                {
                    GST_Certificate_Name = Guid.NewGuid().ToString() + "_" + "Gst_" + objUnit.GST_Certificate.FileName.Replace(" ", "");

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs");



                    var filePath = Path.Combine(uploadsFolder, GST_Certificate_Name);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        objUnit.GST_Certificate.CopyTo(stream);
                    }
                }
                if (objUnit.Pan_Certificate != null && objUnit.Pan_Certificate.Length > 0)

                {
                    Pan_Certificate_Name = Guid.NewGuid().ToString() + "_" + "Pan_" + objUnit.Pan_Certificate.FileName.Replace(" ", "");

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadsDocs");



                    var filePath = Path.Combine(uploadsFolder, Pan_Certificate_Name);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        objUnit.Pan_Certificate.CopyTo(stream);
                    }
                }

                objUnit.Registration_Certificate = null;
                objUnit.Pollution_Clearance_Certificate = null;
                objUnit.GST_Certificate = null;
                objUnit.Pan_Certificate = null;

                objUnit.Registration_Certificate_Name = Registration_Certificate_Name;
                objUnit.Pollution_Clearance_Certificate_Name = Pollution_Clearance_Certificate_Name;
                objUnit.GST_Certificate_Name = GST_Certificate_Name;
                objUnit.Pan_Certificate_Name = Pan_Certificate_Name;

                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/AddCompanyEntry", JsonConvert.SerializeObject(objUnit)));
                return objMessageEF;
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
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/AddCompanyUnitTagging", JsonConvert.SerializeObject(objUnit)));
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

        public List<Unitlist> CompanyTaggingList(MasterE objOpenModel)
        {
            try
            {
                List<Unitlist> objListCompany = new List<Unitlist>();
                objListCompany = JsonConvert.DeserializeObject<List<Unitlist>>(_objIHttpWebClients.PostRequest("CompanyRegistration/CompanyTaggingList", JsonConvert.SerializeObject(objOpenModel)));
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
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/UCompanyTagging", JsonConvert.SerializeObject(objOpenModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       public MessageEF DashboardMarquee(DashboardRequestEF objOpenModel)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("CompanyRegistration/DashboardMarquee", JsonConvert.SerializeObject(objOpenModel)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ProfileEF> Profile(DashboardRequestEF objOpenModel)
        {
            try
            {
                List<ProfileEF> objListChemical = new List<ProfileEF>();
                objListChemical = JsonConvert.DeserializeObject<List<ProfileEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/Profile", JsonConvert.SerializeObject(objOpenModel)));
                return objListChemical;
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
        public MessageEF SendEmail(EmailEF emailef)
        {
            MessageEF msg = new MessageEF();

            try
            {
                //string sender = "care@gyros.farm";
                //string password = "ocminoosyviyvblp";

                //string sender = "gyros.farmindia@gmail.com";
                //string password = "pyiudircghcffzeu";
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
                //


                //string smtpHost = "smtp.gmail.com";
                //int smtpPort = 587;
                //string smtpUsername = "officeddg@gmail.com";
                //string smtpPassword = "JANpath@10";


                //string fromEmail = "officeddg@gmail.com";
                //string toEmail = emailef.EmailAddress;

                //string subject = emailef.Subject;
                //string body = emailef.Message;

                //SmtpClient client = new SmtpClient(smtpHost, smtpPort);
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);


                //MailMessage message = new MailMessage(fromEmail, toEmail, subject, body);

                //// Send the email
                //client.Send(message);

                msg.Satus = "1";
                return msg;
            }
            catch (Exception ex)
            {
                msg.Satus = "0";
                return msg;
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
        public List<CompanyRegistrationEF> CompanyEntryGet(MasterEF objOpenModel)
        {
            try
            {
                List<CompanyRegistrationEF> objCompanyEntryGet = new List<CompanyRegistrationEF>();
                objCompanyEntryGet = JsonConvert.DeserializeObject<List<CompanyRegistrationEF>>(_objIHttpWebClients.PostRequest("CompanyRegistration/CompanyEntryGet", JsonConvert.SerializeObject(objOpenModel)));
                return objCompanyEntryGet;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

