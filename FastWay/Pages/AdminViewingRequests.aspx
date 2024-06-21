<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminViewingRequests.aspx.cs" Inherits="FastWay.Pages.AdminViewingRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Просмотр заявок</title>
    <link href="StyleSheet2.css" rel="stylesheet" />
    <link rel="icon" href="..\PIC\main.png" />

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

        function hideRejectionDiv() {
            var textBoxes = document.querySelectorAll('input[type="text"][data-required="true"]');
            for (var i = 0; i < textBoxes.length; i++) {
                var textBox = textBoxes[i];

                if (textBox.value.trim() == '') {
                    textBox.style.border = '0.5px solid red';

                } else {
                    textBox.style.border = ''; // Сброс стиля границы
                    var rejectionDiv = document.getElementById('rejectionDiv');
                    rejectionDiv.style.display = 'none';
                }
            }
        }
        function showRejectionDiv() {
            var rejectionDiv = document.getElementById('rejectionDiv');
            rejectionDiv.style.display = 'block';
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
                <div class="divPheader" style="float: right; margin-right: 8px">
                    <a href="AdminAllOrders.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Все заявки</p>
                    </a>
                </div>
            </div>

            <div class="itemsAndFilter">
                <div class="centerCheque">
                    <div style="display: block">
                        <div class="divIdRequest" style="display:flex; justify-content:center">
                            <div style="width: 110%; display: flex;">
                                <div style=" width:20%;margin:2px">
                                    <p class="pViewRequest">ID заявки</p>
                                    <asp:DropDownList Class="comboRequest" ID="comboRequests" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboDeliveryType_SelectedIndexChanged" />
                                </div>
                                <div style=" width: 40%;margin:2px">
                                    <p class="pViewRequest">Первый грузчик:</p>
                                    <asp:DropDownList Class="comboRequest" ID="comboMover1" AutoPostBack="true" runat="server" />
                                </div>
                                <div style=" width: 40%;margin:2px">
                                    <p class="pViewRequest">Второй грузчик:</p>
                                    <asp:DropDownList Class="comboRequest" ID="comboMover2" AutoPostBack="true" runat="server" />
                                </div>

                            </div>
                        </div>

                        <div class="divRequest">
                            <div class="divCargoAndOrderInfo">
                                <asp:Label ID="infoCargo" class="pCheque" Style="text-align: center" Text="Информация о грузе" runat="server"></asp:Label>
                                <asp:Label ID="Label1" class="pCheque" Style="text-align: center; color: lightgray; margin-top: -17px" runat="server" Text="___________________________________________"></asp:Label>
                                <asp:Label ID="cargoTitle" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="cargoCategory" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="cargoSubcategory" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="cargoOverallVolume" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="cargoTotalWeight" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="cargoDeliveryType" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="cost" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="sendDate" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="arrivalDate" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="movers" class="pCheque" Text="" runat="server"></asp:Label>
                            </div>
                            <div class="divCargoAndOrderInfo">
                                <asp:Label ID="orderID" class="pCheque" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="infoOrder" class="pCheque" Style="text-align: center" Text="Информация о заказчике" runat="server"></asp:Label>
                                <asp:Label ID="Label2" class="pCheque" Style="text-align: center; color: lightgray; margin-top: -17px" runat="server" Text="___________________________________________"></asp:Label>
                                <asp:Label ID="orderLastName" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderFirstName" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderPatronymic" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderPhone" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderEmail" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderFromAddress" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderToAddress" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderDistance" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="orderDuration" class="pCheque" Text="" runat="server"></asp:Label>
                                <asp:Label ID="dateOrder" class="pCheque" Text="" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; text-align: center; margin-top: 10px">
                            <asp:Button runat="server" ID="confirm" class="button" Text="Подтвердить" OnClick="confirm_Click" />
                            <asp:Button runat="server" ID="reject" class="button" Text="Отклонить" OnClick="reject_Click" />
                        </div>
                        <asp:Panel runat="server" id="rejectionDiv" class="divReject" Visible="false">
                            <asp:TextBox ID="txtRejectionReason" class="okTxb" runat="server" data-required="true" placeholder="Опишите причину отклонения"></asp:TextBox>
                            <asp:Button runat="server" ID="okReject" class="okButton" OnClick="okReject_Click" Text="ОК" />
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Заявка отклонена</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButton" OnClick="messageButton_Click" Style="margin: 5px"></asp:Button>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlMessageConfirm" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Заявка одобрена</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="Button1" OnClick="messageButton_Click" Style="margin: 5px"></asp:Button>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlMessageQuestionConfirm" runat="server" CssClass="message-panel" Visible="false">
            <div class="message" style="width:500px">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Вы действительно хотите подтвердить заявку?</p>
                    <div style="display: flex; justify-content: center; margin-top: 10px">
                        <asp:Button runat="server" Text="Да" class="message-button" ID="messageButtonConfirmRequesYes" OnClick="messageButtonConfirmRequesYes_Click" Style="margin: 5px"></asp:Button>
                        <asp:Button runat="server" Text="Нет" class="message-button" ID="messageButtonConfirmRequesNo" OnClick="messageButtonConfirmRequesNo_Click" Style="margin: 5px"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
