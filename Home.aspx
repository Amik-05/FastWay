<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FastWay.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>
     <style>
        footer {
            background-color: #333; /* Темно-серый цвет */
            color: white; /* Белый текст */
            text-align: left; /* Выравнивание текста по центру */

        }

        header {
            background-color: #333; /* Темно-серый цвет */
            color: white; /* Белый текст */
            text-align: left; /* Выравнивание текста по центру */
            height: 5vh; /* Высота футера */
            line-height: 20px; /* Центрирование текста по вертикали */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="grid" style="width: 100vh">

            <header style="grid-area: h; width: 200vh">
                <div style="margin-top: -7px; margin-left: 30px">
                    <p style="color: white; font-size: 18pt">Магазин электроники Chikidzhaga</p>
                </div>
            </header>

            <div class="itemsAndFilter" style="width:200vh; height:90vh">
                

            </div>

            <div class="footer" style="">
                <div style="display: flex; margin-top: 0px; margin-left: 35px">
                    <p style="color: white; font-size: 15pt; margin-right: 15px">О нас</p>
                    <p style="color: white; font-size: 15pt; margin-right: 15px"">Связаться</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
