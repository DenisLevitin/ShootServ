 //функция вызова ошибки
function showError(text) {
    alert(text);
}

function showInfo(text) {
    alert(text);
}

function redirectToLoginPage(returnUrl) 
{
    if (confirm("Для данного действия требуется выполнить вход. Перейти на страницу авторизации?")) {
        window.location = window.linkslogin + "?ReturnUrl=" + returnUrl;
    }
}