
using FastWay.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace FastWay.Pages
{
    public partial class AdminAllOrders : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        public int i = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            outputRequests();
        }
        protected void viewRequest_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button clickedButton = sender as System.Web.UI.WebControls.Button;
            int i = clickedButton.TabIndex;
            Models.Orders orders = app.Orders.FirstOrDefault(x => x.ID == i);
            Session["dataRequest"] = orders;
            Response.Redirect("AdminСhequeRequest.aspx");
        }
        List<Orders> data;
        List<Orders> sortedOrders;
       
        protected void outputRequests()
        {
            data = app.Orders.ToList();
            data.Clear();
            data.AddRange(app.Orders.ToList());
            for (int i = 0; i < data.Count; i++)
            {
                int? sid = data[i].ID;
                var s = app.AcceptOrder.FirstOrDefault(x => x.AcceptOrderInfo == sid);
                if (s == null)
                {
                    data[i].Status = "В процессе";
                }
                else if (s.Accept == "no")
                {
                    data[i].Status = "Отклонена";
                }
                else if (s.Accept == "yes")
                {
                    data[i].Status = "Одобрена";
                }

            }
            sortedOrders = app.Orders.ToList();
            if (data.Count == 0)
            {
                emptyListPanel.Visible = true;
                txbQuantity.Text = "0";
            }
            else
            {
                emptyListPanel.Visible = false;
                listViewRequests.DataSource = data;
                listViewRequests.DataBind();
                string q = data.Count.ToString();
                txbQuantity.Text = q;
            }

        }
        public void Func()
        {
            var servicesWithFilters = data;
            if (searchLastTxb.Text != "")
            {
                servicesWithFilters = servicesWithFilters.Where(x => x.LastName.ToLower().Trim().Contains(searchLastTxb.Text.ToLower().Trim())).ToList();
                listViewRequests.DataSource = servicesWithFilters;
                listViewRequests.DataBind();
            }
            if (searchFirstTxb.Text != "")
            {
                servicesWithFilters = servicesWithFilters.Where(x => x.FirstName.ToLower().Trim().Contains(searchFirstTxb.Text.ToLower().Trim())).ToList();
                listViewRequests.DataSource = servicesWithFilters;
                listViewRequests.DataBind();
            }
            if (searchPatroTxb.Text != "")
            {
                servicesWithFilters = servicesWithFilters.Where(x => x.Patronymic.ToLower().Trim().Contains(searchPatroTxb.Text.ToLower().Trim())).ToList();
                listViewRequests.DataSource = servicesWithFilters;
                listViewRequests.DataBind();
            }
            switch (fltButton.SelectedIndex)
            {
                case 1:
                    servicesWithFilters = servicesWithFilters.Where(x => x.Status == "В процессе").ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                case 2:
                    servicesWithFilters = servicesWithFilters.Where(x => x.Status == "Одобрена").ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                case 3:
                    servicesWithFilters = servicesWithFilters.Where(x => x.Status == "Отклонена").ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                default:
                    break;
            }
            switch (sortButton.SelectedIndex)
            {
                case 1:
                    servicesWithFilters = servicesWithFilters.OrderByDescending(d => d.DateOrder).ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                case 2:
                    servicesWithFilters = servicesWithFilters.OrderBy(d => d.DateOrder).ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                default:
                    break;
            }
            if (servicesWithFilters.Count == 0)
            {
                txbQuantity.Text = "0";
                emptyListPanel.Visible = true;
            }
            else
            {
                string q = servicesWithFilters.Count.ToString();
                txbQuantity.Text = q;
                emptyListPanel.Visible = false;
            }


        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            Func();
        }


        protected void sortButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func();
        }



        protected void filtButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func();
        }

        protected void resetFilters_Click(object sender, EventArgs e)
        {
            listViewRequests.DataSource = null;
            searchLastTxb.Text = "";
            searchFirstTxb.Text = "";
            searchPatroTxb.Text = "";
            sortButton.SelectedIndex = 0;
            fltButton.SelectedIndex = 0;
            outputRequests();
        }

        protected void messageButtonCloseStatistic_Click(object sender, EventArgs e)
        {
            panelStatistics.Visible = false;
        }

        protected void stat_Click(object sender, EventArgs e)
        {
            panelStatistics.Visible = true;
            var orders = app.Orders.ToList();
            var o = app.Orders.ToList();
            
            var del = app.DeliveryType.ToList();
            List<int> numbers = new List<int>();
            for (int i = 0; i < del.Count; i++)
            {
                int? did = del[i].ID;
                var or = app.Orders.Where(x => x.DeliveryType == did).ToList();
                numbers.Add(or.Count);
                del[i].CountInOrder = or.Count;
            }
            
            var cargo = app.Cargo.ToList();
            List<decimal?> volumeList = new List<decimal?>();
            decimal? vol = 0;
            for (int i = 0; i < cargo.Count; i++)
            {
                decimal? ov = cargo[i].OverallVolume;
                if (cargo[i].Ed == "sm")
                {
                    ov /= 1000000;
                }
                vol += ov;
                volumeList.Add(vol);
            }

            int money = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                money += Convert.ToInt32(orders[i].SummaryCost);
            }

            int maxDelivery = numbers.Max();
            decimal? avVolume = volumeList.Average();
            decimal roundedNumber = Math.Round((decimal)avVolume, 2);
            var delName = del.FirstOrDefault(x => x.CountInOrder == maxDelivery).Type;

            countOfOrders.Text = $"Количество заказов: {orders.Count}";
            moneyOfOrders.Text = $"Выручка: {money} рублей";
            muchDeliveryType.Text = $"Востребованный способ перевозки: {delName}";
            averageVolume.Text = $"Средний объем груза: {roundedNumber} м3";
        }
    }
}