$(document).ready(function () {
    var actor = new shootingClubListActor();

    actor.changeRegion();
    $(document).on("change", "#region-choise", function () {
        actor.changeRegion();
    });
});

var shootingClubListActor = function () {

    // Получить список тиров по региону
    this.getListByRegion = function (idRegion) {
        var result;
        $.ajax({
            url: linksClub.Get,
            dataType: "json",
            data: { idRegion: idRegion },
            async: false,
            success: function (data) {
                /// TODO: в data json, надо построить в listShootingClubs таблицу
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
    this.createGrid = function (shootingClubs) {
        var JQGRIG_SETTINGS = getJQGridSettings();
        var JQGridProperties = Object.assign({
            datatype: "local",
            del: true,
            data: shootingClubs,
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