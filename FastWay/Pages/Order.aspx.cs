using FastWay.Models;
using FastWay.Pages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Activities.Expressions;

namespace FastWay
{

    public partial class Order : System.Web.UI.Page
    {

        ApplicationContext app = new ApplicationContext();
        
        public int _id;
        public Models.Cargo newItem1;
        List<DeliveryType> myType = new List<DeliveryType>();
        
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                comboDeliveryType.SelectedIndex = 0;
                FillDropDownList();
                func();
            }
            txbSummaryCost.ReadOnly = true;
            txbCostForDelivery.ReadOnly = true;
            Sess();
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
        protected void Sess()
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
        }
        protected void Output()
        {
            Models.Profile logcontext = (Models.Profile)Session["log"];
            if (logcontext != null)
            {
                lastNameTxb.Text = logcontext.LastName;
                firstNameTxb.Text = logcontext.FirstName;
                patronymicTxb.Text = logcontext.Patronymic;
                phoneTxb.Text = logcontext.Phone;
                emailTxb.Text = logcontext.Email;
            }
        }
        protected void v_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }

        protected void p_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
       
        protected void FillDropDownList()
        {
            if (myType != null)
            {
                myType.Clear();
                comboDeliveryType.Items.Clear();
            }
            List<DeliveryType> type = app.DeliveryType.ToList();
            myType = app.DeliveryType.ToList();
            List<Cars> car = app.Cars.SqlQuery("select * from Cars").ToList();
            DeliveryType[] myt= new DeliveryType[type.Count];
            Cars[] needCars = new Cars[car.Count];
            comboDeliveryType.DataSource = type;
            comboDeliveryType.DataTextField = "Type"; // Поле для отображения
            comboDeliveryType.DataValueField = "ID";  // Значение поля
            comboDeliveryType.DataBind();
            if (type.Count != 0 && car.Count != 0)
            {
                Models.Cargo cargoItem = (Models.Cargo)Session["myItem1"];
                decimal? overallVolumeFromCargo = cargoItem.OverallVolume;
                if (cargoItem.Ed == "sm")
                {
                    overallVolumeFromCargo /= 1000000; //перевод в кубические метры если единицы измерения в сантиметрах
                }
            }
            

        }

        protected void func()
        {
            List<DeliveryType> type = app.DeliveryType.ToList();
            comboDeliveryType.DataSource = type;
            if (comboDeliveryType.DataSource != null)
            {
                int i = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                
                DeliveryType deliveryType;
                deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == i);
                if (deliveryType != null)
                {
                    int cst = Convert.ToInt32(deliveryType.Cost);
                    string c = deliveryType.Cost.ToString();
                    txbCostForDelivery.Text = c ;
                }
            }
            else
            {
                txbCostForDelivery.Text = "Подходящего способа перевозки нет";
            }
                
        }

        protected void comboDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            func();
        }
        public void e(int id)
        {
            _id = id;
        }

        protected void apply_Click(object sender, EventArgs e)
        {
            lastNameTxb.BorderColor = Color.White;
            firstNameTxb.BorderColor = Color.White;
            patronymicTxb.BorderColor = Color.White;
            emailTxb.BorderColor = Color.White;
            phoneTxb.BorderColor = Color.White;
            txbFromAddress.BorderColor = Color.White;
            txbToAddress.BorderColor = Color.White;
            checkAgree.BackColor = Color.Transparent;
            errorLabelEmpty.Visible = false;
            errorLabelEmail.Visible = false;
            errorLabelPers.Visible = false;
            errorLabelCombo.Visible = false;


            ApplicationContext app = new ApplicationContext();
            if (txbFromAddress.Text != "" && txbToAddress.Text != "" && emailTxb.Text != "" &&
            lastNameTxb.Text != "" && firstNameTxb.Text != "" && patronymicTxb.Text != "" && phoneTxb.Text != "" && txtDateTime.Text != "")
            {
                if (DateTime.Parse(txtDateTime.Text) <= DateTime.Now)
                {
                    errorDateSend.Visible = true;
                    txtDateTime.Style["border"] = "1px solid red";
                }
                else
                {
                    errorDateSend.Visible = false;
                    txtDateTime.Style["border"] = "1px solid white";
                    if (comboDeliveryType.SelectedItem != null)
                    {
                        if (IsValidEmail(emailTxb.Text))
                        {
                            if (checkAgree.Checked == true)
                            {
                                string fromAddress = "";
                                string toAddress = "";
                                if (kvartFromAddress.Text != "" && padikFromAddress.Text != "")
                                {
                                    fromAddress = $"{txbFromAddress.Text}, подъезд {padikFromAddress.Text} кв. {kvartFromAddress.Text}";
                                }
                                else
                                {
                                    fromAddress = txbFromAddress.Text;
                                }
                                if (kvartToAddress.Text != "" && padikToAddress.Text != "")
                                {
                                    toAddress = $"{txbToAddress.Text}, подъезд {padikToAddress.Text} кв. {kvartToAddress.Text}";
                                }
                                else
                                {
                                    toAddress = txbToAddress.Text;
                                }
                                int cmbIndex = Convert.ToInt32(comboDeliveryType.SelectedItem.Value);
                                var deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == cmbIndex);
                                int cst = Convert.ToInt32(deliveryType.Cost);
                                int sc = cst * Convert.ToInt32(txbDistance.Text);
                                string phone = phoneTxb.Text.ToString();
                                string isNeedMv = "";
                                if (checkMovers.Checked == true)
                                {
                                    isNeedMv = "no";
                                }
                                else
                                {
                                    isNeedMv = "yes";
                                }

                                Orders orderItem = new Orders
                                {
                                    LastName = lastNameTxb.Text,
                                    FirstName = firstNameTxb.Text,
                                    Patronymic = patronymicTxb.Text,
                                    Email = emailTxb.Text,
                                    Phone = phone,
                                    FromAddress = fromAddress,
                                    ToAddress = toAddress,
                                    SummaryCost = sc.ToString(),
                                    Duration = txbDuration.Text,
                                    Distance = txbDistance.Text,
                                    DeliveryType = cmbIndex,
                                    CargoID = Convert.ToInt32(_id),
                                    IsAccepted = 0,
                                    DateOrder = DateTime.Now,
                                    SendingDate = DateTime.Parse(hiddenDate.Value),
                                    ArrivalDate = DateTime.Parse(txtDateTime.Text),
                                    IsNeedMovers = isNeedMv
                                };
                                Models.Cargo cargoItem = (Models.Cargo)Session["myItem1"];
                                app.Cargo.Add(cargoItem);
                                app.Orders.Add(orderItem);
                                app.SaveChanges();

                                Session["myItem2"] = orderItem;
                                Models.Profile logcontext = (Models.Profile)Session["log"];
                                if (logcontext != null)
                                {
                                    OrdersInProfile ordInPr = new OrdersInProfile
                                    {
                                        OrderID = orderItem.ID,
                                        ProfileID = logcontext.ID
                                    };
                                    app.OrdersInProfile.Add(ordInPr);
                                    app.SaveChanges();
                                    Session["sendMail"] = 1;
                                    Response.Redirect("ChequeForAutorizated.aspx");
                                }
                                else
                                {
                                    Session["dataRequest"] = orderItem;
                                    Session["sendMail"] = 1;

                                    List<Models.Profile> oo = app.Profile.ToList(); // лист с профилями
                                    int c = 0; //проверка прошел ли if на email
                                    for (int i = 0; i < oo.Count; i++)
                                    {
                                        if (oo[i].Email == emailTxb.Text) //если не авторизовался и заказал, при совпадение email заказ привязывается к профилю с соответствующим email
                                        {
                                            OrdersInProfile ordInPr = new OrdersInProfile
                                            {
                                                OrderID = orderItem.ID,
                                                ProfileID = oo[i].ID
                                            };
                                            app.OrdersInProfile.Add(ordInPr);
                                            app.SaveChanges();
                                            c = 1;
                                            break;
                                        }

                                    }
                                    if (c == 0) //если не зарегал профиль с таким email, то добавляется в таблицу заказов для незареганных юзеров
                                    {
                                        OrdersNonReg ordNonPr = new OrdersNonReg
                                        {
                                            OrderID = orderItem.ID
                                        };
                                        app.OrdersNonReg.Add(ordNonPr);
                                        app.SaveChanges();
                                    }
                                    Response.Redirect("Сheque.aspx");
                                }

                            }
                            else
                            {
                                errorLabelPers.Visible = true;
                            }

                        }
                        else
                        {
                            emailTxb.BorderColor = Color.Red;
                            errorLabelEmail.Visible = true;
                        }

                    }
                    else
                    {
                        comboDeliveryType.BorderColor = Color.Red;
                        errorLabelCombo.Visible = true;
                    }
                }
                

            }
            else
            {
                if (lastNameTxb.Text == "")
                {
                    lastNameTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (firstNameTxb.Text == "")
                {
                    firstNameTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (patronymicTxb.Text == "")
                {
                    patronymicTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (emailTxb.Text == "")
                {
                    emailTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (phoneTxb.Text == "")
                {
                    phoneTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbFromAddress.Text == "")
                {
                    txbFromAddress.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbToAddress.Text == "")
                {
                    txbToAddress.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txtDateTime.Text == "")
                {
                    txtDateTime.Style["border"] = "1px solid red";
                    errorLabelEmpty.Visible = true;
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            // Проверяем, что строка не состоит только из нулей или пробелов
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Проверяем адрес электронной почты с помощью регулярного выражения
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

      
    }
    
}