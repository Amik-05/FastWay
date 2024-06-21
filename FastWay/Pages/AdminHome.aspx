<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="FastWay.AdminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet2.css" rel="stylesheet" />
    <title>Главная</title>
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

            <div class="itemsAndFilter" style="width: 100%;">

                <div class="homeCenter">

                    <p class="zagolHome">Администрирование FastWay</p>

                    <p class="podZagolHome">Выберите действие</p>

                    <div class="divWithChoiseButtons">
                        <div style="margin: 0% auto">
                            <asp:Button runat="server" ID="viewingOrdersButton" OnClick="viewingOrdersButton_Click" class="buttonHome" Text="Просмотр заявок" />
                            <asp:Button runat="server" ID="addTarifsButton" OnClick="addTarifsButton_Click" class="buttonHome" Text="Способы перевозки" />
                            <asp:Button runat="server" ID="addDriversMoversButton" OnClick="addDriversMoversButton_Click" class="buttonHome" Text="Персонал" />
                            
                           
                        </div>
                        <div>
                            <asp:Button runat="server" ID="exit" OnClick="exit_Click" class="buttonHome" Text="Выйти из аккаунта" />
                            <asp:Button runat="server" ID="addCars" OnClick="addCars_Click" class="buttonHome" Text="Транспорт" />
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </form>
</body>
</html>

