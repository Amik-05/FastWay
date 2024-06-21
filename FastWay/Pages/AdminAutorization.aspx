<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAutorization.aspx.cs" Inherits="FastWay.Pages.AdminAutorization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <title>Авторизация</title>
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

            <div class="itemsAndFilter" style="width: 100%;">

                <div class="homeAutoriz">
                    <div class="divAutorization">
                        <div style="display: block">
                            <div style="display: flex; height: 40px; margin-bottom: 10px">
                                <a href="Home.aspx" style="text-decoration: none">
                                    <img src="../PIC/icons8-главная-128.png" runat="server" class="picHomeAutoriz" />
                                </a>
                                <a href="Home.aspx" class="errorLabel" style="height: 40px;  margin-top: -13px; margin-left:2px; text-decoration: none">
                                    <asp:Label Text="Домой" runat="server" Font-Size="12pt" />
                                </a>
                            </div>
                            <div>
                                <p class="pZagol">Вход в систему администрирования FastWay</p>
                            </div>
                            <div style="height: 0px; display: flex; justify-content: center">
                                <asp:Label runat="server" ID="errorLabel" Style="margin-top: -20px" Text="Неверные логин или пароль" class="errorLabel" Visible="false" />
                                <asp:Label runat="server" ID="errorLabelEmpty" Style="margin-top: -20px" Text="Пустые поля!" class="errorLabel" Visible="false" />
                            </div>
                            <div>
                                <asp:TextBox runat="server" ID="txbLogin" class="txbAutoriz" placeholder="Логин" />
                                <asp:TextBox runat="server" TextMode="Password" ID="txbPassword" class="txbAutoriz" placeholder="Пароль" />
                                <asp:Button runat="server" ID="login" OnClick="login_Click" class="buttonLogin" Text="Войти" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
