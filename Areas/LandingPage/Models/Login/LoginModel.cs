using CPDMS.Entities;
using CPDMS.Models.Utility;
using CPDMSEF;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mail;

namespace CPDMS.Areas.LandingPage.Models.Login
{
    public class LoginModel : ILoginModel
    {
        public readonly IOptions<KeyList> _objKeyList;
        IHttpWebClients _objIHttpWebClients;
        public LoginModel(IOptions<KeyList> objKeyList, IHttpWebClients objIHttpWebClients)
        {
            _objKeyList = objKeyList;
            _objIHttpWebClients = objIHttpWebClients;
        }

        public MessageEF ForgotPassword(LoginEF tRegistrationEF)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Login/ForgotPassword", JsonConvert.SerializeObject(tRegistrationEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public UserLoginSession LoginUser(LoginEF ObjloginEF)
        {
            UserLoginSession objUserLoginSession = new UserLoginSession();
            try
            {

                objUserLoginSession = JsonConvert.DeserializeObject<UserLoginSession>(_objIHttpWebClients.PostRequest("Login/UserLogin", JsonConvert.SerializeObject(ObjloginEF)));
                return objUserLoginSession;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                objUserLoginSession = null;
            }

        }

        public List<MenuEF> MenuList(menuonput objmenuonput)
        {
            List<MenuEF> objList = new List<MenuEF>();
            try
            {
                JsonConvert.SerializeObject(objmenuonput);
                objList = JsonConvert.DeserializeObject<List<MenuEF>>(_objIHttpWebClients.PostRequest("Login/MenuList", JsonConvert.SerializeObject(objmenuonput)));
                return objList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public MessageEF SendOTP(TRegistrationEF tRegistrationEF)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Login/SendOTP", JsonConvert.SerializeObject(tRegistrationEF)));
                return objMessageEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //implemented for testing purpose
        public List<MonthlyChemicalEF> ChemicalsList(MonthlyChemicalEF objreq)
        {
            List<MonthlyChemicalEF> objchem = new List<MonthlyChemicalEF>();
            objchem = JsonConvert.DeserializeObject <List < MonthlyChemicalEF> >(_objIHttpWebClients.PostRequest("UnitRegistration/ChemicalsList", JsonConvert.SerializeObject(objreq)));
            return objchem;
        }
        public MessageEF ForgotUserName(LoginEF tRegistrationEF)
        {
            try
            {
                MessageEF objMessageEF = new MessageEF();
                objMessageEF = JsonConvert.DeserializeObject<MessageEF>(_objIHttpWebClients.PostRequest("Login/ForgotUserName", JsonConvert.SerializeObject(tRegistrationEF)));
                return objMessageEF;
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
    
        public VisitcountEF visitcount(VisitcountEF visitcountEF)
        {
            try
            {
                VisitcountEF visitcount = new VisitcountEF();
                visitcount = JsonConvert.DeserializeObject<VisitcountEF>(_objIHttpWebClients.PostRequest("Login/visitcount", JsonConvert.SerializeObject(visitcountEF)));
                return visitcount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DashboardEF Dashboard(DashboardEF dashboardEF)
        {
            try
            {
                DashboardEF dashboard = new DashboardEF();
                dashboard = JsonConvert.DeserializeObject<DashboardEF>(_objIHttpWebClients.PostRequest("Login/Dashboard", JsonConvert.SerializeObject(dashboard)));
                return dashboard;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ContactEF Contact(ContactEF contact)
        {
            try
            {
                ContactEF contacts = new ContactEF();
                contacts = JsonConvert.DeserializeObject<ContactEF>(_objIHttpWebClients.PostRequest("Login/Contact", JsonConvert.SerializeObject(contact)));
                return contacts;
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

        public feedbackEF feedback(feedbackEF feedbackE)
        {
            try
            {
                feedbackEF feedbackEF = new feedbackEF();
                feedbackEF = JsonConvert.DeserializeObject<feedbackEF>(_objIHttpWebClients.PostRequest("Login/feedback", JsonConvert.SerializeObject(feedbackE)));
                return feedbackEF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
