using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fpcore.Model;
using fingerprintv2.Services;

namespace fingerprintv2.Controllers
{
    public class DeliveryController : Controller
    {
       
        public ActionResult Index()
        {
         
            return View();
          //  return RedirectToAction("delivery", "fingerprint");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult DeliveryData(string sortExpression, bool? sortDiretion, int? pageIndex, int? pageSize)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            if (pageIndex == null)
                pageIndex = 1;

            if (sortDiretion == null)
                sortDiretion = false;

            if (pageSize == null)
                pageSize = 25;


            //query 
            List<Delivery> deliveries= objectService.getAllDeliveries(pageIndex.Value, pageSize.Value, sortExpression, sortDiretion.Value, user);
            //set params
          
            int pageCount = 5;
            ViewData.Add("deliveries", deliveries);
            ViewData.Add("sortExpression", sortExpression);
            ViewData.Add("sortDiretion", sortDiretion);
            ViewData.Add("pageIndex", pageIndex);
            ViewData.Add("pageSize", pageSize);
            ViewData.Add("pageCount", pageCount);

            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult Archives(string sortExpression, bool? sortDiretion, int? pageIndex, int? pageSize)
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
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get )]
        public PartialViewResult New()
        {
            return PartialView();
        }

    }
}
