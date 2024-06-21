<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="FastWay.Order" Async="true" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title>Оформление</title>
    <link rel="icon" href="..\PIC\main.png" />
    <script src="https://mapgl.2gis.com/api/js/v1"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/5.0.7/jquery.inputmask.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/locale/ru.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%= txtDateTime.ClientID %>').datetimepicker({
                locale: 'ru',
                format: 'DD.MM.YYYY HH:mm'
            });

            $('#<%= txtDateTime.ClientID %>').on('dp.change', function (e) {
                $(this).val(e.date.format('DD.MM.YYYY HH:mm'));
                updateDateTime();
            });
            updateDateTime();
        });
    </script>
    <script type="text/javascript">
        function isAlphabetic(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            // Разрешить буквы латинского и кириллического алфавита
            if (!(charCode >= 1040 && charCode <= 1103) && charCode != 1105 && charCode != 1025) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
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
        window.onload = function () {
            var delivery = document.getElementById('<%=txbCostForDelivery.ClientID%>').value;
            var distance1 = document.getElementById('<%=txbDistance.ClientID%>').value;
            var dd = delivery * distance1;
            document.getElementById('<%=txbSummaryCost.ClientID%>').value = dd.toString();

        }
        function sendDataToServer() {
            var hiddenDate = document.getElementById('<%= hiddenDate.ClientID %>').value;
            __doPostBack('<%= apply.ClientID %>', hiddenDate);
        }


    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/5.0.6/jquery.inputmask.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#phoneTxb').inputmask('+7(999)999-99-99');
        });
    </script>

