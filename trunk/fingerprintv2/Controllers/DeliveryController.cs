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
            List<Delivery> deliveries = objectService.getAllDeliveries(pageIndex.Value, pageSize.Value, sortExpression, sortDiretion.Value, user);
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

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult New()
        {

            return PartialView();
        }

        public object GetCustomers()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<CustomerContact> ccs = new List<CustomerContact>();
            ccs = objectService.getAllCustomerContact(" and status='default' and ", user);
           
            return Json(ccs);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object add(
            int objectid,
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
            string code
        )
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            Delivery delivery = objectService.getDeliveryById(objectid, user);
            CustomerContact cc = new CustomerContact();
            Customer customer = objectService.getCustomerByCustomerID(code.Trim(), user);
            if (delivery != null)
            {
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
            }
            else
            {
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
            }

           

           

            return RedirectToAction("Index", "delivery");
        }       
    }
}
