using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HonoursProject_v04
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        public void Application_BeginRequest(object src, EventArgs e)
        {
            Context.Items["loadstarttime"] = DateTime.Now;
        }

        public void Application_EndRequest(object src, EventArgs e)
        {
            DateTime starttime = (DateTime)Context.Items["loadstarttime"];
            TimeSpan loadtime = DateTime.Now - starttime;
            Response.Write("this request took " + loadtime.TotalSeconds + " seconds to load");
        }
    }
}