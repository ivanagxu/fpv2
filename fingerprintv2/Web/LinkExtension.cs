using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using fingerprintv2.Web;

namespace System.Web.Mvc.Html
{
    public static class LinkExtension
    {
        public static string link(this HtmlHelper helper, string resourceUrl)
        {
            if (resourceUrl == "")
                return "\"" + AppSettings.AppName + "\"";
            else
                return "\"/" + AppSettings.AppName + "/" + resourceUrl + "\"";
        }
    }
}
