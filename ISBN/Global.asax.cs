using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ISBN
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(Server.MapPath("~/"));

            string connString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            DBHelper.SetConnString(connString);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}