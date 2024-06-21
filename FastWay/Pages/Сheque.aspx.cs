using FastWay.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace FastWay.Pages
{
    public partial class Сheque : System.Web.UI.Page
    {
        DeliveryType deliveryType;
        Category category;
        Subcategory subcategory;
        ApplicationContext app = new ApplicationContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                outputValues();
                outputRequest();
            }
            Sess();
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
        protected void v_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }

        protected void p_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
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
        string edd = "";
        string kgg = "";
        public void outputValues()
        {
            if (Session["myItem2"]!= null && Session["myItem1"]!= null)
            {
                Orders orders = (Orders)Session["myItem2"];
                Models.Cargo cargo = (Models.Cargo)Session["myItem1"];

                
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

                deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == orders.DeliveryType);
                category = app.Category.FirstOrDefault(x => x.ID == cargo.Category);
                subcategory = app.Subcategory.FirstOrDefault(x => x.ID == cargo.Subcategory);
                
                cargoTitle.Text = "Наименование: " + cargo.Title;
                cargoCategory.Text = "Категория: " + category.Category1;
                cargoSubcategory.Text = "Подкатегория: " + subcategory.Subcategory1;
                cargoOverallVolume.Text = "Общий объём: " + cargo.OverallVolume + edd;
                cargoTotalWeight.Text = "Общий вес: " + cargo.TotalWeight.ToString() + kgg;
                cargoDeliveryType.Text = $"Способ перевозки: {deliveryType.Type} ({deliveryType.Cost} руб/км)" ;
                cost.Text = "Стоимость за перевозку: " + orders.SummaryCost + " руб.";
                sendDate.Text = "Дата погрузки: " + orders.SendingDate.ToString("dd.MM.yyyy HH:mm");
                arrivalDate.Text = "Примерное прибытие: " + orders.ArrivalDate.ToString("dd.MM.yyyy HH:mm");
                if (orders.IsNeedMovers == "yes")
                {
                    movers.Text = "Подъём: да";
                }
                else
                {
                    movers.Text = "Подъём: нет";
                }
                

                orderID.Text = orders.ID.ToString();
                int i = Convert.ToInt32(orderID.Text);
                AcceptOrder acceptOrder = app.AcceptOrder.FirstOrDefault(x => x.AcceptOrderInfo == i);
                if (acceptOrder == null)
                {
                    pProverka.Text = "На проверке администратором";
                }
                else if (acceptOrder.Accept == "no")
                {
                    pProverka.Text = "Отклонена";
                    pRejection.Text = $"Причина - {acceptOrder.reasonForRejection}";
                }
                else if (acceptOrder.Accept == "yes")
                {
                    pProverka.Text = "Одобрена";
                }
               
                orderLastName.Text = "Фамилия: " + orders.LastName;
                orderFirstName.Text = "Имя: " + orders.FirstName;
                orderPatronymic.Text = "Отчество: " + orders.Patronymic;
                orderPhone.Text = "Номер телефона: " + orders.Phone.ToString();
                orderEmail.Text = "Электронная почта: " + orders.Email;
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
                dateOrder.Text = "Дата заявки: " + orders.DateOrder;
            }
            
            
        }

        public void outputRequest()
        {
            Orders orders = (Orders)Session["dataRequest"];
            int? i = orders.CargoID;
            Models.Cargo cargo = app.Cargo.FirstOrDefault(x => x.ID == i);

            if (cargo.Ed != null)
            {
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
            }
            

            deliveryType = app.DeliveryType.FirstOrDefault(x => x.ID == orders.DeliveryType);
            category = app.Category.FirstOrDefault(x => x.ID == cargo.Category);
            subcategory = app.Subcategory.FirstOrDefault(x => x.ID == cargo.Subcategory);

            cargoTitle.Text = "Наименование: " + cargo.Title;
            cargoCategory.Text = "Категория: " + category.Category1;
            cargoSubcategory.Text = "Подкатегория: " + subcategory.Subcategory1;
            cargoOverallVolume.Text = "Общий объём: " + cargo.OverallVolume.ToString() + edd;
            cargoTotalWeight.Text = "Общий вес: " + cargo.TotalWeight.ToString() + kgg;
            cargoDeliveryType.Text = $"Способ перевозки: {deliveryType.Type} ({deliveryType.Cost} руб/км)";
            cost.Text = "Стоимость за перевозку: " + orders.SummaryCost + " руб.";
            sendDate.Text = "Дата погрузки: " + orders.SendingDate.ToString("dd.MM.yyyy HH:mm");
            arrivalDate.Text = "Примерное прибытие: " + orders.ArrivalDate.ToString("dd.MM.yyyy HH:mm");
            if (orders.IsNeedMovers == "yes")
            {
                movers.Text = "Подъём: да";
            }
            else
            {
                movers.Text = "Подъём: нет";
            }

            orderID.Text = orders.ID.ToString();
            int ii = Convert.ToInt32(orderID.Text);
            AcceptOrder acceptOrder = app.AcceptOrder.FirstOrDefault(x => x.AcceptOrderInfo == ii);
            if (acceptOrder == null)
            {
                pProverka.Text = " на проверке администратором";
            }
            else if (acceptOrder.Accept == "no")
            {
                pProverka.Text = " отклонена";
                pRejection.Text = $"Причина - {acceptOrder.reasonForRejection}";
            }
            else if (acceptOrder.Accept == "yes")
            {
                pProverka.Text = " одобрена";
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
            dateOrder.Text = "Дата заявки: " + orders.DateOrder;

        }

        private void DownloadFile(string filePath)
        {
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Заявка.pdf");
            Response.TransmitFile(filePath);
            Response.End();
        }

        protected void download_Click(object sender, EventArgs e)
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
                Font font = new Font(bfArial, 12, Font.NORMAL);

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
                doc.Add(new Paragraph(dateOrder.Text, font));

                doc.Close();
            }

            DownloadFile(filePath);
        }

        public void SendMail()
        {
            Orders orders = (Orders)Session["dataRequest"];
            string toAddress = orderEmail.Text;
            string fileName = "Заявка.txt";
            string filePath = SaveFileForMail(fileName);
            string subject = "Заявка на грузоперевозку от " + orders.DateOrder.ToString();
            string body = $"{orders.LastName} {orders.FirstName} {orders.Patronymic}, заявка на грузоперевозку успешно оформлена и отправлена " +
                $"на проверку достоверности заполненных данных администратору. Информация о заявке прикреплена во вложенном файле. " +
                $"С уважением, FastWay.";
            EmailSender emailSender = new EmailSender();
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
                doc.Add(new Paragraph(dateOrder.Text, font));
                doc.Close();
            }
            return filePath;
        }

        protected void send_Click(object sender, EventArgs e)
        {
            SendMail();
            pnlMessageSendMail.Visible = true;
        }

        protected void messageButton_Click(object sender, EventArgs e)
        {
            pnlMessageSendMail.Visible = false;
        }
    }
}