</head>
<body >
    
    <form id="form1" runat="server">
        <asp:HiddenField ID="hiddenDate" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="grid" style="width: 100%; height: 100vh">
            
                    <div class="header" style="grid-area: h; width: 100%; display: flex">
                        <div style="width: 50%; text-align: left">
                            <div class="divPheader">
                                <a href="Home.aspx" style="color: white; text-decoration: none">
                                    <p class="pHeader">Главная</p>
                                </a>
                            </div>
                        </div>
                        <div style="width: 50%; text-align: center">
                            <p class="pHeader" style="font-size: 15pt; margin-top: 10px">Оформление перевозки</p>
                        </div>
                        <div style="width: 50%; text-align: right">
                            <div style="float: right; margin-right: 10px">
                                <asp:Button runat="server" ID="vhod" CssClass="buttonInHeader" Text="Войти" OnClick="v_Click" />
                                <asp:Button runat="server" ID="profile" CssClass="buttonInHeader" Text="Профиль" OnClick="p_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="itemsAndFilter" style="width: 100%;">
                        <div style="width: 100%; padding-top: 40px; height: min-content; position: absolute; top: 0; bottom: 0; left: 0; right: 0; margin: auto;">
                            <div class="centerOrder" style="text-align: center">
                                <div style="display: grid; background-color: transparent; height: 0px; margin-bottom: 10px">
                                    <asp:Label runat="server" ID="errorLabelEmpty" Style="margin-top: -15px" Text="Пустые поля!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorLabelEmail" Style="margin-top: -15px" Text="Некорректный формат почты!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorLabelPers" Style="margin-top: -15px" Text="Примите соглашение на обработку персональных данных!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorLabelCombo" Style="margin-top: -15px" Text="Не выбран способ перевозки!" class="errorLabel" Visible="false" />
                                    <asp:Label runat="server" ID="errorDateSend" Style="margin-top: -15px" Text="Некорректная дата отправки!" class="errorLabel" Visible="false" />
                                </div>
                                <div class="divOrder">
                                    <asp:TextBox ID="lastNameTxb" data-required="true" placeholder="Фамилия" class="txbOrder" runat="server" onkeypress="return isAlphabetic(event)" />
                                    <asp:TextBox ID="firstNameTxb" data-required="true" placeholder="Имя" class="txbOrder" runat="server" onkeypress="return isAlphabetic(event)" />
                                    <asp:TextBox ID="patronymicTxb" data-required="true" placeholder="Отчество" class="txbOrder" runat="server" onkeypress="return isAlphabetic(event)" />
                                </div>
                                <div class="divOrder">
                                    <asp:TextBox ID="phoneTxb" data-required="true" placeholder="+7(___)___-__-__" class="txbOrder" runat="server" />
                                    <asp:TextBox ID="emailTxb" data-required="true" placeholder="Электронная почта" class="txbOrder" runat="server" />

                                </div>
                                <div class="divOrder">
                                    <div style="width: 620px; margin: 5px 10px 5px 5px;">
                                        <asp:TextBox Class="txbOrder" ID="txbFromAddress" Style="margin: 0px" data-required="true" placeholder="Откуда" runat="server" />
                                        <div id="suggestions" class="sugg"></div>
                                    </div>
                                    <div style="width: 317px; display: flex;">
                                        <div style="width: 104px;">
                                            <asp:TextBox ID="padikFromAddress" placeholder="Подъезд" onkeypress="return isNumeric(event)" Style="font-size: 13pt; width: 90%; text-indent: 0px; text-align: center" data-required="true" class="txbOrder" runat="server" />
                                        </div>
                                        <div style="width: 104px">
                                            <asp:TextBox ID="stageFromAddress" placeholder="Этаж" onkeypress="return isNumeric(event)" Style="font-size: 13pt; text-indent: 0px; width: 90%; text-align: center" data-required="true" class="txbOrder" runat="server" />
                                        </div>
                                        <div style="width: 103px">
                                            <asp:TextBox ID="kvartFromAddress" placeholder="Квартира" onkeypress="return isNumeric(event)" Style="font-size: 13pt; text-indent: 0px; width: 90%; text-align: center" data-required="true" class="txbOrder" runat="server" />
                                        </div>

                                    </div>
                                </div>
                                <script type="text/javascript">
                                    document.getElementById('<%=txbFromAddress.ClientID %>').addEventListener('input', function () {
                                        searchAddresses(this.value, 1);
                                    });
                                    document.getElementById('<%=txbToAddress.ClientID %>').addEventListener('input', function () {
                                        searchAddresses(this.value, 2);
                                    });

                                </script>
                                <div class="divOrder">
                                    <div style="width: 620px; margin: 5px 10px 5px 5px;">
                                        <asp:TextBox Class="txbOrder" ID="txbToAddress" Style="margin: 0px" data-required="true" placeholder="Куда" runat="server" />
                                        <div id="suggestions1" class="sugg"></div>
                                    </div>
                                    <div style="width: 317px; display: flex;">
                                        <div style="width: 104px;">
                                            <asp:TextBox ID="padikToAddress" placeholder="Подъезд" onkeypress="return isNumeric(event)" Style="font-size: 13pt; width: 90%; text-indent: 0px; text-align: center" data-required="true" class="txbOrder" runat="server" />
                                        </div>
                                        <div style="width: 104px">
                                            <asp:TextBox ID="stageToAddress" placeholder="Этаж" onkeypress="return isNumeric(event)" Style="font-size: 13pt; text-indent: 0px; width: 90%; text-align: center" data-required="true" class="txbOrder" runat="server" />
                                        </div>
                                        <div style="width: 103px">
                                            <asp:TextBox ID="kvartToAddress" placeholder="Квартира" onkeypress="return isNumeric(event)" Style="font-size: 13pt; text-indent: 0px; width: 90%; text-align: center" data-required="true" class="txbOrder" runat="server" />
                                        </div>

                                    </div>
                                </div>
                                <script type="text/javascript">
                                    document.getElementById('<%=txbFromAddress.ClientID %>').addEventListener('input', function () {
                                        searchAddresses(this.value, 1);
                                    });
                                    document.getElementById('<%=txbToAddress.ClientID %>').addEventListener('input', function () {
                                        searchAddresses(this.value, 2);
                                    });

                                </script>
                                <div class="divOrder">
                                    <div style="width: 650px; margin: 5px 0px 5px 5px;">
                                        <p class="pOrder" style="margin-left: 20px; font-size: 13pt;">Способ перевозки</p>

                                        <asp:DropDownList Class="combo" ID="comboDeliveryType" Style="height: 33px; width: 98.8%; font-size: 13pt; margin-left: 0px; border-radius: 10px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboDeliveryType_SelectedIndexChanged" />


                                    </div>
                                    <div style="width: 316px; display: flex; margin: 5px 0px 5px 0px;">
                                        <div style="width: 158px;">
                                            <asp:Label class="pOrder" Style="font-size: 13pt;" Text="Тариф (руб/км)" runat="server"></asp:Label>
                                            <asp:TextBox ID="txbCostForDelivery" ToolTip="Стоимость тарифа в рублях за километр" Style="width: 90%; font-size: 13pt; height: 33px; text-indent: 0px; text-align: center" data-required="true" placeholder="Стоимость тарифа" class="txbOrder" runat="server" />
                                        </div>
                                        <div style="width: 158px;">
                                            <asp:Label class="pOrder" Style="text-align: left; font-size: 13pt;" Text="Стоимость (руб)" runat="server"></asp:Label>
                                            <asp:TextBox ID="txbSummaryCost" Style="width: 90%; text-indent: 0px; height: 33px; text-align: center; font-size: 13pt;" ToolTip="Финальная стоимость перевозки в рублях" data-required="true" class="txbOrder" runat="server" />
                                        </div>

                                    </div>
                                </div>

                                <div style="width: 100%; justify-content: left; margin-bottom: 0px; display: flex">
                                    <p class="pOrder" style="margin-left: 20px; font-size: 13pt; width: 15%; margin-top: 4px">Дата погрузки</p>
                                    <div class="container" style="width: 25%; height: 35px;">
                                        <div class='col-md-5' style="margin-left: -22px">
                                            <div class="form-group">
                                                <div class='input-group date' id='datetimepicker1'>
                                                    <asp:TextBox ID="txtDateTime" onchange="updateDateTime(); " placeholder="Выберите дату" data-required="true" runat="server" CssClass="form-control" class="txbDateOrder"
                                                        Style="width: 200px; height: 33px; text-align: center; background-color: transparent; color: white; border: 1px solid white; font-weight: 600; font-size: 13pt; border-radius: 10px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label class="pOrder" AutoPostBack="true" Style="width: 59%; font-size: 13pt; margin-top: 4px" runat="server" data-required="true" ID="deliveryDate" />
                                </div>
                                <div class="divOrder" style="margin-left: 15px;">
                                    <asp:CheckBox runat="server" ID="checkAgree" class="checkPersonal" data-required="true" />
                                    <a href="Personal.aspx" target="_blank" style="color: white; text-decoration: underline; margin-left: 5px; margin-top: 5px">
                                        <p class="pPersonal">Я согласен(-а) с обработкой персональных данных</p>
                                    </a>
                                </div>
                                <div class="divOrder" style="margin-left: 15px; height: 27px">
                                    <asp:CheckBox runat="server" ID="checkMovers" class="checkPersonal" data-required="true" />
                                    <asp:Label runat="server" Text="Нет подъема" href="Personal.aspx" ToolTip="Погрузка без грузчиков" target="_blank" Style="color: white; cursor: default; font-size: 13pt; margin-left: 5px; margin-top: 3px" />
                                </div>

                            </div>
                            <div style="width: 100%; text-align: center; margin-top: 10px">
                                <asp:Button runat="server" ID="apply" class="button" OnClick="apply_Click" OnClientClick="sendDataToServer(); return false;" Text="Отправить заявку" />
                            </div>
                        </div>
                    </div>
            <asp:TextBox runat="server" ID="txbDistance" Style="display: none" />
            <asp:TextBox runat="server" ID="txbDuration" data-required="true" AutoPostBack="true" Style="display: none" />
        </div>




        <script type="text/javascript">
            function searchAddresses(query, ver) {
                const apiKey = "7efaffe4-657c-430a-b79c-0bc955078b23"; // Ваш API ключ
                const regionId = "8"; // Идентификатор Астрахани
                if (query.length > 0) { // Начинаем поиск после ввода двух символов
                    fetch(`https://catalog.api.2gis.com/3.0/suggests?q=${query}&region_id=8&key=7efaffe4-657c-430a-b79c-0bc955078b23&suggest_type=route_endpoint`)
                        .then(response => response.json())
                        .then(data => {
                            let suggestions = data.result.items.map(item => item.full_name);
                            displaySuggestions(suggestions, ver);

                        })
                        .catch(error => console.error('Error fetching data:', error));
                } else {
                    document.getElementById('suggestions').innerHTML = "";
                    document.getElementById('suggestions1').innerHTML = "";
                }
            }

            function displaySuggestions(suggestions, ver) {
                if (ver === 1) {
                    let suggestionBox = document.getElementById("suggestions");
                    suggestionBox.innerHTML = "";

                    suggestions.forEach(suggestion => {
                        let suggestionItem = document.createElement("div");
                        suggestionItem.textContent = suggestion;
                        suggestionItem.onclick = function () {
                            document.getElementById('<%= txbFromAddress.ClientID %>').value = suggestion;
                                suggestionBox.innerHTML = "";
                                getCoordinatesAndRoute();
                                updateDateTime();

                            };
                            suggestionBox.appendChild(suggestionItem);
                        });
                    }
                    else {
                        let suggestionBox = document.getElementById("suggestions1");
                        suggestionBox.innerHTML = "";

                        suggestions.forEach(suggestion => {
                            let suggestionItem = document.createElement("div");
                            suggestionItem.textContent = suggestion;
                            suggestionItem.onclick = function () {
                                document.getElementById('<%= txbToAddress.ClientID %>').value = suggestion;
                                suggestionBox.innerHTML = "";
                                getCoordinatesAndRoute();
                                updateDateTime();
                            };
                            suggestionBox.appendChild(suggestionItem);
                        });
                }

            }

            const apiKey = "7efaffe4-657c-430a-b79c-0bc955078b23";

            async function getCoordinates(address) {
                const url = `https://catalog.api.2gis.com/3.0/items/geocode?q=${encodeURIComponent(address)}&fields=items.point&key=${apiKey}`;
                const response = await fetch(url);
                if (response.ok) {
                    const data = await response.json();
                    if (data.result.items.length > 0) {
                        const point = data.result.items[0].point;
                        return { lat: point.lat, lon: point.lon };
                    }
                }
                throw new Error('Failed to get coordinates');
            }

            async function getRoute(fromCoords, toCoords) {
                const url = `https://catalog.api.2gis.ru/carrouting/6.0.1/global?key=${apiKey}`;
                const body = {
                    filter: [],
                    locale: "ru",
                    points: [
                        { x: fromCoords.lon, y: fromCoords.lat, type: "stop" },
                        { x: toCoords.lon, y: toCoords.lat, type: "stop" }
                    ],
                    type: "jam",
                    utc: Math.floor(Date.now() / 1000)
                };
                const response = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(body)
                });
                if (response.ok) {
                    const result = await response.json();
                    if (result.result && result.result[0]) {
                        const distance = Math.round(result.result[0].total_distance / 1000); // расстояние в километрах
                        const duration = Math.round(result.result[0].total_duration / 60); // время в минутах
                        document.getElementById('<%= txbDistance.ClientID %>').value = distance.toString();
                            document.getElementById('<%= txbDuration.ClientID %>').value = duration.toString();
                            var delivery = document.getElementById('<%=txbCostForDelivery.ClientID%>').value;
                            var distance1 = document.getElementById('<%=txbDistance.ClientID%>').value;
                            var dd = delivery * distance1;
                            document.getElementById('<%=txbSummaryCost.ClientID%>').value = dd.toString();
                        }
                    } else {
                        document.getElementById('<%=txbSummaryCost.ClientID%>').value = '-200';
                }
            }

            async function getCoordinatesAndRoute() {
                const fromAddress = document.getElementById('<%= txbFromAddress.ClientID %>').value;
                    const toAddress = document.getElementById('<%= txbToAddress.ClientID %>').value;
                try {
                    const fromCoords = await getCoordinates(fromAddress);
                    const toCoords = await getCoordinates(toAddress);
                    await getRoute(fromCoords, toCoords);
                    updateDateTime();
                } catch (error) {
                    console.error(error.message);
                }
            }

            function searchAddresses(query, ver) {
                if (query.length > 0) {
                    fetch(`https://catalog.api.2gis.com/3.0/suggests?q=${query}&region_id=8&key=${apiKey}&suggest_type=route_endpoint`)
                        .then(response => response.json())
                        .then(data => {
                            let suggestions = data.result.items.map(item => item.full_name);
                            displaySuggestions(suggestions, ver);
                            updateDateTime();
                        })
                        .catch(error => console.error('Error fetching data:', error));
                } else {
                    document.getElementById('suggestions').innerHTML = "";
                    document.getElementById('suggestions1').innerHTML = "";
                }
            }

            function displaySuggestions(suggestions, ver) {
                if (ver === 1) {
                    let suggestionBox = document.getElementById("suggestions");
                    suggestionBox.innerHTML = "";

                    suggestions.forEach(suggestion => {
                        let suggestionItem = document.createElement("div");
                        suggestionItem.textContent = suggestion;
                        suggestionItem.onclick = function () {
                            document.getElementById('<%= txbFromAddress.ClientID %>').value = suggestion;
                                suggestionBox.innerHTML = "";
                                getCoordinatesAndRoute(); // Вызов функции при выборе предложения
                                updateDateTime();
                            };
                            suggestionBox.appendChild(suggestionItem);
                        });
                    }
                    else {
                        let suggestionBox = document.getElementById("suggestions1");
                        suggestionBox.innerHTML = "";

                        suggestions.forEach(suggestion => {
                            let suggestionItem = document.createElement("div");
                            suggestionItem.textContent = suggestion;
                            suggestionItem.onclick = function () {
                                document.getElementById('<%= txbToAddress.ClientID %>').value = suggestion;
                            suggestionBox.innerHTML = "";
                            getCoordinatesAndRoute(); // Вызов функции при выборе предложения
                            updateDateTime();
                        };
                        suggestionBox.appendChild(suggestionItem);
                    });
                }
            }

            function formatDate(date) {
                const day = String(date.getDate()).padStart(2, '0');
                const month = String(date.getMonth() + 1).padStart(2, '0');
                const year = date.getFullYear();
                const hours = String(date.getHours()).padStart(2, '0');
                const minutes = String(date.getMinutes()).padStart(2, '0');

                return `${day}.${month}.${year} ${hours}:${minutes}`;
            }

            function updateDateTime() {
                const dateString = document.getElementById('<%= txtDateTime.ClientID %>').value;
                const time = parseInt(document.getElementById('<%= txbDuration.ClientID %>').value, 10);
                if (document.getElementById('<%= txbDuration.ClientID %>').value.toString() !== "") {
                    const [datePart, timePart] = dateString.split(' ');
                    const [day, month, year] = datePart.split('.').map(Number);
                    const [hours, minutes] = timePart.split(':').map(Number);
                    const initialDate = new Date(year, month - 1, day, hours, minutes);
                    initialDate.setMinutes(initialDate.getMinutes() + time);
                    const finalDateString = formatDate(initialDate);
                    document.getElementById('<%= deliveryDate.ClientID %>').textContent = 'Примерная дата прибытия: ' + finalDateString;
                    document.getElementById('<%= hiddenDate.ClientID %>').value = finalDateString;
                }
            }
        </script>
    </form>
   
</body>
</html>
