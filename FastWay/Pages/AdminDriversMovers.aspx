<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDriversMovers.aspx.cs" Inherits="FastWay.Pages.AdminDriversMovers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Персонал</title>
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
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="grid" style="width: 100%; height: 100vh">
            <div class="header" style="grid-area: h; width: 100%; border-bottom: 0.1px solid #ffffff59">
                <div style="float: left; margin-top: 0px; display: flex;width:100%">
                    <div style="width: 50%; text-align: left">
                        <div class="divPheader">
                            <a href="AdminHome.aspx" style="color: white; text-decoration: none">
                                <p class="pHeader">Главная</p>
                            </a>
                        </div>
                    </div>
                    <div style="width: 50%; text-align: center">
                        <p class="pHeader" style="font-size: 15pt; margin-top: 10px; margin-left: 20px">Персонал</p>
                    </div>
                    <div style="width: 50%; text-align: right">
                        <div style="float: right; margin-right: 10px; margin-bottom: 5px">
                            <asp:Button runat="server" ID="addPersonal" CssClass="buttonInHeader" OnClick="addPersonal_Click" Style="font-size: 10pt; height: 35px" Text="Добавить" />
                        </div>
                    </div>


                </div>


            </div>
            <div class="itemsAndFilter" style="width: 100%; display: flex">
                <div style="width: 50%; height: auto; border-right: 0.5px solid #ffffff50">
                    <p style="color: white; font-size: 14pt; margin: 5px; margin-left: 40%">Водители</p>
                    <div style="display: flex; flex-wrap: wrap; overflow-wrap: anywhere">
                        <asp:ListView runat="server" ItemType="FastWay.Models.Drivers" ID="listDrivers">
                            <ItemTemplate>
                                <div class="divDrCard">
                                    <div style="display: flex; justify-content: center;">
                                        <div style="width: 100%; margin-left: 10px; margin-right: 10px;">
                                            <p class="pDrCard">Фамилия: <%# Item.LastName %></p>
                                            <p class="pDrCard">Имя: <%# Item.FirstName %></p>
                                            <p class="pDrCard">Отчество: <%# Item.Patronymic %></p>
                                            <p class="pDrCard">Возраст: <%# Item.Age %></p>
                                            <p class="pDrCard">Транспорт: <%# Item.Status %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>

                <div style="width: 50%; height: auto;">
                    <p style="color: white; font-size: 14pt; margin: 5px; margin-left: 40%">Грузчики</p>
                    <div style="display: flex; flex-wrap: wrap; overflow-wrap: anywhere">
                        <asp:ListView runat="server" ItemType="FastWay.Models.Movers" ID="listMovers">
                            <ItemTemplate>
                                <div class="divDrCard">
                                    <div style="display: flex; justify-content: center;">
                                        <div style="width: 100%; margin-left: 10px; margin-right: 10px;">
                                            <p class="pDrCard">Фамилия: <%# Item.LastName %></p>
                                            <p class="pDrCard">Имя: <%# Item.FirstName %></p>
                                            <p class="pDrCard">Отчество: <%# Item.Patronymic %></p>
                                            <p class="pDrCard">Возраст: <%# Item.Age %></p>
                                            <p class="pDrCard">Статус: <%# Item.Status %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>
            </div>
        </div>

    </form>
</body>
</html>
