using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FastWay
{
    public partial class Order : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                comboCategory.SelectedIndex = 0;
                FillDropDownList();
                FillDropDownList1();
            }
            
        }

        protected void FillDropDownList()
        {
            
            // Подключение к базе данных
            string connectionString = "data source=ASUS\\SQLEXPRESS;initial catalog=FastWay;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие соединения
                connection.Open();

                // Запрос к базе данных
                string query = "SELECT ID, Category FROM Category";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                // Заполнение DropDownList данными из базы
                comboCategory.DataSource = reader;
                comboCategory.DataTextField = "Category"; // Поле для отображения
                comboCategory.DataValueField = "ID";  // Значение поля
                comboCategory.DataBind();

                // Закрытие ридера и соединения
                reader.Close();
                connection.Close();
            }
        }



        protected void FillDropDownList1()
        {
            // Подключение к базе данных
            string connectionString = "data source=ASUS\\SQLEXPRESS;initial catalog=FastWay;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие соединения
                connection.Open();

                // Запрос к базе данных

                int cid = comboCategory.SelectedIndex + 1;
                string query = "SELECT ID, Subcategory FROM Subcategory where CategoryID = " + cid + "";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                // Заполнение DropDownList данными из базы
                comboSubcategory.DataSource = reader;
                comboSubcategory.DataTextField = "Subcategory"; // Поле для отображения
                comboSubcategory.DataValueField = "ID";  // Значение поля
                comboSubcategory.DataBind();

                // Закрытие ридера и соединения
                reader.Close();
                connection.Close();
            }
        }
        protected void MyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            ApplicationContext app = new ApplicationContext();
            if (txbTitle.Text != "" && txbOverallVolume.Text != "" && txbTotalWeight.Text != ""
                && comboSubcategory.SelectedItem != null && comboCategory.SelectedItem != null)
            {
                string title = txbTitle.Text;
                string o = txbOverallVolume.Text;
                string v = txbTotalWeight.Text;
                string overalVolume = o.Substring(0, o.Length - 3); 
                string totalWeight = v.Substring(0, v.Length - 3);
                var c = comboCategory.SelectedIndex + 1;
                var sc = comboSubcategory.SelectedIndex + 1;
                Models.Cargo newItem = new Models.Cargo
                {
                    Title = title,
                    Category = c,
                    Subcategory = sc,
                    OverallVolume = Convert.ToDecimal(overalVolume),
                    TotalWeight = Convert.ToDecimal(totalWeight)
                };
                Session["myItem1"] = newItem;
                Response.Redirect("Cargo.aspx");
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
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

        
    }
}