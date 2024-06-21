using System;
using FastWay.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay
{
    public partial class Home : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
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
            Timer();
        }

        protected void Timer()
        {
            var m = app.Movers.ToList();
            for (int i = 0; i < m.Count; i++)
            {
                int? mId = m[i].ID;

                var m1io = app.MoversInOrder.FirstOrDefault(x => x.Mover1 == mId);
                if (m1io != null)
                {
                    var o1 = app.Orders.FirstOrDefault(x => m1io.OrderID == x.ID);

                    if (o1 != null && o1.ArrivalDate <= DateTime.Now)
                    {
                        m[i].Status = "Свободен";
                        app.SaveChanges();
                    }
                }

                var m2io = app.MoversInOrder.FirstOrDefault(x => x.Mover2 == mId);
                if (m2io != null )
                {
                    var o2 = app.Orders.FirstOrDefault(x => m2io.OrderID == x.ID);
                    if (o2 != null && o2.ArrivalDate <= DateTime.Now)
                    {
                        m[i].Status = "Свободен";
                        app.SaveChanges();
                    }
                }

            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cargo.aspx");
        }

        protected void v_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }

        protected void p_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }

        protected void adminAutoriz_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAutorization.aspx");
        }
    }
}