<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAddCars.aspx.cs" Inherits="FastWay.Pages.AdminAddCars" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Автомобили</title>
    <link href="StyleSheet2.css" rel="stylesheet" />
    <link rel="icon" href="..\PIC\main.png" />
    <script type="text/javascript">
        function isNumberKey1(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var inputControl = evt.target || evt.srcElement;

            // Разрешаем ввод только цифр (0-9) и точки
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Запрещаем ввод более одной точки
            if (charCode === 46 && inputControl.value.indexOf('.') !== -1) {
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


    </script>
    <script type="text/javascript">
        function isVIN(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var char = String.fromCharCode(charCode);

            // Разрешить только буквы латинского алфавита (включая нижний регистр), исключая I, O, Q в любом регистре
            // Разрешить цифры
            if (!((charCode >= 65 && charCode <= 90 && char !== 'I' && char !== 'O' && char !== 'Q') ||
                (charCode >= 97 && charCode <= 122 && char !== 'i' && char !== 'o' && char !== 'q') ||
                (charCode >= 48 && charCode <= 57))) {
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
                    <a href="AdminHome.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Главная</p>
                    </a>
                </div>
            </div>
            <div class="itemsAndFilter" style="width: 100%;">
                <div style="position: absolute; top: 0; bottom: 0; left: 0; right: 0; margin: auto; align-items: center; display: flex; justify-content: center; height: 500px">
                    <div class="divMeth" style="height:500px">
                        <div style="display: flex; justify-content: center">
                            <div>
                                <asp:Label class="pMeth" Text="Редактирование" runat="server" Style="margin-top: 10px;margin-bottom:10px; font-weight:bold;font-size:18pt; text-align: center"></asp:Label>
                                <div style="margin: 5px; justify-content: center; height: min-content; width: 450px">
                                    <asp:Label class="pMeth" Text="Автомобиль" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:DropDownList CssClass="combo" ID="comboCars" Style="height: 35px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboCars_SelectedIndexChanged" />
                                    <asp:Label class="pMeth" Text="Название автомобиля" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:TextBox ID="txbNewName" class="inputNewCost" runat="server" />
                                    <asp:Label class="pMeth" Text="Объём кузова в кубических метрах" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:TextBox ID="txbNewVolume" class="inputNewCost" onkeypress="return isNumberKey1(event)" runat="server" />
                                    <asp:Label class="pMeth" Text="Максимальная грузоподъёмность в тоннах" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:TextBox ID="txbNewMaxWeight" class="inputNewCost" onkeypress="return isNumberKey1(event)" runat="server" />
                                    <asp:Label class="pMeth" Text="VIN-номер транспорта" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:TextBox ID="txbNewVIN" class="inputNewCost" onkeypress="return isVIN(event)" MaxLength="17" runat="server" />
                                    <asp:Panel runat="server" ID="panelEdit" Style="display: flex; justify-content: center">
                                        <asp:Button runat="server" ID="edit" class="buttonEditTarif" Style="margin: 5px;height:35px" OnClick="edit_Click" Text="Редактировать" />
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panelApplyCancel" Style="display: flex; justify-content: center" Visible="false">
                                        <asp:Button runat="server" ID="apply" class="buttonEditTarif" Style="margin: 5px;height:35px" OnClick="apply_Click" Text="Применить" />
                                        <asp:Button runat="server" ID="cancel" class="buttonEditTarif" Style="margin: 5px;height:35px" OnClick="cancel_Click" Text="Отмена" />
                                    </asp:Panel>
                                    <div style="display: flex; justify-content: center">
                                        <asp:Button runat="server" ID="delete" class="buttonEditTarif" Style="margin: 5px;height:35px" OnClick="delete_Click" Text="Удалить" />
                                    </div>

                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="divMeth" style="height:450px">
                        <div style="display: flex; justify-content: center">
                            <div>
                                <asp:Label class="pMeth" Text="Добавление" runat="server" Style="margin-top: 20px;font-weight:bold; font-size:18pt; text-align: center"></asp:Label>
                                <div style="margin: 0px; justify-content: center; height: min-content; width: 420px">
                                    <asp:TextBox ID="txbName" placeholder="Полное название автомобиля" class="inputNewCost" Style="margin-top: 18px" runat="server" />
                                    <asp:TextBox ID="txbVolume" placeholder="Объём кузова в кубических метрах" class="inputNewCost" onkeypress="return isNumberKey1(event)" runat="server" />
                                    <asp:TextBox ID="txbMaxWeight" placeholder="Максимальная грузоподъёмность в тоннах" class="inputNewCost" onkeypress="return isNumberKey1(event)" runat="server" />
                                    <asp:TextBox ID="txbVIN" MaxLength="17" placeholder="VIN-номер транспорта" class="inputNewCost" onkeypress="return isVIN(event)" runat="server" />
                                    <div style="display: flex; justify-content: center; margin-top: -15px">
                                        <asp:Button runat="server" ID="add" class="buttonEditTarif" OnClick="add_Click" Text="Добавить" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <asp:Panel ID="pnlMessage" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Вы действительно хотите удалить выбранный транспорт?</p>
                    <div style="display: flex; justify-content: center; margin-top: 10px">
                        <asp:Button runat="server" Text="Да" class="message-button" ID="messageButtonYes" OnClick="messageButtonYes_Click" Style="margin: 5px"></asp:Button>
                        <asp:Button runat="server" Text="Нет" class="message-button" ID="messageButtonNo" OnClick="messageButtonNo_Click" Style="margin: 5px"></asp:Button>
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
        <asp:Panel ID="pnlMessageCarDeleted" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Способ перевозки удален</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonCarDeleted" OnClick="messageButtonCarDeleted_Click"></asp:Button>

                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
