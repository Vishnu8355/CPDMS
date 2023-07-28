using CPDMS.Entities;
using CPDMS.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
  

namespace CPDMS.Controllers
{
    public class AccountController : Controller
    {
        AdminDcpcContext ent = new AdminDcpcContext();
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(TRegistrationDTO tRegistrationDTO)
        {
            //var db = Registration(tRegistrationDTO.NameOfUnit??null);
            var UserId = ent.TRegistrations.Where(a => a.UserId == tRegistrationDTO.UserId).FirstOrDefault();
            if (UserId != null)
            {
                TempData["Message"] = "User Id Already Exist";
                return RedirectToAction("Registration");
            }

            var data = new TRegistration()
            {
              NameOfUnit = tRegistrationDTO.NameOfUnit,
              CpciicCode = tRegistrationDTO.CpciicCode,
              UserId = tRegistrationDTO.UserId,
              Password = tRegistrationDTO.Password,
              Role = "UNIADM",
              MobileNo = tRegistrationDTO.MobileNo,
              EmailId = tRegistrationDTO.EmailId,
              MobileOtp = 12345,
              EmailOtp = 12345,
              EntryDate = DateTime.Now,
              IpAddress = Dns.GetHostAddresses(Dns.GetHostName())[3].ToString(),
              IsActive = "N",
              Islogin = "N"
            };
            ent.Add(data);
            ent.SaveChanges();
            return RedirectToAction("RegistrationOTP", new { Id = data.Id });
        }

        [HttpGet]
        public IActionResult RegistrationOTP(int Id)
        {
            TRegistrationDTO tRegistrationDTO = new TRegistrationDTO();
            tRegistrationDTO.Id = Id;
            return View(tRegistrationDTO);
        }
        [HttpPost]
        public IActionResult RegistrationOTP(TRegistrationDTO tRegistrationDTO)
        {
            var UserId = ent.TRegistrations.Where(a => a.Id == tRegistrationDTO.Id 
            && a.MobileOtp == tRegistrationDTO.MobileOtp 
            && a.EmailOtp == tRegistrationDTO.EmailOtp).FirstOrDefault();
            if (UserId == null)
            {
                TempData["Message"] = "Please Enter Valid OTP";
                return RedirectToAction("RegistrationOTP", new { Id = tRegistrationDTO.Id });
            }
            else
            {
                UserId.EmailOtpConfirm ="Y";
                UserId.MobileOtpConfirm = "Y";
                ent.SaveChanges();
                TempData["Messages"] = "Registration Successfull";
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult ComRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ComRegistration(TRegistrationDTO tRegistrationDTO)
        {
            //var db = Registration(tRegistrationDTO.NameOfUnit??null);
            var UserId = ent.TRegistrations.Where(a => a.UserId == tRegistrationDTO.UserId).FirstOrDefault();
            if (UserId != null)
            {
                TempData["Message"] = "User Id Already Exist";
                return RedirectToAction("Registration");
            }

            var data = new TRegistration()
            {
                NameOfUnit = tRegistrationDTO.NameOfUnit,
                CpciicCode = tRegistrationDTO.CpciicCode,
                UserId = tRegistrationDTO.UserId,
                Password = tRegistrationDTO.Password,
                Role = "COMADM",
                MobileNo = tRegistrationDTO.MobileNo,
                EmailId = tRegistrationDTO.EmailId,
                MobileOtp = 12345,
                EmailOtp = 12345,
                EntryDate = DateTime.Now,
                IpAddress = Dns.GetHostAddresses(Dns.GetHostName())[3].ToString(),
                IsActive = "N",
                Islogin = "N"
            };
            ent.Add(data);
            ent.SaveChanges();
            return RedirectToAction("ComRegistrationOTP", new { Id = data.Id });
        }

        [HttpGet]
        public IActionResult ComRegistrationOTP(int Id)
        {
            TRegistrationDTO tRegistrationDTO = new TRegistrationDTO();
            tRegistrationDTO.Id = Id;
            return View(tRegistrationDTO);
        }

        [HttpPost]
        public IActionResult ComRegistrationOTP(TRegistrationDTO tRegistrationDTO)
        {
            var UserId = ent.TRegistrations.Where(a => a.Id == tRegistrationDTO.Id
            && a.MobileOtp == tRegistrationDTO.MobileOtp
            && a.EmailOtp == tRegistrationDTO.EmailOtp).FirstOrDefault();
            if (UserId == null)
            {
                TempData["Message"] = "Please Enter Valid OTP";
                return RedirectToAction("ComRegistrationOTP", new { Id = tRegistrationDTO.Id });
            }
            else
            {
                UserId.EmailOtpConfirm = "Y";
                UserId.MobileOtpConfirm = "Y";
                ent.SaveChanges();
                TempData["Messages"] = "Registration Successfull";
                return RedirectToAction("Login");
            }
        }
    }
}
