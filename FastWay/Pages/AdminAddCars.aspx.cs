using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay.Pages
{
    public partial class AdminAddCars : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        Cars updatedCars;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                SetNonEditable();
                comboCars.SelectedIndex = 0;
                FillDropDownList();
                func();
            }
        }

        protected void FillDropDownList()
        {
            var cars = app.Cars.ToList();
            comboCars.DataSource = cars;
            for (int i = 0; i< cars.Count; i++)
            {
                cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
            }
            comboCars.DataTextField = "NameAndVIN"; // Поле для отображения
            comboCars.DataValueField = "ID";  // Значение поля
            comboCars.DataBind();
            func();
        }

        public void func()
        {
            if (comboCars.Items.Count != 0)
            {
                int ind = Convert.ToInt32(comboCars.SelectedItem.Value);
                Cars c = app.Cars.FirstOrDefault(x => x.ID == ind);
                txbNewName.Text = c.FullName;
                txbNewVolume.Text = c.Volume;
                txbNewMaxWeight.Text = c.Max_weight;
                txbNewVIN.Text = c.VIN;
            }
        }
        protected void comboCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            func();
        }

        protected void apply_Click(object sender, EventArgs e)
        {
            txbNewName.BorderColor = Color.White;
            txbNewVolume.BorderColor = Color.White;
            txbNewMaxWeight.BorderColor = Color.White;
            txbNewVIN.BorderColor = Color.White;
            //применить редактирование
            if (comboCars.SelectedIndex >= 0)
            {
                int ind = Convert.ToInt32(comboCars.SelectedItem.Value);
                updatedCars = app.Cars.FirstOrDefault(x => x.ID == ind);
                if (txbNewName.Text != "" && txbNewVolume.Text != "" && txbNewMaxWeight.Text != "" && txbNewVIN.Text != "")
                {
                    if (txbNewVIN.Text.Length == 17)
                    {
                        updatedCars.FullName = txbNewName.Text;
                        updatedCars.Volume = txbNewVolume.Text;
                        updatedCars.Max_weight = txbNewMaxWeight.Text;
                        updatedCars.VIN = txbNewVIN.Text.ToUpper();
                        app.SaveChanges();
                        comboCars.SelectedIndex = 0;
                        FillDropDownList();
                        func();
                        pnlMessageApply.Visible = true;
                    }
                    else
                    {
                        txbNewVIN.BorderColor = Color.Red;
                    }
                }
                else
                {
                    if (txbNewName.Text == "")
                    {
                        txbNewName.BorderColor = Color.Red;
                    }
                    if (txbNewVolume.Text == "")
                    {
                        txbNewVolume.BorderColor = Color.Red;
                    }
                    if (txbNewMaxWeight.Text == "")
                    {
                        txbNewMaxWeight.BorderColor = Color.Red;
                    }
                    if (txbNewVIN.Text == "")
                    {
                        txbNewVIN.BorderColor = Color.Red;
                    }
                }
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            //отображение подтверждения удаления
            pnlMessage.Visible = true;
        }
        protected void messageButtonYes_Click(object sender, EventArgs e)
        {
            //удаление
            if (comboCars.SelectedIndex >= 0)
            {
                int ind = Convert.ToInt32(comboCars.SelectedItem.Value);
                var c = app.Cars.FirstOrDefault(x => x.ID == ind);
                var d = app.DeliveryType.Where(x => x.CarID == c.ID).ToList();
                var dr = app.Drivers.FirstOrDefault(x => x.CarID == c.ID);
                int? nullableInt = null;
                for (int i = 0; i < d.Count; i++)
                {
                    d[i].CarID = nullableInt;
                }
                dr.CarID = nullableInt;
                dr.Status = "Нет";
                app.Cars.Remove(c);
                app.SaveChanges();
                txbNewName.Text = "";
                txbNewVolume.Text = "";
                txbNewMaxWeight.Text = "";
                comboCars.DataSource = null;
                FillDropDownList();
                func();
                pnlMessage.Visible = false;
                pnlMessageCarDeleted.Visible = true;
            }
        }

        protected void messageButtonNo_Click(object sender, EventArgs e)
        {
            //отмена удаления
            pnlMessage.Visible = false;
        }

        protected void add_Click(object sender, EventArgs e)
        {
            txbName.BorderColor = Color.White;
            txbVolume.BorderColor = Color.White;
            txbMaxWeight.BorderColor = Color.White;
            txbVIN.BorderColor = Color.White;
            //добавление
            if (txbName.Text != "" && txbVolume.Text != "" && txbMaxWeight.Text != "" && txbVIN.Text != "")
            {
                if (txbVIN.Text.Length == 17)
                {
                    Cars newItem = new Cars
                    {
                        FullName = txbName.Text,
                        Volume = txbVolume.Text,
                        Max_weight = txbMaxWeight.Text,
                        VIN = txbVIN.Text.ToUpper().Trim(),
                        IsHaveDriver = "no",
                        IsHaveDeliveryType = "no",
                    };
                    app.Cars.Add(newItem);
                    app.SaveChanges();
                    txbName.Text = "";
                    txbVolume.Text = "";
                    txbMaxWeight.Text = "";
                    comboCars.DataSource = null;
                    FillDropDownList();
                    comboCars.SelectedIndex = 0;
                    func();
                    pnlMessageApply.Visible = true;
                }
                else
                {

                }
            }
            else
            {
                if (txbName.Text == "")
                {
                    txbName.BorderColor = Color.Red;
                }
                if (txbVolume.Text == "")
                {
                    txbVolume.BorderColor = Color.Red;
                }
                if (txbMaxWeight.Text == "")
                {
                    txbMaxWeight.BorderColor = Color.Red;
                }
                if (txbVIN.Text == "")
                {
                    txbVIN.BorderColor = Color.Red;
                }
            }
        }

        protected void messageButtonApply_Click(object sender, EventArgs e)
        {
            //закрытие уведомления об изменении
            pnlMessageApply.Visible = false;
            pnlMessage.Visible = false;
            pnlMessageCarDeleted.Visible = false;
        }

        protected void messageButtonCarDeleted_Click(object sender, EventArgs e)
        {
            pnlMessageApply.Visible = false;
            pnlMessage.Visible = false;
            pnlMessageCarDeleted.Visible = false;
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            txbNewName.BorderColor = Color.White;
            txbNewVolume.BorderColor = Color.White;
            txbNewMaxWeight.BorderColor = Color.White;
            txbNewVIN.BorderColor = Color.White;
            SetNonEditable();
            panelApplyCancel.Visible = false;
            panelEdit.Visible = true;
            ClearEditTxb();
            FillDropDownList();
            
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            SetEditable();
            panelApplyCancel.Visible = true;
            panelEdit.Visible = false;
        }

        public void SetEditable()
        {
            txbNewName.ReadOnly = false;
            txbNewVolume.ReadOnly = false;
            txbNewMaxWeight.ReadOnly = false;
            txbNewVIN.ReadOnly = false;

            txbNewName.Style["opacity"] = "1";
            txbNewVolume.Style["opacity"] = "1";
            txbNewMaxWeight.Style["opacity"] = "1";
            txbNewVIN.Style["opacity"] = "1";
        }

        public void SetNonEditable()
        {
            txbNewName.ReadOnly = true;
            txbNewVolume.ReadOnly = true;
            txbNewMaxWeight.ReadOnly = true;
            txbNewVIN.ReadOnly = true;

            txbNewName.Style["opacity"] = "0.5";
            txbNewVolume.Style["opacity"] = "0.5";
            txbNewMaxWeight.Style["opacity"] = "0.5";
            txbNewVIN.Style["opacity"] = "0.5";
        }
        public void ClearEditTxb()
        {
            txbNewName.Text = "";
            txbNewVolume.Text = "";
            txbNewMaxWeight.Text = "";
            txbNewVIN.Text = "";
        }
    }
}