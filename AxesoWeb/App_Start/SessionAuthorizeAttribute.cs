using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxesoWeb.App_Start
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                return httpContext.Session["IdUsuario"] != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/");
            //filterContext.Result = new RedirectResult("/Account/Index");
            //filterContext.Result = new RedirectResult("/AxesoWeb/");       /*PRODUCTION MODE*/
        }
    }
}