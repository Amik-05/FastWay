using System;
using FastWay.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Policy;
using System.Drawing;
using System.Text.RegularExpressions;

namespace FastWay.Pages
{
    public partial class Registration : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            txbCode.MaxLength = 6;
        }
        protected void reg_Click(object sender, EventArgs e)
        {
            ApplicationContext app = new ApplicationContext();
            string lastName = lastNameTxb.Text;
            string firstName = firstNameTxb.Text;
            string patronymic = patronymicTxb.Text;
            string phone = phoneTxb.Text;
            string email = emailTxb.Text;
            string password = passwordTxb.Text;

            lastNameTxb.BorderColor = Color.White;
            firstNameTxb.BorderColor = Color.White;
            patronymicTxb.BorderColor = Color.White;
            emailTxb.BorderColor = Color.White;
            phoneTxb.BorderColor = Color.White;
            passwordTxb.BorderColor = Color.White;
            errorLabelEmpty.Visible = false;
            errorLabelEmail.Visible = false;
            errorLabelPers.Visible = false;
            errorLabelPass.Visible = false;
            errorLabel.Visible = false;

            if (password != "" && lastName != "" && firstName != "" && patronymic != "" && phone != "" && email != "")
            {
                if (IsValidEmail(email))
                {
                    if (checkAgree.Checked == true)
                    {
                        Models.Profile profileList = app.Profile.FirstOrDefault(x => x.Email == email);
                        if (profileList == null)
                        {

                            if (IsPasswordValid(passwordTxb.Text))
                            {
                                hiddenPass.Text = password;
                                pnlEmailConfirm.Visible = true;
                                SendMail();
                                
                                
                            }
                            else
                            {
                                errorLabelPass.Visible = true;
                                passwordTxb.BorderColor = Color.Red;
                            }
                        }
                        else
                        {
                            passwordTxb.Text = "";
                            errorLabel.Visible = true;
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
                if (lastName == "")
                {
                    lastNameTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (firstName == "")
                {
                    firstNameTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (patronymic == "")
                {
                    patronymicTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (email == "")
                {
                    emailTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (phone == "")
                {
                    phoneTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (password == "")
                {
                    passwordTxb.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
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
        protected void isRightCode_Click(object sender, EventArgs e)
        {
            if (txbCode.Text == "")
            {
                errorLabelEmptyCode.Visible = true;
                txbCode.BorderColor = Color.Red;
            }
            else
            {
                errorLabelEmptyCode.Visible = false;
                txbCode.BorderColor = Color.White;
                string code = txbCode.Text;
                IfRightCode(code);
            }
        }

        private void IfRightCode(string code)
        {
            if (code == hiddenCode.Text)
            {
                txbCode.BorderColor = Color.Lime;
                errorLabelWrongCode.Visible = false;
                txbCode.Style["opacity"] = "0.25";
                isRightCode.Style["display"] = "none";
                confirmDelete.Style["display"] = "inline";
                txbCode.ReadOnly = true;
            }
            else
            {
                txbCode.BorderColor = Color.Red;
                errorLabelWrongCode.Visible = true;
            }
        }
        public int intcode = 0;
        public void SendMail()
        {
            Random random = new Random();
            intcode = random.Next(100000, 999999);
            string email = emailTxb.Text.Trim().ToLower();
            string subject = "Подтверждение аккаунта";
            string body = $"{lastNameTxb.Text} {firstNameTxb.Text} {patronymicTxb.Text}, для подтверждения аккаунта используйте код: {intcode}. С уважением, Администрация FastWay";
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmailWithoutAttachment(subject, body, email);
            hiddenCode.Text = intcode.ToString();
        }
        protected void confirmDelete_Click(object sender, EventArgs e)
        {
            
            ApplicationContext app = new ApplicationContext();
            string lastName = lastNameTxb.Text;
            string firstName = firstNameTxb.Text;
            string patronymic = patronymicTxb.Text;
            string phone = phoneTxb.Text;
            string email = emailTxb.Text;
            string password = hiddenPass.Text;

            errorLabelPass.Visible = false;
            lastNameTxb.BorderColor = Color.White;
            firstNameTxb.BorderColor = Color.White;
            patronymicTxb.BorderColor = Color.White;
            emailTxb.BorderColor = Color.White;
            phoneTxb.BorderColor = Color.White;
            passwordTxb.BorderColor = Color.White;
            errorLabelEmpty.Visible = false;
            Models.Profile newItem = new Models.Profile
            {
                LastName = lastName,
                FirstName = firstName,
                Patronymic = patronymic,
                Phone = phone,
                Email = email,
                Password = password
            };
            app.Profile.Add(newItem);
            app.SaveChanges();
            var o = app.OrdersNonReg.ToList(); //список ID заказов из таблицы OrdersNonReg
            List<Orders> oo = app.Orders.ToList(); //пустой лист с заказами
            oo.Clear();
            for (int i = 0; i < o.Count; i++) //заполняем лист заявками для нерег пользователей
            {
                int? er = o[i].OrderID; //79
                var orders = app.Orders.FirstOrDefault(x => x.ID == er);
                if (orders != null)
                {
                    oo.Add(orders);
                }

            }
            for (int i = 0; i < o.Count; i++) //добавляем пользователя и заказ в таблицу OrdersInProfile
            {
                if (newItem.Email == oo[i].Email)
                {
                    OrdersInProfile or = new OrdersInProfile
                    {
                        OrderID = oo[i].ID,
                        ProfileID = newItem.ID
                    };
                    app.OrdersInProfile.Add(or);
                    app.SaveChanges();
                }
            }
            
            pnlMessage.Visible = true;
        }
        protected void cancelDelete_Click(object sender, EventArgs e)
        {
            txbCode.BorderColor = Color.White;
            txbCode.Text = "";
            txbCode.Style["opacity"] = "1";
            confirmDelete.Style["display"] = "none";
            errorLabelWrongCode.Visible = false;
            txbCode.ReadOnly = false;
            pnlEmailConfirm.Visible = false;
        }
        protected void messageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Autorization.aspx");
        }

        
    }
}