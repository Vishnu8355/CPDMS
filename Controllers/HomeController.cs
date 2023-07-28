using CPDMS.ActionFilter;
using CPDMS.Models;
using CPDMS.Utility;
using CPDMS.Web;
using CPDMSEF;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace CPDMS.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserLoginSession objUserLoginSession;
        private string strLclSalt;
        public readonly IOptions<KeyList> _objKeyList;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CommonUtility c = new CommonUtility();
            c.GetData();
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}