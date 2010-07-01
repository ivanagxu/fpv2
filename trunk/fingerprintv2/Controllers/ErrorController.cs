using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace fingerprintv2.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return RedirectToAction("error", "fingerprint");
        }
        public ActionResult error()
        {
            return RedirectToAction("error", "fingerprint");
        }
    }
}
