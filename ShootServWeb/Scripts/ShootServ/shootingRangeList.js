$(document).ready(function () {
    var actor = new shootingRangeListActor();

    actor.changeRegion();
    $(document).on("change", "#region-choise", function ()
    {
        actor.changeRegion();
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
});

var shootingRangeListActor = function () {
    
    // Нужно вызвать эту функцию при изменении региона
    this.changeRegion = function () {
        var idRegion = $("#region-choise").val();
        this.getListByRegion(idRegion);
    };

    this.construct = function(){
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
};