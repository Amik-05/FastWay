<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminTransMethods.aspx.cs" Inherits="FastWay.Pages.AdminTransMethods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <link rel="icon" href="..\PIC\main.png" />
    <title>Способы перевозки</title>
    <script type="text/javascript">
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
    </script>
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
                    <div class="divMeth">
                        <div style="display: flex; justify-content: center">
                            <div>
                                <asp:Label class="pMeth" Text="Редактирование" runat="server" Style="margin-top: 20px; font-size:16pt;font-weight:bold ;text-align: center"></asp:Label>
                                <div style="margin: 20px; justify-content: center; height: min-content; width: 450px">
                                    <asp:Label class="pMeth" Text="Способ перевозки" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:DropDownList CssClass="combo" ID="comboDeliveryType" Style="height: 35px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboDeliveryType_SelectedIndexChanged" />
                                    <div style="display: flex">
                                        <asp:CheckBox runat="server" ID="checkCar" AutoPostBack="true" Style="margin-left: 10px; margin-top: 3px; transform: scale(1.2); margin-right: -7px" OnCheckedChanged="checkCar_CheckedChanged" />
                                        <asp:Label class="pMeth" ID="labelCheckCar" Text="Транспорт" Style="margin-left: 10px;" runat="server"></asp:Label>
                                    </div>
                                    <asp:DropDownList CssClass="combo" ID="comboCarsForEdit" Style="height: 35px" AutoPostBack="true" runat="server" />
                                    <asp:Label ID="txbCostForDelivery" class="pMeth" AutoPostBack="true" Style="margin-left: 10px" runat="server"></asp:Label>
                                    <asp:TextBox ID="txbNewCost" placeholder="Новая цена за километр в рублях" class="inputNewCost" onkeypress="return isNumeric(event)" runat="server" />
                                    <asp:Panel runat="server" ID="panelEdit" Style="display: flex; justify-content: center">
                                        <asp:Button runat="server" ID="edit" class="buttonEditTarif" Style="margin: 5px" OnClick="edit_Click" Text="Редактировать" />
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panelApplyCancel" Style="display: flex; justify-content: center" Visible="false">
                                        <asp:Button runat="server" ID="apply" class="buttonEditTarif" Style="margin: 5px" OnClick="apply_Click" Text="Применить" />
                                        <asp:Button runat="server" ID="cancel" class="buttonEditTarif" Style="margin: 5px" OnClick="cancel_Click" Text="Отмена" />
                                    </asp:Panel>
                                    <div style="display: flex; justify-content: center">
                                        <asp:Button runat="server" ID="delete" class="buttonEditTarif" Style="margin: 5px" OnClick="delete_Click" Text="Удалить" />
                                    </div>

                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="divMeth">
                        <div style="display: flex; justify-content: center; ">
                            <div >
                                <asp:Label class="pMeth" Text="Добавление" runat="server" Style="margin-top: 20px;font-size:16pt; font-weight:bold ; text-align: center"></asp:Label>
                                <div style="margin: 11px; justify-content: center; height: min-content; width: 450px">
                                    <asp:Label class="pMeth" Text="Транспорт для перевозки" Style="margin-left: 10px;margin-top: 18px" runat="server"></asp:Label>
                                    <asp:DropDownList CssClass="combo" ID="comboCarsForAdd" Style="height: 35px" AutoPostBack="true" runat="server" />
                                    <asp:TextBox ID="txbName" placeholder="Название способа перевозки" class="inputNewCost"  runat="server" />
                                    <asp:TextBox ID="txbCost" placeholder="Цена за километр" class="inputNewCost" onkeypress="return isNumeric(event)" runat="server" />
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
                    <p class="pMessage">Вы действительно хотите удалить выбранный способ перевозки?</p>
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
         <asp:Panel ID="pnlMessageTransDeleted" runat="server" CssClass="message-panel" Visible="false">
     <div class="message">
         <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
             <p class="pMessage">Способ перевозки удален</p>
             <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonTransDeleted" OnClick="messageButtonTransDeleted_Click"></asp:Button>

         </div>
     </div>
 </asp:Panel>
    </form>
</body>
</html>
