using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace HonoursProject_v04
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usertype"] == null)
                {
                    Response.Redirect("Logout.aspx");
                }
                else
                {
                    if (Session["usertype"].ToString() != "Project Co-ordinator")
                    {
                        lnkUsers.Visible = false;
                        lnkStudents.Visible = false;
                        lnkSelfProposedTopics.Visible = false;
                    }

                    if (Session["usertype"].ToString() != "Student")
                    {
                        lnkMyTopics.Visible = false;
                    }
                }
            }
            catch { }
        }

    }
}