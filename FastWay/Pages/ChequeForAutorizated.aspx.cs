using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay.Pages
{
    public partial class ChequeForAutorizated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = Session["log"];
            if (s != null)
            {
                vhod.Visible = false;
                profile.Visible = true;
            }
            else
            {
                profile.Visible = false;
                vhod.Visible = true;
            }
        }
        protected void v_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }

        protected void p_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void goToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }


    }
}