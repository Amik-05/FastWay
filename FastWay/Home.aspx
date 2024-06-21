<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FastWay.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>


</head>
<body>
    <form id="form1" runat="server" >
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

                <div class="homeCenter">

                    <p style="font-size: 50pt; font-weight: bold; margin-bottom:5%; font-family: 'Segoe UI'; color: white">Грузоперевозки FastWay</p>

                    <p style="font-size: 25pt;font-weight: bold;  font-family: 'Segoe UI'; margin-bottom:5%; color: white">Перевозки грузов по всей России недорого, с высокой скоростью и в безопасности!</p>
                    
                    <asp:Button runat="server" OnClick="Unnamed_Click" class="button" Text="Заказать" />

                </div>
            </div>
        </div>
    </form>
</body>
</html>

