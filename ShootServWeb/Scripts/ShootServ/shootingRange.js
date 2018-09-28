$(document).ready(function ()
{
    changeRegion();
    $(document).on("change", "#RegionId", function ()
    {
        changeRegion();
    });

    $(document).on("change", "#CountryId", function () {
        changeCountry();
    });

    // Клик на ссылке удалить тир
    $(document).on("click", ".delRange", function () {

        var a = $(this);
        var tr = a.closest("tr");
        var id = a.attr("idShootRange");

        if (checkLogin()) {
            $.ajax({
                url: linksShootingRange.ShootingRangeDelete,
                dataType: "json",
                data: { idShootingRange: id },
                async: false,
                success: function (data) {
                    if (data.IsOk) {
                        ShowInfo("Тир удален");
                        tr.remove();
                    } else showError(data.Message);
                },
                error: function (data) {
                    showError("Ошибка ajax");
                }
            });
        }
        else {
            redirectLoginPage(linksShootingRange.Index);
        }
    });

    $(document).on("click", "#addBt", function ()
    {
        if (checkLogin()) {
            if (validateInput()) {
                $.ajax({
                    url: linksShootingRange.Add,
                    dataType: "json",
                    data: $("form").serialize(),
                    async: false,
                    success: function (data) {
                        if (data.IsOk) {
                            var idRegion = $("#RegionId").val();
                            getListByRegion(idRegion);
                        } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                    },
                    error: function (data) {
                        showError("Ошибка ajax");
                    }
                });
            }
        }
        else {
            redirectLoginPage(linksShootingRange.Index);
        }
    });

    changeCountry();
});

// Нужно вызвать эту функцию при изменении региона
function changeRegion()
{
    var idRegion = $("#RegionId").val();
    getListByRegion(idRegion);
}

// Нужно вызвать эту функцию при изменении региона
function changeCountry() {
    var idCountry = $("#CountryId").val();
    getRegionsByCountry(idCountry);
}

// Проверить ввод
function validateInput()
{
    if ($("#RegionId").val() <= 0)
    {
        showError("Не выбран регион");
        return false;
    }

    if (!$("#Name").val())
    {
        showError("Не введено название тира");
        return false;
    }

    if (!$("#Address").val()) {
        showError("Не введен адрес тира");
        return false;
    }

    return true;
}

// Получить список тиров по региону
function getListByRegion(idRegion)
{
    if (idRegion) {
        $.ajax({
            url: links.shootingRangeGetListByRegion,
            dataType: "html",
            data: { idRegion: idRegion },
            async: false,
            success: function (data) {
                $("#listShootingRanges").html("");
                $("#listShootingRanges").html(data);
            },
            error: function ()
            {
                showError("Ошибка ajax");
            }
        });
    }
}

// Получить список регионов по стране
function getRegionsByCountry (idCountry) {
    if (idCountry) {

        var tagName = "RegionId";

        $.ajax({
            url: linksCommon.GetRegionsByCountry,
            dataType: "html",
            data: {
                idCountry: idCountry,
                tagName: tagName,
                addAll : true
            },
            async: false,
            success: function (data) {
                $("#tdRegionId").html("");
                $("#tdRegionId").html(data);
            },
            error: function () {
                showError("Ошибка ajax");
            }
        });
    }
}

//все для выпадающего контента
$(document).ready(function() {
    togetherContent("#divAddShootingRange", "#divAddShootingR", 'Добавить тир');
    togetherContent("#divShowShootingRange", "#divShowShootingR", 'Список тиров');
    addSumbol($("#divAddShootingRange"), true, 'Добавить тир');
    addSumbol($("#divShowShootingRange"), true, 'Список тиров');
});