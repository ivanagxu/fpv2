using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fingerprintv2.Web;
using fpcore.Model;
using fingerprintv2.Services;
using System.Text;

namespace fingerprintv2.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        [AuthenticationFilterAttr]
        public ActionResult customer()
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
        

            List<Customer> customers = objectService.getAllCustomer(iLimit, iStart, sort, bSortDir, user);
            int count = objectService.countCustomer(null, user);

            if (customers.Count() == 0)
                return Content("{total:0,data:[]}");

            StringBuilder jobJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            for (int i = 0; i < customers.Count; i++)
            {
                CustomerContact cc = objectService.getCustomerContactByCode(customers[i].company_code.Trim(), "default", user);
                if (i > 0)
                    jobJson.Append(",");
                jobJson.Append(JSONTool.getCustomerJson(customers[i], cc));
            }
            jobJson.Append("]}");
            return Content(jobJson.ToString());
        }

        [AuthenticationFilterAttr]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object addcustomer(string code, string name, string person, string tel, string address, string cid)
        {

            var result = string.Empty;
            bool bresult = false;
            try
            {
                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
                int objectid = 0;
                int.TryParse(cid, out objectid);
                var customer = objectService.getCustomerByID(objectid, user);
                string customer_code = string.Empty;


                if (customer != null)
                    customer_code = customer.company_code.Trim();
                var customer1 = objectService.getCustomerByCustomerID(code.Trim(), user);
                if (customer1 != null && customer == null)
                {
                    result = "has exist the company code !";
                    bresult = false;
                }
                else
                {
                    if (customer != null)
                    {
                        customer.company_code = code.Trim();
                        customer.company_name = name.Trim();
                        service.updateCustomer(customer, user);
                        result = "update information successfully !";
                        bresult = true;
                    }
                    else
                    {
                        customer = new Customer();
                        customer.company_code = code.Trim();
                        customer.company_name = name.Trim();
                        service.addCustomer(customer, user);
                        result = "add information successfully !";
                        bresult = true;
                    }

                    if (customer_code != string.Empty && customer_code.Trim() != code.Trim())
                    {
                        var customercontacts = objectService.getContactsByCode(customer_code.Trim(), user);

                        if (customercontacts.Count() > 0)
                        {
                            foreach (var contact in customercontacts)
                            {
                                contact.customer = customer;
                                service.updateCustomerContact(contact, user);
                            }
                        }
                    }

                    var cc = objectService.getCustomerContactByCode(code.Trim(), "default", user);
                    if (cc != null)
                    {
                        cc.address = address.Trim();
                        cc.contact_person = person.Trim();
                        cc.tel = tel.Trim();
                        cc.ctype = "default";
                        cc.customer = customer;
                        service.updateCustomerContact(cc, user);
                    }
                    else
                    {
                        cc = new CustomerContact();
                        cc.address = address.Trim();
                        cc.contact_person = person.Trim();
                        cc.tel = tel.Trim();
                        cc.ctype = "default";
                        cc.customer = customer;
                        service.addCustomerContact(cc, user);
                    }

                }
                return Content("{success:" + bresult.ToString ().ToLower () + ", result:\"" + result + "\"}");
            }
            catch (Exception ex)
            {
                return Content("{success:false, result:\"" + ex.Message + "\"}");
            }
        }

        [AuthenticationFilterAttr]
        public ActionResult DeleteCustomer()
        {
            try
            {
                String objectid = Request.Params["pid"];
                String pwd = Request.Params["pwd"];

                UserAC user = (UserAC)Session["user"];


                if (pwd != user.user_password)
                    return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");


                Customer customer = objectService.getCustomerByID(int.Parse(objectid), user);

                if (customer == null)
                {
                    return Content("{success:false, result:\"Customer is not found.\"}");

                }

                service.deleteCustomer(customer, user);

                return Content("{success:true, result:\"Update success\"}");
            }
            catch (Exception ex)
            {
                return Content("{success:false,result:\"" + ex.Message + "\"}");
            }
        }

    }
}
