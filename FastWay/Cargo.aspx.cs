using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FastWay
{
    
    public partial class Cargo : System.Web.UI.Page
    {
        
        ApplicationContext app = new ApplicationContext();

        public int _id;
        public Models.Cargo newItem1;
        
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                comboDeliveryType.SelectedIndex = 0;
                FillDropDownList();
                func();
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
                string query = "SELECT ID, Type FROM DeliveryType";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                // Заполнение DropDownList данными из базы
                comboDeliveryType.DataSource = reader;
                comboDeliveryType.DataTextField = "Type"; // Поле для отображения
                comboDeliveryType.DataValueField = "ID";  // Значение поля
                comboDeliveryType.DataBind();

                // Закрытие ридера и соединения
                reader.Close();
                connection.Close();
            }
        }

        protected void func()
        {
            int i = comboDeliveryType.SelectedIndex + 1;
            DeliveryType deliveryType;
            deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == i);
            string c = deliveryType.Cost.ToString();
            txbCostForDelivery.Text = "Стоимость перевозки " + c + " рублей";
        }

        protected void comboDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            func();
        }
        public void e(int id)
        {
            _id = id;
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            ApplicationContext app = new ApplicationContext();
            if (txbAddress.Text != "" && txbEmail.Text != "" && txbFIO.Text != "" && txbPhone.Text != ""
            && comboDeliveryType.SelectedItem != null)
            {
                if (checkAgree.Checked == true)
                {
                    int cmbIndex = comboDeliveryType.SelectedIndex + 1;
                    Orders newItem = new Orders
                    {
                        FIO = txbFIO.Text,
                        Email = txbEmail.Text,
                        Phone = txbPhone.Text,
                        Address = txbAddress.Text,
                        DeliveryType = cmbIndex,
                        CargoID = Convert.ToInt32(_id),
                        IsAccepted = 0
                    };
                    Models.Cargo myItem = (Models.Cargo)Session["myItem1"];
                    app.Cargo.Add(myItem);
                    app.Orders.Add(newItem);
                    app.SaveChanges();
                    MessageBox.Show("Заявка отправлена", "Уведомление");
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    MessageBox.Show("Согласитесь с обработкой перснальных данных", "Ошибка");
                }

            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }
    }
}