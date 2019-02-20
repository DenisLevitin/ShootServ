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
                url: linksShootingRange.Delete,
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
        var rez;
            $.ajax({
                url: linksShootingRange.GetListByRegion,
                dataType: "json",
                data: { idRegion: idRegion },
                async: false,
                success: function (data) {
                    /// TODO: в data json, надо построить в listShootingRanges таблицу
                    rez = data;
                },
                error: function () {
                    showError("Ошибка ajax");
                }
            });
        
        return rez;
    };

    //таблица JQGrid
    //очистить
    this.clearTable = function () {
        $(".gridContainer").html('<table id="list"><tr><td></td></tr></table>');
    };
   
    //создать
    this.createGrid = function (ranges) {
        var JQGridProperties = Object.assign({
            datatype: "local",
            data: ranges,
            colNames: ['Тир', 'Телефон', 'Регион', 'Адрес'],
            colModel: [
                { name: 'Name', "label": 'Name', "sortable": true },
                { name: 'Phone', "label": 'phone', "sortable": false },
                { name: 'RegionName', "label": 'RegionName', "sortable": true, "sorttype": "string" },
                { name: 'Address', "sortable": false }
            ]
        }, window.common.JQGRID_PARAMETRES);
        

        this.clearTable();
        //инициализация jqGrid
        $("#list").jqGrid(JQGridProperties);
    };

    // Нужно вызвать эту функцию при изменении региона
    this.changeRegion = function () {
        var idRegion = $("#region-choise").val();
            var rages = this.getListByRegion(idRegion);
            this.createGrid(rages);
    };

    this.construct = function () {
    };
};