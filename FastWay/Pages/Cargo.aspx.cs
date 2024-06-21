using FastWay.Models;
using FastWay.Pages;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FastWay
{
    public partial class Cargo : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                comboCategory.SelectedIndex = 0;
                FillDropDownList();
                FillDropDownList1();
            }
            SetTextBoxesNonEditable();
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

        private void SetTextBoxesNonEditable()
        {
            txbOverallVolume.ReadOnly = true;
            txbTotalWeight.ReadOnly = true;
            txbVolume.ReadOnly = true;
        }

        protected void FillDropDownList()
        {
            var categories = app.Category.ToList();
            comboCategory.DataSource = categories;
            comboCategory.DataTextField = "Category1"; // Поле для отображения
            comboCategory.DataValueField = "ID";  // Значение поля
            comboCategory.DataBind();
            comboCategory.SelectedIndexChanged += comboCategory_SelectedIndexChanged;
        }
        int sc = 0;
        protected void FillDropDownList1()
        {
            int cid = comboCategory.SelectedIndex + 1;
            if (cid == 7)
            {
                var subcategories = app.Subcategory.Where(x => x.CategoryID == 10000).ToList();
                comboSubcategory.DataSource = subcategories;
                comboSubcategory.DataTextField = "Subcategory1"; // Поле для отображения
                comboSubcategory.DataValueField = "ID";
                comboSubcategory.DataBind();
                sc = 49;
            }
            else
            {
                var subcategories = app.Subcategory.Where(x => x.CategoryID == cid).ToList();
                comboSubcategory.DataSource = subcategories;
                comboSubcategory.DataTextField = "Subcategory1"; // Поле для отображения
                comboSubcategory.DataValueField = "ID";
                comboSubcategory.DataBind();
            }
        }

        protected void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDropDownList1();
        }
        public double length;
        public double width;
        public double height;
        public double volume;
        public double v;
        public double ov;
        public double tw;
        protected void function()
        {
            if (txbLength.Text == "" || txbWidth.Text == "" || txbHeight.Text == "")
            {
                txbVolume.Text = "";
                txbOverallVolume.Text = "";
            }

            if (txbWeight.Text == "")
            {
                txbTotalWeight.Text = "";
            }
            CultureInfo culture = new CultureInfo("en-US");
            if (double.TryParse(txbLength.Text, NumberStyles.Float, culture, out double l))
            {
                length = l;
            }

            if (double.TryParse(txbWidth.Text, NumberStyles.Float, culture, out double w))
            {
                width = w;
            }

            if (double.TryParse(txbHeight.Text, NumberStyles.Float, culture, out double h))
            {
                height = h;
            }

            if (txbLength.Text != "" && txbWidth.Text != "" && txbHeight.Text != "")
            {
                v = length * width * height;
                txbVolume.Text = Convert.ToString($"{v} м3");
            }

            if (txbVolume.Text != "" && txbQuantity.Text != "")
            {
                double q = Convert.ToDouble(txbQuantity.Text);
                ov = v * q;
                txbOverallVolume.Text = Convert.ToString($"{ov} м3");
            }

            if (txbQuantity.Text != "" && txbWeight.Text != "")
            {
                if (double.TryParse(txbWeight.Text, NumberStyles.Float, culture, out double we))
                {
                    int q = Convert.ToInt32(txbQuantity.Text);
                    tw = q * we;
                    txbTotalWeight.Text = Convert.ToString($"{tw} кг");
                }

            }
        }
        public Models.Cargo newItem1;
        protected void apply_Click(object sender, EventArgs e)
        {
            string overallVolume = hiddenOverallVolume.Value;
            string volume = hiddenVolume.Value;
            string totalWeight = hiddenTotalWeight.Value;
            string title = hiddenTitle.Value;
            ApplicationContext app = new ApplicationContext();

            txbTitle.BorderColor = Color.White;
            txbLength.BorderColor = Color.White;
            txbWidth.BorderColor = Color.White;
            txbHeight.BorderColor = Color.White;
            txbQuantity.BorderColor = Color.White;
            txbWeight.BorderColor = Color.White;
            errorLabelEmpty.Visible = false;
            errorLabelNonRight.Visible = false;

            if (overallVolume != ""  && title != "" && title != "0" && totalWeight != "" && txbLength.Text != "" 
                && txbWidth.Text != "" && txbHeight.Text != "" && txbQuantity.Text != "" && txbWeight.Text != "")
            {
                if (overallVolume != "0" && totalWeight != "0" && volume != "0" && title != "")
                {
                    decimal ovv;
                    decimal.TryParse(overallVolume, NumberStyles.Any, CultureInfo.InvariantCulture, out ovv);
                    if (hiddenEd.Value == "2")
                    {
                        ovv /= 1000000;
                    }
                    var cars = app.Cars.ToList();
                    decimal maxVolume = 0;
                    for (int i = 0; i < cars.Count; i++)
                    {
                        decimal carVolume = Convert.ToDecimal(cars[i].Volume);
                        if (i == 0)
                        {
                            maxVolume = carVolume;
                        }
                        else
                        {
                            if (carVolume > maxVolume)
                            {
                                maxVolume = carVolume;
                            }
                        }
                    }
                    if (Convert.ToDecimal(ovv) > maxVolume)
                    {
                        errorMaxVolume.Text = $"Превышен максимально перевозимый объем груза: {maxVolume} м3";
                        errorMaxVolume.Visible = true;
                    }
                    else
                    {
                        var c = Convert.ToInt32(comboCategory.SelectedValue);
                        if (comboSubcategory.SelectedValue == "")
                        {
                            sc = 49;
                        }
                        else
                        {
                            sc = Convert.ToInt32(comboSubcategory.SelectedValue);
                        }

                        decimal overallVolumeDecimal;
                        decimal totalWeightDecimal;

                        // Попробуем конвертировать строку в десятичное число
                        if (decimal.TryParse(overallVolume, NumberStyles.Any, CultureInfo.InvariantCulture, out overallVolumeDecimal) &&
                            decimal.TryParse(totalWeight, NumberStyles.Any, CultureInfo.InvariantCulture, out totalWeightDecimal))
                        {
                            errorLabelNonRight.Visible = false;
                            TextBox v = txbVolume as TextBox;
                            if (hiddenEd.Value == "1")
                            {
                                Models.Cargo newItem = new Models.Cargo
                                {
                                    Title = title,
                                    Category = c,
                                    Subcategory = sc,
                                    OverallVolume = overallVolumeDecimal,
                                    TotalWeight = totalWeightDecimal,
                                    Ed = "m"
                                };
                                Session["myItem1"] = newItem;
                                Response.Redirect("Order.aspx");
                            }
                            else if (hiddenEd.Value == "2")
                            {
                                Models.Cargo newItem = new Models.Cargo
                                {
                                    Title = title,
                                    Category = c,
                                    Subcategory = sc,
                                    OverallVolume = overallVolumeDecimal,
                                    TotalWeight = totalWeightDecimal,
                                    Ed = "sm"
                                };
                                Session["myItem1"] = newItem;
                                Response.Redirect("Order.aspx");
                            }
                        }
                    }
                    

                }
                else
                {
                    if (txbLength.Text == "0")
                    {
                        txbLength.BorderColor = Color.Red;
                        errorLabelNonRight.Visible = true;
                    }
                    if (txbWidth.Text == "0")
                    {
                        txbWidth.BorderColor = Color.Red;
                        errorLabelNonRight.Visible = true;
                    }
                    if (txbHeight.Text == "0")
                    {
                        txbHeight.BorderColor = Color.Red;
                        errorLabelNonRight.Visible = true;
                    }
                    if (txbQuantity.Text == "0")
                    {
                        txbQuantity.BorderColor = Color.Red;
                        errorLabelNonRight.Visible = true;
                    }
                    if (txbWeight.Text == "0")
                    {
                        txbWeight.BorderColor = Color.Red;
                        errorLabelNonRight.Visible = true;
                    }
                    
                }
            }
            else
            {
                if (txbTitle.Text == "")
                {
                    txbTitle.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbLength.Text == "")
                {
                    txbLength.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbWidth.Text == "")
                {
                    txbWidth.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbHeight.Text == "")
                {
                    txbHeight.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbQuantity.Text == "")
                {
                    txbQuantity.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbWeight.Text == "")
                {
                    txbWeight.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
            }
        }

        protected void txbLenght_TextChanged(object sender, EventArgs e)
        {
            function();
        }

        protected void txbWidth_TextChanged(object sender, EventArgs e)
        {
            function();
        }

        protected void txbHeight_TextChanged(object sender, EventArgs e)
        {
            function();
        }

        protected void txbWeight_TextChanged(object sender, EventArgs e)
        {
            function();
        }

        protected void txbQuantity_TextChanged(object sender, EventArgs e)
        {
            function();
        }

        protected void v_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }

        protected void p_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}