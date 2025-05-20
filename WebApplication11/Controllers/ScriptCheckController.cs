using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication11.InjectionDetection;

namespace WebApplication11.Controllers
{
    public class ScriptCheckController : Controller
    {
        [HttpPost]
        public JsonResult AnalyzeScript(string input)
        {
            var isMalicious = ScriptAnalyzer.IsMalicious(input, out var detected);
            return Json(new
            {
                IsMalicious = isMalicious,
                Patterns = detected
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
