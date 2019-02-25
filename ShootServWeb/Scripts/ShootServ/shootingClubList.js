$(document).ready(function () {
    var actor = new shootingRangeListActor();

    actor.changeRegion();
    $(document).on("change", "#region-choise", function () {
        actor.changeRegion();
    });

    // Клик на ссылке удалить тир
    $(document).on("click", ".delRange", function () {

        if (isAuthorize) {
            var a = $(this);
            var tr = a.closest("tr");
            var id = a.attr("idShootRange");

            $.ajax({
                url: linksClub.Delete,
                dataType: "json",
                data: { idShootingRange: id },
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

    // Получить список тиров по региону
    this.getListByRegion = function (idRegion) {
        var result;
        $.ajax({
            url: linksClub.Get,
            dataType: "json",
            data: { idRegion: idRegion },
            async: false,
            success: function (data) {
                /// TODO: в data json, надо построить в listShootingRanges таблицу
                result = data;
            },
            error: function () {
                showError("Ошибка ajax");
            }
        });

        return result;
    };

    //таблица JQGrid
    //очистить
    this.clearTable = function () {
        $(".gridContainer").html('<table id="list"><tr><td></td></tr></table>');
    };

    //создать
    this.createGrid = function (shootingRanges) {
        var JQGRIG_SETTINGS = getJQGridSettings();
        var JQGridProperties = Object.assign({
            datatype: "local",
            del: true,
            data: shootingRanges,
            pager: '#pagernav2',
            colNames: ['Клуб', 'Телефон', 'Регион', 'Адрес'],
            colModel: [
                { name: 'Name', "label": 'Name', "sortable": true },
                { name: 'Phone', "label": 'phone', "sortable": false },
                { name: 'RegionName', "label": 'RegionName', "sortable": true, "sorttype": "string" },
                { name: 'Address', "sortable": false }
            ]
        }, JQGRIG_SETTINGS);


        this.clearTable();
        //инициализация jqGrid
        $("#list").jqGrid(JQGridProperties);
    };

    // Нужно вызвать эту функцию при изменении региона
    this.changeRegion = function () {
        var idRegion = $("#region-choise").val();
        var shootingClubs = this.getListByRegion(idRegion);
        this.createGrid(shootingClubs);
    };

    this.construct = function () {
    };
};