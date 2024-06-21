using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace FastWay.Pages
{
    public partial class Autorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Настройка заголовков кэширования для предотвращения возврата назад
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore();
            }

        }

        protected void login_Click(object sender, EventArgs e)
        {
            ApplicationContext app = new ApplicationContext();
            txbEmail.BorderColor = Color.White;
            txbPassword.BorderColor = Color.White;
            errorLabel.Visible = false;
            string em = txbEmail.Text;
            string pass = txbPassword.Text;
            
            if (em == "" || pass == "")
            {
                if (em == "")
                {
                    txbEmail.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (pass == "")
                {
                    txbPassword.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                
            }
            else
            {
                if (IsValidEmail(txbEmail.Text))
                {
                    var logContext = app.Profile.FirstOrDefault(s => s.Email == em && s.Password == pass);
                    var isHaveEmail = app.Profile.FirstOrDefault(x => x.Email == txbEmail.Text);
                    if (isHaveEmail == null)
                    {
                        errorNotHaveEmail.Visible = true;
                    }
                    else
                    {
                        errorNotHaveEmail.Visible = false;
                        txbEmail.BorderColor = Color.White;
                        txbPassword.BorderColor = Color.White;
                        errorLabelEmpty.Visible = false;
                        if (logContext != null)
                        {
                            Session["log"] = logContext;
                            Response.Redirect("Profile.aspx");
                        }
                        else
                        {
                            txbPassword.Text = "";
                            errorLabel.Visible = true;
                        }
                    }
                    
                }
                else
                {
                    txbEmail.BorderColor = Color.Red;
                    errorLabelEmail.Visible = true;
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