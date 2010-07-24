using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fingerprintv2.Web;
using fpcore.Model;
using fingerprintv2.Services;
using System.Text;

namespace fingerprintv2.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }
        //[AuthenticationFilterAttr]
        //[AcceptVerbs(HttpVerbs.Get)]
        //public PartialViewResult admin(string query,bool? direction)
        //{
        //    UserAC user = (UserAC)Session["user"];
        //    IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
        //    IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
        //    if (direction == null)
        //        direction = true;
        //    if (string.IsNullOrEmpty(query))
        //        query = "UserAC.ObjectId";
        //    string sort = direction == true ? " ASC " : " desc ";

        //    List<UserAC> users = objectService.getSales(" order by " + query + sort, user);
        //    ViewData.Add("direction", direction);
        //    ViewData.Add("users", users);
        //    return PartialView();
        //}

        //
        // GET: /Job/
        [AuthenticationFilterAttr]
        public ActionResult admin()
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

            string query = null;
            if (sort != null && sort != "group")
                query = " order by " + sort + " " + sortDir;

            List<UserAC> users = objectService.getSales(query, user);
            int count = users.Count();

            if (users.Count() == 0)
                return Content("{total:0,data:[]}");

            StringBuilder jobJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            for (int i = 0; i < users.Count; i++)
            {
                if (i > 0)
                    jobJson.Append(",");
                jobJson.Append(JSONTool.getAdminJson(users[i]));
            }
            jobJson.Append("]}");
            return Content(jobJson.ToString());
        }

        [AuthenticationFilterAttr]
        [ValidateInput (false )]
        [AcceptVerbs(HttpVerbs.Post)]
        public object AddAdmin(string objectid,string username, string nameen, string namecn, string post, string email, string pwd, string remark, string status)
        {
            try
            {
                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
                List<UserAC> users = objectService.getSales(null, user);
                var str = string.Empty;
                int id = 0;
                int.TryParse(objectid, out id);

                UserAC opuser = objectService.getUserByID(id, user);
                bool bresult = false;
                if (opuser == null)
                {
                    if (!users.Exists(u => u.user_name == username.Trim()))
                    {
                        bool result = service.addNewUserAC(new UserAC
                        {
                            user_name = username,
                            user_password = pwd,
                            chi_name = namecn,
                            eng_name = nameen,
                            remark = remark,
                            status = status,
                            email = email,
                            post = post,

                        }, user);
                        str = "your information added successfully!";
                        bresult = true;
                    }
                    else
                    {
                        str = "add information failed ,exist the same username!";
                        bresult = false;
                    }
                }
                else
                {
                    if (!users.Exists(u => u.user_name == username.Trim() && u.objectId != int.Parse(objectid)))
                    {
                        opuser.user_name = username;
                        opuser.user_password = pwd;
                        opuser.chi_name = namecn;
                        opuser.eng_name = nameen;
                        opuser.remark = remark;
                        opuser.status = status;
                        opuser.email = email;
                        opuser.post = post;

                        bool result = service.updateUserAC(opuser, user);
                        str = "your information updated successfully!";
                        bresult = true;
                    }
                    else
                    {
                        str = "update information failed,exist the same username!";
                        bresult = false;
                    }
                }

                return Content("{success:" + bresult.ToString().ToLower() + ", result:\"" + str + "\"}");
            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + e.Message + "\"}");
            }
        }


        [AuthenticationFilterAttr]
        public ActionResult DeleteAdmin()
        {
            String objectid = Request.Params["pid"];
            String pwd = Request.Params["pwd"];

            UserAC user = (UserAC)Session["user"];


            if (pwd != user.user_password)
                return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

            if (user.roles.Where(c => c.name == "system admin").Count() <= 0)
                return Content("{success:false, result:\"Sorry, You are not authorized to do this action.\"}");

            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            UserAC admin = objectService.getUserByID(int.Parse(objectid), user);

            if (admin == null)
            {
                return Content("{success:false, result:\"Admin is not found.\"}");

            }

            service.deleteUserAC(admin, user);

            return Content("{success:true, result:\"Update success\"}");
        }

        //[AuthenticationFilterAttr]
        //[ValidateInput(false)]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public object DeleteAdmin(string ids)
        //{
        //    UserAC user = (UserAC)Session["user"];
        //    IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
        //    IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

        //    string str = string.Empty;
        //    if (!string.IsNullOrEmpty(ids))
        //    {
        //        var array = ids.Split(',');
        //        foreach (var item in array)
        //        {
        //            if (!string.IsNullOrEmpty(item))
        //            {
        //                int id = 0;
        //                int.TryParse(item, out id);
        //                var obj = objectService.getUserByID(id, user);
        //                service.deleteUserAC(obj, user);                        
        //            }
        //        }                
        //    }
        //    str = "delete success!"; 
        //    return Json(str);
        //}

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult group()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<FPRole> roles = objectService.getRoles(null, user);
            List<UserAC> users = objectService.getSales(null, user);
            ViewData.Add("roles", roles);
            ViewData.Add("users", users);
            return PartialView();
        }

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult RenderGroup(string roleID)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

           
            List<UserAC> users = objectService.getUsersByRole(roleID, user);
            FPRole role=objectService.getRoles (" and FPObject.ObjectId='"+roleID +"'",user).FirstOrDefault ();
            ViewData.Add("role", role);
            ViewData.Add("users", users);
            return PartialView();
        }

        [AuthenticationFilterAttr]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object AddGroup(string roleID, string name, string userids)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            int rid = 0;
            int.TryParse(roleID, out rid);
            FPRole role = objectService.getRoles(" where FPObject.ObjectId='" + rid + "'", user).FirstOrDefault();

            string result = string.Empty;
            if (role ==null)//ÐÂÔö
            {
                role = new FPRole();
                role.name = name;
                service.addRole(role, user);
                result = "your information added sucessfully!";
            }
            else//ÐÞ¸Ä
            {
                role.name = name;
                service.updateRole(role, user);
                result = "your information updated sucessfully!";
            }

            List<UserAC> users = objectService.getUsersByRole(roleID, user);
            if (users != null)
            {
                foreach (var u in users)
                {
                    if (u.roles != null && u.roles.Exists (c=>c.objectId ==role.objectId))
                    {
                        u.roles.RemoveAll(c => c.objectId == role.objectId);                      
                        service.updateUserRole(u);
                    }
                }
            }

            if (!string.IsNullOrEmpty(userids))
            {
                var array = userids.Split(',');
                foreach (var item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        UserAC u = objectService.getUserByID(int.Parse(item), user);
                        if (u.roles .Count()==0|| (u.roles != null && u.roles.Exists(c => c.objectId == role.objectId)==false))
                        {
                            u.roles.Add(role);
                            service.updateUserRole(u);
                        }
                    }
                }
            }

            return Json(result);
        }

        [AcceptVerbs (HttpVerbs.Post)]
        public object DeleteGroup(string ids)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");          

            if (!string.IsNullOrEmpty(ids))
            {
                var array = ids.Split(',');
                foreach (var item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int id = 0;
                        int.TryParse(item, out id);
                        FPRole role = objectService.getRoles(" where FPObject.ObjectId='" + id + "'", user).FirstOrDefault();
                        service.deleteRole(role, user);
                        List<UserAC> users = objectService.getUsersByRole(item, user);                      
                        if (users != null)
                        {
                            foreach (var u in users)
                            {
                                if (u.roles != null && u.roles.Exists(c => c.objectId == role.objectId))
                                {
                                    u.roles.RemoveAll(c => c.objectId == role.objectId);                                   
                                    service.updateUserRole(u);
                                }
                            }
                        }
                    }
                }
            }

            return Json("delete successfully !");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult customer(string sortExpression, bool? sortDiretion, int? pageIndex, int? pageSize)
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

            if (sortDiretion == null)
                sortDiretion = true;

            var customers = objectService.getAllCustomer(pageSize.Value, (pageIndex.Value - 1) * pageSize.Value, sortExpression, sortDiretion.Value, user);
            int count = objectService.countCustomer(" where IsDeleted = 0 ", user);
            int pageCount = count % pageSize.Value == 0 ? count / pageSize.Value : count / pageSize.Value + 1;
            ViewData.Add("customers", customers);
            ViewData.Add("sortExpression", sortExpression);
            ViewData.Add("sortDiretion", sortDiretion);
            ViewData.Add("pageIndex", pageIndex);
            ViewData.Add("pageSize", pageSize);
            ViewData.Add("pageCount", pageCount);
            ViewData.Add("count", count);
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult customercontact(string companyid, string companyname, string code)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            CustomerContact customercontact = objectService.getCustomerContactByCode(code.Trim(),"default", user);

            ViewData.Add("companyid", companyid);
            ViewData.Add("companycode", code);
            ViewData.Add("companyname", companyname);
            ViewData.Add("customercontact", customercontact);
            return PartialView();
        }

        public object addcustomer(string code, string name, string person, string tel, string address, string cid)
        {
            var result = string.Empty;
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            int objectid = 0;
            int.TryParse(cid, out objectid);
            var customer = objectService.getCustomerByID(objectid, user);
            string customer_code = string.Empty;
           

            if(customer !=null )
                customer_code = customer.company_code.Trim();
            var customer1 = objectService.getCustomerByCustomerID(code.Trim(), user);
            if (customer1 != null && customer == null)
            {
                result = "has exist the company code !";
            }
            else
            {
                if (customer != null)
                {
                    customer.company_code = code.Trim();
                    customer.company_name = name.Trim();
                    service.updateCustomer(customer, user);
                    result = "update information successfully !";
                }
                else
                {
                    customer = new Customer();
                    customer.company_code = code.Trim();
                    customer.company_name = name.Trim();
                    service.addCustomer(customer, user);
                    result = "add information successfully !";
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
            return Json(result);
        }

        public object deletecustomer(string ids)
        {
            string result = string.Empty;

            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            if (!string.IsNullOrEmpty(ids))
            {
                var array = ids.Split(',');

                foreach (var item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var customer = objectService.getCustomerByID(int.Parse(item), user);
                        if (customer != null)
                        {
                            service.deleteCustomer(customer, user);
                            result = "delete successfully !";
                        }                       
                    }
                }
            }

            return Json(result);
        }
    }
}
