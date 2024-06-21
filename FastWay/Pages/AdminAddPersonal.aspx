<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAddPersonal.aspx.cs" Inherits="FastWay.Pages.AdminAddPersonal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <link rel="icon" href="..\PIC\main.png" />
    <title>Добавление персонала</title>

    <script type="text/javascript">
        function isAlphabetic(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить буквы латинского и кириллического алфавита
             if (!(charCode >= 1040 && charCode <= 1103) && charCode != 1105 && charCode != 1025) {
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
                    <a href="AdminDriversMovers.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Назад</p>
                    </a>
                </div>
            </div>
            <div class="itemsAndFilter" style="width: 100%;">
                <div style="position: absolute; top: 0; bottom: 0; left: 0; right: 0; margin: auto; align-items: center; display: flex; justify-content: center; height: 500px;">
                    <div class="divAddDrMv" style="width:63%">
                        <div style="display: flex; justify-content: center">
                            <div>
                                <asp:Label class="pAddDrMv" Text="Редактирование" runat="server" Style="margin-top: 20px; margin-bottom:10px; font-size: 16pt; font-weight: bold; text-align: center"></asp:Label>
                                <div style="display: flex">
                                    <div style="margin: 10px; justify-content: center; height: min-content; width: 390px">
                                        <asp:Label class="pAddDrMv" Text="Водитель" Style="margin-left: 10px" runat="server"></asp:Label>
                                        <asp:DropDownList CssClass="combo" ID="comboDrivers" Style="height: 35px; font-size: 13pt" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboDrivers_SelectedIndexChanged" />
                                        <div style="display:flex">
                                            <asp:CheckBox runat="server" ID="checkCar" AutoPostBack="true" style="margin-left:10px; margin-top:3px; transform:scale(1.2); margin-right:-7px" OnCheckedChanged="checkCar_CheckedChanged"/>
                                            <asp:Label ID="labelCheckCar" class="pAddDrMv" Text="Прикрепить к транспорту" Style="margin-left: 10px" runat="server"></asp:Label>
                                        </div>
                                        <asp:DropDownList CssClass="combo" ID="comboCarForDriver" Style="height: 35px" AutoPostBack="true" runat="server" />
                                        <asp:TextBox ID="editDriverLastNameTxb" placeholder="Фамилия" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="editDriverFirstNameTxb" placeholder="Имя" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="editDriverPatronymicTxb" placeholder="Отчество" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="editDriverAgeTxb" placeholder="Возраст" class="inputNewCost" runat="server" onkeypress="return isNumeric(event)"/>
                                        <asp:Panel runat="server" ID="panelEditDriver" Style="display: flex; justify-content: center">
                                            <asp:Button runat="server" ID="editDriver" class="buttonAddDrMv" Style="margin: 5px" OnClick="editDriver_Click" Text="Редактировать" />
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panelApplyCancelEditDriver" Style="display: flex; justify-content: center" Visible="false">
                                            <asp:Button runat="server" ID="applyDriver" class="buttonAddDrMv" Style="margin: 5px" OnClick="applyDriver_Click" Text="Применить" />
                                            <asp:Button runat="server" ID="cancelDriver" class="buttonAddDrMv" Style="margin: 5px" OnClick="cancelDriver_Click" Text="Отмена" />
                                        </asp:Panel>
                                        <div style="display: flex; justify-content: center">
                                            <asp:Button runat="server" ID="deleteDriver" class="buttonAddDrMv" Style="margin: 5px; width: 150px" OnClick="deleteDriver_Click" Text="Удалить" />
                                        </div>
                                    </div>
                                    <div style="margin: 10px;  justify-content: center; height: min-content; width: 390px">
                                        <asp:Label class="pAddDrMv" Text="Грузчик" Style="margin-left: 10px" runat="server"></asp:Label>
                                        <asp:DropDownList CssClass="combo" ID="comboMovers" Style="height: 35px; font-size: 13pt" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboMovers_SelectedIndexChanged" />
                                        <asp:TextBox ID="editMoverLastNameTxb" placeholder="Фамилия" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="editMoverFirstNameTxb" placeholder="Имя" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="editMoverPatronymicTxb" placeholder="Отчество" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="editMoverAgeTxb" placeholder="Возраст" class="inputNewCost" runat="server" onkeypress="return isNumeric(event)"/>
                                        <asp:Panel runat="server" ID="panelEditMover" Style="display: flex; justify-content: center">
                                            <asp:Button runat="server" ID="editMover" class="buttonAddDrMv" Style="margin: 5px" OnClick="editMover_Click" Text="Редактировать" />
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="panelApplyCancelEditMover" Style="display: flex; justify-content: center" Visible="false">
                                            <asp:Button runat="server" ID="applyMover" class="buttonAddDrMv" Style="margin: 5px" OnClick="applyMover_Click" Text="Применить" />
                                            <asp:Button runat="server" ID="cancelMover" class="buttonAddDrMv" Style="margin: 5px" OnClick="cancelMover_Click" Text="Отмена" />
                                        </asp:Panel>
                                        <div style="display: flex; justify-content: center">
                                            <asp:Button runat="server" ID="deleteMover" class="buttonAddDrMv" Style="margin: 5px; width: 150px" OnClick="deleteMover_Click" Text="Удалить" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="divAddDrMv" style="width:35%">
                        <div style="display: flex; justify-content: center">
                            <div>
                                <asp:Label class="pAddDrMv" Text="Добавление" runat="server" Style="margin-top: -35px;margin-bottom:15px; font-size: 16pt; font-weight: bold; text-align: center"></asp:Label>
                                <div style="display: flex">
                                    <div style="margin: 5px; justify-content: center; height: min-content; width: 390px">
                                        <asp:Label class="pMeth" Text="Выберите роль" Style="margin-left: 10px; " runat="server"></asp:Label>
                                        <asp:DropDownList CssClass="combo" ID="comboRole" Style="height: 35px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboRole_SelectedIndexChanged" >
                                            <asp:ListItem Text="Водитель" />
                                            <asp:ListItem Text="Грузчик" />
                                        </asp:DropDownList>
                                        <asp:Label class="pMeth" Text="Транспорт" Style="margin-left: 10px; " runat="server"></asp:Label>
                                        <asp:DropDownList CssClass="combo" ID="comboCars" Style="height: 35px" AutoPostBack="true" runat="server" />
                                        <asp:TextBox ID="addDriverMoverLastNameTxb" placeholder="Фамилия" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="addDriverMoverFirstNameTxb" placeholder="Имя" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="addDriverMoverPatronymicTxb" placeholder="Отчество" class="inputNewCost" runat="server" onkeypress="return isAlphabetic(event)"/>
                                        <asp:TextBox ID="addDriverMoverAgeTxb" placeholder="Возраст" class="inputNewCost" runat="server" onkeypress="return isNumeric(event)"/>
                                        <div style="display: flex; justify-content: center; margin-top: -15px">
                                            <asp:Button runat="server" ID="addDriverMover" class="buttonAddDrMv" OnClick="addDriverMover_Click" Text="Добавить" />
                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <asp:Panel ID="pnlMessageDeleteDriver" runat="server" CssClass="message-panel" Visible="false">
            <div class="message" style="width:500px">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Вы действительно хотите удалить водителя из системы?</p>
                    <div style="display: flex; justify-content: center; margin-top: 10px">
                        <asp:Button runat="server" Text="Да" class="message-button" ID="messageButtonDeleteDriverYes" OnClick="messageButtonDeleteDriverYes_Click" Style="margin: 5px"></asp:Button>
                        <asp:Button runat="server" Text="Нет" class="message-button" ID="messageButtonDeleteDriverNo" OnClick="messageButtonDeleteDriverNo_Click" Style="margin: 5px"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlMessageDeleteMover" runat="server" CssClass="message-panel" Visible="false">
            <div class="message" style="width:500px">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Вы действительно хотите удалить грузчика из системы?</p>
                    <div style="display: flex; justify-content: center; margin-top: 10px">
                        <asp:Button runat="server" Text="Да" class="message-button" ID="messageButtonDeleteMoverYes" OnClick="messageButtonDeleteMoverYes_Click" Style="margin: 5px"></asp:Button>
                        <asp:Button runat="server" Text="Нет" class="message-button" ID="messageButtonDeleteMoverNo" OnClick="messageButtonDeleteMoverNo_Click" Style="margin: 5px"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMessageApply" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Данные сохранены</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonApply" OnClick="messageButtonApply_Click"></asp:Button>

                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMessageDriverDeleted" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Водитель удален из системы</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonDriverDeleted" OnClick="messageButtonDriverDeleted_Click"></asp:Button>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMessageMoverDeleted" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Водитель удален из системы</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonMoverDeleted" OnClick="messageButtonMoverDeleted_Click"></asp:Button>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
