using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace fingerprintv2.Controllers
{
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/

        public ActionResult Index()
        {
            return RedirectToAction("inventory", "fingerprint");
        }
        public ActionResult inventory()
        {
            return RedirectToAction("inventory", "fingerprint");
        }
    }
}
