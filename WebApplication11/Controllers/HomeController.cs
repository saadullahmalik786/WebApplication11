using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication11.InjectionDetection;

namespace WebApplication11.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckScript(string scriptInput)
        {
            if (string.IsNullOrWhiteSpace(scriptInput))
            {
                ViewBag.Result = "⚠️ Please enter a script to analyze.";
                ViewBag.Details = "";
                return View("Index");
            }

            try
            {
                List<string> detectedPatterns;
                bool isMalicious = ScriptAnalyzer.IsMalicious(scriptInput, out detectedPatterns);

                if (isMalicious)
                {
                    ViewBag.Result = "⚠️ Malicious Script Detected!";
                    ViewBag.Details = string.Join(", ", detectedPatterns);
                }
                else
                {
                    ViewBag.Result = "✅ Script is safe.";
                    ViewBag.Details = "No known malicious patterns detected.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = "❌ An error occurred during analysis.";
                ViewBag.Details = ex.Message;
            }

            return View("Index");
        }
    }
}
