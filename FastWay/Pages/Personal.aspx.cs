using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay
{
    public partial class Personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void close_Click(object sender, EventArgs e)
        {
            string script = "window.close();";
            ClientScript.RegisterStartupScript(this.GetType(), "closeWindowScript", script, true);
        }
    }
}