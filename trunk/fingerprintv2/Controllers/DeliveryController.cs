using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fpcore.Model;
using fingerprintv2.Services;
using fingerprintv2.Web;
using System.Text;

namespace fingerprintv2.Controllers
{
    public class DeliveryController : Controller
    {
        //[AuthenticationFilterAttr]
        //public ActionResult delivery()
        //{

        //    return View();
        //    //  return RedirectToAction("delivery", "fingerprint");
        //}

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
            List<Delivery> deliveries = objectService.getAllDeliveries("",pageSize.Value, pageSize.Value * (pageIndex.Value - 1), sortExpression, sortDiretion.Value, user);
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
        [AuthenticationFilterAttr]
        public ActionResult Delivery()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            string query = "";

            String start = Request.Params["start"];
            String limit = Request.Params["limit"];
            String sort = Request.Params["sort"];
            String sortDir = Request.Params["dir"];

            String jt = Request.Params["jt"];
            String js = Request.Params["js"];
            String ft = Request.Params["ft"];
            String fv = Request.Params["fv"];

            if (jt != null && jt != string.Empty)
            {
                query = " and delivery_type='" + jt + "' ";
            }

            if (js != null && js != string.Empty)
            {
                query = query + " and status='" + js + "' ";
            }

            if (fv != null && (fv + "").Trim() != "")
            {
                if (ft == "customer_code")
                    query = query + " and contact in (select distinct cc.ObjectId from Customer_Contact cc inner join Customer c on cc.cid = c.company_code and c.company_code like '%" + fv + "%') ";
                if (ft == "customer_name")
                    query = query + " and contact in (select distinct cc.ObjectId from Customer_Contact cc inner join Customer c on cc.cid = c.company_code and c.company_name like '%" + fv + "%') ";
                if (ft == "deliveryID")
                    query = query + " and Delivery.ObjectId like '%" + fv + "%' ";
                if (ft == "jobno")
                    query = query + " and number like '%" + fv + "%' ";
            }


            int iStart = int.Parse(start == null ? "0" : start);
            int iLimit = int.Parse(limit == null ? "20" : limit);
            bool bSortDir = sortDir == "DESC";


          //  List<UserAC> users = objectService.getSales(null, user);
            //query 
            List<Delivery> deliveries = objectService.getAllDeliveries(query,iLimit, iStart, sort, bSortDir, user);
            //set params
            int count = objectService.deliveryCount(" where IsDeleted = 0 "+query, user);

            if (deliveries.Count() == 0)
                return Content("{total:0,data:[]}");

            StringBuilder deliveryJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            for (int i = 0; i < deliveries.Count; i++)
            {
                if (i > 0)
                    deliveryJson.Append(",");
                if (deliveries[i].customer != null)
                {
                    deliveries[i].contact = objectService.getCustomerContactByCode(deliveries[i].customer.company_code, "default", user);
                }
                if (deliveries[i].contact == null)
                    deliveries[i].contact = new CustomerContact();

                deliveryJson.Append(JSONTool.getDeliveryJson(deliveries[i]));
            }
            deliveryJson.Append("]}");

            return Content(deliveryJson.ToString());
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
                return Content("{success:true,result:\"updated successfully !\"}");
            }
            else
            {
                return Content("{success:failed,result:\"updated failed !\"}");
            }
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

