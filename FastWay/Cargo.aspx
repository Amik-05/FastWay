<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cargo.aspx.cs" Inherits="FastWay.Cargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>

    <script type="text/javascript">
        function isAlphabetic(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить буквы латинского и кириллического алфавита
            if (!((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 1040 && charCode <= 1105))) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить только цифры (коды ASCII для цифр)
            if (charCode < 48 || charCode > 57) {
                return false;
            }
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="grid" style="width: 100%; height: 100vh">

            <div class="header" style="grid-area: h; width: 100%;">

                <div class="divPheader">
                    <a href="Home.aspx" style="color: white; text-decoration: underline">
                        <p class="pHeader">Главная</p>
                    </a>
                    <a href="Information.aspx" style="color: white; text-decoration: underline">
                        <p class="pHeader">О компании</p>
                    </a>
                </div>
            </div>

            <div class="itemsAndFilter" style="width: 100%;">
                <div class="centerOrder" style="text-align: center">
                    <div class="divCargo">
                        <asp:TextBox ID="txbFIO" placeholder="ФИО" class="txbCargo" runat="server" onkeypress="return isAlphabetic(event)" />
                    </div>
                    <div class="divCargo">
                        <asp:TextBox ID="txbPhone" placeholder="Номер телефона 7999ххххххх" class="txbCargo" onkeypress="return isNumeric(event)"  MaxLength="11" runat="server"  />
                    </div>
                    <div class="divCargo">
                        <asp:TextBox ID="txbEmail" placeholder="Электронная почта" class="txbCargo" runat="server"/>
                    </div>
                    <div class="divCargo">
                        <asp:TextBox ID="txbAddress" placeholder="Конечный адрес" class="txbCargo" runat="server"  />
                    </div>
                    <div class="divCargo">
                        <p class="pCST">Способ перевозки</p>
                        <asp:DropDownList Class="combo" ID="comboDeliveryType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboDeliveryType_SelectedIndexChanged"/>
                    </div>
                    <div class="divCargo" style="margin: 0px auto; text-align: left">
                        <asp:Label ID="txbCostForDelivery" class="pCST" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="divCargo" style="margin: 0px auto; text-align: left; display: flex">
                        <asp:CheckBox runat="server" ID="checkAgree" class="checkPersonal" />
                        <p class="pPersonal">Я согласен с </p>
                        <a href="Personal.aspx" target="_blank" style="color: white; text-decoration: underline; margin-left: 10px">
                            <p class="pPersonal">обработкой персональных данных</p>
                        </a>
                    </div>
                    <div style="width: 100%; text-align: center; margin-top: 10px;">
                        <asp:Button runat="server" class="button" OnClick="Unnamed_Click" Text="Отправить заявку" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
