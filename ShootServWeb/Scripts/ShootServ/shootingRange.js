$(document).ready(function ()
{
    var actor = new shootingRangePageActor();

    actor.changeRegion();
    $(document).on("change", "#region-choise", function ()
    {
        actor.changeRegion();
    });

    $(document).on("change", "#CountryId", function () {
        actor.changeCountry();
    });

    // Клик на ссылке удалить тир
    $(document).on("click", ".delRange", function () {

        if ( isAuthorize) {
            var a = $(this);
            var tr = a.closest("tr");
            var id = a.attr("idShootRange");

            $.ajax({
                url: linksShootingRange.Delete,
                dataType: "json",
                data: {idShootingRange: id},
                async: false,
                success: function (data) {
                    if (data.IsOk) {
                        showInfo("Тир удален");
                        tr.remove();
                    } else showError(data.Message);
                },
                error: function (data) {
                    showError("Ошибка ajax");
                }
            });
        }
        else {
            redirectToLoginPage(linksShootingRange.Index);
        }
    });

    $(document).on("click", "#addBt", function ()
    {
        if( isAuthorize) {
            if (actor.validateInput()) {
                $.ajax({
                    url: linksShootingRange.Add,
                    dataType: "json",
                    data: $("form").serialize(),
                    method: "POST",
                    async: false,
                    success: function (data) {
                        if (data.IsOk) {
                            var idRegion = $("#RegionId").val();
                            actor.getListByRegion(idRegion);
                        } else {
                            if (data.Message) {
                                showError(data.Message); // сообщение об ошибке как -то показать на странице 
                            }
                            console.dir(data.ValidateMessages);
                            // чтобы было совсем хорошо
                            window.common.lightValidationMessagesOnForm($("form"), data.ValidationMessages);
                        }
                    },
                    error: function (data) {
                        showError("Ошибка ajax");
                    }
                });
            }
        }
        else {
            redirectToLoginPage(linksShootingRange.Index);
        }
    });

    actor.changeCountry();
});

var shootingRangePageActor = function () {
    
    // Нужно вызвать эту функцию при изменении региона
    this.changeRegion = function()
    {
        var idRegion = $("#region-choise").val();
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
                url: linksShootingRange.GetListByRegion,
                dataType: "html",
                data: { idRegion: idRegion },
                async: false,
                success: function (data) {
                    /// TODO: в data json, надо построить в listShootingRanges таблицу
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
    this.getRegionsByCountry = function (idCountry) {
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
                    $("#tdRegionId").html(data);
                },
                error: function () {
                    showError("Ошибка ajax");
                }
            });
        }
    }
};