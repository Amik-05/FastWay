<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminСhequeRequest.aspx.cs" Inherits="FastWay.Pages.AdminСhequeRequest" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <title>Чек</title>
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
                    <a href="AdminAllOrders.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Назад</p>
                    </a>
                </div>
            </div>

            <div class="itemsAndFilter" style="width: 100%;">
                <div class="centerCheque" style="text-align: center;">
                    <div style="display: block;width:max-content; height:max-content; ">
                        <div class="divYourOrder">
                            <div style="display: flex; justify-content: center; text-align: center">
                                <p class="pYourOrder">Статус: </p>
                                <asp:Label runat="server" ID="pProverka" class="pYourOrder" Style="margin-left: 7px"></asp:Label>
                            </div>
                            <asp:Label runat="server" ID="pRejection" class="pYourOrder" Style="text-align: center"></asp:Label>
                        </div>
                        <div class="divRequest">
                            <div class="divCargoAndOrderInfo">
                                <asp:Label ID="infoCargo" class="pCheque" Style="text-align: center" Text="Информация о грузе" runat="server"></asp:Label>
                                <asp:Label ID="Label1" class="pCheque" Text="___________________________________________" Style="text-align: center; color: lightgray; margin-top: -13px" runat="server"></asp:Label>
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
                                <asp:Label ID="Label2" class="pCheque" Text="___________________________________________" Style="text-align: center; color: lightgray; margin-top: -13px" runat="server"></asp:Label>
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
                        <div style="width: 100%; text-align: center; margin-top: 0px; display: flex; justify-content:center; height: max-content; ">
                            <div>
                                <asp:Button runat="server" ID="download" class="button" Width="130" style="border-bottom-right-radius:0;border-top-right-radius:0" Text="Скачать" OnClick="download_Click" />
                            </div>
                            <div >
                                <asp:Button runat="server" ID="send" class="button" style="border-bottom-left-radius:0;border-top-left-radius:0" Width="200" Text="Отправить на почту" OnClick="send_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <asp:Panel ID="pnlMessageSendMail" runat="server" CssClass="message-panel" Visible="false">
            <div class="message">
                <div style="display: inline; margin-top: 65px; justify-content: center; width: 100%; height: 70px;">
                    <p class="pMessage">Письмо отправлено на почту</p>
                    <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButton" OnClick="messageButton_Click"></asp:Button>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
