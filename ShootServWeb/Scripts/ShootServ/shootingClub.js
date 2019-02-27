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
                        method: "POST",
                        success: function(data) {
                            if (data.IsOk) {
                                var relocation = function () { window.location = linksClub.List; };
                                showInfo("Стрелковый клуб успешно добавлен", relocation);

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

    actor.changeCountry();
    actor.changeRegion();
});

var renderShootingRanges = function(data)
{
    renderJsonArrayToSelect($("#idShootingRange"), "Id", "Name", data);
};

var shootingClubPageActor = function () {
    this.validateInput = function() {
        var name = $("#name").val();
        if (!name) {
            showError("Не введено название стрелкового клуба");
            return false;
        }

        var idShootingRange = $("#idShootingRange").val();
        if (!idShootingRange) {
            showError("Не выбран тир");
            return false;
        }

        return true;
    };

    this.changeCountry = function() {
        var idCountry = $("#idCountry").val();
        var regions = getRegions(idCountry, $("#idRegion"));
        renderJsonArrayToSelect($("#idRegion"), "Id", "Name", regions);
        this.changeRegion();
    };

    this.changeRegion = function() {
        var idRegion = $("#idRegion").val();

        var shootingRanges = getShootingRanges(idRegion);
        renderShootingRanges(shootingRanges);
    };
};