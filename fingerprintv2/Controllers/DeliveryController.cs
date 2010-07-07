using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fpcore.Model;
using fingerprintv2.Services;
using fingerprintv2.Web;

namespace fingerprintv2.Controllers
{
    public class DeliveryController : Controller
    {
        [AuthenticationFilterAttr]
        public ActionResult delivery()
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
                sortDiretion = true;

            if (pageSize == null)
                pageSize = 25;


            List<UserAC> users = objectService.getSales(null, user);
            //query 
            List<Delivery> deliveries = objectService.getAllDeliveries(pageSize.Value, pageSize.Value *(pageIndex .Value -1), sortExpression, sortDiretion.Value, user);
            //set params

            int count = objectService.deliveryCount(" where IsDeleted = 0 ", user);
            int pageCount = count % pageSize.Value == 0 ? count / pageSize.Value : count / pageSize.Value + 1;
            ViewData.Add("deliveries", deliveries);
            ViewData.Add("sortExpression", sortExpression);
            ViewData.Add("sortDiretion", sortDiretion);
            ViewData.Add("pageIndex", pageIndex);
            ViewData.Add("pageSize", pageSize);
            ViewData.Add("pageCount", pageCount);
            ViewData.Add("count", count);
            ViewData.Add("users", users);

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

        public object updatedelivery(string id,string name,string value)
        {
            string result = string.Empty;
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            int objectid=0;
            int.TryParse(id, out objectid);

            Delivery delivery = objectService.getDeliveryById(objectid, user);

            if (name == "type")
                delivery.delivery_type = value;
            else if (name == "user")
            {
                UserAC u = objectService.getUserByID(int.Parse(value), user);
                delivery.handled_by = u;
            }
            else if(name =="status")
            {
                delivery.status = value;
            }
            
            if (delivery != null)
            {
                service.updateDelivery(delivery, user);
                result = "updated successfully !";
            }
            else
            {
                result = "updated failed !";
            }
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult New(int? objectid)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            if (objectid == null)
                objectid = 0;
            Delivery delivery = objectService.getDeliveryById(objectid.Value, user);
            ViewData.Add("delivery", delivery);
            return PartialView();
        }

        public object GetCustomers()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<CustomerContact> ccs = new List<CustomerContact>();
            ccs = objectService.getAllCustomerContact(" and ctype='default' ", user);
           
            return Json(ccs);
        }

        public object getUsers()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<UserAC> ccs = new List<UserAC>();
            ccs = objectService.getSales (null, user);

            return Json(ccs);
        }



        [AuthenticationFilterAttr]
        public ActionResult add(
            string objectid,
            string city,
            string companyname,
            string contact,
            string deadline,
            string district,
            string handleby,
            string height,
            string length,
            string mobile,
            string nonorder,
            string notes,
            string number,
            string partno,
            string remarks,
            string requestby,
            string street1,
            string street2,
            string street3,
            string tel,
            string updateby,
            string updatedate,
            string weight,
            string width,
            string code,
            string delivery_type
        )
        {
            int objid = 0;
            int.TryParse(objectid, out objid);

            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            Delivery delivery = objectService.getDeliveryById(objid, user);
            CustomerContact cc = new CustomerContact();
            Customer customer = objectService.getCustomerByCustomerID(code.Trim(), user);

            int handuserid=0;
            int.TryParse (handleby,out handuserid );
            UserAC handuser = objectService.getUserByID(handuserid, user);

            int requestuserid = 0;
            int.TryParse(requestby, out requestuserid);
            UserAC requestuser = objectService.getUserByID(requestuserid, user);

            DateTime dead = new DateTime();
            if (string.IsNullOrEmpty(deadline))
                dead = DateTime.Now;
            else
                dead = DateTime.Parse(deadline);

            if (customer == null)
                customer = new Customer();

            if (delivery != null)
            {

                delivery.assigned_by = user;
                delivery.deadline = dead;
                delivery.status = "processing";
                delivery.handled_by = handuser;
                delivery.height = height;
                delivery.isDeleted = false;
                delivery.length = length;
                delivery.non_order = nonorder;
                delivery.notes = notes;
                delivery.number = number;
                delivery.objectId = objid;
                delivery.part_no = partno;
                delivery.requested_by = requestuser;
                delivery.weight = weight;
                delivery.width = width;
                delivery.delivery_type = delivery_type;

                cc = delivery.contact;
                cc.city = city;
                cc.cid = code;
                cc.cname = companyname;
                cc.contact_person = contact;
                cc.createDate = DateTime.Now;
                cc.ctype = "normal";
                cc.customer = customer;
                cc.district = district;
                cc.tel = tel;
                cc.isDeleted = false;
                cc.mobile = mobile;
                cc.remarks = remarks;
                cc.street1 = street1;
                cc.street2 = street2;
                cc.street3 = street3;
                service.updateCustomerContact(cc, user);


                delivery.contact = cc;
                service.updateDelivery(delivery, user);
            }
            else
            {
                delivery = new Delivery();

                delivery.assigned_by = user;
                delivery.deadline = dead;
                delivery.status = "processing";
                delivery.handled_by = handuser;
                delivery.height = height;
                delivery.isDeleted = false;
                delivery.length = length;
                delivery.non_order = nonorder;
                delivery.notes = notes;
                delivery.number = number;
                delivery.objectId = objid;
                delivery.part_no = partno;
                delivery.requested_by = requestuser ;
                delivery.weight = weight;
                delivery.width = width;
                delivery.delivery_type = delivery_type;

                cc.city = city;
                cc.cid = code;
                cc.cname = companyname;
                cc.contact_person = contact;
                cc.createDate = DateTime.Now;
                cc.ctype = "normal";
                cc.customer = customer;
                cc.district = district;
                cc.tel = tel;
                cc.isDeleted = false;
                cc.mobile = mobile;
                cc.remarks = remarks;
                cc.street1 = street1;
                cc.street2 = street2;
                cc.street3 = street3;
                service.addCustomerContact(cc, user);


                delivery.contact = cc;
                service.addDelivery(delivery, user);
            }

            return RedirectToAction("delivery", "delivery");
        }

        public object deletedelivery(string ids)
        {
            string result = string.Empty;

            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            if (!String.IsNullOrEmpty(ids))
            {
                int id=0;
                int.TryParse (ids,out id);
                Delivery delivery =objectService .getDeliveryById (id,user);
                if (delivery != null)
                {
                    service.deleteDelivery(delivery, user).ToString();
                    result = "delete successfully ! ";
                }
                else
                    result = "delete failed ! ";
            }

            return Json(result);
        }
    }
}
