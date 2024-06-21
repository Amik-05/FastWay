using FastWay.Models;
using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace FastWay.Pages
{
    public partial class AdminTransMethods : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                SetNonEditable();
                comboDeliveryType.SelectedIndex = 0;
                FillDropDownList();
                func();
            }
        }

        protected void FillDropDownList()
        {
            var deliveryType = app.DeliveryType.ToList();
            
            if (deliveryType.Count != 0)
            {
                comboDeliveryType.DataSource = deliveryType;
                comboDeliveryType.DataTextField = "Type"; // Поле для отображения
                comboDeliveryType.DataValueField = "ID";  // Значение поля
                comboDeliveryType.DataBind();
                int ind = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                DeliveryType d = app.DeliveryType.FirstOrDefault(x => x.ID == ind);
                if (d.CarID != null)
                {
                    checkCar.Checked = true;
                    if (panelEdit.Visible == false)
                    {
                        comboCarsForEdit.Style["pointer-events"] = "auto";
                        comboCarsForEdit.Style["opacity"] = "1";
                    }
                }
                else
                {
                    checkCar.Checked = false;
                    comboCarsForEdit.Style["pointer-events"] = "none";
                    comboCarsForEdit.Style["opacity"] = "0.5";
                }
            }
            else
            {
                comboDeliveryType.Items.Clear();
            }


            var cars = app.Cars.Where(x => x.IsHaveDeliveryType == "no").ToList();
            if (cars.Count != 0)
            {
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                }
                if (deliveryType.Count != 0)
                {
                    comboCarsForEdit.DataSource = cars;
                    comboCarsForEdit.DataTextField = "NameAndVIN"; // Поле для отображения
                    comboCarsForEdit.DataValueField = "ID";  // Значение поля
                    comboCarsForEdit.DataBind();
                }
                var carsForAdd = app.Cars.ToList();
                comboCarsForAdd.DataSource = carsForAdd;
                comboCarsForAdd.DataTextField = "NameAndVIN"; // Поле для отображения
                comboCarsForAdd.DataValueField = "ID";  // Значение поля
                comboCarsForAdd.DataBind();
                func();
            }
            else
            {
                comboCarsForAdd.Items.Clear();
            }
            

        }


        public void func()
        {
            if (comboDeliveryType.Items.Count != 0)
            {
                int ind = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                DeliveryType deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == ind);
                if (deliveryType != null)
                {
                    txbCostForDelivery.Text = "Стоимость за километр: " + Convert.ToString(deliveryType.Cost) + " рублей";
                }
                if (comboDeliveryType.SelectedItem != null)
                {
                    var d = app.DeliveryType.FirstOrDefault(x => x.ID == ind);
                    if (deliveryType.CarID != null)
                    {
                        var carId = app.DeliveryType.FirstOrDefault(x => x.CarID == d.CarID).CarID.ToString();
                        var cars = app.Cars.ToList();
                        for (int i = 0; i < cars.Count; i++)
                        {
                            cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                        }
                        comboCarsForEdit.DataSource = cars;
                        comboCarsForEdit.DataTextField = "NameAndVIN"; // Поле для отображения
                        comboCarsForEdit.DataValueField = "ID";  // Значение поля
                        comboCarsForEdit.DataBind();
                        ListItem item = comboCarsForEdit.Items.FindByValue(carId.ToString());
                        if (item != null)
                        {
                            comboCarsForEdit.ClearSelection();
                            item.Selected = true;
                        }
                    }
                    else
                    {
                        var cars = app.Cars.Where(x => x.IsHaveDeliveryType == "no").ToList();
                        for (int i = 0; i < cars.Count; i++)
                        {
                            cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                        }
                        comboCarsForEdit.DataSource = cars;
                        comboCarsForEdit.DataTextField = "NameAndVIN"; // Поле для отображения
                        comboCarsForEdit.DataValueField = "ID";  // Значение поля
                        comboCarsForEdit.DataBind();
                    }
                    
                }
            }
            
        }
        protected void comboDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            func();
            int i = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
            DeliveryType d = app.DeliveryType.FirstOrDefault(x => x.ID == i);
            if (d.CarID != null)
            {
                checkCar.Checked = true;
                if (panelEdit.Visible == false)
                {
                    comboCarsForEdit.Style["pointer-events"] = "auto";
                    comboCarsForEdit.Style["opacity"] = "1";
                }
            }
            else
            {
                checkCar.Checked = false;
                comboCarsForEdit.Style["pointer-events"] = "none";
                comboCarsForEdit.Style["opacity"] = "0.5";
            }
        }

        protected void apply_Click(object sender, EventArgs e)
        {
            comboCarsForEdit.BorderColor = Color.White;
            //применить редактирование
            if (checkCar.Checked == true)
            {
                if (comboCarsForEdit.SelectedItem != null)
                {
                    int ind = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                    var d = app.DeliveryType.FirstOrDefault(x => x.ID == ind);
                    if (txbNewCost.Text != "")
                    {
                        d.Cost = Convert.ToInt32(txbNewCost.Text);
                    }
                    d.CarID = Convert.ToInt32(comboCarsForEdit.SelectedItem.Value);
                    var c = app.Cars.FirstOrDefault(x => x.ID == d.CarID);
                    c.IsHaveDeliveryType = "yes";
                    app.SaveChanges();
                    comboDeliveryType.DataSource = null;
                    comboCarsForEdit.DataSource = null;
                    txbNewCost.Text = "";
                    FillDropDownList();
                    func();
                    pnlMessageApply.Visible = true;
                }
                else
                {
                    comboCarsForEdit.BorderColor = Color.Red;
                }
            }
            else
            {
                int ind = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                var d = app.DeliveryType.FirstOrDefault(x => x.ID == ind);
                if (txbNewCost.Text != "")
                {
                    d.Cost = Convert.ToInt32(txbNewCost.Text);
                }
                if (comboCarsForEdit.SelectedItem != null)
                {
                    var c = app.Cars.FirstOrDefault(x => x.ID == d.CarID);
                    c.IsHaveDeliveryType = "no";
                    d.CarID = null;
                }
                app.SaveChanges();
                txbNewCost.Text = "";
                FillDropDownList();
                func();
                pnlMessageApply.Visible = true;
            }
            

        }

        protected void delete_Click(object sender, EventArgs e)
        {
            //отображение подтверждения удаления
            pnlMessage.Visible = true;
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            panelApplyCancel.Visible = true;
            panelEdit.Visible = false;
            SetEditable();
        }
        protected void messageButtonYes_Click(object sender, EventArgs e)
        {
            //удаление
            if (comboDeliveryType.SelectedIndex >= 0)
            {
                int ind = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                var d = app.DeliveryType.FirstOrDefault(x => x.ID == ind);
                if (comboCarsForEdit.SelectedItem != null)
                {
                    int? cc = Convert.ToInt32(comboCarsForEdit.SelectedItem.Value);
                    var cars = app.Cars.FirstOrDefault(x => x.ID == cc);
                    cars.IsHaveDeliveryType = "no";
                    app.SaveChanges();
                }
                app.DeliveryType.Remove(d);
                app.SaveChanges();
                comboDeliveryType.SelectedIndex = 0;
                txbNewCost.Text = "";
                comboDeliveryType.DataSource = null;
                comboCarsForEdit.DataSource = null;
                FillDropDownList();
                func();
                pnlMessage.Visible = false;
                pnlMessageTransDeleted.Visible = true;
            }
        }

        protected void messageButtonNo_Click(object sender, EventArgs e)
        {
            //отмена удаления
            pnlMessage.Visible = false;
        }

        protected void add_Click(object sender, EventArgs e)
        {
            //добавление
            if (txbName.Text != "" && txbCost.Text != "")
            {
                int? c = Convert.ToInt32(txbCost.Text);
                var d = app.DeliveryType.ToList();
                DeliveryType newItem = new DeliveryType
                {
                    ID = d.Count + 1,
                    Type = txbName.Text,
                    Cost = c,
                    CarID = Convert.ToInt32(comboCarsForAdd.SelectedItem.Value)
                };
                if (comboCarsForAdd.SelectedItem != null)
                {
                    int? cc = Convert.ToInt32(comboCarsForAdd.SelectedItem.Value);
                    var cars = app.Cars.FirstOrDefault(x => x.ID == cc);
                    cars.IsHaveDeliveryType = "yes";
                    app.SaveChanges();
                }
                app.DeliveryType.Add(newItem);
                app.SaveChanges();
                comboDeliveryType.DataSource = null;
                comboCarsForEdit.DataSource = null;
                FillDropDownList();
                func();
                txbName.Text = "";
                txbCost.Text = "";
                pnlMessageApply.Visible = true;
            }
        }

        protected void messageButtonApply_Click(object sender, EventArgs e)
        {
            pnlMessageApply.Visible = false;
            pnlMessage.Visible = false;
            pnlMessageTransDeleted.Visible = false;
        }

        protected void messageButtonTransDeleted_Click(object sender, EventArgs e)
        {
            pnlMessageApply.Visible = false;
            pnlMessage.Visible = false;
            pnlMessageTransDeleted.Visible = false;
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            panelApplyCancel.Visible = false;
            panelEdit.Visible = true;
            SetNonEditable();
            FillDropDownList();
            func();
        }

        public void SetEditable()
        {
            txbNewCost.ReadOnly = false;
            txbNewCost.Style["opacity"] = "1";
            labelCheckCar.Style["opacity"] = "1";
            checkCar.Style["opacity"] = "1";
            checkCar.Style["pointer-events"] = "auto";
            int idDr = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
            var dr = app.DeliveryType.FirstOrDefault(x => x.ID == idDr);
            if (dr.CarID != null)
            {
                checkCar.Checked = true;
                comboCarsForEdit.Style["pointer-events"] = "auto";
                comboCarsForEdit.Style["opacity"] = "1";
            }
            else
            {
                checkCar.Checked = false;
                comboCarsForEdit.Style["pointer-events"] = "none";
                comboCarsForEdit.Style["opacity"] = "0.5";
            }
        }

        public void SetNonEditable()
        {
            txbNewCost.ReadOnly = true;
            comboCarsForEdit.Style["pointer-events"] = "none";
            comboCarsForEdit.Style["opacity"] = "0.5";
            txbNewCost.Style["opacity"] = "0.5";
            labelCheckCar.Style["opacity"] = "0.5";
            checkCar.Style["opacity"] = "0.5";
            checkCar.Style["pointer-events"] = "none";
        }

        protected void checkCar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCar.Checked == true)
            {
                comboCarsForEdit.Style["pointer-events"] = "auto";
                comboCarsForEdit.Style["opacity"] = "1";
            }
            else
            {
                comboCarsForEdit.Style["pointer-events"] = "none";
                comboCarsForEdit.Style["opacity"] = "0.5";
            }
        }
    }
}