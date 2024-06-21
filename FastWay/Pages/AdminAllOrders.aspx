<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAllOrders.aspx.cs" Inherits="FastWay.Pages.AdminAllOrders" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Все заявки</title>
    <link href="StyleSheet2.css" rel="stylesheet" />
    <link rel="icon" href="..\PIC\main.png" />

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
                    <a href="AdminViewingRequests.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Назад</p>
                    </a>
                </div>
                <div style="float: right; margin-right: 8px">
                    <asp:Button runat="server" ID="stat" CssClass="buttonInHeader" Text="Статистика" OnClick="stat_Click"/>
                </div>
            </div>
            <div class="itemsAndFilter" style="width: 100%;">

                <div class="itemsProfle">

                    <div class="imgAndDesc">
                        <div style="display: flex; width: 100%; height: 100%; justify-content: center;">
                            <div style="width:70%;display:flex">
                                <div style="width: 8%; margin-top: 5px; margin-right: 5px">
                                    <asp:Button ID="resetFilters" runat="server" class="buttonSerach" Text="Сбросить" ToolTip="Сбросить все фильтры" OnClick="resetFilters_Click" />
                                </div>
                                <div style="width: 89%; margin-top: 0px; margin-right: 12px; display: flex">
                                    <asp:TextBox ID="searchLastTxb" placeholder="Фамилия" runat="server" class="serachInput" onkeypress="return isAlphabetic(event)"/>
                                    <asp:TextBox ID="searchFirstTxb" placeholder="Имя" runat="server" class="serachInput" onkeypress="return isAlphabetic(event)"/>
                                    <asp:TextBox ID="searchPatroTxb" placeholder="Отчество" runat="server" class="serachInput" onkeypress="return isAlphabetic(event)"/>
                                </div>
                                <div style="width: 7%; margin-top: 5px; margin-left: -13px">
                                    <asp:Button ID="searchButton" runat="server" class="buttonSerach" Text="Поиск" OnClick="searchButton_Click" />
                                </div>

                            </div>
                        </div>
                        <div style="display: flex; width: 100%; height: 100%; justify-content: center">
                            <div style="margin-top: 4px;">
                                <asp:Label runat="server" Text='Сортировка' CssClass="pProfileRequest" />
                            </div>
                            <div style="margin-top: 0px; margin-right: 5px">
                                <asp:DropDownList ID="sortButton" runat="server" AutoPostBack="true" class="picFilter" OnSelectedIndexChanged="sortButton_SelectedIndexChanged">
                                    <asp:ListItem Text="Без сортировки" />
                                    <asp:ListItem Text="От новых к старым" />
                                    <asp:ListItem Text="От старых к новым" />
                                </asp:DropDownList>
                            </div>
                            <div style="margin-top: 4px;">
                                <asp:Label runat="server" Text='Статус' CssClass="pProfileRequest" />
                            </div>
                            <div style="margin-top: 0px; margin-right: 5px">
                                <asp:DropDownList ID="fltButton" runat="server" AutoPostBack="true" class="picFilter" Style="width: 130px" OnSelectedIndexChanged="filtButton_SelectedIndexChanged">
                                    <asp:ListItem Text="Не выбрано" />
                                    <asp:ListItem Text="В процессе" />
                                    <asp:ListItem Text="Одобрена" />
                                    <asp:ListItem Text="Отклонена" />
                                </asp:DropDownList>
                            </div>
                            <div style="margin-top: 4px;">
                                <asp:Label runat="server" Text='Количество:' CssClass="pProfileRequest" />
                            </div>
                            <div style="margin-top: 4px;">
                                <asp:Label runat="server" ID="txbQuantity" CssClass="pProfileRequest" />
                            </div>
                        </div>
                    </div>

                    <div class="divRequests">
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
                                            <p class="pProfileRequest">|</p>
                                            <p class="pProfileRequest">Заказчик: </p>
                                            <asp:Label runat="server" ID="lnRequest" Text='<%# Eval("LastName") %>' CssClass="pProfileRequest" />
                                            <asp:Label runat="server" ID="fnRequest" Text='<%# Eval("FirstName") %>' CssClass="pProfileRequest" />
                                            <asp:Label runat="server" ID="ptRequest" Text='<%# Eval("Patronymic") %>' CssClass="pProfileRequest" />
                                            <asp:Label runat="server" ID="emailRequest" Text='<%# Eval("Email") %>' CssClass="pProfileRequest" />


                                        </div>
                                        <div style="float: right; width: 7.5%; display: flex; justify-content: right;">
                                            <asp:Button ID="viewRequest" TabIndex='<%#Convert.ToInt32(Eval("ID")) %>' Text="Подробнее" CssClass="buttonPodrobnee" runat="server" OnClick="viewRequest_Click" />
                                        </div>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                        <asp:Panel ID="emptyListPanel" runat="server" Style="height: 45px; width: auto; border-radius: 10px" Visible="false">
                            <div class="request" style="background: none; justify-content: center;display:flex">
                                <p class="pProfileRequest">Список заявок пуст</p>
                            </div>
                        </asp:Panel>

                    </div>
                </div>

            </div>
        </div>
        <asp:Panel ID="panelStatistics" runat="server" CssClass="message-panel" Visible="false" >
            <div class="message" style="width: 50%; height: 50%;">
                <div style="display: flex; justify-content: center; padding:20px; width:100% ">
                    <div style="width:100%; height:max-content; margin:auto">
                        <div style="width:100%; border-bottom:2px solid #ffffff99; margin-bottom:10px;text-align:left">
                            <asp:Label runat="server" style="font-size: 19pt;" Text="Статистика за месяц" class="pProfileRequest" />
                        </div>
                        <div style="text-align:left">
                            <asp:Label runat="server" ID="countOfOrders" class="pProfileRequest" style="font-size:16pt" />
                        </div>
                        <div style="text-align:left">
                            <asp:Label runat="server" ID="moneyOfOrders" class="pProfileRequest" style="font-size:16pt"/>
                        </div>
                        <div style="text-align:left">
                            <asp:Label runat="server" ID="muchDeliveryType" class="pProfileRequest" style="font-size:16pt"/>
                        </div>
                        <div style="text-align:left">
                            <asp:Label runat="server" ID="averageVolume" class="pProfileRequest" style="font-size:16pt"/>
                        </div>
                        <div style="margin-top:10px">
                            <asp:Button runat="server" Text="Закрыть" class="message-button" ID="messageButtonCloseStatistic" OnClick="messageButtonCloseStatistic_Click"></asp:Button>
                        </div>
                    </div>

                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
