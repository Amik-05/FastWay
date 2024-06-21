﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Information.aspx.cs" Inherits="FastWay.Information" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>О компании</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script type="text/javascript">
    history.pushState(null, null, location.href);
    window.onpopstate = function () {
        history.go(1);
    };
    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <div class="grid" style="width: 100%; height: 100vh">

            <div class="header" style="grid-area: h; width: 100%;">

                <div class="divPheader">
                    <a href="Home.aspx" style="color: white; text-decoration: none">
                        <p class="pHeader">Главная</p>
                    </a>
                </div>
            </div>

            <div class="itemsAndFilter" style="width: 100%;">

                    <div class="info" style="display: block">
                        <p class="pInfo" style="display: block">
                            FastWay – это не просто компания по грузоперевозкам, это твой персональный спутник в мире быстрой и надежной доставки грузов. Одной из наших ключевых особенностей является скорость. Мы понимаем, что время – это деньги, особенно в мире бизнеса, поэтому мы делаем все возможное для максимально быстрой доставки.
                        <br />
                            Вы хотите отправить груз? На нашей интуитивной платформе Вы сможете легко внести всю информацию о своем грузе, а затем выбрать категорию и подкатегорию, чтобы мы могли максимально точно организовать доставку. Без лишних заморочек и задержек!
                        <br />
                            Но скорость не означает компромисса с безопасностью. Мы придаем огромное значение безопасности доставки и принимаем все необходимые меры для защиты Вашего груза в пути. Наши профессиональные водители и логистические эксперты гарантируют контроль и заботу о каждом грузе, обеспечивая его безопасность во время быстрой доставки.
                        <br />
                            Итак, FastWay не просто ускоряет процесс доставки, но и гарантирует безопасность груза на протяжении всего пути. Мы стремимся к тому, чтобы Вы могли сосредоточиться на своем бизнесе, зная, что груз будет доставлен быстро, безопасно и надежно.
                        </p>
                        <div style="width: 100%; text-align: center; margin-top: 10px">
                            <asp:Button ID="goBack" runat="server" OnClick="goBack_Click" class="button" Text="Закрыть" />
                        </div>
                        

                    </div>
                    
                </div>
        </div>
    </form>
</body>
</html>

