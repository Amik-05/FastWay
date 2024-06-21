<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutCompany.aspx.cs" Inherits="FastWay.AboutCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>


</head>
<body>
    <form id="form1" runat="server" style="overflow: hidden;">
        <div class="grid" style="width: 100%; height: 100vh">

            <div class="header" style="grid-area: h; width: 100%;">

                <div style="margin-left: 10px; float: left; margin-top: -7px; display: flex;">
                    <a  href="Home.aspx" style="color: white; text-decoration: underline">
                        <p class="pHeader">Главная</p>
                    </a>
                </div>
            </div>

            <div class="itemsAndFilter" style="width: 100%;">

                <div class="center">

                    <p style="font-size: 50pt; font-weight: bold; margin-bottom:5%; font-family: 'Segoe UI'; color: white">Грузоперевозки FastWay</p>

                    <p style="font-size: 25pt;font-weight: bold;  font-family: 'Segoe UI'; margin-bottom:5%; color: white">Перевозки грузов по всей России недорого, с высокой скоростью и в безопасности!</p>
                    <a>
                        <asp:Button runat="server" class="button" Text="Заказать" />
                    </a>

                </div>
            </div>

        </div>
    </form>
</body>
</html>


