<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="FastWay.Personal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Обработка персональных данных</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script type="text/javascript">
        function closeTab() {
            window.open('', '_self', ''); window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="grid" style="width: 100%; height: 100vh">
            <div class="itemsAndFilter" style="width: 100%;">
                <div class="info" style="display:block;width:1100px;height:min-content;">
                    <p class="pInfo" style="">
                        Информационная система грузоперевозок «FastWay» в соответствии с Федеральным законом РФ «О персональных данных» от 27.07.2006 г. №152-ФЗ обрабатывает персональные данные Пользователей (имя, фамилия, отчество, мобильный телефон, электронная почта, пароль).
                        <br />
                        Информационная система грузоперевозок «FastWay» собирает, обрабатывает и хранит только ту персональную информацию, которая необходима для регистрации профиля и оформления заявки на перевозку груза.
                        <br />
                        Информация, содержащая персональные данные, разрешенные субъектом персональных данных для распространения, может быть использована исключительно для ознакомления, дальнейшее копирование, распространение и передача этих данных запрещена.
                        <br />
                        Пользователь может в любой момент изменить предоставленную им персональную информацию или ее часть, воспользовавшись функцией редактирования персональной информации в разделе личного кабинета.
                        <br />
                        При оформленнии без регистрации заявки со внесенными данными сохраняются в базе дыннх, которые, после регистрации, автоматически привяжутся к профилю пользователя.
                    </p>
                    <div style="width: 100%; text-align: center; margin-top: 10px">
                        <asp:Button runat="server" ID="close" OnClick="close_Click" class="button" Text="Закрыть" />
                    </div>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>

