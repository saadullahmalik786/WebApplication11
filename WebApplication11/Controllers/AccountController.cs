using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication11.InjectionDetection;
using System;
using System.Net;
using System.Net.Mail;

namespace WebApplication11.Controllers
{
    public class AccountController : Controller
    {
        // Simulated login check
        private bool ValidateUser(string username, string password)
        {
            return username == "admin" && password == "1234";
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ValidateUser(username, password))
            {
                // Generate random 6-digit OTP
                Random rand = new Random();
                int otp = rand.Next(100000, 999999);
                Session["OTP"] = otp;
                Session["User"] = username;

                // Send OTP to user (update with real email)
                SendOtpEmail("youremail@example.com", otp);

                return RedirectToAction("VerifyOTP");
            }

            ViewBag.Error = "Invalid login credentials";
            return View();
        }

        public ActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOTP(string inputOtp)
        {
            if (Session["OTP"] != null && inputOtp == Session["OTP"].ToString())
            {
                Session["IsAuthenticated"] = true;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid OTP.";
            return View();
        }

        private void SendOtpEmail(string toEmail, int otp)
        {
            var fromEmail = "youremail@example.com";
            var fromPassword = "yourpassword"; 

            MailMessage message = new MailMessage(fromEmail, toEmail);
            message.Subject = "Your 2FA OTP Code";
            message.Body = $"Your OTP code is: {otp}";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential(fromEmail, fromPassword);
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}
