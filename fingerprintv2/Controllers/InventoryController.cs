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
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/

        [AcceptVerbs (HttpVerbs.Get )]
        public ActionResult inventory()
        {
            return View();
        }

        [AcceptVerbs (HttpVerbs.Get )]
        public PartialViewResult inventorydata(string query,string sortExpression, bool? sortDiretion, int? pageIndex, int? pageSize)
        {
            ViewData.Add("query", query);
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            if (pageIndex == null)
                pageIndex = 1;

            if (sortDiretion == null)
                sortDiretion = true;

            if (pageSize == null)
                pageSize = 25;
            if (!string.IsNullOrEmpty(query))
            {
                query = " and status='" + query + "'";
            }

            List<UserAC> users = objectService.getSales(null, user);
            //query 
            List<Inventory> inventories = objectService.getInventories(query,pageSize.Value, pageSize.Value * (pageIndex.Value - 1), sortExpression, sortDiretion.Value, user);
            //set params

            int count = objectService.inventoryCount(null, user);
            int pageCount = count % pageSize.Value == 0 ? count / pageSize.Value : count / pageSize.Value + 1;
            
            ViewData.Add("inventories", inventories);
            ViewData.Add("sortExpression", sortExpression);
            ViewData.Add("sortDiretion", sortDiretion);
            ViewData.Add("pageIndex", pageIndex);
            ViewData.Add("pageSize", pageSize);
            ViewData.Add("pageCount", pageCount);
            ViewData.Add("count", count);
            ViewData.Add("users", users);

            return  PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult History()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult New(int? objectid)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            if (objectid == null)
                objectid = 0;
            Inventory inventory = objectService.getInventoryById(objectid.Value, user);
            List<Consumption> consumptions = null;
            if (inventory != null)
            {
                consumptions = objectService.getConsumptions(null, user);
            }

            ViewData.Add("inventory", inventory);
            ViewData.Add("consumptions", consumptions);

            return PartialView();
        }
    }
}
