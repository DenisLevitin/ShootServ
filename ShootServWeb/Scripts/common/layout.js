 //функция вызова ошибки
function showError(text) {
    // notImplemented
}

function showInfo(text) {
    // notImplemented
}

function redirectToLoginPage(returnUrl) 
{
    if (confirm("Для данного действия требуется выполнить вход. Перейти на страницу авторизации?")) {
        window.location = window.linkslogin + "?ReturnUrl=" + returnUrl;
    }
}