          //  List<CustomerContact> ccs = new List<CustomerContact>();
          //  ccs = objectService.getAllCustomerContact(" and ctype='default' ", user);
            string query = Request["query"];
            query = "  where isdeleted = 0  and company_code like '%" + query + "%' ";
            return getdefaultcustomerbyquery(query);
        }

        public object getcustomersbyname()
        {
            string query = Request["query"];
            query = "  where isdeleted = 0  and company_name like '%" + query + "%' ";
            return getdefaultcustomerbyquery(query);
        }

        private object getdefaultcustomerbyquery(string query)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<Customer> customers = objectService.getDefaultCustomers(query, 10, 0, null, true, user);

            if (customers.Count == 0)
                return Content("{tags:[{id:'0',name:' '}]}");

            StringBuilder usersJson = new StringBuilder("{tags:[");
            for (int i = 0; i < customers.Count; i++)
            {
                CustomerContact cc = objectService.getCustomerContactByCode(customers[i].company_code, "default", user);
                if (i > 0)
                    usersJson.Append(",");

                if (cc == null)
                    cc = new CustomerContact();
                usersJson.Append("{code:'").Append(customers[i].company_code).Append("',")
                     .Append("objectid:'").Append(customers[i].objectId).Append("',");
                if (cc.street1 != null && cc.street1.Trim() != string.Empty)
                    usersJson.Append("street1:'").Append(cc.street1).Append("',");
                else
                    usersJson.Append("street1:'").Append(cc.street1 == null ? string.Empty : cc.street1.ToString().Replace("'", "\\\'")).Append("',");
                usersJson.Append("street2:'").Append(cc.street2 == null ? string.Empty : cc.street2.ToString().Replace("'", "\\\'")).Append("',")
                 .Append("street3:'").Append(cc.street3 == null ? string.Empty : cc.street3.ToString().Replace("'", "\\\'").Replace("\n", "").Replace("\r", "")).Append("',")
                .Append("district:'").Append(cc.district).Append("',")
                .Append("city:'").Append(cc.city).Append("',")
                .Append("contact:'").Append(cc.contact_person).Append("',")
                .Append("tel:'").Append(cc.tel).Append("',")
                .Append("mobile:'").Append(cc.mobile).Append("',")
                .Append("remark:'").Append(cc.remarks).Append("',")
                .Append("name:'").Append(customers[i].company_name).Append("'}");
            }
            usersJson.Append("]}");

            return Content(usersJson.ToString());
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
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object add(
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
            string delivery_type,
            string status,
            string goods_type
        )
        {
            try
            {
                int objid = 0;
                int.TryParse(objectid, out objid);
                var q = Request["width"];
                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                Delivery delivery = objectService.getDeliveryById(objid, user);
            //    CustomerContact cc = new CustomerContact();
            //    if (code == null)
            //        throw new Exception("null contact code !");

                Customer customer = objectService.getCustomerByCustomerID(code.Trim(), user);

            //    if (customer == null)
          //          throw new Exception("this customer is not exist,please input exist customer .");

                if (customer == null)
                    customer = new Customer();

                int handuserid = 0;
                int.TryParse(handleby, out handuserid);
                UserAC handuser = objectService.getUserByID(handuserid, user);

                int requestuserid = 0;
                int.TryParse(requestby, out requestuserid);
                UserAC requestuser = objectService.getUserByID(requestuserid, user);

                DateTime dead = new DateTime();
                if (string.IsNullOrEmpty(deadline))
                    dead = DateTime.Now;
                else
                {
                    var m = deadline.Substring(deadline.Length - 2);
                    if (m.ToLower().Contains("pm") || m.ToLower().Contains("am"))
                    {

                        var datetime = deadline.Split(' ');
                        var date = datetime[0].Split('/');
                        var time = datetime[1].Replace(m, string.Empty).Split(':');

                        if (m.ToLower().Trim() == "pm")
                        {
                            dead = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]) + 12, int.Parse(time[1]), 0);
                        }
                        else
                        {
                            dead = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]), int.Parse(time[1]), 0);

                        }
                    }
                    else
                    {
                        dead = DateTime.Now;
                    }
                }

               // if (customer == null)
              //      customer = new Customer();

                if (delivery != null)
                {

                    delivery.assigned_by = user;
                    delivery.deadline = dead;
                    delivery.status = "processing";
                    delivery.handled_by = handuser;
                    delivery.height = height;
                    delivery.isDeleted = false;
                    delivery.length = length;
                    delivery.goods_type = goods_type;
                    delivery.non_order = nonorder;
                    delivery.notes = notes;
                    delivery.number = number;
                    delivery.objectId = objid;
                    delivery.part_no = partno;
                    delivery.requested_by = requestuser;
                    delivery.weight = weight;
                    delivery.width = width;
                    delivery.delivery_type = delivery_type;
                    delivery.status = status;
                    delivery.remarks = remarks;

                    delivery.customer = customer;

                    //cc = delivery.contact;
                    //if (cc != null)
                    //{
                    //    cc.city = city;
                    //    cc.cid = code;
                    //    cc.cname = companyname;
                    //    cc.contact_person = contact;
                    //    cc.createDate = DateTime.Now;
                    //    cc.ctype = "normal";
                    //    cc.customer = customer;
                    //    cc.district = district;
                    //    cc.tel = tel;
                    //    cc.isDeleted = false;
                    //    cc.mobile = mobile;
                    // //   cc.remarks = remarks;
                    //    cc.street1 = street1;
                    //    cc.street2 = street2;
                    //    cc.street3 = street3;
                    //    service.updateCustomerContact(cc, user);
                    //}
                    //else
                    //{
                    //    cc = new CustomerContact();
                    //    cc.city = city;
                    //    cc.cid = code;
                    //    cc.cname = companyname;
                    //    cc.contact_person = contact;
                    //    cc.createDate = DateTime.Now;
                    //    cc.ctype = "normal";
                    //    cc.customer = customer;
                    //    cc.district = district;
                    //    cc.tel = tel;
                    //    cc.isDeleted = false;
                    //    cc.mobile = mobile;
                    // //   cc.remarks = remarks;
                    //    cc.street1 = street1;
                    //    cc.street2 = street2;
                    //    cc.street3 = street3;
                    //    service.addCustomerContact(cc, user);
                    //}


                   // delivery.contact = cc;
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
                    delivery.requested_by = requestuser;
                    delivery.weight = weight;
                    delivery.width = width;
                    delivery.delivery_type = delivery_type;
                    delivery.remarks =remarks ;
                    delivery.goods_type = goods_type;

                    delivery.customer = customer;

                  //  cc.city = city;
                  //  cc.cid = code;
                  //  cc.cname = companyname;
                  //  cc.contact_person = contact;
                  //  cc.createDate = DateTime.Now;
                  //  cc.ctype = "normal";
                  //  cc.customer = customer;
                  //  cc.district = district;
                  //  cc.tel = tel;
                  //  cc.isDeleted = false;
                  //  cc.mobile = mobile;
                  ////  cc.remarks = remarks;
                  //  cc.street1 = street1;
                  //  cc.street2 = street2;
                  //  cc.street3 = street3;
                  //  service.addCustomerContact(cc, user);


                  //  delivery.contact = cc;
                    service.addDelivery(delivery, user);
                }

             

                return Content("{success:true,result:\"successfully !\",objectid:" + delivery.objectId + "}");
            }
            catch (Exception ex)
            {
                return Content("{success:false,result:\"" + ex.Message + "\",objectid:" + 0 + "}");
            }
        }

        public object deletedelivery(string ids)
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

                if (user.roles.Where(c => c.name == "system admin" ||c.name =="delivery admin").Count() <= 0)
                    return Content("{success:false, result:\"Sorry, You are not authorized to do this action.\"}");

                if (!String.IsNullOrEmpty(ids))
                {
                    int id = 0;
                    int.TryParse(ids, out id);
                    Delivery delivery = objectService.getDeliveryById(id, user);
                    if (delivery != null)
                    {
                        service.deleteDelivery(delivery, user).ToString();
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
            catch(Exception ex)
            {
                return Content("{success:false,result:\"" + ex.Message + "\"}");
            }
        }

        public object GetOrderItemsDetails()
        {
            string query = Request["query"];

            UserAC user = (UserAC)Session["user"];
            IFPObjectService fs = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<PrintOrder> pos = fs.getAllOrder("  where isdeleted = 0  and pid like '%" + query + "%'", 20, 1, null, false, user);

            StringBuilder jobsJson = new StringBuilder("{tags:[");
            foreach (var order in pos)
            {
               
                jobsJson.Append("{pid:'").Append(order.pid).Append("',");
                if (order.customer_contact != null)
                {
                    jobsJson.Append("code:'").Append(order.customer_contact.customer.company_code).Append("',");
                    jobsJson.Append("name:'").Append(order.customer_contact.customer.company_name).Append("',");
                    jobsJson.Append("person:'").Append(order.customer_contact.contact_person).Append("',");
                    jobsJson.Append("address:'").Append(order.customer_contact.address).Append("',");
                    jobsJson.Append("city:'").Append(order.customer_contact.city).Append("',");
                    jobsJson.Append("district:'").Append(order.customer_contact.district).Append("',");
                    jobsJson.Append("email:'").Append(order.customer_contact.email).Append("',");
                    jobsJson.Append("fax:'").Append(order.customer_contact.fax).Append("',");
                    jobsJson.Append("mobile:'").Append(order.customer_contact.mobile).Append("',");
                    jobsJson.Append("street1:'").Append(order.customer_contact.street1).Append("',");
                    jobsJson.Append("street2:'").Append(order.customer_contact.street2).Append("',");
                    jobsJson.Append("street3:'").Append(order.customer_contact.street3).Append("',");
                    jobsJson.Append("tel:'").Append(order.customer_contact.tel).Append("',");
                }
                List<PrintItem> items = fs.getPrintJobByOrder(order, user);         
                jobsJson.Append("item_details:'");
                foreach (var job in items)
                {
                    String itemType = "";
                    for (int i = 0; i < job.print_job_items.Count; i++)
                    {
                        if (itemType != job.print_job_items[i].category_name)
                        {
                            jobsJson.Append("\\n").Append(job.print_job_items[i].category_name).Append(" : ");
                            itemType = job.print_job_items[i].category_name;
                        }
                        jobsJson.Append(job.print_job_items[i].code_desc).Append(" ");
                    }

                }
                jobsJson.Append("'},");
            }
            if (pos.Count() > 0)
            {
                jobsJson = jobsJson.Remove(jobsJson.Length-1, 1);
            }
         
            jobsJson.Append("]}");
            return Content(jobsJson.ToString());
        }

        public object getcontactbydeliveryid(string cid,string did)
        {

            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
          
            int deliveryid = 0;
            int.TryParse(did, out deliveryid);
           
            List<CustomerContact> css = objectService.getAllCustomerContact(" and cid='" + cid + "' and deliveryid!=0 and deliveryid='" + deliveryid + "' ", user);
            Customer customer = objectService.getCustomerByCustomerID(cid, user);
           
            if (css == null)
                return Content("{total:0,data:[]}");

            StringBuilder deliveryJson = new StringBuilder("{total:0,data:[");

            for (int i = 0; i < css.Count; i++)
            {
                if (i > 0)
                    deliveryJson.Append(",");
                deliveryJson.Append(JSONTool.getCustomerJson(customer, css[i]));
            }

            deliveryJson.Append("]}");

            return Content(deliveryJson.ToString());
        }

        [AuthenticationFilterAttr]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object addcustomer( string objectid, string name, string street1, string street2, string street3, string city,string district, string contact, string tel, string mobile,
                            string customerid)
        {

            var result = string.Empty;
            try
            {

                int deliveryid = 0;
                int.TryParse(objectid, out deliveryid);
                if (deliveryid == 0)
                {
                    throw new Exception("Please add a delivery before adding customer contact!");
                }
                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                var customer = objectService.getCustomerByCustomerID(customerid, user);
                if (customer == null)
                {
                    throw new Exception("The customer is not exist!");
                }
                string customer_code = string.Empty;
               
                

                CustomerContact cc = new CustomerContact();

                cc = new CustomerContact();
                cc.customer = customer;
                cc.street1 =street1 ;
                cc.street2 =street2 ;
                cc.street3 =street3 ;
                cc.contact_person = contact;
                cc.tel = tel.Trim();
                cc.ctype = "normal";
                cc.customer = customer;
                cc.deliveryid = deliveryid;
                cc.city = city;
                cc.district = district;
                cc.mobile = mobile;
                service.addCustomerContact(cc, user);

                Delivery de = objectService.getDeliveryById(deliveryid, user);
                if (de != null)
                {
                    if (de.customer != null && de.customer.objectId != customer.objectId)
                    {
                        de.customer = customer;
                        service.updateDelivery(de, user);
                    }
                }

                return Content("{success:true, result:\"" + "add successfully!" + "\"}");
            }
            catch (Exception ex)
            {
                return Content("{success:false, result:\"" + ex.Message + "\"}");
            }
        }

        [AuthenticationFilterAttr]
        public object deletecustomercontact()
        {
            try
            {
                String objectid = Request.Params["pid"];
              //  String pwd = Request.Params["pwd"];

                UserAC user = (UserAC)Session["user"];


                //if (pwd != user.user_password)
                //    return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

                //if (user.roles.Where(c => c.name == "system admin").Count() <= 0)
                //    return Content("{success:false, result:\"Sorry, You are not authorized to do this action.\"}");

                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");


                CustomerContact customer = objectService.getCustomerContactByID(int.Parse(objectid), user);

                if (customer == null)
                {
                    return Content("{success:false, result:\"Customer is not found.\"}");

                }

                service.deleteCustomerContact(customer, user);

                return Content("{success:true, result:\"Update success\"}");
            }
            catch (Exception ex)
            {
                return Content("{success:false,result:\"" + ex.Message + "\"}");
            }
        }
    }
}
