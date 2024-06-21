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
    public partial class Profile : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        List<Orders> dataCopy = new List<Orders>();
        public List<Orders> data;
        public List<Orders> oldOrders;
        public string oldEmail = "";
        public Models.Profile profileContext;
        public Models.Profile logcontext;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore();
                outputProfile();
                Session["sendMail"] = null;
                SetTextBoxesNonEditable();
            }
            outputRequests();
            

        }

        protected void outputProfile()
        {
            logcontext = (Models.Profile)Session["log"];
            if (logcontext != null)
            {
                idProfile.Text = logcontext.ID.ToString();
                lastNameTxb.Text = logcontext.LastName;
                firstNameTxb.Text = logcontext.FirstName;
                patronymicTxb.Text = logcontext.Patronymic;
                phoneTxb.Text = logcontext.Phone;
                emailTxb.Text = logcontext.Email; 
                passwordTxb.Text = logcontext.Password;
                hiddenOldEmail.Text = logcontext.Email;
            }

        }

        protected void outputRequests()
        {
            logcontext = (Models.Profile)Session["log"];
            
            if (logcontext != null)
            {
                
                var o = app.Profile.FirstOrDefault(ss => ss.Email == logcontext.Email);
                var ordInPr = app.OrdersInProfile.Where(s => s.ProfileID == o.ID).ToList();
                int? ordId;
                data = app.Orders.ToList();
                data.Clear();
                for (int i = 0; i < ordInPr.Count; i++)
                {
                    ordId = ordInPr[i].OrderID;
                    data.AddRange(app.Orders.Where(x => x.ID == ordId).ToList());
                }
                for (int i = 0; i < data.Count; i++)
                {
                    int? sid = data[i].ID;
                    var s = app.AcceptOrder.FirstOrDefault(x => x.AcceptOrderInfo == sid);
                    if (s == null)
                    {
                        data[i].Status = "В процессе";
                    }
                    else if (s.Accept == "no")
                    {
                        data[i].Status = "Отклонена";
                    }
                    else if (s.Accept == "yes")
                    {
                        data[i].Status = "Одобрена";
                    }

                }
                if (ordInPr.Count == 0)
                {
                    emptyListPanel.Visible = true;
                    txbQuantity.Text = "0";
                }
                else
                {
                    emptyListPanel.Visible = false;
                    listViewRequests.DataSource = data;
                    listViewRequests.DataBind();
                    string q = data.Count.ToString();
                    txbQuantity.Text = q;
                }
                dataCopy = data;
            }
            
        }
        
        public void Func()
        {
            var servicesWithFilters = dataCopy;
            switch (fltButton.SelectedIndex)
            {
                case 1:
                    servicesWithFilters = servicesWithFilters.Where(x => x.Status == "В процессе").ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                case 2:
                    servicesWithFilters = servicesWithFilters.Where(x => x.Status == "Одобрена").ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                case 3:
                    servicesWithFilters = servicesWithFilters.Where(x => x.Status == "Отклонена").ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                default:
                    break;
            }
            switch (sortButton.SelectedIndex)
            {
                case 1:
                    servicesWithFilters = servicesWithFilters.OrderByDescending(d => d.DateOrder).ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                case 2:
                    servicesWithFilters = servicesWithFilters.OrderBy(d => d.DateOrder).ToList();
                    listViewRequests.DataSource = servicesWithFilters;
                    listViewRequests.DataBind();
                    break;
                default:
                    break;
            }
            if (servicesWithFilters.Count == 0)
            {
                txbQuantity.Text = "0";
                emptyListPanel.Visible = true;
            }
            else
            {
                string q = servicesWithFilters.Count.ToString();
                txbQuantity.Text = q;
                emptyListPanel.Visible = false;
            }


        }

        protected void exit_Click(object sender, EventArgs e)
        {
            pnlMessage.Visible = true;
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            SetTextBoxesEditable();
            applyButton.Visible = true;
            declineButton.Visible = true;
        }

        protected void resetFilters_Click(object sender, EventArgs e)
        {
            listViewRequests.DataSource = null;
            sortButton.SelectedIndex = 0;
            fltButton.SelectedIndex = 0;
            outputRequests();
        }

        protected void applyButton_Click(object sender, EventArgs e)
        {
            ApplicationContext app = new ApplicationContext();
            Models.Profile updatedProfile = app.Profile.FirstOrDefault(x => x.Email == logcontext.Email);
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
            errorLabelEmail.Visible = false;
            errorLabelSameEmail.Visible = false;
            errorLabelEmpty.Visible = false;
            errorLabelPass.Visible = false;

            if (password != "" && lastName != "" && firstName != "" && patronymic != "" && phone != "" && email != "")
            {
                if (IsValidEmail(email))
                { 
                    //Если почта из поля является почтой профиля владельца, то есть он не менял почту
                    if (logcontext.Email == email)
                    {
                        errorLabelSameEmail.Visible = false;
                        if (IsPasswordValid(passwordTxb.Text))
                        {
                            errorLabelPass.Visible = false;
                            applyButton.Visible = false;
                            declineButton.Visible = false;
                            updatedProfile.LastName = lastNameTxb.Text;
                            updatedProfile.FirstName = firstNameTxb.Text;
                            updatedProfile.Patronymic = patronymicTxb.Text;
                            updatedProfile.Email = emailTxb.Text;
                            updatedProfile.Phone = phoneTxb.Text;
                            updatedProfile.Password = passwordTxb.Text;
                            app.SaveChanges();
                            var logContext = app.Profile.FirstOrDefault(x => x.Email == emailTxb.Text);
                            Session["log"] = logContext;
                            SetTextBoxesNonEditable();
                            pnlMessageApply.Visible = true;
                        }
                        else
                        {
                            errorLabelPass.Visible = true;
                            passwordTxb.BorderColor = Color.Red;
                        }
                        
                    }
                    else
                    {                                                               
                        Models.Profile profileList = app.Profile.FirstOrDefault(x => x.Email == email);
                        //Если почты из поля нет в системе, то есть почта не занята
                        if (profileList == null)
                        {
                            errorLabelSameEmail.Visible = false;
                            if (IsPasswordValid(passwordTxb.Text))
                            {
                                hiddenPass.Text = password;
                                
                                pnlChangeEmailConfirm.Visible = true;
                                SendMailForChange();
                            }
                            else
                            {
                                errorLabelPass.Visible = true;
                                passwordTxb.BorderColor = Color.Red;
                            }
                        }
                        else
                        {
                            errorLabelSameEmail.Visible = true;
                            emailTxb.BorderColor = Color.Red;
                        }
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
        
        protected void declineButton_Click(object sender, ImageClickEventArgs e)
        {
            SetTextBoxesNonEditable();
            applyButton.Visible = false;
            declineButton.Visible = false;
            lastNameTxb.BorderColor = Color.White;
            firstNameTxb.BorderColor = Color.White;
            patronymicTxb.BorderColor = Color.White;
            phoneTxb.BorderColor = Color.White;
            emailTxb.BorderColor = Color.White;
            passwordTxb.BorderColor = Color.White;
            errorLabelEmpty.Visible = false;
            errorLabelEmail.Visible = false;
            outputProfile();
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
        protected void messageButtonApply_Click(object sender, EventArgs e)
        {
            pnlMessageApply.Visible = false;
            pnlChangeEmailConfirm.Visible = false;
            txbCodeChangeEmail.BorderColor = Color.White;
            txbCodeChangeEmail.Text = "";
            txbCodeChangeEmail.Style["opacity"] = "1";
            confirmChangeEmail.Style["display"] = "none";
            errorLabelWrongCodeChange.Visible = false;
            txbCodeChangeEmail.ReadOnly = false;
            SetTextBoxesNonEditable();
            applyButton.Visible = false;
            declineButton.Visible = false;
            lastNameTxb.BorderColor = Color.White;
            firstNameTxb.BorderColor = Color.White;
            patronymicTxb.BorderColor = Color.White;
            phoneTxb.BorderColor = Color.White;
            emailTxb.BorderColor = Color.White;
            passwordTxb.BorderColor = Color.White;
            errorLabelEmpty.Visible = false;
            errorLabelEmail.Visible = false;
            outputProfile();
            outputRequests();
        }
        

        protected void viewRequest_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button clickedButton = sender as System.Web.UI.WebControls.Button;
            int i = clickedButton.TabIndex;
            Models.Orders orders = app.Orders.FirstOrDefault(x => x.ID == i);
            Session["dataRequest"] = orders;
            Response.Redirect("Сheque.aspx");
        }

        


        protected void messageButtonYes_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session["log"] = null;
            Response.Redirect("Home.aspx");
        }

        protected void messageButtonNo_Click(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;
        }

        protected void deleteProfile_Click(object sender, EventArgs e)
        {
            pnlDeleteProfile.Visible = true;
            
        }

        protected void deleteYes_Click(object sender, EventArgs e)
        {
            pnlDeleteEmailConfirm.Visible = true;
            SendMail();
        }

        protected void deleteNo_Click(object sender, EventArgs e)
        {
            pnlDeleteProfile.Visible = false;
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

        private void IfRightCode(string code)
        {
            if (code == hiddenCode1.Text)
            {
                txbCode.BorderColor = Color.Lime;
                txbCode.Style["opacity"] = "0.25";
                confirmDelete.Style["display"] = "inline";
                txbCode.ReadOnly = true;
            }
            else
            {
                txbCode.BorderColor = Color.Red;
                errorLabelWrongCode.Visible = true;
            }
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
           
        }
        public int intcode = 0;
        public void SendMail()
        {
            var em = app.Profile.FirstOrDefault(x => x.Email == emailTxb.Text);
            if (em != null)
            {
                Random random = new Random();
                intcode = random.Next(100000, 999999);
                Orders orders = (Orders)Session["dataRequest"];
                string email = emailTxb.Text.Trim().ToLower();
                string subject = "Удаление аккаунта";
                string body = $"{em.LastName} {em.FirstName} {em.Patronymic}, Вы запросили удаение Вашего аккаунта на сайте FastWay. Если Вы этого не делали, проигнорируйте это письмо. Для подтверждения операции используйте код: {intcode}. С уважением, Администрация FastWay";
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmailWithoutAttachment(subject, body, email);
                hiddenCode1.Text = intcode.ToString();
            }

        }

        protected void confirmDelete_Click(object sender, EventArgs e)
        {
            if (txbCode.Text != "")
            {
                var p = app.Profile.FirstOrDefault(x => x.Email == emailTxb.Text);
                var op = app.OrdersInProfile.FirstOrDefault(x => x.ProfileID == p.ID);
                var o = app.Orders.Where(x => x.Email == emailTxb.Text).ToList();
                for (int i = 0; i < o.Count; i++)
                {
                    int? cId = o[i].CargoID;
                    int? aId = o[i].ID;
                    var c = app.Cargo.FirstOrDefault(x => x.ID == cId);
                    var ao = app.AcceptOrder.FirstOrDefault(x => x.AcceptOrderInfo == aId);
                    var mino = app.MoversInOrder.FirstOrDefault(x => x.OrderID == aId);
                    var oinp = app.OrdersInProfile.FirstOrDefault(x => x.OrderID == aId);
                    var onr = app.OrdersNonReg.FirstOrDefault(x => x.OrderID == aId);
                    if (mino != null)
                    {
                        app.MoversInOrder.Remove(mino);
                        app.SaveChanges();
                    }
                    if (ao != null)
                    {
                        app.AcceptOrder.Remove(ao);
                        app.SaveChanges();
                    }
                    if (oinp != null)
                    {
                        app.OrdersInProfile.Remove(oinp);
                        app.SaveChanges();
                    }
                    if (onr != null)
                    {
                        app.OrdersNonReg.Remove(onr);
                        app.SaveChanges();
                    }
                    app.Cargo.Remove(c);
                    app.SaveChanges();
                    app.Orders.Remove(o[i]);
                    app.SaveChanges();
                }
                app.Profile.Remove(p);
                app.SaveChanges();
                pnlDeleteProfile.Visible = false;
                Session.Clear();
                Session["log"] = null;
                pnlMessageDeleteCompleted.Visible = true;
                
            }
            else
            {
                txbCode.BorderColor = Color.Red;
            }

        }

        protected void cancelDelete_Click(object sender, EventArgs e)
        {
            txbCode.BorderColor = Color.White;
            txbCode.Text = "";
            txbCode.Style["opacity"] = "1";
            confirmDelete.Style["display"] = "none";
            errorLabelWrongCode.Visible = false;
            txbCode.ReadOnly = false;
            pnlDeleteProfile.Visible = false;
            pnlDeleteEmailConfirm.Visible = false;
        }

        protected void messageButtonDeleteCompleted_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Home.aspx");
        }

        protected void confirmChangeEmail_Click(object sender, EventArgs e)
        {
            Models.Profile updatedProfile = app.Profile.FirstOrDefault(x => x.Email == logcontext.Email);
            errorLabelPass.Visible = false;
            applyButton.Visible = false;
            declineButton.Visible = false;
            updatedProfile.LastName = lastNameTxb.Text;
            updatedProfile.FirstName = firstNameTxb.Text;
            updatedProfile.Patronymic = patronymicTxb.Text;
            updatedProfile.Email = emailTxb.Text;
            updatedProfile.Phone = phoneTxb.Text;
            updatedProfile.Password = hiddenPass.Text;
            List<Orders> o = app.Orders.Where(x => x.Email == hiddenOldEmail.Text).ToList();
            for (int i = 0; i< o.Count; i++)
            {
                o[i].Email = emailTxb.Text;
            }
            app.SaveChanges();
            var logContext = app.Profile.FirstOrDefault(x => x.Email == emailTxb.Text);
            Session["log"] = logContext;
            SetTextBoxesNonEditable();
            pnlMessageApply.Visible = true;
        }

        protected void cancelChangeEmail_Click(object sender, EventArgs e)
        {
            pnlChangeEmailConfirm.Visible = false;
            SetTextBoxesNonEditable();
            applyButton.Visible = false;
            declineButton.Visible = false;
            lastNameTxb.BorderColor = Color.White;
            firstNameTxb.BorderColor = Color.White;
            patronymicTxb.BorderColor = Color.White;
            phoneTxb.BorderColor = Color.White;
            emailTxb.BorderColor = Color.White;
            passwordTxb.BorderColor = Color.White;
            errorLabelEmpty.Visible = false;
            errorLabelEmail.Visible = false;
            outputProfile();
            outputRequests();
            txbCodeChangeEmail.BorderColor = Color.White;
            txbCodeChangeEmail.Text = "";
            txbCodeChangeEmail.Style["opacity"] = "1";
            confirmChangeEmail.Style["display"] = "none";
            errorLabelWrongCodeChange.Visible = false;
            txbCodeChangeEmail.ReadOnly = false;
        }

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            string code = txbCodeChangeEmail.Text;
            if (code.Length == 6)
            {
                // Ваш код обработки
                IfRightCode1(code);
            }
        }

        private void IfRightCode1(string code)
        {
            if (code == hiddenCode1.Text)
            {
                txbCodeChangeEmail.BorderColor = Color.Lime;
                errorLabelWrongCodeChange.Visible = false;
                txbCodeChangeEmail.Style["opacity"] = "0.25";
                confirmChangeEmail.Style["display"] = "inline";
                txbCodeChangeEmail.ReadOnly = true;
            }
            else
            {
                txbCodeChangeEmail.BorderColor = Color.Red;
                errorLabelWrongCodeChange.Visible = true;
            }


        }
        public int intcode1 = 0;
        public void SendMailForChange()
        {
            Random random = new Random();
            intcode1 = random.Next(100000, 999999);
            string email = emailTxb.Text.Trim().ToLower();
            string subject = "Подтверждение аккаунта";
            string body = $"{lastNameTxb.Text} {firstNameTxb.Text} {patronymicTxb.Text}, для подтверждения аккаунта используйте код: {intcode1}. С уважением, Администрация FastWay";
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmailWithoutAttachment(subject, body, email);
            hiddenCode1.Text = intcode1.ToString();
        }




        private void SetTextBoxesEditable()
        {
            lastNameTxb.ReadOnly = false;
            firstNameTxb.ReadOnly = false;
            patronymicTxb.ReadOnly = false;
            phoneTxb.ReadOnly = false;
            emailTxb.ReadOnly = false;
            passwordTxb.ReadOnly = false;

            lastNameTxb.Style["opacity"] = "1";
            firstNameTxb.Style["opacity"] = "1";
            patronymicTxb.Style["opacity"] = "1";
            phoneTxb.Style["opacity"] = "1";
            emailTxb.Style["opacity"] = "1";
            passwordTxb.Style["opacity"] = "1";
        }

        private void SetTextBoxesNonEditable()
        {
            lastNameTxb.ReadOnly = true;
            firstNameTxb.ReadOnly = true;
            patronymicTxb.ReadOnly = true;
            phoneTxb.ReadOnly = true;
            emailTxb.ReadOnly = true;
            passwordTxb.ReadOnly = true;

            lastNameTxb.Style["opacity"] = "0.5";
            firstNameTxb.Style["opacity"] = "0.5";
            patronymicTxb.Style["opacity"] = "0.5";
            phoneTxb.Style["opacity"] = "0.5";
            emailTxb.Style["opacity"] = "0.5";
            passwordTxb.Style["opacity"] = "0.5";

        }


        protected void sortButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func();
        }



        protected void filtButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func();
        }
    }
}