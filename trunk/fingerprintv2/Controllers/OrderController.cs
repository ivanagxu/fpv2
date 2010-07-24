using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;
using fingerprintv2.Web;
using fpcore.Model;
using fingerprintv2.Services;

namespace fingerprintv2.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        [AuthenticationFilterAttr]
        public ActionResult getOrder()
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

            List<PrintOrder> orders = objectService.getAllOrder(iLimit, iStart, sort, bSortDir, user);
            int count = objectService.countAllOrder(" where IsDeleted = 0 ", user);

            if (orders.Count == 0)
                return Content("{total:0,data:[]}");

            StringBuilder orderJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            for (int i = 0; i < orders.Count; i++)
            {
                if (i > 0)
                    orderJson.Append(",");
                orderJson.Append(JSONTool.getOrderJson(orders[i]));
            }
            orderJson.Append("]}");
            return Content(orderJson.ToString());
        }

        [AuthenticationFilterAttr]
        public ActionResult getOrderByPid()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String pid = Request.Params["pid"];

            PrintOrder order = objectService.getPrintOrderByID(pid, user);

            String orderJson = JSONTool.getOrderJson(order);

            return Content(orderJson.ToString());
        }

        [AuthenticationFilterAttr]
        public ActionResult getCustomer()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<Customer> customers = objectService.getAllCustomer(10000, 0, "", false, user);

            if (customers.Count == 0)
                return Content("{tags:[{id:'0',name:' '}]}");

            StringBuilder customerJson = new StringBuilder("{tags:[");
            for (int i = 0; i < customers.Count; i++)
            {
                if (i > 0)
                    customerJson.Append(",");
                customerJson.Append("{id:'").Append(customers[i].company_code).Append("',name:'").Append(customers[i].company_name == null ? "" : customers[i].company_name.Replace("'", "`")).Append("'}");
            }
            customerJson.Append("]}");
            return Content(customerJson.ToString());
        }
        [AuthenticationFilterAttr]
        public ActionResult getJobType()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            List<PrintJobCategory> categorys = objectService.getAllPrintJobCategory(user);

            StringBuilder jobTypeJson = new StringBuilder();
            jobTypeJson.Append("{tags:[");

            for(int i = 0 ; i < categorys.Count; i++)
            {
                if(i > 0)
                    jobTypeJson.Append(",");
                jobTypeJson.Append(JSONTool.getJobTypeJson(categorys[i]));
            }
            jobTypeJson.Append("]}");
           
            return Content(jobTypeJson.ToString());
        }
        [AuthenticationFilterAttr]
        public ActionResult getJobDetailByCategory()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String sCategoryId = Request.Params["categoryId"];
            PrintJobCategory category = objectService.getPrintJobCategoryByID(sCategoryId, user);

            if (category == null)
            {
                return Content("[]");
            }
            List<String> categoryItems = objectService.getCategoryItemCodesByCategory(category, user);
            List<PrintItemDetail> lookupItems = new List<PrintItemDetail>();

            StringBuilder jobDetailJson = new StringBuilder();
            jobDetailJson.Append("[");
            String lookupName = "";
            for (int i = 0; i < categoryItems.Count; i++)
            {
                lookupItems = objectService.getPrintJobLookupByCategory(categoryItems[i], user);
                if (i > 0)
                    jobDetailJson.Append(",");


                if (categoryItems[i] == "F4")
                    jobDetailJson.Append("{type:'").Append(i + 1).Append("',name:'%NAME%',others:[{id:'newjob-detail-other4_0',label:'Delivery Address 送貨地點',name:'Fdelivery_address'},{id:'newjob-detail-other4_1',label:'Delivery Date 送貨日期',name:'Fdelivery_date'}],items:[");
                else if(categoryItems[i] == "F5")
                    jobDetailJson.Append("{type:'").Append(i + 1).Append("',name:'%NAME%',others:[{id:'newjob-detail-other5_0',label:'Paper 用紙',name:'Fpaper'},{id:'newjob-detail-other5_1',label:'Color 印色',name:'Fcolor'}],items:[");
                else
                    jobDetailJson.Append("{type:'").Append(i + 1).Append("',name:'%NAME%',items:[");
                
                
                for (int j = 0; j < lookupItems.Count; j++)
                {
                    if (j > 0)
                        jobDetailJson.Append(",");
                    jobDetailJson.Append(JSONTool.getJobLookupItemsJson(lookupItems[j]));
                    lookupName = lookupItems[j].category_name;
                }
                jobDetailJson.Replace("%NAME%", lookupName);
                jobDetailJson.Append("]}");
            }
            jobDetailJson.Append("]");

            return Content(jobDetailJson.ToString());
        }

        [AuthenticationFilterAttr]
        public ActionResult addNewOrder()
        {
            try
            {
                //{orderNo=--&customer=&received_date=&customer_tel=&received_by=&customer_contact_person=&order_deadline=&remark=&update_by=ivan&newjobtype=
                String sOrderNo = Request.Params["orderNo"];
                String sCustomer = Request.Params["customer"];
                String sReceived_date = Request.Params["received_date"];
                String sCustomer_tel = Request.Params["customer_tel"];
                String sReceived_by = Request.Params["received_by"];
                String sCustomerContactPerson = Request.Params["customer_contact_person"];
                String sOrderDeadline = Request.Params["order_deadline"];
                String sRemark = Request.Params["remark"];
                String sUpdateBy = Request.Params["update_by"];
                String sNewJobType = Request.Params["newjobtype"];

                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                PrintOrder order = objectService.getPrintOrderByID(sOrderNo, user);
                if (order != null)
                {
                    Customer customer = objectService.getCustomerByCustomerID(sCustomer, user);

                    if (order.customer_contact == null)
                        order.customer_contact = new CustomerContact();
                    order.customer_contact.customer = customer;
                    order.customer_contact.contact_person = sCustomerContactPerson;
                    order.customer_contact.tel = sCustomer_tel;

                    order.order_deadline = sOrderDeadline;
                    order.received_date = DateTime.Parse(sReceived_date);
                    order.updateDate = DateTime.Now;
                    order.remarks = sRemark;
                    order.updateBy = user.user_name;

                    bool success = service.updateOrder(order, user);

                    if (success)
                    {
                        return Content("{success:true, result:\"Update success\"}");
                    }
                    else
                    {
                        return Content("{success:false, result:\"" + "Update order failed" + "\"}");
                    }
                }
                else
                {

                    order = Session["newOrder"] == null ? new PrintOrder() : (PrintOrder)Session["newOrder"];

                    Customer customer = objectService.getCustomerByCustomerID(sCustomer, user);
                    order.customer_contact = new CustomerContact();
                    order.customer_contact.customer = customer;
                    order.customer_contact.contact_person = sCustomerContactPerson;
                    order.customer_contact.tel = sCustomer_tel;

                    order.invoice_no = null;
                    order.order_deadline = sOrderDeadline;
                    order.status = PrintOrder.STATUS_PENDING;
                    try
                    {
                        order.received_by = objectService.getUserByID(int.Parse(sReceived_by), user);
                    }
                    catch (Exception e) { }
                    order.received_date = DateTime.Parse(sReceived_date);
                    order.updateDate = DateTime.Now;
                    order.remarks = sRemark;
                    try
                    {
                        order.sales_person = objectService.getUserByID(int.Parse(sReceived_by), user);
                    }
                    catch (Exception e) { }
                    order.updateBy = user.user_name;


                    if(order.received_by == null || order.order_deadline == "" || order.customer_contact == null)
                        return Content("{success:false, result:\"" + "Missing information such as sales ,deadline, customer" + "\"}");

                    Boolean success = service.newOrder(order, user);

                    if (success)
                    {
                        Session["newOrder"] = new PrintOrder();
                        return Content("{success:true, result:\"Update success\"}");
                    }
                    else
                    {
                        return Content("{success:false, result:\"" + "Create new order failed" + "\"}");
                    }
                }
            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + e.Message + "\"}");
            }
        }

        [AuthenticationFilterAttr]
        public ActionResult deleteOrder()
        {
            String sOrderNo = Request.Params["pid"];
            String pwd = Request.Params["pwd"];

            UserAC user = (UserAC)Session["user"];


            if(pwd != user.user_password)
                return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

            if (user.roles.Where(c => c.name == "system admin" || c.name == "order admin").Count() <= 0)
                return Content("{success:false, result:\"Sorry, You are not authorized to do this action.\"}");

            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            PrintOrder order = objectService.getPrintOrderByID(sOrderNo, user);

            if (order == null)
            {
                return Content("{success:false, result:\"Order is not found.\"}");
            
            }

            service.deleteOrder(order, user);

            return Content("{success:true, result:\"Update success\"}");
        }

        [AuthenticationFilterAttr]
        public ActionResult getSalesComboList()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            try
            {
                List<UserAC> users = objectService.getSales(null, user);

                if (users.Count == 0)
                    return Content("{tags:[{id:'0',name:' '}]}");

                StringBuilder usersJson = new StringBuilder("{tags:[");
                for (int i = 0; i < users.Count; i++)
                {
                    if (i > 0)
                        usersJson.Append(",");
                    usersJson.Append("{id:'").Append(users[i].objectId).Append("',name:'").Append(users[i].eng_name).Append("'}");
                }
                usersJson.Append("]}");

                return Content(usersJson.ToString());
            }
            catch (Exception e)
            {
                return Content("[[]]");
            }


        }
        [AuthenticationFilterAttr]
        public ActionResult getCustomerInfoByNo()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            try
            {
                String number = Request.Params["customer_no"];
                CustomerContact c = objectService.getCustomerContactByCode(number, "default", user);


                if (c != null)
                {
                    String profile = "{id:'" + c.customer.company_code + "',name:'" + c.customer.company_name + "',tel:'" + c.tel + "',contact:'" + c.contact_person + "'}";
                    return Content(profile);
                }

                return Content("{}");
            }
            catch (Exception e)
            {
                return Content("{}");
            }
        }
    }
}
