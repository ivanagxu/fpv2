using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fingerprintv2.Web;
using fingerprintv2.Services;
using fpcore.Model;

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
        [AuthenticationFilterAttr]
        public ActionResult error()
        {
            return View("error");
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
                UserAC user = service.login(userName, userPwd);
                Session["user"] = user;
                if(user != null)
                    Session["userName"] = user.eng_name;
            }
            catch (Exception e)
            {
                throw e;
            }

            if (Session["user"] != null)
            {
                return Content("{success:true, result:\"Login success\"}");
            }
            else
                return Content("{success:false, result:\"Login failed\"}");
        }
    }
}
