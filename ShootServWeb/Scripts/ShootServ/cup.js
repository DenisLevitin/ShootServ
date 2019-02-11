$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional['ru']);
    $(".datepicker").datepicker();
    
    changeRegion();

    $(document).on("change", "#CountryFilter", function() {
        changeCountryCupFilter();
    });

    $(document).on("change", "#IdCountry", function() {
        changeCountry();
    });

    $(document).on("change", "#IdRegion", function() {
        changeRegion();
    });

    $(document).on("click", "#Show", function () {
        var dateFromDate = $("#DateFrom").datepicker("getDate");
        var dateToDate = $("#DateTo").datepicker("getDate");
        var dateFrom = dateFromDate && dateFromDate ? dateFromDate.toISOString() : new Date(1970,1,1).toISOString();
        var dateTo = dateToDate && dateToDate ? dateToDate.toISOString() : new Date(2100, 1, 1).toISOString();
        var regionId = $("#RegionFilter").val();

        getCupsList(regionId, dateFrom, dateTo);
    });

    $(document).on("click", "#addBt", function() {
        if (isAuthorize) {
            addCup();
        }
        else
        {
            redirectToLoginPage(linksCup.CupIndex);
        }
    });

    // Клик на кнопке редактировать
    $(document).on("click", "#editBt", function() {
        if (isAuthorize) {
            editCup();
        }
        else
        {
            redirectToLoginPage(linksCup.CupIndex);
        }
    });

    // Клик на ссылке Ред.
    $(document).on("click", ".editCup", function() {
        var a = $(this);
        var idCup = a.attr("idcup");
        window.location = linksCup.CupIndex + "?idCup=" + idCup;
    });

    $(document).on("click", ".delCup", function () {
        if (isAuthorize) {

            var a = $(this);
            var id = a.attr("idcup");

            $.ajax({
                method:"POST",
                url: linksCup.Delete,
                dataType: "JSON",
                data: { idCup: id },
                async: false,
                success: function (data) {
                    if (data.IsOk) {

                        var tr = a.closest("tr");
                        $(tr).remove();

                    } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                },
                error: function (data) {
                    showError("Ошибка ajax");
                }
            });
        }
        else
        {
            redirectToLoginPage(linksCup.CupIndex);
        }
    });

    togetherContent("#divAddCup", "#divAddHidden", 'Добавить соревнование');
    togetherContent("#divListCup", "#rightDiv", 'Список соревнований');

    addSumbol($("#divAddCup"), true, 'Добавить соревнование');
    addSumbol($("#divListCup"), true, 'Список соревнований');
});

// Получить список регионов по стране ( это много где копируется )
function getRegions(idCountry, element, tagName) {
    $.ajax({
        url: linksCommon.GetRegionsByCountry,
        dataType: "html",
        type: "GET",
        async: false,
        data: {
            idCountry: idCountry,
            tagName: tagName,
            addAll : true
        },
        success: function (data) {
            $(element).html(data);
            changeRegion();
        },
        error: function () {
            showError("Ошибка ajax");
        }
    });
}

// Валидация ввода
function validateInput() {

    if (!$("#Name").val()) {
        showError("Не введено название соревнования");
        return false;
    }

    if (!$("#IdCupType").val()) {
        showError("Не введен тип соревнования");
        return false;
    }

    if (!$("#IdShootingRange").val()) {
        showError("Не введен тир, где будет проходить соревнование");
        return false;
    }

    var dateStart = $("#DateStart").datepicker("getDate");
    var dateEnd = $("#DateEnd").datepicker("getDate");

    if (dateStart > dateEnd) {
        showError("Дата начала соревнования не может быть позже даты окончания");
        return false;
    }

    if (!$("#IdShootingRange").val()) {
        showError("Не введен тир, где будет проходить соревнование");
        return false;
    }

    return true;
}

