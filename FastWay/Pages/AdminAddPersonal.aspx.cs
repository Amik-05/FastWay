using FastWay.Models;
using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace FastWay.Pages
{
    public partial class AdminAddPersonal : System.Web.UI.Page
    {
        ApplicationContext app = new ApplicationContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                comboDrivers.SelectedIndex = 0;
                comboMovers.SelectedIndex = 0;
                FillDropDownList();
                SetEditDriverNonEditable();
                SetEditMoverNonEditable();
                func("dr");
                func("mv");
            }
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

        protected void FillDropDownList()
        {
            var drivers = app.Drivers.ToList();
            var movers = app.Movers.ToList();
            var cars = app.Cars.Where(x=> x.IsHaveDriver == "no").ToList();
            if (cars.Count != 0)
            {
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                }
                comboCars.DataSource = cars;
                comboCars.DataTextField = "NameAndVIN"; // Поле для отображения
                comboCars.DataValueField = "ID";  // Значение поля
                comboCars.DataBind();
                comboCarForDriver.DataSource = cars;
                comboCarForDriver.DataTextField = "NameAndVIN"; // Поле для отображения
                comboCarForDriver.DataValueField = "ID";  // Значение поля
                comboCarForDriver.DataBind();
                func("dr");
            }
            else
            {
                comboCarForDriver.Items.Clear();
                comboCars.Items.Clear();
            }
            if (drivers.Count != 0)
            {
                for (int i = 0; i < drivers.Count; i++)
                {
                    drivers[i].FIO = $"{drivers[i].LastName} {drivers[i].FirstName} {drivers[i].Patronymic}";
                }
                comboDrivers.DataSource = drivers;
                comboDrivers.DataTextField = "FIO"; // Поле для отображения
                comboDrivers.DataValueField = "ID";  // Значение поля
                comboDrivers.DataBind();
                if (cars.Count != 0)
                {
                    for (int i = 0; i < cars.Count; i++)
                    {
                        cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                    }
                    comboCars.DataSource = cars;
                    comboCars.DataTextField = "NameAndVIN"; // Поле для отображения
                    comboCars.DataValueField = "ID";  // Значение поля
                    comboCars.DataBind();
                    comboCarForDriver.DataSource = cars;
                    comboCarForDriver.DataTextField = "NameAndVIN"; // Поле для отображения
                    comboCarForDriver.DataValueField = "ID";  // Значение поля
                    comboCarForDriver.DataBind();
                }
                else
                {
                    comboCars.Items.Clear();
                    comboCarForDriver.Items.Clear();
                }
                func("dr");
            }
            else
            {
                comboDrivers.Items.Clear();
                ClearEditDriverTxb();
            }

            if (movers.Count != 0)
            {
                for (int i = 0; i < movers.Count; i++)
                {
                    movers[i].FIO = $"{movers[i].LastName} {movers[i].FirstName} {movers[i].Patronymic}";
                }
                comboMovers.DataSource = movers;
                comboMovers.DataTextField = "FIO"; // Поле для отображения
                comboMovers.DataValueField = "ID";  // Значение поля
                comboMovers.DataBind();
                func("mv");
            }
            else
            {
                comboMovers.Items.Clear();
                ClearEditMoverTxb();
            }

            

        }


        public void func(string role)
        {
            
            if (role == "dr")
            {
                if (comboDrivers.SelectedItem != null)
                {
                    int idDr = Convert.ToInt32(comboDrivers.SelectedItem.Value);
                    var dr = app.Drivers.FirstOrDefault(x => x.ID == idDr);
                    if (dr.CarID != null)
                    {
                        checkCar.Checked = true;
                        if (panelEditDriver.Visible == false)
                        {
                            comboCarForDriver.Style["pointer-events"] = "auto";
                            comboCarForDriver.Style["opacity"] = "1";
                        }
                    }
                    else
                    {
                        checkCar.Checked = false;
                        comboCarForDriver.Style["pointer-events"] = "none";
                        comboCarForDriver.Style["opacity"] = "0.5";
                    }
                    if (dr != null)
                    {
                        editDriverLastNameTxb.Text = dr.LastName;
                        editDriverFirstNameTxb.Text = dr.FirstName;
                        editDriverPatronymicTxb.Text = dr.Patronymic;
                        editDriverAgeTxb.Text = dr.Age;
                        if (dr.CarID != null)
                        {
                            var cars = app.Cars.ToList();
                            for (int i = 0; i < cars.Count; i++)
                            {
                                cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                            }
                            comboCarForDriver.DataSource = cars;
                            comboCarForDriver.DataTextField = "NameAndVIN"; // Поле для отображения
                            comboCarForDriver.DataValueField = "ID";  // Значение поля
                            comboCarForDriver.DataBind();
                            var carId = app.Cars.FirstOrDefault(x => x.ID == dr.CarID).ID.ToString();
                            ListItem item = comboCarForDriver.Items.FindByValue(carId.ToString());
                            if (item != null)
                            {
                                comboCarForDriver.ClearSelection();
                                item.Selected = true;
                            }
                        }
                        else
                        {
                            var cars = app.Cars.Where(x=> x.IsHaveDriver == "no").ToList();
                            for (int i = 0; i < cars.Count; i++)
                            {
                                cars[i].NameAndVIN = $"{cars[i].FullName} {cars[i].VIN}";
                            }
                            comboCarForDriver.DataSource = cars;
                            comboCarForDriver.DataTextField = "NameAndVIN"; // Поле для отображения
                            comboCarForDriver.DataValueField = "ID";  // Значение поля
                            comboCarForDriver.DataBind();
                        }
                        
                    }
                    else
                    {
                        editDriverLastNameTxb.Text = "";
                        editDriverFirstNameTxb.Text = "";
                        editDriverPatronymicTxb.Text = "";
                        editDriverAgeTxb.Text = "";
                    }

                }
                
            }
            else if (role == "mv")
            {
                if (comboMovers.SelectedItem != null)
                {
                    int idMv = Convert.ToInt32(comboMovers.SelectedItem.Value);
                    var mv = app.Movers.FirstOrDefault(x => x.ID == idMv);
                    editMoverLastNameTxb.Text = mv.LastName;
                    editMoverFirstNameTxb.Text = mv.FirstName;
                    editMoverPatronymicTxb.Text = mv.Patronymic;
                    editMoverAgeTxb.Text = mv.Age;
                }
                else
                {
                    editMoverLastNameTxb.Text = "";
                    editMoverFirstNameTxb.Text = "";
                    editMoverPatronymicTxb.Text = "";
                    editMoverAgeTxb.Text = "";
                }
            }
            
        }
        protected void comboDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            func("dr");
        }
        protected void comboMovers_SelectedIndexChanged(object sender, EventArgs e)
        {
            func("mv");
        }

        protected void messageButtonApply_Click(object sender, EventArgs e)
        {
            pnlMessageApply.Visible = false;
            pnlMessageDeleteDriver.Visible = false;
            pnlMessageDeleteMover.Visible = false;
            pnlMessageDriverDeleted.Visible = false;
            pnlMessageMoverDeleted.Visible = false;
            FillDropDownList();
            func("dr");
        }

        protected void editDriver_Click(object sender, EventArgs e)
        {
            //отобразить кнопки apply cancel
            SetEditDriverEditable();
            panelApplyCancelEditDriver.Visible = true;
            panelEditDriver.Visible = false;
        }

        protected void applyDriver_Click(object sender, EventArgs e)
        {
            //применить редактирование водителя
            editDriverLastNameTxb.BorderColor = Color.White;
            editDriverFirstNameTxb.BorderColor = Color.White;
            editDriverPatronymicTxb.BorderColor = Color.White;
            editDriverAgeTxb.BorderColor = Color.White;
            int drId = Convert.ToInt32(comboDrivers.SelectedItem.Value);
            var dr = app.Drivers.FirstOrDefault(x => x.ID == drId);
            if (editDriverLastNameTxb.Text != "" && editDriverFirstNameTxb.Text != "" && editDriverPatronymicTxb.Text != "" && editDriverAgeTxb.Text != "")
            {

                if (comboCarForDriver.SelectedItem != null)
                {
                    int c = Convert.ToInt32(comboCarForDriver.SelectedItem.Value);
                    dr.LastName = editDriverLastNameTxb.Text;
                    dr.FirstName = editDriverFirstNameTxb.Text;
                    dr.Patronymic = editDriverPatronymicTxb.Text;
                    dr.Age = editDriverAgeTxb.Text;
                    
                    if (checkCar.Checked == true)
                    {
                        dr.CarID = c;
                        var cv = app.Cars.FirstOrDefault(x => x.ID == c);
                        cv.IsHaveDriver = "yes";
                    }
                    else
                    {
                        var cv = app.Cars.FirstOrDefault(x => x.ID == c);
                        cv.IsHaveDriver = "no";
                        dr.CarID = null;
                    }
                    
                    app.SaveChanges();
                    FillDropDownList();
                    pnlMessageApply.Visible = true;
                }
                else
                {
                    dr.LastName = editDriverLastNameTxb.Text;
                    dr.FirstName = editDriverFirstNameTxb.Text;
                    dr.Patronymic = editDriverPatronymicTxb.Text;
                    dr.Age = editDriverAgeTxb.Text;
                    app.SaveChanges();
                    FillDropDownList();
                    pnlMessageApply.Visible = true;
                }
            }
            else
            {
                if (editDriverLastNameTxb.Text == "")
                {
                    editDriverLastNameTxb.BorderColor = Color.Red;
                }
                if (editDriverFirstNameTxb.Text == "")
                {
                    editDriverFirstNameTxb.BorderColor = Color.Red;
                }
                if (editDriverPatronymicTxb.Text == "")
                {
                    editDriverPatronymicTxb.BorderColor = Color.Red;
                }
                if (editDriverAgeTxb.Text == "")
                {
                    editDriverAgeTxb.BorderColor = Color.Red;
                }
            }
        }

        protected void cancelDriver_Click(object sender, EventArgs e)
        {
            //отменить редактирование водителя
            SetEditDriverNonEditable();
            panelApplyCancelEditDriver.Visible = false;
            panelEditDriver.Visible = true;
            FillDropDownList();
        }

        protected void deleteDriver_Click(object sender, EventArgs e)
        {
            //удалить водителя из системы
            pnlMessageDeleteDriver.Visible = true;
        }

        protected void messageButtonDeleteDriverYes_Click(object sender, EventArgs e)
        {
            //удаление водителя
            int drId = Convert.ToInt32(comboDrivers.SelectedItem.Value);
            var dr = app.Drivers.FirstOrDefault(x => x.ID == drId);
            app.Drivers.Remove(dr);
            app.SaveChanges();
            FillDropDownList();
            pnlMessageDriverDeleted.Visible = true;
            pnlMessageDeleteDriver.Visible = false;
            panelApplyCancelEditDriver.Visible = false;
            panelEditDriver.Visible = true;
            SetEditDriverNonEditable();
        }

        protected void messageButtonDriverDeleted_Click(object sender, EventArgs e)
        {
            //уведомление водитель удален
            pnlMessageDriverDeleted.Visible = false;
        }
        protected void messageButtonDeleteDriverNo_Click(object sender, EventArgs e)
        {
            //отмена удаления водителя
            pnlMessageDeleteDriver.Visible = false;
        }


        protected void editMover_Click(object sender, EventArgs e)
        {
            //редактировать грузчика
            SetEditMoverEditable();
            panelApplyCancelEditMover.Visible = true;
            panelEditMover.Visible = false;
        }

        protected void applyMover_Click(object sender, EventArgs e)
        {
            //применить редактирование грузчика
            editMoverLastNameTxb.BorderColor = Color.White;
            editMoverFirstNameTxb.BorderColor = Color.White;
            editMoverPatronymicTxb.BorderColor = Color.White;
            editMoverAgeTxb.BorderColor = Color.White;
            int mvId = Convert.ToInt32(comboMovers.SelectedItem.Value);
            var mv = app.Movers.FirstOrDefault(x => x.ID == mvId);
            if (editMoverLastNameTxb.Text != "" && editMoverFirstNameTxb.Text != "" && editMoverPatronymicTxb.Text != "" && editMoverAgeTxb.Text != "")
            {
                mv.LastName = editMoverLastNameTxb.Text;
                mv.FirstName = editMoverFirstNameTxb.Text;
                mv.Patronymic = editMoverPatronymicTxb.Text;
                mv.Age = editMoverAgeTxb.Text;
                app.SaveChanges();
                FillDropDownList();
                pnlMessageApply.Visible = true;
            }
            else
            {
                if (editMoverLastNameTxb.Text == "")
                {
                    editMoverLastNameTxb.BorderColor = Color.Red;
                }
                if (editMoverFirstNameTxb.Text == "")
                {
                    editMoverFirstNameTxb.BorderColor = Color.Red;
                }
                if (editMoverPatronymicTxb.Text == "")
                {
                    editMoverPatronymicTxb.BorderColor = Color.Red;
                }
                if (editMoverAgeTxb.Text == "")
                {
                    editMoverAgeTxb.BorderColor = Color.Red;
                }
            }
        }

        protected void cancelMover_Click(object sender, EventArgs e)
        {
            //отменить редактирование грузчика
            SetEditMoverNonEditable();
            panelApplyCancelEditMover.Visible = false;
            panelEditMover.Visible = true;
            FillDropDownList();
        }

        protected void deleteMover_Click(object sender, EventArgs e)
        {
            //удалить грузчика
            pnlMessageDeleteMover.Visible = true;
        }

        protected void messageButtonDeleteMoverYes_Click(object sender, EventArgs e)
        {
            //удаление грузчика
            int mvId = Convert.ToInt32(comboMovers.SelectedItem.Value);
            var mv = app.Movers.FirstOrDefault(x => x.ID == mvId);
            app.Movers.Remove(mv);
            app.SaveChanges();
            FillDropDownList();
            pnlMessageMoverDeleted.Visible = true;
            pnlMessageDeleteMover.Visible = false;
            panelApplyCancelEditMover.Visible = false;
            panelEditMover.Visible = true;
            SetEditMoverNonEditable();
        }

        protected void messageButtonMoverDeleted_Click(object sender, EventArgs e)
        {
            //уведомление грузчик удален
            pnlMessageMoverDeleted.Visible = false;
        }
        protected void messageButtonDeleteMoverNo_Click(object sender, EventArgs e)
        {
            //отмена удаления грузчика
            pnlMessageDeleteMover.Visible = false;
        }

        protected void addDriverMover_Click(object sender, EventArgs e)
        {
            addDriverMoverLastNameTxb.BorderColor = Color.White;
            addDriverMoverFirstNameTxb.BorderColor = Color.White;
            addDriverMoverPatronymicTxb.BorderColor = Color.White;
            addDriverMoverAgeTxb.BorderColor = Color.White;
            if (addDriverMoverLastNameTxb.Text != "" && addDriverMoverFirstNameTxb.Text != "" && addDriverMoverPatronymicTxb.Text != "" && addDriverMoverAgeTxb.Text != "")
            {
                if (comboRole.SelectedIndex == 0)
                {
                    if (comboCars.SelectedItem != null)
                    {
                        int c = Convert.ToInt32(comboCars.SelectedItem.Value);
                        var cn = app.Cars.FirstOrDefault(x => x.ID == c);
                        string cfn = cn.NameAndVIN = $"{cn.FullName} {cn.VIN}";
                        Drivers newItem = new Drivers
                        {
                            LastName = addDriverMoverLastNameTxb.Text,
                            FirstName = addDriverMoverFirstNameTxb.Text,
                            Patronymic = addDriverMoverPatronymicTxb.Text,
                            Age = addDriverMoverAgeTxb.Text,
                            CarID = c,
                            Status = cfn
                        };
                        cn.IsHaveDriver = "yes";
                        app.Drivers.Add(newItem);
                        app.SaveChanges();
                        pnlMessageApply.Visible = true;
                        FillDropDownList();
                        ClearAddTxb();
                    }
                    else
                    {
                        Drivers newItem = new Drivers
                        {
                            LastName = addDriverMoverLastNameTxb.Text,
                            FirstName = addDriverMoverFirstNameTxb.Text,
                            Patronymic = addDriverMoverPatronymicTxb.Text,
                            Age = addDriverMoverAgeTxb.Text,
                            Status = "Нет"
                        };
                        
                        app.Drivers.Add(newItem);
                        app.SaveChanges();
                        pnlMessageApply.Visible = true;
                        FillDropDownList();
                        ClearAddTxb();
                    }
                    
                }
                else
                {
                    Movers newItem = new Movers
                    {
                        LastName = addDriverMoverLastNameTxb.Text,
                        FirstName = addDriverMoverFirstNameTxb.Text,
                        Patronymic = addDriverMoverPatronymicTxb.Text,
                        Age = addDriverMoverAgeTxb.Text,
                        Status = "Свободен"
                    };
                    app.Movers.Add(newItem);
                    app.SaveChanges();
                    pnlMessageApply.Visible = true;
                    FillDropDownList();
                    ClearAddTxb();
                }
            }
            else
            {
                if (addDriverMoverLastNameTxb.Text == "")
                {
                    addDriverMoverLastNameTxb.BorderColor = Color.Red;
                }
                if (addDriverMoverFirstNameTxb.Text == "")
                {
                    addDriverMoverFirstNameTxb.BorderColor = Color.Red;
                }
                if (addDriverMoverPatronymicTxb.Text == "")
                {
                    addDriverMoverPatronymicTxb.BorderColor = Color.Red;
                }
                if (addDriverMoverAgeTxb.Text == "")
                {
                    addDriverMoverAgeTxb.BorderColor = Color.Red;
                }
            }

        }

        public void SetEditDriverEditable()
        {
            editDriverLastNameTxb.ReadOnly = false;
            editDriverFirstNameTxb.ReadOnly = false;
            editDriverPatronymicTxb.ReadOnly = false;
            editDriverAgeTxb.ReadOnly = false;
            labelCheckCar.Style["opacity"] = "1";
            checkCar.Style["opacity"] = "1";
            checkCar.Style["pointer-events"] = "auto";
            int idDr = Convert.ToInt32(comboDrivers.SelectedItem.Value);
            var dr = app.Drivers.FirstOrDefault(x => x.ID == idDr);
            if (dr.CarID != null)
            {
                checkCar.Checked = true;
                comboCarForDriver.Style["pointer-events"] = "auto";
                comboCarForDriver.Style["opacity"] = "1";
            }
            else
            {
                checkCar.Checked = false;
                comboCarForDriver.Style["pointer-events"] = "none";
                comboCarForDriver.Style["opacity"] = "0.5";
            }
            editDriverLastNameTxb.Style["opacity"] = "1";
            editDriverFirstNameTxb.Style["opacity"] = "1";
            editDriverPatronymicTxb.Style["opacity"] = "1";
            editDriverAgeTxb.Style["opacity"] = "1";
        }

        public void SetEditMoverEditable()
        {
            editMoverLastNameTxb.ReadOnly = false;
            editMoverFirstNameTxb.ReadOnly = false;
            editMoverPatronymicTxb.ReadOnly = false;
            editMoverAgeTxb.ReadOnly = false;

            editMoverLastNameTxb.Style["opacity"] = "1";
            editMoverFirstNameTxb.Style["opacity"] = "1";
            editMoverPatronymicTxb.Style["opacity"] = "1";
            editMoverAgeTxb.Style["opacity"] = "1";
        }

        public void SetEditDriverNonEditable()
        {
            editDriverLastNameTxb.ReadOnly = true;
            editDriverFirstNameTxb.ReadOnly = true;
            editDriverPatronymicTxb.ReadOnly = true;
            editDriverAgeTxb.ReadOnly = true;
            labelCheckCar.Style["opacity"] = "0.5";
            checkCar.Style["opacity"] = "0.5";
            checkCar.Style["pointer-events"] = "none";
            comboCarForDriver.Style["pointer-events"] = "none";
            comboCarForDriver.Style["opacity"] = "0.5";
            editDriverLastNameTxb.Style["opacity"] = "0.5";
            editDriverFirstNameTxb.Style["opacity"] = "0.5";
            editDriverPatronymicTxb.Style["opacity"] = "0.5";
            editDriverAgeTxb.Style["opacity"] = "0.5";
        }

        public void SetEditMoverNonEditable()
        {
            editMoverLastNameTxb.ReadOnly = true;
            editMoverFirstNameTxb.ReadOnly = true;
            editMoverPatronymicTxb.ReadOnly = true;
            editMoverAgeTxb.ReadOnly = true;
            editMoverLastNameTxb.Style["opacity"] = "0.5";
            editMoverFirstNameTxb.Style["opacity"] = "0.5";
            editMoverPatronymicTxb.Style["opacity"] = "0.5";
            editMoverAgeTxb.Style["opacity"] = "0.5";
        }

        public void ClearAddTxb()
        {
            addDriverMoverLastNameTxb.Text = "";
            addDriverMoverFirstNameTxb.Text = "";
            addDriverMoverPatronymicTxb.Text = "";
            addDriverMoverAgeTxb.Text = "";
        }
        public void ClearEditDriverTxb()
        {
            editDriverLastNameTxb.Text = "";
            editDriverFirstNameTxb.Text = "";
            editDriverPatronymicTxb.Text = "";
            editDriverAgeTxb.Text = "";
        }
        public void ClearEditMoverTxb()
        {
            editMoverLastNameTxb.Text = "";
            editMoverFirstNameTxb.Text = "";
            editMoverPatronymicTxb.Text = "";
            editMoverAgeTxb.Text = "";
        }

        protected void checkCar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCar.Checked == true)
            {
                comboCarForDriver.Style["pointer-events"] = "auto";
                comboCarForDriver.Style["opacity"] = "1";
            }
            else
            {
                comboCarForDriver.Style["pointer-events"] = "none";
                comboCarForDriver.Style["opacity"] = "0.5";
            }
        }

        protected void comboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboRole.SelectedIndex == 1)
            {
                comboCars.Style["opacity"] = "0.5";
                comboCars.Style["pointer-events"] = "none";
            }
            else
            {
                comboCars.Style["opacity"] = "1";
                comboCars.Style["pointer-events"] = "auto";
            }
        }
    }
}