<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="FastWay.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>
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

            // Разрешить ввод цифр и точки (код ASCII: 48-57 для цифр, 46 для точки)
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Убедитесь, что вводить можно только одну точку
            var input = document.getElementById("<%= txbLength.ClientID %>").value;
            if (charCode === 46 && input.indexOf('.') !== -1) {
                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">
        function isNumberKey1(e1vt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить ввод цифр и точки (код ASCII: 48-57 для цифр, 46 для точки)
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Убедитесь, что вводить можно только одну точку
            var input = document.getElementById("<%= txbWidth.ClientID %>").value;
            if (charCode === 46 && input.indexOf('.') !== -1) {
                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">
        function isNumberKey1(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить ввод цифр и точки (код ASCII: 48-57 для цифр, 46 для точки)
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Убедитесь, что вводить можно только одну точку
            var input = document.getElementById("<%= txbHeight.ClientID %>").value;
            if (charCode === 46 && input.indexOf('.') !== -1) {
                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">
        function isNumberKey1(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить ввод цифр и точки (код ASCII: 48-57 для цифр, 46 для точки)
            if ((charCode < 48 || charCode > 57) && charCode !== 46) {
                return false;
            }

            // Убедитесь, что вводить можно только одну точку
            var input = document.getElementById("<%= txbWeight.ClientID %>").value;
            if (charCode === 46 && input.indexOf('.') !== -1) {
                return false;
            }

            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="grid" style="width: 100%; height: 100vh; vertical-align: central">

            <div class="header" style="grid-area: h; width: 100%;">

                <div class="divPheader">
                    <a href="Home.aspx" style="color: white; text-decoration: underline">
                        <p class="pHeader">Главная</p>
                    </a>
                    <a href="Information.aspx" style="color: white; text-decoration: underline">
                        <p class="pHeader">О компании</p>
                    </a>
                </div>
            </div>

            <div class="itemsAndFilter" style="width: 100%; text-align: center">
                <div class="centerOrder" style="text-align: center">

                    <div class="cs">
                        <div style="width:100%;margin-right:5px">
                            <p class="pCST">Категория</p>
                            <asp:DropDownList Class="combo" ID="comboCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="MyDropDownList_SelectedIndexChanged" />
                        </div>
                        <div  style="width:100%">
                            <p class="pCST">Подкатегория</p>
                            <asp:DropDownList Class="combo" ID="comboSubcategory" AutoPostBack="true" runat="server" />
                        </div>
                    </div>

                    <asp:TextBox ID="txbTitle" placeholder="Наименование" class="title" AutoPostBack="true" runat="server" />

                    <div class="divHWL" >

                        <div class="divInHWL">
                            <div style="margin-right: 10px">
                                <p class="pCST1">Длина</p>
                                <asp:TextBox ID="txbLength" placeholder="м" class="inputHWL" onchange="this.form.submit();" OnTextChanged="txbLenght_TextChanged" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>
                            <div style="margin-right: 10px">
                                <p class="pCST1">Ширина</p>
                                <asp:TextBox ID="txbWidth" placeholder="м" class="inputHWL" onchange="this.form.submit();" OnTextChanged="txbWidth_TextChanged" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>
                            <div style="margin-right: 10px">
                                <p class="pCST1">Высота</p>
                                <asp:TextBox ID="txbHeight" placeholder="м" class="inputHWL" onchange="this.form.submit();" OnTextChanged="txbHeight_TextChanged" runat="server" onkeypress="return isNumberKey1(event)" />
                            </div>
                            <div style="margin-right: 10px; margin-top: 35px">
                                <p class="pCST1">=</p>
                            </div>
                            <div style="margin-right: 30px">
                                <p class="pCST1">Объём</p>
                                <asp:Label ID="txbVolume" class="pCST" runat="server"></asp:Label>
                            </div>
                        </div>
                        
                        <div class="divInHWL">
                            <div style="margin-right: 10px">
                                <p class="pCST1">Кол-во</p>
                                <asp:TextBox ID="txbQuantity" placeholder="шт" onchange="this.form.submit();" CssClass="inputHWL" runat="server" onkeypress="return isNumberKey(event)" OnTextChanged="txbQuantity_TextChanged" />
                            </div>
                            <div style="margin-right: 30px">
                                <p class="pCST1">Вес</p>
                                <asp:TextBox ID="txbWeight" placeholder="кг" onchange="this.form.submit();" class="inputHWL" runat="server" onkeypress="return isNumberKey1(event)" OnTextChanged="txbWeight_TextChanged" />
                            </div>
                            <div style="margin-right: 30px">
                                <p class="pCST1">Общий объём</p>
                                <asp:Label ID="txbOverallVolume" class="pCST" runat="server"></asp:Label>
                            </div>
                            <div style="margin-right: 10px">
                                <p class="pCST1">Общий вес</p>
                                <asp:Label ID="txbTotalWeight" class="pCST" Text=" " runat="server"></asp:Label>
                            </div>
                        </div>
                        
                    </div>
                    <div style="width: 100%; text-align: center; margin-top: 50px">
                        <asp:Button runat="server" OnClick="Unnamed_Click" class="button" Text="Оформить" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
