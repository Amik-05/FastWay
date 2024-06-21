<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FastWay.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Главная</title>
    <link rel="icon" href="..\PIC\main.png" />

    <script type="text/javascript">
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="grid" style="width: 100%; height: 100vh">

            <div class="header">

                <div class="divPheader" style="float: left">
                    <a href="Information.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">О компании</p>
                    </a>
                </div>

                <div style="float: right; margin-right: 10px">
                    <asp:Button runat="server" ID="adminAutoriz" CssClass="buttonInHeader" Text="Администрирование" OnClick="adminAutoriz_Click" style="margin-right:5px" />
                    <asp:Button runat="server" ID="vhod" CssClass="buttonInHeader" Text="Войти" OnClick="v_Click" />
                    <asp:Button runat="server" ID="profile" CssClass="buttonInHeader" Text="Профиль" OnClick="p_Click" />
                </div>

            </div>

            <div class="itemsAndFilter" style="width: 100%;">

                <div class="homeCenter">

                    <div style="display:block">
                        <p class="homeCenterZagol">Грузоперевозки FastWay</p>
                        <p class="homeCenterPodZagol">Перевозки грузов по Астраханской области недорого, с высокой скоростью и в безопасности!</p>
                        <asp:Button runat="server" OnClick="Unnamed_Click" class="button" Text="Заказать" />
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>

