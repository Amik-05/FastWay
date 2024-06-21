using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using FastWay.Models;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace FastWay.Pages
{
    public partial class AdminViewingRequests : System.Web.UI.Page
    {
        Models.Orders orders;
        Models.Cargo cargo;
        Models.DeliveryType deliveryType;
        Models.Category category;
        Models.Subcategory subcategory;
        ApplicationContext app = new ApplicationContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropDownList();
                outputValues();
            }
        }

        protected void FillDropDownList()
        {
            var i = app.Orders.Where(x => x.IsAccepted == 0).ToList();
            comboRequests.DataSource = i;
            comboRequests.DataTextField = "ID";
            comboRequests.DataValueField = "ID";
            comboRequests.DataBind();
        }


        protected int _orderID
        {
            get { return ViewState["_orderID"] as int? ?? 0; }
            set { ViewState["_orderID"] = value; }
        }
        protected int orderssID
        {
            get { return ViewState["orderssID"] as int? ?? 0; }
            set { ViewState["orderssID"] = value; }
        }
        string edd = " м3";
        string kgg = " кг";
        public void outputValues()
        {
            DropDownList comboRequests = FindControl("comboRequests") as DropDownList;
            if (comboRequests != null)
            {
                
                var selectedItem = comboRequests.SelectedItem as System.Web.UI.WebControls.ListItem;

                if (selectedItem != null)
                {
                    confirm.Style["opacity"] = "1";
                    confirm.Style["pointer-events"] = "auto";
                    reject.Style["opacity"] = "1";
                    reject.Style["pointer-events"] = "auto";
                    int ordersID;
                    if (int.TryParse(selectedItem.Value, out ordersID))
                    {
                        
                        orderssID  = ordersID;
                        _orderID = ordersID;
                        orders = app.Orders.FirstOrDefault(x => x.ID == ordersID);
                        cargo = app.Cargo.FirstOrDefault(x => x.ID == orders.CargoID);
                        deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == orders.DeliveryType);
                        category = app.Category.FirstOrDefault(x => x.ID == cargo.Category);
                        subcategory = app.Subcategory.FirstOrDefault(x => x.ID == cargo.Subcategory);
                        if (cargo.Ed == "m")
                        {
                            edd = " м3";
                            kgg = " кг";
                        }
                        else
                        {
                            edd = " см3";
                            kgg = " г";
                        }
                        cargoTitle.Text = "Наименование: " + cargo.Title;
                        cargoCategory.Text = "Категория: " + category.Category1;
                        cargoSubcategory.Text = "Подкатегория: " + subcategory.Subcategory1;
                        cargoOverallVolume.Text = "Общий объём: " + cargo.OverallVolume.ToString() + edd;
                        cargoTotalWeight.Text = "Общий вес: " + cargo.TotalWeight.ToString() + kgg;
                        cargoDeliveryType.Text = $"Способ перевозки: {deliveryType.Type} ({deliveryType.Cost} руб/км)";
                        cost.Text = "Стоимость за перевозку: " + orders.SummaryCost + " руб.";
                        sendDate.Text = "Дата погрузки: " + orders.SendingDate.ToString("dd.MM.yyyy HH:mm");
                        arrivalDate.Text = "Примерное прибытие: " + orders.ArrivalDate.ToString("dd.MM.yyyy HH:mm");
                        var m = app.Movers.Where(x => x.Status == "Свободен").ToList();
                        var m1 = app.Movers.Where(x => x.Status == "Свободен").ToList();
                        var m2 = app.Movers.Where(x => x.Status == "Свободен").ToList();
                        m1.Clear();
                        m2.Clear();
                        for (int i = 0; i < m.Count; i++)
                        {
                            if (i % 2 == 0)
                            {
                                m1.Add(m[i]);
                            }
                            else
                            {
                                m2.Add(m[i]);
                            }

                        }
                        for (int i = 0; i < m1.Count; i++)
                        {
                            m1[i].FIO = $"{m1[i].ID} {m1[i].LastName} {m1[i].FirstName} {m1[i].Patronymic}";
                        }
                        for (int i = 0; i < m2.Count; i++)
                        {
                            m2[i].FIO = $"{m2[i].ID} {m2[i].LastName} {m2[i].FirstName} {m2[i].Patronymic}";
                        }
                        comboMover1.DataSource = m1;
                        comboMover1.DataTextField = "FIO";
                        comboMover1.DataValueField = "ID";
                        comboMover1.DataBind();
                        comboMover2.DataSource = m2;
                        comboMover2.DataTextField = "FIO";
                        comboMover2.DataValueField = "ID";
                        comboMover2.DataBind();
                        if (orders.IsNeedMovers == "yes")
                        {
                            movers.Text = "Подъем: да";
                            comboMover1.Style["opacity"] = "1";
                            comboMover1.Style["pointer-events"] = "auto";
                            comboMover2.Style["opacity"] = "1";
                            comboMover2.Style["pointer-events"] = "auto";
                        }
                        else
                        {
                            movers.Text = "Подъем: нет";
                            comboMover1.Style["opacity"] = "0.5";
                            comboMover1.Style["pointer-events"] = "none";
                            comboMover2.Style["opacity"] = "0.5";
                            comboMover2.Style["pointer-events"] = "none";
                        }

                        orderLastName.Text = "Фамилия: " + orders.LastName;
                        orderFirstName.Text = "Имя: " + orders.FirstName;
                        orderPatronymic.Text = "Отчество: " + orders.Patronymic;
                        orderPhone.Text = "Номер телефона: " + orders.Phone.ToString();
                        orderEmail.Text = "Почта: " + orders.Email;
                        orderFromAddress.Text = $"Откуда: {orders.FromAddress}";
                        orderToAddress.Text = $"Куда: {orders.ToAddress}";
                        int distance = Convert.ToInt32(orders.Distance);
                        string kilometersText;
                        if (distance % 10 == 1 && distance % 100 != 11)
                        {
                            kilometersText = "километр";
                        }
                        else if (distance % 10 >= 2 && distance % 10 <= 4 && (distance % 100 < 10 || distance % 100 >= 20))
                        {
                            kilometersText = "километра";
                        }
                        else if (distance == 11 || distance == 12 || distance == 13 || distance == 14)
                        {
                            kilometersText = "километров";
                        }
                        else
                        {
                            kilometersText = "километров";
                        }
                        int duration = Convert.ToInt32(orders.Duration);
                        string minutesText;
                        if (duration % 10 == 1 && duration % 100 != 11)
                        {
                            minutesText = "минута";
                        }
                        else if (duration % 10 >= 2 && duration % 10 <= 4 && (duration % 100 < 10 || duration % 100 >= 20))
                        {
                            minutesText = "минуты";
                        }
                        else if (duration == 11)
                        {
                            minutesText = "минут";
                        }
                        else
                        {
                            minutesText = "минут";
                        }
                        orderDistance.Text = $"Расстояние: {distance} {kilometersText}";
                        orderDuration.Text = $"Время в пути: {duration} {minutesText}";
                        dateOrder.Text = "Дата: " + orders.DateOrder;
                    }
                }
                else
                {
                    confirm.Style["opacity"] = "0.5";
                    confirm.Style["pointer-events"] = "none";
                    reject.Style["opacity"] = "0.5";
                    reject.Style["pointer-events"] = "none";
                }
            }
            
        }



        public void ClearValues()
        {
            cargoTitle.Text = "";
            cargoCategory.Text = "";
            cargoSubcategory.Text = "";
            cargoOverallVolume.Text = "";
            cargoTotalWeight.Text = "";
            cargoDeliveryType.Text = "";
            cost.Text = "";
            sendDate.Text = "";
            arrivalDate.Text = "";
            movers.Text = "";

            orderLastName.Text = "";
            orderFirstName.Text = "";
            orderPatronymic.Text = "";
            orderPhone.Text = "";
            orderEmail.Text = "";
            orderFromAddress.Text = "";
            orderToAddress.Text = "";
            orderDistance.Text = "";
            orderDuration.Text = "";
            dateOrder.Text = "";
        }

        protected void comboDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            outputValues();

        }

        protected void confirm_Click(object sender, EventArgs e)
        {
            if (comboRequests.SelectedIndex >= 0)
            {
                pnlMessageQuestionConfirm.Visible = true;
            }
        }
        protected void reject_Click(object sender, EventArgs e)
        {
            rejectionDiv.Visible = true;
        }
        protected void okReject_Click(object sender, EventArgs e)
        {
            if (comboRequests.SelectedIndex >= 0)
            {
                if (txtRejectionReason.Text != "")
                {
                    txtRejectionReason.BorderColor = Color.White;
                    Orders orders = new Orders();
                    orders = app.Orders.FirstOrDefault(x => x.ID == orderssID);
                    orders.IsAccepted = 1;
                    app.SaveChanges();

                    AcceptOrder newItem = new AcceptOrder
                    {
                        AcceptOrderInfo = _orderID,
                        Accept = "no",
                        reasonForRejection = txtRejectionReason.Text
                    };
                    app.AcceptOrder.Add(newItem);
                    app.SaveChanges();
                    rejectionDiv.Visible = false;
                    SendMail(1); 
                    pnlMessage.Visible = true;
                    ClearValues();
                    FillDropDownList();
                    outputValues();
                    
                }
                else
                {
                    txtRejectionReason.BorderColor = Color.Red;
                }
            }
        }

        public void SendMail(int i)
        {
            Orders orders = app.Orders.FirstOrDefault(x => x.ID == orderssID);
            string toAddress = orders.Email.ToString();
            string filePath = SaveFileForMail("Заявка");
            string subject = "Заявка на грузоперевозку от " + orders.DateOrder.ToString();
            string body;
            if (i == 1 && txtRejectionReason.Text != "")
            {
                body = $"{orders.LastName} {orders.FirstName} {orders.Patronymic}, заявка на грузоперевозку отклонена администратором. " +
                    $"Причина: {txtRejectionReason.Text}. Пожалуйста, внесите корректировки в свою заявку и отправьте ее заново. " +
                    $"Информация о Вашей заявке прикреплена во вложенном файле. " +
                    $"С уважением, FastWay.";
            }
            else
            {
                body = $"{orders.LastName} {orders.FirstName} {orders.Patronymic}, заявка на грузоперевозку одобрена и будет принята в работу." +
                    $"Информация о Вашей заявке прикреплена во вложенном файле. " +
                    $"С уважением, FastWay.";
            }
            FastWay.EmailSender emailSender = new FastWay.EmailSender();
            emailSender.SendEmailWithAttachment(toAddress, subject, body, filePath);

        }

        private string SaveFileForMail(string fileName)
        {
            string appDataPath = Server.MapPath("~/App_Data");

            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            string filePath = Path.Combine(appDataPath, "Заявка.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // Загрузка шрифта Arial
                string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont bfArial = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bfArial, 12, iTextSharp.text.Font.NORMAL);

                // Добавление содержания в PDF с использованием шрифта Arial
                doc.Add(new Paragraph("Информация о грузе:", font));
                doc.Add(new Paragraph(cargoTitle.Text, font));
                doc.Add(new Paragraph(cargoCategory.Text, font));
                doc.Add(new Paragraph(cargoSubcategory.Text, font));
                doc.Add(new Paragraph(cargoOverallVolume.Text, font));
                doc.Add(new Paragraph(cargoTotalWeight.Text, font));
                doc.Add(new Paragraph(cargoDeliveryType.Text, font));
                doc.Add(new Paragraph(cost.Text, font));
                doc.Add(new Paragraph(sendDate.Text, font));
                doc.Add(new Paragraph(arrivalDate.Text, font));
                doc.Add(new Paragraph(movers.Text, font));
                doc.Add(new Paragraph(" ", font));
                doc.Add(new Paragraph("Информация о заказчике:", font));
                doc.Add(new Paragraph(orderLastName.Text, font));
                doc.Add(new Paragraph(orderFirstName.Text, font));
                doc.Add(new Paragraph(orderPatronymic.Text, font));
                doc.Add(new Paragraph(orderPhone.Text, font));
                doc.Add(new Paragraph(orderEmail.Text, font));
                doc.Add(new Paragraph(orderFromAddress.Text, font));
                doc.Add(new Paragraph(orderToAddress.Text, font));
                doc.Add(new Paragraph(orderDistance.Text, font));
                doc.Add(new Paragraph(orderDuration.Text, font));
                doc.Add(new Paragraph(dateOrder.Text, font));

                doc.Close();
            }
            return filePath;
        }

        protected void messageButton_Click(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;
            pnlMessageConfirm.Visible = false;
        }

        protected void messageButtonConfirmRequesYes_Click(object sender, EventArgs e)
        {
            comboMover1.BorderColor = Color.White;
            comboMover2.BorderColor = Color.White;
            if (movers.Text == "Грузчики: да")
            {
                if (comboMover1.SelectedItem != null && comboMover2.SelectedItem != null)
                {
                    int? m1ID = Convert.ToInt32(comboMover1.SelectedItem.Value);
                    int? m2ID = Convert.ToInt32(comboMover2.SelectedItem.Value);
                    var mv1 = app.Movers.FirstOrDefault(x => x.ID == m1ID);
                    var mv2 = app.Movers.FirstOrDefault(x => x.ID == m2ID);
                    int oid = Convert.ToInt32(comboRequests.SelectedItem.Value);
                    MoversInOrder moverItem = new MoversInOrder
                    {
                        OrderID = oid,
                        Mover1 = mv1.ID,
                        Mover2 = mv2.ID
                    };
                    mv1.Status = "Занят";
                    mv2.Status = "Занят";
                    app.SaveChanges();
                    int? oid1 = Convert.ToInt32(comboRequests.SelectedItem.Value);
                    app.MoversInOrder.Add(moverItem);
                    app.SaveChanges();
                    Orders orders = new Orders();
                    orders = app.Orders.FirstOrDefault(x => x.ID == orderssID);
                    orders.IsAccepted = 1;
                    app.SaveChanges();
                    AcceptOrder newItem = new AcceptOrder
                    {
                        AcceptOrderInfo = _orderID,
                        Accept = "yes",

                    };
                    app.AcceptOrder.Add(newItem);
                    app.SaveChanges();
                    pnlMessageQuestionConfirm.Visible = false;
                    pnlMessageConfirm.Visible = true;
                    SendMail(2);
                    ClearValues();
                    FillDropDownList();
                    outputValues();
                }
                else
                {
                    if (comboMover1.SelectedItem == null)
                    {
                        comboMover1.BorderColor = Color.Red;
                    }
                    if (comboMover2.SelectedItem == null)
                    {
                        comboMover2.BorderColor = Color.Red;
                    }
                }
                    
            }
            else
            {
                Orders orders = new Orders();
                orders = app.Orders.FirstOrDefault(x => x.ID == orderssID);
                orders.IsAccepted = 1;
                app.SaveChanges();
                AcceptOrder newItem = new AcceptOrder
                {
                    AcceptOrderInfo = _orderID,
                    Accept = "yes",

                };
                app.AcceptOrder.Add(newItem);
                app.SaveChanges();
                pnlMessageQuestionConfirm.Visible = false;
                pnlMessageConfirm.Visible = true;
                SendMail(2);
                ClearValues();
                FillDropDownList();
                outputValues();
            }
            
        }

        protected void messageButtonConfirmRequesNo_Click(object sender, EventArgs e)
        {
            pnlMessageQuestionConfirm.Visible = false;
            pnlMessageConfirm.Visible = false;
        }
    }
}