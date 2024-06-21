<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChequeForAutorizated.aspx.cs" Inherits="FastWay.Pages.ChequeForAutorizated" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Заявка</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
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

            <div class="header" style="grid-area: h; width: 100%;">

                <div class="divPheader">
                    <a href="Home.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Главная</p>
                    </a>
                </div>
                <div style="float: right; margin-right: 10px">
                    <asp:Button runat="server" ID="vhod" CssClass="buttonInHeader" Text="Войти" OnClick="v_Click" />
                    <asp:Button runat="server" ID="profile" CssClass="buttonInHeader" Text="Профиль" OnClick="p_Click" />
                </div>
            </div>

            <div class="itemsAndFilter" style="width: 100%;">
                <div class="centerCheque">
                    <div style="display:block">
                        <div class="divYourOrder">
                            <p class="pYourOrder" style="text-align: center">Ваша заявка на проверке</p>
                        </div>
                        <div class="divRequest">
                            <img class="imgModer" src="../PIC/watches.png" />
                        </div>
                        <div style="width: 100%; text-align: center; margin-top: 10px;">
                            <asp:Button runat="server" ID="goToProfile" class="button" Text="Перейти в профиль" OnClick="goToProfile_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
