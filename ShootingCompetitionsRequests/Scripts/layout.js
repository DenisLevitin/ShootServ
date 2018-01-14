
//функция вызова ошибки

function ShowError(text) {
    $("#divShowError").text(text)
    $("#InfoError").animate({ "opacity": "100%" }, "slow");
    setTimeout(function () {
        $("#InfoError").animate({ "opacity": "0" }, "slow")
    }, 5000);
}

function ShowInfo(text) {
    $("#ShowInfo").text(text);
}

// Перевод на страницу логина
function RedirectLoginPage(returnUrl) {
    if (confirm("Для данного действия требуется выполнить вход. Перейти на страницу авторизации?")) {
        window.location = $("#newAccountUrl").val() + "?ReturnUrl=" + returnUrl;
    }
}

//скролл
function scroll(div) {
    var topcoords = $(div).offset().top;
    window.scrollTo(0, topcoords);
}

// Добавление стрелок на все дивы - кнопки с выпадающим контентом
function AddSumbol(div, isUp, text) {
    var a = isUp ? "&#8659;" : "&#8657;"
    var text = a + a + a + text + a + a + a;
    div.html(text);
}

//на весь выпадоющий контент
function togetherContent(button, content, text) {
    $(document).on("click", button, function () {
        if ($(content).is(":hidden")) {
            $(content).slideDown();
            AddSumbol($(button), false, text);
            $(button).addClass("light");
            scroll(button);
        }
        else {
            $(content).slideUp();
            AddSumbol($(button), true, text);
            $(button).removeClass("light");
        }
    });
}

// Форматируем дату для правильной передачи в контроллер
function FormatDate(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
}

$(document).ready(function () {

    //  Делаем кликабельные блоки в меню
    $(document).on("click", ".mainMenu div", function () {
        window.location = $(this).find("a").attr("href"); return false;
    });

    //закрываем showInfo
    $(document).on("click", "#closeShowInfo", function () {
        $("#InfoError").animate({ "opacity": "0" }, "slow");
    });
});

var hellopreloader = document.getElementById("hellopreloader_preload");
function fadeOutnojquery(el) {
    el.style.opacity = 1;
    var interhellopreloader = setInterval(function () {
        el.style.opacity = el.style.opacity - 0.05;
        if (el.style.opacity <= 0.05) {
            clearInterval(interhellopreloader);
            hellopreloader.style.display = "none";
        }
    }, 16);
}
window.onload = function () {
    setTimeout(function () {
        fadeOutnojquery(hellopreloader);
    }, 1000);
};