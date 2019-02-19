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





    // Получить список тиров по региону
    this.getListByRegion = function(idRegion)
    {
        var rez;
        if (idRegion) {
            $.ajax({
                url: linksShootingRange.GetListByRegion,
                dataType: "json",
                data: { idRegion: idRegion },
                async: false,
                success: function (data) {
                    /// TODO: в data json, надо построить в listShootingRanges таблицу
                    rez = data;
                },
                error: function ()
                {
                    showError("Ошибка ajax");
                }
            });
        }
        return rez;
};

    //таблица JQGrid
//очистить
this.clearTable = function () {
    $(".gridContainer").html('<table id="list"><tr><td></td></tr></table>');
};
//создать
    this.createGrid = function (ranges) {
        clearTable();
        $("#list").jqGrid({
            datatype: "local",
            data: ranges,
            colNames: ['Тир', 'Телефон', 'Регион', 'Адрес'],
            colModel: [
                { name: 'Name', "label": 'Name', "sortable": true },
                { name: 'Phone', "label": 'phone', "sortable": false },
                { name: 'RegionName', "label": 'RegionName', "sortable": true, "sorttype": "string" },
                { name: 'Address', "sortable": false }
            ],
            styleUI: 'Bootstrap',
            rowNum: 10,
            rowList: [10, 20, 30],
            gridview: true,
            autoencode: true,
            viewrecords: true,
            height: 250,
            autoWidth: true
        });
    };
var shootingRangeListActor = function () {

    // Нужно вызвать эту функцию при изменении региона
    this.changeRegion = function () {
        var idRegion = $("#region-choise").val();
        if (idRegion) {
            var rages = getListByRegion(idRegion);
            createGrid(rages);
        }
        };

    this.construct = function () {
    };
};