<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Autorization.aspx.cs" Inherits="FastWay.Pages.Autorization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Авторизация</title>
    <link rel="icon" href="..\PIC\main.png" />

    <script type="text/javascript">
        function validateInputs() {
            // Проверка TextBox
            var textBoxes = document.querySelectorAll('input[data-required="true"]');
            for (var i = 0; i < textBoxes.length; i++) {
                var textBox = textBoxes[i];

                if (textBox.value.trim() === '') {
                    textBox.style.border = '0.5px solid red';

                } else {
                    textBox.style.border = ''; // Сброс стиля границы
                }
            }
        }

        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="autorizCenter">
            <div class="divAutorization">
                <div>
                    <div style="display: flex; height: 40px; margin-bottom: 10px">
                        <a href="Home.aspx" style="text-decoration: none">
                            <img src="../PIC/icons8-главная-128.png" runat="server" class="picHomeAutoriz" />
                        </a>
                        <a href="Home.aspx" class="errorLabel" style="height: 40px; margin-top: 8px;margin-left:2px; text-decoration: none">
                            <asp:Label Text="Домой" runat="server" />
                        </a>
                    </div>
                    <div style="margin-bottom: 20px">
                        <asp:Label runat="server" Text="Авторизация" class="pAutoriz" />
                    </div>
                    <div style="display: block; ">
                        
                        <asp:TextBox runat="server" ID="txbEmail" data-required="true" class="txbAutoriz" placeholder="Электронная почта" />
                        <asp:TextBox runat="server" TextMode="Password" ID="txbPassword" data-required="true" class="txbAutoriz" placeholder="Пароль" />
                    </div>
                    <div style="display: flex; justify-content: center">
                        <asp:Label runat="server" ID="errorLabel" Text="Неверные почта или пароль!" class="errorLabel" Visible="False" />
                        <asp:Label runat="server" ID="errorLabelEmail" Text="Некорректный формат почты!" class="errorLabel" visible="false"/>
                        <asp:Label runat="server" ID="errorLabelEmpty" Text="Пустые поля!" class="errorLabel"  Visible="False"/>
                        <asp:Label runat="server" ID="errorNotHaveEmail" Text="Профиля с таким email нет в системе!" class="errorLabel"  Visible="False"/>

                    </div>
                    <div style="margin-top:10px">
                        <asp:Button runat="server" ID="login" OnClick="login_Click" class="buttonLogin" Text="Вход" Style="margin-top: 10px" />
                    </div>
                    <div>
                        <p class="pAutoriz1">Нет аккаунта? <a class="pAutoriz1" href="Registration.aspx">Регистрация</a></p>
                        <a class="pAutoriz2" href="ResetPassword.aspx">Забыли пароль?</a>
                    </div>


                </div>

            </div>
        </div>

    </form>
</body>
</html>