// Получить в формате Json добавляемый список упражнений
function getCompetitionsJson() {

    var trList = $("#tableCompList tr");

    var tmp = [];
    var j = 0;

    trList.each( function(i, element) {

        var checked = $(element).find(":checked");
        if (checked && checked.is(":checked")) {
            j++;
            // Добавляем в JSON
            var idCompetitionType = $(checked).attr("idCompetitionType");
            var idCupCompetitionType = $(checked).attr("idCupCompetitionType");
            var name = $(checked).attr("Name");

            tmp.push({
                IdCompetitionType: idCompetitionType,
                IdCupCompetitionType : idCupCompetitionType,
                Name: name,
                TimeFirstShift: $(element).find(":text").datepicker("getDate")
            });
        }
    });

    return $.toJSON(tmp);
}

// Редактировать соревнование
function editCup() {

    if ( validateInput() ) {

        var competitionsJson = getCompetitionsJson();

        var dateStart = formatDate($("#DateStart").datepicker("getDate"));
        var dateEnd = formatDate($("#DateEnd").datepicker("getDate"));

        // dataSend собирается говнокодом, то что ниже желательно не трогать. Очень важен порядок конкатенации
        var dataSend = "DateStart=" + dateStart + "&DateEnd=" + dateEnd + "&" + $("#add").serialize() + "&competitionTypes=" + competitionsJson + "&idEditCup=" + "@Model.Id";

        $.ajax({
            url: linksCup.Update,
            dataType: "JSON",
            data: dataSend,
            async: false,
            success: function(data) {
                if (data.IsOk) {
                    showInfo("Соревнование отредактировано"); // showInfo
                } else showError(data.Message); // сообщение об ошибке как -то показать на странице
            },
            error: function(data) {
                showError("Ошибка ajax");
            }
        });
    }
}

// Добавить соревнование
function addCup() {
    if (validateInput()) {
        var competitionsJson = getCompetitionsJson();
        var dateStart = formatDate($("#DateStart").datepicker("getDate"));
        var dateEnd = formatDate($("#DateEnd").datepicker("getDate"));
        // dataSend собирается говнокодом, то что ниже желательно не трогать. Очень важен порядок конкатенации
        var dataSend = "DateStart=" + dateStart + "&DateEnd=" + dateEnd + "&" + $("#add").serialize() + "&competitionTypes=" + competitionsJson;

        $.ajax({
            url: linksCup.AddCup,
            dataType: "JSON",
            data: dataSend,
            async: false,
            success: function(data) {
                if (data.IsOk == true) {
                    showInfo("Соревнование добавлено успешно"); // showInfo
                    window.location = "@Url.Action("Index", "ViewCup")" + "?idCup=" + data.IdCup;
                } else showError(data.Message); // сообщение об ошибке как -то показать на странице
            },
            error: function(data) {
                showError("Ошибка ajax");
            }
        });
    }
}

// Изменение фильтра по стране
function changeCountryCupFilter() {
    var idCountry = $("#CountryFilter").val();
    getRegions(idCountry, $("#cupRegionFilter"), "RegionFilter");
}

// Должна вызываться при изменении страны
function changeCountry() {
    var idCountry = $("#IdCountry").val();
    getRegions(idCountry, $("#tdRegion"), "IdRegion");
}

// Должна вызываться при изменении региона
function  changeRegion() {
    var idRegion = $("#IdRegion").val();
    getShootingRanges(idRegion);
}

// Получить список тиров по региону
function getShootingRanges(idRegion) {

    if (idRegion) {
        $.ajax({
            url: linksCommon.GetShootingRangesByRegion,
            dataType: "html",
            data: { idRegion: idRegion},
            async: false,
            success: function (data) {
                $("#tdRange").html(data);
            },
            error: function (data) {
                showError("Ошибка ajax");
            }
        });
    }
}

// Получить список соревнований
function getCupsList(idRegion, dateFrom, dateTo)
{
        $.ajax({
            url: linksCup.GetCupsList,
            dataType: "html",
            data: { idRegion: idRegion, dateFrom : dateFrom, dateTo : dateTo },
            async: false,
            success: function (data) {
                $("#divCupsList").html(data);
            },
            error: function (data) {
                showError("Ошибка ajax");
            }
        });
}