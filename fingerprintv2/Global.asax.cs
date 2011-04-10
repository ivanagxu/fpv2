using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using fingerprintv2.Web;
using System.Web.Configuration;
using fpcore.DAO;
using fingerprintv2.Services;

namespace fingerprintv2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}.aspx/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            //AppSetting init
            AppSettings.AppName = WebConfigurationManager.AppSettings["AppName"];
            AppSettings.DatabaseType = WebConfigurationManager.AppSettings["DatabaseType"];
            AppSettings.ConnStr = WebConfigurationManager.AppSettings["ConnStr"];

            //Initialize services
            FPServiceHolder.getInstance().addService("fpService", FPServiceFactory.getInstance().createFPService(WebConfigurationManager.AppSettings));
            FPServiceHolder.getInstance().addService("fpObjectService", FPServiceFactory.getInstance().createFPObjectService(WebConfigurationManager.AppSettings));


            //Initialize the DAO Factory
            DAOFactory.getInstance(AppSettings.DatabaseType, AppSettings.ConnStr);

            String xmlFile = HttpContext.Current.Server.MapPath(@"RolePrivilegeSettings.xml");
            PrivilegeMachine privilegeMachine = new PrivilegeMachine(xmlFile);
            FPServiceHolder.getInstance().addService("fpPrivilegeMachine", privilegeMachine);
        }
    }
}