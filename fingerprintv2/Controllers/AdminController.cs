using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fingerprintv2.Web;
using fpcore.Model;

namespace fingerprintv2.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        [AuthenticationFilterAttr]
        public ActionResult Index()
        {
            return RedirectToAction("admin", "fingerprint");
        }
        [AuthenticationFilterAttr]
        public ActionResult admin()
        {
            return RedirectToAction("admin", "fingerprint");
        }
    }
}
