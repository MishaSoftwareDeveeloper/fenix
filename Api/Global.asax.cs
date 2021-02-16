using Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Headers["Authorization"]))
            {
                string token = Request.Headers["Authorization"].ToString();
                var validtoken = TokenHelper.ValidateToken(token);

                if (!validtoken)
                {
                    Response.StatusCode = 404;
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}
