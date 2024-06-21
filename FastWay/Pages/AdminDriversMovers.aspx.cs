using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay.Pages
{
    public partial class AdminDriversMovers : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Output();
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
                if (m2io != null)
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

        public void Output()
        {
            var dr = app.Drivers.ToList();
            if (dr.Count != 0)
            {
                for (int i =0; i < dr.Count; i++)
                {
                    if (dr[i].CarID != null)
                    {
                        int? cid = dr[i].CarID;
                        var c = app.Cars.FirstOrDefault(x => x.ID == cid);
                        dr[i].Status = $"{c.FullName} {c.VIN}";
                    }
                }
                listDrivers.DataSource = dr;
                listDrivers.DataBind();
            }
            var mv = app.Movers.ToList();
            if (mv.Count != 0)
            {
                listMovers.DataSource = mv;
                listMovers.DataBind();
            }
            
        }

        protected void addPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAddPersonal.aspx");
        }
    }
}