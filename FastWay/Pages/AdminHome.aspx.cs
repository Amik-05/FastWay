using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay
{
    public partial class AdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void editTarifsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEditingTarifs.aspx");
        }

        protected void viewingOrdersButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminViewingRequests.aspx");
        }

        protected void exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAutorization.aspx");
        }

        protected void addTarifsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminTransMethods.aspx");
        }

        protected void addCars_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAddCars.aspx");
        }

        protected void addDriversMoversButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDriversMovers.aspx");
        }
    }
}