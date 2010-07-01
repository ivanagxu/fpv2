using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace fingerprintv2.Controllers
{
    public class DeliveryController : Controller
    {
        //
        // GET: /Delivery/

        public ActionResult Index()
        {
            return RedirectToAction("delivery", "fingerprint");
        }
        public ActionResult delivery()
        {
            return RedirectToAction("delivery", "fingerprint");
        }

    }
}
