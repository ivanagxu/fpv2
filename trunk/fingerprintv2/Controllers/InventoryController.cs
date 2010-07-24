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
                deliveryJson.Append(JSONTool.getInventoryJson(inventories[i]));
            }
            deliveryJson.Append("]}");

            return Content(deliveryJson.ToString());
        }

       [AuthenticationFilterAttr]
       public object addconsumption(string objectid, string inventoryid, string total, string totalunit, string store, string storeunit, string used, string usedunit, string asdate)
       {
           int conid = 0;
           int iid = 0;

           DateTime date = DateTime.Now;
           DateTime.TryParse(asdate, out date);

           int.TryParse(objectid, out conid);
           int.TryParse(inventoryid, out iid);

           string result = string.Empty;

           try
           {
               UserAC user = (UserAC)Session["user"];
               String pwd = Request.Params["pwd"];
               IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
               IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

               Consumption consumption = objectService.getconsumption(conid, user);
               Inventory inventory = objectService.getInventoryById(iid, user);

               if (inventory == null)
                   throw new Exception("This inventory is not exist !");

               if (consumption == null)
                   consumption = new Consumption();

               consumption.asdate = date;
               consumption.total = total;
               consumption.totalunit = totalunit;
               consumption.store = store;
               consumption.storeunit = storeunit;
               consumption.used = used;
               consumption.usedunit = usedunit;
               consumption.inventory = inventory;

               if (conid == 0)
               {
                   service.addConsumption(consumption, user);
                   result = "Add Successfully!";
               }
               else
               {
                   service.updateConsumption(consumption, user);
                   result = "Update Successfully !";
               }


               return Content("{success:true,result:\"" + result + "\",objectid:\"" + objectid.ToString() + "\"}");
           }
           catch (Exception ex)
           {
               return Content("{success:false,result:\"" + ex.Message + "\",objectid:\"--\"}");
           }
       }

       [AuthenticationFilterAttr]
       public object add(string cid, string category, string productno, string productnameen, string productnamecn,
                                string dimension, string unit, string unitcost, string receivedby, string deadline, string receiveddate,
                                string person, string tel, string remark,string description,string quantity)
       {
           int objectid = 0;
           int uid =0;
           int.TryParse(cid, out objectid);
           int.TryParse (receivedby ,out uid );
           string result = "";
           DateTime orderdeadline = DateTime.Now;
           DateTime.TryParse(deadline, out orderdeadline);

           DateTime rd=DateTime.Now ;
           DateTime .TryParse (receiveddate ,out rd);

       

           try
           {
               UserAC user = (UserAC)Session["user"];
               String pwd = Request.Params["pwd"];
               IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
               IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

               Inventory inventory = objectService.getInventoryById(objectid, user);
               UserAC ru = objectService.getUserByID(uid, user);

               var inventories = objectService.getInventories(" and productno='" + productno + "' and inventory.objectid <> '" + objectid + "'", 100, 0, null, false, user);

               if (inventories.Count() > 0)
                   throw new Exception("Product No. Exist !");
              

               if (inventory == null)
                   inventory = new Inventory();

               inventory.category = category;
               inventory.contactperson = person;
               inventory.description = description;
               inventory.dimension = dimension;
               inventory.orderdeadline = orderdeadline;
               inventory.productnamecn = productnamecn;
               inventory.productnameen = productnameen;
               inventory.productno = productno;
               inventory.receiveddate = rd;
               inventory.remark = remark;
               inventory.quantity = quantity;
               inventory.Tel = tel;
               inventory.unit = unit;
               inventory.unitcost = unitcost;
               if (ru != null)
                   inventory.receivedby = ru;

               if (objectid == 0)
               {
                   objectid = service.addInventory(inventory, user);
                   result = "Add Successfully!";
               }
               else
               {
                   service.updateInventory(inventory, user);
                   result = "Update Successfully !";
               }


               return Content("{success:true,result:\"" + result + "\",objectid:\"" + objectid.ToString() + "\"}");
           }
           catch (Exception ex)
           {
               return Content("{success:false,result:\"" + ex.Message.Replace("'", "\\\'").Replace("\r\n", "\\\r\\\n") + "\",objectid:\"--\"}");
           }
       }

       [AuthenticationFilterAttr]
       public object deleteinventory(string ids)
       {
           string result = string.Empty;
           try
           {
               UserAC user = (UserAC)Session["user"];
               String pwd = Request.Params["pwd"];
               IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
               IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

               if (pwd != user.user_password)
                   return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

               if (!String.IsNullOrEmpty(ids))
               {
                   int id = 0;
                   int.TryParse(ids, out id);
                   Inventory  inventory = objectService.getInventoryById(id, user);
                   if (inventory != null)
                   {
                       service.deleteInventory(inventory, user).ToString();
                       result = "delete successfully ! ";
                       return Content("{success:true,result:\"" + result + "\"}");
                   }
                   else
                   {
                       result = "delete failed ! ";
                       return Content("{success:false,result:\"" + result + "\"}");
                   }
               }
               else
               {
                   return Content("{success:false,result:\"" + "object id is null" + "\"}");
               }
           }
           catch (Exception ex)
           {
               return Content("{success:false,result:\"" + ex.Message + "\"}");
           }
       }

       [AuthenticationFilterAttr]
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

       [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult History()
        {
            return PartialView();
        }

       [AuthenticationFilterAttr]
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

       [AuthenticationFilterAttr]
        public object getconsumptions(string cid)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            int inventoryid=0;
            int.TryParse (cid,out inventoryid );
            List<Consumption> css = objectService.getConsumptions(" and inventoryid='" + inventoryid + "' ", user);

            if (css==null)
                return Content("{total:0,data:[]}");

            StringBuilder deliveryJson = new StringBuilder("{total:0,data:[");
            for (int i = 0; i < css.Count; i++)
            {
                if (i > 0)
                    deliveryJson.Append(",");
                deliveryJson.Append(JSONTool.getConsumptionJson(css[i]));
            }
            deliveryJson.Append("]}");

            return Content(deliveryJson.ToString());

        }

       [AuthenticationFilterAttr]
        public object deleteconsumption(string ids)
        {
            string result = string.Empty;
            try
            {
                UserAC user = (UserAC)Session["user"];
                String pwd = Request.Params["pwd"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                if (pwd != user.user_password)
                    return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

                if (!String.IsNullOrEmpty(ids))
                {
                    int id = 0;
                    int.TryParse(ids, out id);
                    Consumption consumption = objectService.getconsumption(id, user);
                    if (consumption != null)
                    {
                        service.deleteConsumption(consumption, user).ToString();
                        result = "delete successfully ! ";
                        return Content("{success:true,result:\"" + result + "\"}");
                    }
                    else
                    {
                        result = "delete failed ! ";
                        return Content("{success:false,result:\"" + result + "\"}");
                    }
                }
                else
                {
                    return Content("{success:false,result:\"" + "object id is null" + "\"}");
                }
            }
            catch (Exception ex)
            {
                return Content("{success:false,result:\"" + ex.Message + "\"}");
            }
        }
    }
}
