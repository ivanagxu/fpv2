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

        [AcceptVerbs (HttpVerbs.Get )]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs (HttpVerbs.Get )]
        public PartialViewResult inventory(string sortExpression, bool? sortDiretion, int? pageIndex, int? pageSize)
        {

            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }
            //query 

            //set params
            int pageCount = 5;

            ViewData.Add("sortExpression", sortExpression);
            ViewData.Add("sortDiretion", sortDiretion);
            ViewData.Add("pageIndex", pageIndex);
            ViewData.Add("pageSize", pageSize);
            ViewData.Add("pageCount", pageCount);

            return  PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult History()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult New()
        {
            return PartialView();
        }
    }
}
