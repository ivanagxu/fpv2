using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

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
            if (context.Session["user"] == null)
            {
                context.Response.Redirect(loginPageUrl);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
