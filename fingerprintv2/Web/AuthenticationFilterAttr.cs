using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using fpcore.Model;

namespace fingerprintv2.Web
{
    public class AuthenticationFilterAttr : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase context = filterContext.HttpContext;
            String loginPageUrl = WebConfigurationManager.AppSettings["LoginPage"];
            string errorPageUrl = WebConfigurationManager.AppSettings["ErrorPage"];
            UserAC user = context.Session["user"] as UserAC;

            if (user != null && user.roles.Count() > 0)
            {
                switch (filterContext.ActionDescriptor.ActionName.ToLower().Trim())
                {
                    case "index":
                    case "order":
                        if (user.roles.Where(r => r.name == "system admin" || r.name == "order admin" || r.name == "order user").Count() <= 0)
                        {
                            context.Session["errorMsg"] = "You do not have the authority to access this order page! ";
                            context.Response.Redirect(errorPageUrl);
                        }
                        break;
                    case "job":
                        if (user.roles.Where(r => r.name == "system admin" || r.name == "job admin" || r.name == "job user").Count() <= 0)
                        {
                            context.Session["errorMsg"] = "You do not have the authority to access this job page! ";
                            context.Response.Redirect(errorPageUrl);
                        }
                        break;
                    case "delivery":
                        if (user.roles.Where(r => r.name == "system admin" || r.name == "delivery admin" || r.name == "delivery user").Count() <= 0)
                        {
                            context.Session["errorMsg"] = "You do not have the authority to access this delivery page! ";
                            context.Response.Redirect(errorPageUrl);
                        }
                        break;
                    case "inventory":
                        if (user.roles.Where(r => r.name == "system admin" || r.name == "inventory admin" || r.name == "inventory user").Count() <= 0)
                        {
                            context.Session["errorMsg"] = "You do not have the authority to access this inventory page! ";
                            context.Response.Redirect(errorPageUrl);
                        }
                        break;
                    case "customer":
                    case "group":
                    case "admin":
                        if (user.roles.Where(r => r.name == "system admin").Count() <= 0)
                        {
                            context.Session["errorMsg"] = "You do not have the authority to access this admin page! ";
                            context.Response.Redirect(errorPageUrl);
                        }
                        break;
                }
            }
            else
            {
                context.Session["errorMsg"] = "You do not have the authority to access this system ! ";
                context.Response.Redirect(errorPageUrl );
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
