<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="FastWay.Pages.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Авторизация</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

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
    </script>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить ввод цифр и точки (код ASCII: 48-57 для цифр, 46 для точки)
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Убедитесь, что вводить можно только одну точку
            var input = document.getElementById("<%= txbCode.ClientID %>").value;
            if (charCode === 46 && input.indexOf('') !== -1) {
                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">


        function resendEmail()
        {
            __doPostBack('<%= btnSubmit2.ClientID %>', '');
        }

        window.onload = function () {
            // Не запускаем таймер сразу при загрузке, ожидаем действия пользователя
        }

        function checkCodeLength() {
            var codeInput = document.getElementById('<%= txbCode.ClientID %>');
            if (codeInput.value.length === 6) {
                // Запуск метода на сервере
                __doPostBack('<%= btnSubmit.UniqueID %>', '');
            }
        }

        
    </script>
    <script type="text/javascript">
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="autorizCenter">
            <div class="divAutorization" style="width:550px">
                <asp:TextBox ID="hiddenCode" runat="server" style="display:none"/>
                <asp:TextBox ID="hiddenYes" runat="server" style="display:none"/>
                <div style="display: flex;height:40px;margin-bottom:10px">
                    <a href="Autorization.aspx" style="text-decoration:none">                 
                        <img src="../PIC/icons8-назад-100.png"  runat="server" class="picBack" />
                    </a>
                    <a href="Autorization.aspx" class="errorLabel" style="height:40px;margin-top:8px; margin-left:2px; text-decoration:none">  
                        <asp:Label Text="Назад" runat="server" />
                    </a>
                </div>
                <div style="display: block;margin-bottom:35px">
                    <asp:Label runat="server" Text="Восстановление пароля" class="pAutoriz" />
                </div>
                <div>
                    <div style="display:flex;margin-top:10px">
                        <asp:TextBox runat="server" ID="txbEmail" data-required="true" class="txbRes" placeholder="Введите электронную почту" />
                        <asp:TextBox runat="server" ID="txbCode" ReadOnly="true" style="opacity:0.25" data-required="true" onkeypress="return isNumberKey(event)" class="txbRes" placeholder="Введите код" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Style="display:none;" />
                    </div>
                    <div style="margin:3px;">
                        <asp:LinkButton ID="lnkResend" runat="server" Text="Отправить письмо" CssClass="errorLabel" OnClientClick="resendEmail(); return false;"/>
                        <asp:Label ID="lblMessage" runat="server" Text="Письмо отправлено" CssClass="errorLabel" Visible="false" />
                        <asp:Button ID="btnSubmit2" runat="server" Text="Submit" OnClick="btnSubmit2_Click" Style="display:none;" />
                    </div>

                    <div style="display:flex;">
                        <asp:TextBox runat="server" ID="txbPass" ReadOnly="true" style="opacity:0.25" data-required="true" class="txbRes" placeholder="Введите новый пароль" />
                    </div>   
                    <div style="margin-bottom:3px;display:flex; justify-content:center">
                        <asp:Label ID="errorLabelNonSys" Visible="false" Text="Аккаунта с такой электронной почтой нет в системе!" runat="server" class="errorLabel"/>
                        <asp:Label ID="errorLabelWrongCode" Visible="false" Text="Неверный код!" runat="server" class="errorLabel" />
                        <asp:Label ID="errorLabelEmail" Visible="false" Text="Некорректный формат электронной почты!" runat="server" class="errorLabel" />
                        <asp:Label ID="errorLabelNon" style="display:none" Text="Заполните поля!" runat="server" class="errorLabel" />
                        <asp:Label ID="errorLabelSamePass" Visible="false" Text="Новый пароль совпадает со старым!" runat="server" class="errorLabel" />
                        <asp:Label ID="errorLabelWrongPass" Visible="false" Text="Необходимо не менее 6 символов, одной строчной и одной заглавной буквы!" runat="server" class="errorLabel" style="font-size:14px"/>
                    </div>
                    
                    <script type="text/javascript">
                        document.getElementById('<%= txbCode.ClientID %>').addEventListener('input', checkCodeLength);
                    </script>
                </div>

                <asp:Button runat="server" ID="login" OnClick="login_Click" OnClientClick="validateInputs()" class="buttonLogin" Text="Сохранить" style="margin-top:20px"/>
            </div>
        </div>
        <asp:Panel ID="pnlMessage" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top:65px; justify-content:center; width:100%; height:70px;">
                    <p class="pMessage">Пароль успешно сменен</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButton" OnClick="messageButton_Click"></asp:Button>

                </div>
            </div>
        </asp:Panel>
         <asp:Panel ID="pnlMessageSendMail" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Письмо отправлено на почту</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonCloseMailSended" OnClick="messageButtonCloseMailSended_Click"></asp:Button>
                </div>
            </div>
        </asp:Panel>

    </form>
    
</body>
</html>
