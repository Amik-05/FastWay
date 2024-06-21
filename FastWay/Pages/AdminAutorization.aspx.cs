
using FastWay.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastWay.Pages
{
    public partial class AdminAutorization : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
        }

        protected void login_Click(object sender, EventArgs e)
        {
            ApplicationContext app = new ApplicationContext();
            
            string log = txbLogin.Text;
            string pass = txbPassword.Text;
            errorLabelEmpty.Visible = false;
            errorLabel.Visible = false;
            txbLogin.BorderColor = Color.White;
            txbPassword.BorderColor = Color.White;

            var logContext = app.Login.FirstOrDefault(s => s.login1 == log);
            var passContext = app.Login.FirstOrDefault(s => s.password == pass);
            if (log != "" &&  pass != "")
            {
                errorLabelEmpty.Visible = false;
                txbLogin.BorderColor = Color.White;
                txbPassword.BorderColor = Color.White;
                if (logContext != null && passContext != null)
                {
                    Response.Redirect("AdminHome.aspx");
                }
                else
                {
                    errorLabel.Visible = true;
                }
            }
            else
            {
                if (txbLogin.Text == "")
                {
                    txbLogin.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
                if (txbPassword.Text == "")
                {
                    txbPassword.BorderColor = Color.Red;
                    errorLabelEmpty.Visible = true;
                }
            }
            
        }
    }
}