$(document).ready(function () {
    var actor = new shootingClubPageActor();

    $(document).on("change",
        "#idCountry",
        function() {
            actor.changeCountry();
        });

    $(document).on("change",
        "#idRegion",
        function() {
            actor.changeRegion();
        });

    // Клик на "Удалить"
    $(document).on("click",
        ".delClub",
        function() {
            if (isAuthorize) {
                var a = $(this);
                var idClub = a.attr("idClub");

                $.ajax({
                    url: linksClub.Delete,
                    dataType: "json",
                    data: { idClub: idClub },
                    async: false,
                    success: function(data) {
                        if (data.IsOk) {
                            showInfo("Стрелковый клуб удален"); // showInfo
                            a.closest("tr").remove();
                        } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                    },
                    error: function(data) {
                        showError("Ошибка ajax");
                    }
                });
            } else {
                redirectToLoginPage(linksClub.Index);
            }

        });

    $(document).on("click",
        "#addBt",
        function() {
            if (isAuthorize) {
                if (actor.validateInput()) {

                    $.ajax({
                        url: linksClub.Add,
                        dataType: "json",
                        data: $("form").serialize(),
                        async: false,
                        success: function(data) {
                            if (data.IsOk) {
                                showInfo("Стрелковый клуб добавлен"); // showInfo

                                var idRegion = $("#idRegion").val();
                                var idCountry = $("#idCountry").val();

                                actor.getShootingClubs(idCountry, idRegion);

                            } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                        },
                        error: function(data) {
                            showError("Ошибка ajax");
                        }
                    });
                }
            } else {
                redirectToLoginPage(linksClub.Index);
            }

        });

    ChangeCountry();
});

var shootingClubPageActor = function () {
    
    // Получить список стрелковых клубов в регионе
    this.getShootingClubs = function(idCountry, idRegion) {
        $.ajax({
            url: linksClub.Get,
            dataType: "html",
            type: "GET",
            async: false,
            data: {
                idCountry: idCountry,
                idRegion: idRegion
            },
            success: function(data) {
                $("#list").html(data);
            },
            error: function() {
                showError("Ошибка ajax");
            }
        });
    };

    this.validateInput = function() {
        var name = $("#Name").val();
        if (!name) {
            showError("Не введено название тира");
            return false;
        }

        var idShootingRange = $("#IdShootingRange").val();
        if (!idShootingRange) {
            showError("Не выбран тир");
            return false;
        }

        return true;
    };

    this.changeCountry = function() {
        var idCountry = $("#idCountry").val();
        getRegions(idCountry, "idRegion", ""); /// TODO: не работает
    };

    this.changeRegion = function() {
        var idRegion = $("#idRegion").val();
        var idCountry = $("#idCountry").val();

        this.getShootingClubs(idCountry, idRegion, function(data) {
            $("#tdRegion").html(data);
        });
    };
};