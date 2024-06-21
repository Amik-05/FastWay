<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" EnableEventValidation="false" Inherits="FastWay.Pages.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Регистрация</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/5.0.6/jquery.inputmask.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.min.js"></script>

    <script type="text/javascript">
        function isAlphabetic(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить буквы латинского и кириллического алфавита
            if (!(charCode >= 1040 && charCode <= 1103) && charCode != 1105 && charCode != 1025) {
                return false;
            }
            return true;
        }

        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить только цифры (коды ASCII для цифр)
            if (charCode < 48 || charCode > 57) {
                return false;
            }
            return true;
        }


        $(document).ready(function () {
            $('#phoneTxb').inputmask('+7(999)999-99-99'); // Например, маска для даты (день/месяц/год)
        });


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
        <div class="regCenter">
            <div class="divRegistration">
                <div>
                    <div style="display: flex; height: 40px; margin-top: -20px">
                        <a href="Autorization.aspx" style="text-decoration: none">
                            <img src="../PIC/icons8-назад-100.png" runat="server" class="picBack" />
                        </a>
                        <a href="Autorization.aspx" class="errorLabel" style="height: 40px; margin-top: 8px; margin-left: 2px; text-decoration: none">
                            <asp:Label Text="Назад" runat="server" />
                        </a>
                    </div>
                    <div style="display: grid; background-color: transparent; height: 60px; margin-bottom: -10px">
                        <asp:Label runat="server" Text="Регистрация" class="pAutoriz" />
                        <asp:Label runat="server" ID="errorLabel" Style="margin-top: -10px" Text="Профиль с указанным email уже существует!" class="errorLabel" Visible="false" />
                        <asp:Label runat="server" ID="errorLabelEmpty" Style="margin-top: -10px" Text="Пустые поля!" class="errorLabel" Visible="false" />
                        <asp:Label runat="server" ID="errorLabelEmail" Style="margin-top: -10px" Text="Некорректный формат почты!" class="errorLabel" Visible="false" />
                        <asp:Label runat="server" ID="errorLabelPers" Style="margin-top: -10px" Text="Примите соглашение на обработку персональных данных!" class="errorLabel" Visible="false" />
                        <asp:Label runat="server" ID="errorLabelPass" Style="margin-top: -10px" Text="Пароль должен содержать не менее 6 символов, одну строчную и одну заглавную букву!" class="errorLabel" Visible="false" />
                    </div>
                    <div style="display: flex">
                        <asp:TextBox runat="server" data-required="true" ID="lastNameTxb" class="txbReg" placeholder="Фамилия" onkeypress="return isAlphabetic(event)" />
                        <asp:TextBox runat="server" data-required="true" ID="firstNameTxb" class="txbReg" placeholder="Имя" onkeypress="return isAlphabetic(event)" />
                        <asp:TextBox runat="server" data-required="true" ID="patronymicTxb" class="txbReg" placeholder="Отчество" onkeypress="return isAlphabetic(event)" />
                    </div>
                    <div style="display: flex">
                        <asp:TextBox runat="server" data-required="true" ID="phoneTxb" class="txbReg" placeholder="+7(___)___-__-__" />
                        <asp:TextBox runat="server" data-required="true" ID="emailTxb" class="txbReg" placeholder="Электронная почта" />
                        <asp:TextBox runat="server" data-required="true" ID="passwordTxb" class="txbReg" placeholder="Пароль" TextMode="Password" />
                    </div>
                    <div class="divOrder" style="margin-left: 5px; margin-top: 10px; margin-bottom: -10px">
                        <asp:CheckBox runat="server" ID="checkAgree" class="checkPersonal" data-required="true" />
                        <a href="Personal.aspx" target="_blank" style="color: white; text-decoration: underline; margin-left: 10px">
                            <p class="pPersonal">Я согласен(-а) с обработкой персональных данных</p>
                        </a>
                    </div>
                    <asp:Button runat="server" ID="reg" class="buttonLogin" OnClick="reg_Click" Text="Зарегистрироваться" />

                </div>

            </div>
            <asp:Panel ID="pnlEmailConfirm" runat="server" CssClass="message-panel" Visible="false">
                <div class="message">
                    <div style="display: inline; margin: 10px; margin-top: 15px; justify-content: center; width: 100%;">
                        <div style="height: 20px;">
                            <asp:Label ID="errorLabelWrongCode" Visible="false" Text="Неверный код!" runat="server" class="errorLabel" />
                            <asp:Label ID="errorLabelEmptyCode" Visible="false" Text="Заполните поле!" runat="server" class="errorLabel" />
                        </div>
                        <div>
                            <p class="pMessage">Код для подтверждения аккаунта отправлен на почту</p>
                        </div>
                        <div style="justify-content: center; margin-top: 10px">
                            <div style="display: flex; justify-content: center">
                                <asp:TextBox runat="server" ID="txbCode" data-required="true" onkeypress="return isNumeric(event)" class="txbRes1" Style="height: 30px; width: 250px" placeholder="Введите код" />
                            </div>
                            <div style="display: flex; justify-content: center; margin-top: 0px">
                                <asp:Button runat="server" Text="Проверить" class="message-button" ID="isRightCode" OnClick="isRightCode_Click" Style="margin: 5px; width: 140px; display: inline"></asp:Button>
                                <asp:Button runat="server" Text="Подтвердить" class="message-button" ID="confirmDelete" OnClick="confirmDelete_Click" Style="margin: 5px; width: 140px; display: none"></asp:Button>
                                <asp:Button runat="server" Text="Отмена" class="message-button" ID="cancelDelete" OnClick="cancelDelete_Click" Style="margin: 5px; width: 140px"></asp:Button>
                            </div>
                        </div>
                        <asp:TextBox ID="hiddenCode" runat="server" Style="display: none" />
                        <asp:TextBox ID="hiddenPass" runat="server" Style="display: none" />

                    </div>
                </div>
            </asp:Panel>
        </div>
        
        <asp:Panel ID="pnlMessage" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Регистрация успешна</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButton" OnClick="messageButton_Click"></asp:Button>

                </div>
            </div>
        </asp:Panel>


    </form>
</body>
</html>
