using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastWay.Models;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace FastWay.Pages
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        public int intcode = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SendMail()
        {
            var em = app.Profile.FirstOrDefault(x => x.Email == txbEmail.Text);
            if (txbEmail.Text == "")
            {
                txbEmail.BorderColor = Color.Red;
                errorLabelNon.Visible = true;
            }
            else if (IsValidEmail(txbEmail.Text))
            {
                errorLabelEmail.Visible = false;
                if (em != null)
                {
                    lblMessage.Visible = true;
                    Random random = new Random();
                    intcode = random.Next(100000, 999999);
                    Orders orders = (Orders)Session["dataRequest"];
                    string email = txbEmail.Text.Trim().ToLower();
                    string subject = "Восстановление пароля";
                    string body = $"{em.LastName} {em.FirstName} {em.Patronymic}, Вы запросили восстановление Вашего пароля на сайте FastWay. Если Вы этого не делали, проигнорируйте это письмо. Чтобы поменять пароль на другой, используйте код: {intcode}. С уважением, Администрация FastWay";
                    EmailSender emailSender = new EmailSender();
                    emailSender.SendEmailWithoutAttachment(subject, body, email);
                    errorLabelNonSys.Visible = false;
                    hiddenCode.Text = intcode.ToString();
                    txbCode.Text = "";
                    txbEmail.BorderColor = Color.White;
                    txbCode.BorderColor = Color.White;
                    txbCode.ReadOnly = false;
                    txbCode.Style["opacity"] = "1";
                    lblMessage.Visible = false;
                    pnlMessageSendMail.Visible = true;
                }
                else
                {
                    txbEmail.BorderColor = Color.Red;
                    errorLabelNonSys.Visible = true;
                    errorLabelWrongCode.Visible = false;
                    txbPass.ReadOnly = true;
                }
            }
            else
            {
                txbEmail.BorderColor = Color.Red;
                errorLabelEmail.Visible = true;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string code = txbCode.Text;
            if (code.Length == 6)
            {
                // Ваш код обработки
                IfRightCode(code);
            }
        }
        protected void login_Click(object sender, EventArgs e)
        {
            if (txbPass.Text.Trim() != "")
            {
                var p = app.Profile.Where(x => x.Email == txbEmail.Text).ToList();
                if (p[0].Password == txbPass.Text)
                {
                    txbPass.BorderColor = Color.Red;
                    errorLabelSamePass.Visible = true;
                }
                else
                {
                    if (IsPasswordValid(txbPass.Text))
                    {
                        errorLabelWrongPass.Visible = false;
                        errorLabelSamePass.Visible = false;
                        txbPass.BorderColor = Color.White;
                        p[0].Password = txbPass.Text;
                        app.SaveChanges();
                        pnlMessage.Visible = true;
                    }
                    else
                    {
                        errorLabelWrongPass.Visible = true;
                        txbPass.BorderColor = Color.Red;
                    }
                }
            }
        }
        private bool IsPasswordValid(string password)
        {
            //Проверка длины пароля
            if (password.Length < 6)
                return false;

            //Проверка наличия заглавных и строчных букв
            if (!Regex.IsMatch(password, "[a-z]") || !Regex.IsMatch(password, "[A-Z]"))
                return false;

            //Проверка наличия хотя бы одной цифры
            if (!Regex.IsMatch(password, "[0-9]"))
                return false;

            return true;
        }

        protected void messageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }
        
        

        private void IfRightCode(string code)
        {
            if (code == hiddenCode.Text)
            {
                txbCode.BorderColor = Color.Lime;
                lnkResend.Enabled = false;
                txbCode.ReadOnly = true;
                txbEmail.ReadOnly = true;
                txbPass.ReadOnly = false;
                txbPass.Style["opacity"] = "1";
                txbCode.Style["opacity"] = "0.25";
                txbEmail.Style["opacity"] = "0.25";
                lnkResend.Style["color"] = "gray";
                txbEmail.BorderColor = Color.White;
                errorLabelWrongCode.Visible = false;
                errorLabelEmail.Visible = false;
            }
            else
            {
                txbCode.BorderColor = Color.Red;
                errorLabelWrongCode.Visible = true;
                txbCode.ReadOnly = false;
            }
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            SendMail();
            
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

        protected void messageButtonCloseMailSended_Click(object sender, EventArgs e)
        {
            pnlMessageSendMail.Visible = false;
        }
    }
}