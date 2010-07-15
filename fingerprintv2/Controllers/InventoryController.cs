using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fpcore.Model;
using fingerprintv2.Services;
using System.Text;
using fingerprintv2.Web;

namespace fingerprintv2.Controllers
{
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/

       [AuthenticationFilterAttr]
        public ActionResult inventory()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String start = Request.Params["start"];
            String limit = Request.Params["limit"];
            String sort = Request.Params["sort"];
            String sortDir = Request.Params["dir"];

            int iStart = int.Parse(start);
            int iLimit = int.Parse(limit);
            bool bSortDir = sortDir == "DESC";


            //  List<UserAC> users = objectService.getSales(null, user);
            //query 
            List<Inventory> inventories = objectService.getInventories("",iLimit, iStart, sort, bSortDir, user);
            //set params
            int count = objectService.inventoryCount(null, user);

            if (inventories.Count() == 0)
                return Content("{total:0,data:[]}");

            StringBuilder deliveryJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            for (int i = 0; i < inventories.Count; i++)
            {
                if (i > 0)
                    deliveryJson.Append(",");
                deliveryJson.Append(JSONTool.getDeliveryJson(inventories[i]));
            }
            deliveryJson.Append("]}");

            return Content(deliveryJson.ToString());
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
