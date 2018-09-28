$(document).ready(function ()
{
    var actor = new shootingRangePageActor();

    actor.changeRegion();
    $(document).on("change", "#RegionId", function ()
    {
        actor.changeRegion();
    });

    $(document).on("change", "#CountryId", function () {
        actor.changeCountry();
    });

    // Клик на ссылке удалить тир
    $(document).on("click", ".delRange", function () {

        var a = $(this);
        var tr = a.closest("tr");
        var id = a.attr("idShootRange");
        
            $.ajax({
                url: linksShootingRange.Delete,
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
    });

    $(document).on("click", "#addBt", function ()
    {
            if (actor.validateInput()) {
                $.ajax({
                    url: linksShootingRange.Add,
                    dataType: "json",
                    data: $("form").serialize(),
                    async: false,
                    success: function (data) {
                        if (data.IsOk) {
                            var idRegion = $("#RegionId").val();
                            actor.getListByRegion(idRegion);
                        } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                    },
                    error: function (data) {
                        showError("Ошибка ajax");
                    }
                });
            }
    });

    actor.changeCountry();
});

var shootingRangePageActor = function () {
    
    // Нужно вызвать эту функцию при изменении региона
    this.changeRegion = function()
    {
        var idRegion = $("#RegionId").val();
        this.getListByRegion(idRegion);
    };
    
    // Нужно вызвать эту функцию при изменении региона
    this.changeCountry = function() {
        var idCountry = $("#CountryId").val();
        this.getRegionsByCountry(idCountry);
    };

    this.construct = function(){
    };
    
    // Проверить ввод
    this.validateInput = function()
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
    };
    
    // Получить список тиров по региону
    this.getListByRegion = function(idRegion)
    {
        if (idRegion) {
            $.ajax({
                url: links.GetListByRegion,
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
    };
    
    // Получить список регионов по стране
    this.getRegionsByCountry = function(idCountry) {
        if (idCountry) {
            $.ajax({
                url: linksCommon.GetRegionsByCountry,
                dataType: "html",
                data: {
                    idCountry: idCountry,
                    tagName: "RegionId",
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
};

//все для выпадающего контента
$(document).ready(function() {
    togetherContent("#divAddShootingRange", "#divAddShootingR", 'Добавить тир');
    togetherContent("#divShowShootingRange", "#divShowShootingR", 'Список тиров');
    addSumbol($("#divAddShootingRange"), true, 'Добавить тир');
    addSumbol($("#divShowShootingRange"), true, 'Список тиров');
});