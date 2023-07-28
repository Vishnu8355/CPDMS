using CPDMSEF;

namespace CPDMS.Areas.LandingPage.Models.Login
{
    public interface ILoginModel
    {
        UserLoginSession LoginUser(LoginEF ObjloginEF);
        List<MenuEF> MenuList(menuonput objmenuonput);

        MessageEF ForgotPassword(LoginEF tRegistrationEF);
        MessageEF SendOTP(TRegistrationEF tRegistrationEF);
        public List<MonthlyChemicalEF> ChemicalsList(MonthlyChemicalEF objreq);
        MessageEF ForgotUserName(LoginEF tRegistrationEF);
        MessageEF SendEmail(EmailEF emailef);
        VisitcountEF visitcount(VisitcountEF visitcountEF);
        DashboardEF Dashboard(DashboardEF dashboardEF);

        ContactEF Contact(ContactEF contact);
        List<FinYear> GetFinancialYear(MonthlyChemicalEF objOpenModel);
        List<Month> GetMonth(MonthlyChemicalEF objOpenModel);
        feedbackEF feedback(feedbackEF feedback);

    }
}
