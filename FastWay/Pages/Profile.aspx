<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" EnableEventValidation="false" Inherits="FastWay.Pages.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Профиль</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/5.0.6/jquery.inputmask.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.min.js"></script>
   
    <script type="text/javascript">
        function isAlphabetic(evt) {
            var charCode = (event.which) ? event.which : event.keyCode;

            // Разрешаем ввод только латинских и русских букв
            if (!((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 1040 && charCode <= 1103))) {
                event.preventDefault();
                return false;
            }

            return true;
        }
    history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };

         $(document).ready(function () {
             $('#phoneTxb').inputmask('+7(999)999-99-99'); // Например, маска для даты (день/месяц/год)
         });

        function checkCodeLength() {
            var codeInput = document.getElementById('<%= txbCode.ClientID %>');
            if (codeInput.value.length === 6) {
                // Запуск метода на сервере
                __doPostBack('<%= btnSubmit.UniqueID %>', '');
            }
        }

        function checkCodeLength1() {
            var codeInput = document.getElementById('<%= txbCodeChangeEmail.ClientID %>');
            if (codeInput.value.length === 6) {
                // Запуск метода на сервере
                __doPostBack('<%= btnSubmit1.UniqueID %>', '');
            }
        }

        function apply() {
            
            __doPostBack('<%= btnApply.UniqueID %>', '');
        }

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
                    <asp:Button runat="server" ID="deleteProfile" CssClass="buttonInHeader" OnClick="deleteProfile_Click" Text="Удалить профиль" />
                    <asp:Button runat="server" ID="exit" CssClass="buttonInHeader" OnClick="exit_Click" Text="Выйти" />
                </div>
            </div>
            <asp:Label runat="server" ID="idProfile" Style="display: none" />
            <div class="itemsAndFilter" style="width: 100%;height:100vh;min-height:100vh">

                <div class="itemsProfle">
                    <div class="imgAndDesc">
                        <div class="divDesc">
                            <div style="display: flex; width: 100%">
                                <div>
                                    <asp:Image runat="server" CssClass="img" ImageUrl="../PIC/profile.png" />
                                </div>
                                <div style="width: 100%">
                                    <div style="display: flex">
                                        <asp:TextBox runat="server" data-required="true" ReadOnly="true" ID="lastNameTxb" class="txbProfile" placeholder="Фамилия" onkeypress="return isAlphabetic(event)" />
                                        <asp:TextBox runat="server" data-required="true" ReadOnly="true" ID="firstNameTxb" class="txbProfile" placeholder="Имя" onkeypress="return isAlphabetic(event)" />
                                        <asp:TextBox runat="server" data-required="true" ReadOnly="true" ID="patronymicTxb" class="txbProfile" placeholder="Отчество" onkeypress="return isAlphabetic(event)" />

                                    </div>
                                    <div style="display: flex">
                                        <asp:TextBox runat="server" data-required="true" ReadOnly="true" ID="phoneTxb" class="txbProfile" placeholder="+7(___)___-__-__" />
                                        <asp:TextBox runat="server" data-required="true" ReadOnly="true" ID="emailTxb" class="txbProfile" placeholder="Электронная почта" />
                                        <asp:TextBox runat="server" data-required="true" ReadOnly="true" ID="passwordTxb" class="txbProfile" placeholder="Пароль" />
                                    </div>

                                    <asp:Label runat="server" ID="errorLabelEmpty" Style="margin-left: 40%" Text="Пустые поля!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorLabelEmail" Style="margin-left: 40%" Text="Некорректный формат почты!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorLabelSameEmail" Style="margin-left: 40%" Text="Данный email занят!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorLabelPass" Style="margin-left: 40%" Text="Пароль должен содержать не менее 6 символов, одну строчную и одну заглавную букву!" class="errorLabel" Visible="false" />
                                </div>

                                <div style="display: block; margin-left: 3px">
                                    <div style="margin-top: 20px;">
                                        <asp:ImageButton ID="editButton" ToolTip="Редактировать" AlternateText=" " CssClass="editBtn" runat="server" ImageUrl="../PIC/edit.png" OnClick="editButton_Click" />
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <asp:ImageButton ID="applyButton" ToolTip="Применить" AlternateText=" " CssClass="editBtn" runat="server" ImageUrl="../PIC/check.png" OnClientClick="apply(); return false"  Visible="false" />
                                        <asp:Button ID="btnApply" runat="server" Text="Submit" OnClick="applyButton_Click" Style="display:none;" />
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <asp:ImageButton ID="declineButton" ToolTip="Отменить" AlternateText=" " CssClass="editBtn" runat="server" ImageUrl="../PIC/decline.png" OnClick="declineButton_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="divRequests">
                        <div style="display: flex; width: 100%; background-color: transparent; justify-content: center;">
                            <p class="pProfileRequest" style="margin-top: 1px; margin-left: 5px">Количество:</p>
                            <asp:Label runat="server" ID="txbQuantity" class="pProfileRequest" Style="margin-top: 1px" />
                            <div style="margin-left: 5px; margin-right: 5px">
                                <asp:Button ID="resetFilters" runat="server" class="buttonSerach" Text="Сбросить фильтры" ToolTip="Сбросить все фильтры" OnClick="resetFilters_Click" />
                            </div>
                            <p class="pProfileRequest" style="margin-top: 1px">Сортировка</p>
                            <div style="margin-right: 5px">
                                <asp:DropDownList ID="sortButton" runat="server" AutoPostBack="true" class="picFilter1" OnSelectedIndexChanged="sortButton_SelectedIndexChanged">
                                    <asp:ListItem Text="Без сортировки" />
                                    <asp:ListItem Text="От новых к старым" />
                                    <asp:ListItem Text="От старых к новым" />
                                </asp:DropDownList>
                            </div>
                            <p class="pProfileRequest" style="margin-top: 1px">Статус</p>
                            <div style="margin-right: 5px">
                                <asp:DropDownList ID="fltButton" runat="server" AutoPostBack="true" class="picFilter1" Style="width: 130px" OnSelectedIndexChanged="filtButton_SelectedIndexChanged">
                                    <asp:ListItem Text="Не выбрано" />
                                    <asp:ListItem Text="В процессе" />
                                    <asp:ListItem Text="Одобрена" />
                                    <asp:ListItem Text="Отклонена" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <asp:ListView runat="server" ID="listViewRequests">
                            <ItemTemplate>
                                <div class="request">

                                    <div style="display: flex; max-width: 100%">
                                        <div class="divPRequest">
                                            <p class="pProfileRequest">Заявка от</p>
                                            <asp:Label ID="OrderID" Text='<%# Eval("ID") %>' runat="server" Visible="false" />
                                            <asp:Label runat="server" ID="dateRequest" Text='<%# Eval("DateOrder") %>' CssClass="pProfileRequest" />
                                            <p class="pProfileRequest">|</p>
                                            <p class="pProfileRequest">Статус: </p>
                                            <asp:Label runat="server" ID="stRequest" Text='<%# Eval("Status") %>' CssClass="pProfileRequest" />
                                        </div>
                                        <div style="float: right; width: 7.5%; display: flex; justify-content: right;">
                                            <asp:Button ID="viewRequest" TabIndex='<%#Convert.ToInt32(Eval("ID")) %>' Text="Подробнее" CssClass="buttonPodrobnee" runat="server" OnClick="viewRequest_Click" />
                                        </div>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                        <asp:Panel ID="emptyListPanel" runat="server" Style="height: 45px; width: auto; border-radius: 10px" Visible="false">
                            <div class="request" style="background: none; display: flex; justify-content: center; margin-top: 15px">
                                <p class="pProfileRequest">Список заявок пуст.</p>
                                <a href="Cargo.aspx" style="text-decoration-color: white">
                                    <p class="pProfileRequest">Перейти к заказу</p>
                                </a>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlMessage" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 55px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Вы действительно хотите выйти из профиля?</p>
                    <div style="display: flex; justify-content: center; margin-top: 10px">
                        <asp:Button runat="server" Text="Да" class="message-button" ID="messageButtonYes" OnClick="messageButtonYes_Click" Style="margin: 5px"></asp:Button>
                        <asp:Button runat="server" Text="Нет" class="message-button" ID="messageButtonNo" OnClick="messageButtonNo_Click" Style="margin: 5px"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlDeleteProfile" runat="server" CssClass="message-panel" Visible="false">
            <div class="message" style="">
                <div style="display: inline; margin-top: 40px;  justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Вы действительно хотите удалить профиль? Все заявки и информация об аккаунте будут безвозвратно удалены.</p>
                    <div style="display: flex; justify-content: center; margin-top: 10px">
                        <asp:Button runat="server" Text="Да" class="message-button" ID="deleteYes" OnClick="deleteYes_Click" Style="margin: 5px"></asp:Button>
                        <asp:Button runat="server" Text="Нет" class="message-button" ID="deleteNo" OnClick="deleteNo_Click" Style="margin: 5px"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlDeleteEmailConfirm" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin: 10px; margin-top:15px; justify-content: center; width: 100%;">
                    <div style="height:20px;">
                        <asp:Label ID="errorLabelWrongCode" Visible="false" Text="Неверный код!" runat="server" class="errorLabel"/>
                    </div>
                    <div>
                        <p class="pMessage">Код для подтверждения удаления профиля отправлен на почту</p>
                    </div>
                    <div style="justify-content: center; margin-top: 10px">
                        <div style="display:flex;justify-content:center">
                             <asp:TextBox runat="server" ID="txbCode" data-required="true" onkeypress="return isNumberKey(event)" class="txbRes1" style="height:30px; width:250px" placeholder="Введите код" />
                        </div>
                        <div style="display:flex; justify-content:center;margin-top:0px">
                            <asp:Button runat="server" Text="Подтвердить" class="message-button" ID="confirmDelete" OnClick="confirmDelete_Click" Style="margin:5px;width:140px;display:none"></asp:Button>
                            <asp:Button runat="server" Text="Отмена" class="message-button" ID="cancelDelete" OnClick="cancelDelete_Click" Style="margin:5px; width:140px"></asp:Button>
                        </div>
                    </div>
                    <script type="text/javascript">
                        document.getElementById('<%= txbCode.ClientID %>').addEventListener('input', checkCodeLength);
                    </script>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Style="display:none;" />
                    <asp:TextBox ID="hiddenCode" runat="server" style="display:none"/>
                </div>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="pnlChangeEmailConfirm" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin: 10px; margin-top: 15px; justify-content: center; width: 100%;">
                    <div style="height: 20px;">
                        <asp:Label ID="errorLabelWrongCodeChange" Visible="false" Text="Неверный код!" runat="server" class="errorLabel" />
                        <asp:Label ID="errorLabelEmptyCodeChange" Visible="false" Text="Заполните поле!" runat="server" class="errorLabel" />
                    </div>
                    <div>
                        <p class="pMessage">Код для подтверждения смены почты отправлен на указаный email</p>
                    </div>
                    <div style="justify-content: center; margin-top: 10px">
                        <div style="display: flex; justify-content: center">
                            <asp:TextBox runat="server" ID="txbCodeChangeEmail" data-required="true" onkeypress="return isNumeric(event)" class="txbRes1" Style="height: 30px; width: 250px" placeholder="Введите код" />
                        </div>
                        <div style="display: flex; justify-content: center; margin-top: 0px">
                            <asp:Button runat="server" Text="Подтвердить" class="message-button" ID="confirmChangeEmail" OnClick="confirmChangeEmail_Click" Style="margin: 5px; width: 140px; display: none"></asp:Button>
                            <asp:Button runat="server" Text="Отмена" class="message-button" ID="cancelChangeEmail" OnClick="cancelChangeEmail_Click" Style="margin: 5px; width: 140px"></asp:Button>
                        </div>
                    </div>
                    <script type="text/javascript">
                        document.getElementById('<%= txbCodeChangeEmail.ClientID %>').addEventListener('input', checkCodeLength1);
                    </script>
                    <asp:Button ID="btnSubmit1" runat="server" Text="Submit" OnClick="btnSubmit1_Click" Style="display:none;" />
                    <asp:TextBox ID="hiddenCode1" runat="server" Style="display: none" />
                    <asp:TextBox ID="hiddenPass" runat="server" Style="display: none" />
                    <asp:TextBox ID="hiddenOldEmail" runat="server" Style="display: none" />

                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlMessageApply" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top:65px; justify-content:center; width:100%; height:70px;">
                    <p class="pMessage">Изменения сохранены</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonApply" OnClick="messageButtonApply_Click"></asp:Button>

                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlMessageDeleteCompleted" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Удаление успешно</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonDeleteCompleted" OnClick="messageButtonDeleteCompleted_Click"></asp:Button>

                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
