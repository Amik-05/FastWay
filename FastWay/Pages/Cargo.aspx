<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cargo.aspx.cs" Inherits="FastWay.Cargo" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Информация о грузе</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить ввод цифр и точки (код ASCII: 48-57 для цифр, 46 для точки)
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Убедитесь, что вводить можно только одну точку
            var input = document.getElementById("<%= txbQuantity.ClientID %>").value;
            if (charCode === 46 && input.indexOf('') !== -1) {
                return false;
            }

            return true;
        }
    </script>

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
    </script>

    <script type="text/javascript">
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
    </script>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%= hiddenEd.ClientID %>').value = 1;
            if (document.getElementById('<%= txbLength.ClientID %>').placeholder == "м") {
                calculateVolume(1);
            }
            if (document.getElementById('<%= txbLength.ClientID %>').placeholder == "см") {
                calculateVolume(2);
            }


        }
        function calculateVolume(ed) {
            if (ed == 1) {
                document.getElementById('<%= txbLength.ClientID %>').placeholder = "м";
                document.getElementById('<%= txbHeight.ClientID %>').placeholder = "м";
                document.getElementById('<%= txbWidth.ClientID %>').placeholder = "м";
                document.getElementById('<%= txbWeight.ClientID %>').placeholder = "кг";
                document.getElementById('<%=hiddenEd.ClientID%>').value = 1;

                var title = document.getElementById('<%= txbTitle.ClientID %>').value;
                var length = parseFloat(document.getElementById('<%= txbLength.ClientID %>').value) || 0;
                var height = parseFloat(document.getElementById('<%= txbHeight.ClientID %>').value) || 0;
                var width = parseFloat(document.getElementById('<%= txbWidth.ClientID %>').value) || 0;
                var quantity = parseFloat(document.getElementById('<%= txbQuantity.ClientID %>').value) || 0;
                var weight = parseFloat(document.getElementById('<%= txbWeight.ClientID %>').value) || 0;

                var volume = length * height * width;
                document.getElementById('<%= txbVolume.ClientID %>').value = volume + " м3";
                document.getElementById('<%= hiddenVolume.ClientID %>').value = volume;

                var ov = (volume * quantity);
                document.getElementById('<%= txbOverallVolume.ClientID %>').value = ov + " м3";
                document.getElementById('<%= hiddenOverallVolume.ClientID %>').value = ov;

                var tw = weight * quantity;
                document.getElementById('<%= txbTotalWeight.ClientID %>').value = tw + " кг";
                document.getElementById('<%= hiddenTotalWeight.ClientID %>').value = tw;
                var t = title;
                document.getElementById('<%= hiddenTitle.ClientID %>').value = t;
            }
            if (ed == 2) {
                document.getElementById('<%= txbLength.ClientID %>').placeholder = "см";
                document.getElementById('<%= txbHeight.ClientID %>').placeholder = "см";
                document.getElementById('<%= txbWidth.ClientID %>').placeholder = "см";
                document.getElementById('<%= txbWeight.ClientID %>').placeholder = "г";
                document.getElementById('<%=hiddenEd.ClientID%>').value = 2;

                var title = document.getElementById('<%= txbTitle.ClientID %>').value;
                var length = parseFloat(document.getElementById('<%= txbLength.ClientID %>').value) || 0;
                var height = parseFloat(document.getElementById('<%= txbHeight.ClientID %>').value) || 0;
                var width = parseFloat(document.getElementById('<%= txbWidth.ClientID %>').value) || 0;
                var quantity = parseFloat(document.getElementById('<%= txbQuantity.ClientID %>').value) || 0;
                var weight = parseFloat(document.getElementById('<%= txbWeight.ClientID %>').value) || 0;


                var volume = length * height * width;
                document.getElementById('<%= txbVolume.ClientID %>').value = volume + " см3";
                document.getElementById('<%= hiddenVolume.ClientID %>').value = volume;

                var ov = (volume * quantity);
                document.getElementById('<%= txbOverallVolume.ClientID %>').value = ov + " см3";
                document.getElementById('<%= hiddenOverallVolume.ClientID %>').value = ov;

                var tw = weight * quantity;
                document.getElementById('<%= txbTotalWeight.ClientID %>').value = tw + " г";
                document.getElementById('<%= hiddenTotalWeight.ClientID %>').value = tw;
                var t = title;
                document.getElementById('<%= hiddenTitle.ClientID %>').value = t;
            }
        }

        function sendDataToServer() {
            var overallVolume = document.getElementById('<%= hiddenOverallVolume.ClientID %>').value;
            __doPostBack('<%= apply.ClientID %>', overallVolume);
            var totalWeight = document.getElementById('<%= hiddenTotalWeight.ClientID %>').value;
            __doPostBack('<%= apply.ClientID %>', totalWeight);
            var title = document.getElementById('<%= hiddenTitle.ClientID %>').value;
            __doPostBack('<%= apply.ClientID %>', title);
        }


    </script>

    <link href="StyleSheet1.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hiddenTitle" runat="server" />
        <asp:HiddenField ID="hiddenVolume" runat="server" />
        <asp:HiddenField ID="hiddenOverallVolume" runat="server" />
        <asp:HiddenField ID="hiddenTotalWeight" runat="server" />
        <asp:HiddenField ID="hiddenEd" runat="server" />

        <div class="grid" style="width: 100%; height: 100vh; vertical-align: central">

            <div class="header" style="grid-area: h; width: 100%; display:flex; ">
                <div style="width: 50%; text-align: left">
                    <div class="divPheader">
                        <a href="Home.aspx" style="color: white; text-decoration: none">
                            <p class="pHeader">Главная</p>
                        </a>
                    </div>
                </div>
                <div style="width: 50%; text-align: center">
                    <p class="pHeader" style="font-size: 15pt;margin-top:10px">Заполнение информации о грузе</p>
                </div>
                <div style="width: 50%; text-align: right">
                    <div style="float: right; margin-right: 10px">
                        <asp:Button runat="server" ID="vhod" CssClass="buttonInHeader" Text="Войти" OnClick="v_Click" />
                        <asp:Button runat="server" ID="profile" CssClass="buttonInHeader" Text="Профиль" OnClick="p_Click" />
                    </div>
                </div>
                
               

            </div>

            <div class="itemsAndFilter">
                <div style="width: 100%; height: 60vh; position: absolute; top: 0; bottom: 0; left: 0; right: 0; margin: auto;">
                    <div class="centerCargo">
                        <div style="margin-bottom: -10px; margin-top: -15px">
                            <div style="display: flex; grid-area: ed; width: 310px; float: right">
                                <div style="margin-top: 4.5px;">
                                    <asp:Label runat="server" Text='Единицы измерения' CssClass="pProfileRequest" Style="text-shadow: rgb(0,0,0 1) 0px 0px 10px 10px" />
                                </div>
                                <div style="margin-top: 0px; margin-right: 5px">
                                    <asp:Button ID="mkg" Text="м/кг" runat="server" class="picFilter" OnClientClick="calculateVolume(1); return false;" />
                                </div>
                                <div style="margin-top: 0px; margin-right: 5px">
                                    <asp:Button ID="smg" Text="см/г" runat="server" class="picFilter" OnClientClick="calculateVolume(2); return false;" />
                                </div>
                            </div>
                        </div>


                        <div class="cs">
                            <div style="width: 100%; margin-right: 5px">
                                <p class="pCST" style="margin-left: 15px">Категория</p>
                                <asp:DropDownList Class="comboCategory" ID="comboCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboCategory_SelectedIndexChanged" />
                            </div>
                            <div style="width: 100%">
                                <p class="pCST" style="margin-left: 15px">Подкатегория</p>
                                <asp:DropDownList Class="comboCategory" ID="comboSubcategory" AutoPostBack="true" runat="server" />
                            </div>
                        </div>

                        <asp:TextBox ID="txbTitle" data-required="true" placeholder="Наименование" class="title" runat="server" />

                        <div class="divHWL">
                            <div class="divInHWL">
                                <p class="pHWL">Длина</p>
                                <asp:TextBox ID="txbLength" data-required="true" placeholder="м" class="inputHWL" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>
                            <div class="divInHWL">
                                <p class="pHWL">Ширина</p>
                                <asp:TextBox ID="txbWidth" data-required="true" placeholder="м" class="inputHWL" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>
                            <div class="divInHWL">
                                <p class="pHWL">Высота</p>
                                <asp:TextBox ID="txbHeight" data-required="true" placeholder="м" class="inputHWL" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>

                            <div class="divInHWL">
                                <p class="pHWL">Кол-во</p>
                                <asp:TextBox ID="txbQuantity" data-required="true" placeholder="шт" CssClass="inputHWL" runat="server" onkeypress="return isNumberKey(event)" />
                            </div>
                            <div class="divInHWL">
                                <p class="pHWL">Вес</p>
                                <asp:TextBox ID="txbWeight" data-required="true" placeholder="кг" class="inputHWL" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>

                            <div class="divInHWL">
                                <p class="pHWL">Объём</p>
                                <asp:TextBox ID="txbVolume" data-required="true" CssClass="inputHWL1" runat="server"></asp:TextBox>
                            </div>
                            <div class="divInHWL">
                                <p class="pHWL">Общий объём</p>
                                <asp:TextBox ID="txbOverallVolume" data-required="true" CssClass="inputHWL1" runat="server"></asp:TextBox>
                            </div>
                            <div class="divInHWL">
                                <p class="pHWL">Общий вес</p>
                                <asp:TextBox ID="txbTotalWeight" data-required="true" CssClass="inputHWL1" runat="server"></asp:TextBox>
                            </div>
                            <script type="text/javascript">
                                //Обработчики oninput для каждого текстового поля
                                document.getElementById('<%= txbTitle.ClientID %>').addEventListener('input', function () { calculateVolume(parseFloat(document.getElementById('<%= hiddenEd.ClientID %>').value)); });
                                document.getElementById('<%= txbLength.ClientID %>').addEventListener('input', function () { calculateVolume(parseFloat(document.getElementById('<%= hiddenEd.ClientID %>').value)); });
                                document.getElementById('<%= txbHeight.ClientID %>').addEventListener('input', function () { calculateVolume(parseFloat(document.getElementById('<%= hiddenEd.ClientID %>').value)); });
                                document.getElementById('<%= txbWidth.ClientID %>').addEventListener('input', function () { calculateVolume(parseFloat(document.getElementById('<%= hiddenEd.ClientID %>').value)); });
                                document.getElementById('<%= txbQuantity.ClientID %>').addEventListener('input', function () { calculateVolume(parseFloat(document.getElementById('<%= hiddenEd.ClientID %>').value)); });
                                document.getElementById('<%= txbWeight.ClientID %>').addEventListener('input', function () { calculateVolume(parseFloat(document.getElementById('<%= hiddenEd.ClientID %>').value)); });
                            </script>
                        </div>

                        <div style="display: flex; justify-content: center; background-color: red; height: 0px">
                            <asp:Label runat="server" ID="errorLabelEmpty" Style="margin-top: 15px; font-size: 13pt; margin-right: 5px" Text="Пустые поля!" class="errorLabel" Visible="false" />
                            <asp:Label runat="server" ID="errorLabelNonRight" Style="margin-top: 15px; font-size: 13pt; margin-right: 5px" Text="Неверно заполненные данные!" class="errorLabel" Visible="false" />
                            <asp:Label runat="server" ID="errorMaxVolume" Style="margin-top: 15px; font-size: 13pt; margin-right: 5px" class="errorLabel" Visible="false" />

                        </div>


                    </div>
                    <div style="width: 100%; text-align: center; margin-top: 10px">
                        <asp:Button runat="server" ID="apply" OnClientClick="sendDataToServer(); return false;" OnClick="apply_Click" class="button" Text="Оформить" />
                    </div>


                </div>
            </div>
        </div>
    </form>
</body>
</html>
