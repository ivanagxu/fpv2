using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fingerprintv2.Web;
using fingerprintv2.Services;
using fpcore.Model;
using System.Linq;

namespace fingerprintv2.Controllers
{
    public class FingerprintController : Controller
    {
        //
        // GET: /Fingerprint/

        [AuthenticationFilterAttr]
        public ActionResult Index()
        {
            Session["newOrder"] = new PrintOrder();
            return View("order");
        }

        [AuthenticationFilterAttr]
        public ActionResult order()
        {
            Session["newOrder"] = new PrintOrder();
            return View("order");
        }

        [AuthenticationFilterAttr]
        public ActionResult job()
        {
            return View("job");
        }

        [AuthenticationFilterAttr]
        public ActionResult delivery()
        {
            return View("delivery");
        }

        [AuthenticationFilterAttr]
        public ActionResult inventory()
        {
            return View("inventory");
        }

        [AuthenticationFilterAttr]
        public ActionResult customer()
        {
            return View("customer");
        }

        [AuthenticationFilterAttr]
        public ActionResult group()
        {
            return View("group");
        }

        [AuthenticationFilterAttr]
        public ActionResult admin()
        {
            UserAC user = (UserAC)Session["user"];
            if (false) // not admin
            {
                Session["errorMsg"] = "Permission denied";
                return View("error");
            }
            else
            {
                return View("admin");
            }
        }
       
        public ActionResult error()
        {
            return View("error");
        }

        [AuthenticationFilterAttr]
        public ActionResult getUser()
        {
            UserAC user = (UserAC)Session["user"];
            String userJson = "{ObjectId:" + user.objectId + ",eng_name:'" + user.eng_name + "'}";
            return Content(userJson);
        }
        [AuthenticationFilterAttr]
        public ActionResult hasPrivilege()
        {
            UserAC user = (UserAC)Session["user"];
            String action = Request.Params["action"];
            if (user.roles == null)
                return Content("{result:false}");
            if(user.roles.Count == 0)
                return Content("{result:false}");

            PrivilegeMachine pm = (PrivilegeMachine)FPServiceHolder.getInstance().getService("fpPrivilegeMachine");

            for (int i = 0; i < user.roles.Count; i++)
            { 
                if(pm.hasPrivilege(user.roles[i].name,action))
                {
                    return Content("{result:true}");
                }
            }

            return Content("{result:false}");
        }
        public ActionResult login()
        {
            Session["user"] = null;



            String userName = Request.Params["loginName"];
            String userPwd = Request.Params["loginPassword"];

            if (userName == null || userPwd == null)
                return View();

            if (userName == "" || userPwd == "")
                return Content("{success:false, result:\"Please input the user name and password to login\"}");

            try
            {
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                //perform login
                UserAC user1 = service.login(userName, userPwd);
                Session["user"] = user1;
                if (user1 != null)
                    Session["userName"] = user1.eng_name;
            }
            catch (Exception e)
            {
                throw e;
            }

            UserAC user = Session["user"] as UserAC;

            if (Session["user"] != null && user.roles.Count() > 0)
            {


                string action = "index";
                
                if (user.roles.Where(r => r.name.Contains("order")).Count() > 0)
                    action = "order";
                else if (user.roles.Where(r => r.name.Contains("job")).Count() > 0)
                    action = "job";
                else if (user.roles.Where(r => r.name.Contains("delivery")).Count() > 0)
                    action = "delivery";
                else if (user.roles.Where(r => r.name.Contains("inventory")).Count() > 0)
                    action = "inventory";
                else if (user.roles.Where(r => r.name.Contains("system")).Count() > 0)
                    action = "admin";

                return Content("{success:true, result:\"Login success\",data:\"" + action + "\"}");
            }
            else
                return Content("{success:false, result:\"Login failed\"}");
        }
    }
}
