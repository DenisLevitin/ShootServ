
$(document).ready(function() {
    var actor = new registrationActor();
    $("#shooterTable").hide();

    var editMode = "@Model.IsEditMode";
    if (editMode) {
        var role = $("#IdRole").val();
        registrationFormShifterByName(role);
    }

    $.datepicker.setDefaults($.datepicker.regional['ru']);
    $(".datepicker").datepicker();

    $(document).on("change", "#idCountry", function () {
        actor.changeCountry();
    });

    $(document).on("change", "#idRegion", function() {
        actor.changeRegion();
    });

    $(document).on("change", "#IdRole", function() {
        var role = $("#IdRole").val();
        registrationFormShifterByName(role);
    });

    // клик на сохранить
    $(document).on("click", "#editBt", function () {

        var needUpdatePassword = $("#Password").val().length > 0; // требуется ли обновить пароль пользователя

        if (ValidateInput(true))
        {
            $.ajax({
                url: "@Url.Action("UpdateUser")",
                dataType: "json",
                data: $("form").serialize() + "&idExistingUser="+"@Model.IdExistingUser"+"&needUpdatePassword="+needUpdatePassword,
                async: false,
                success: function (data) {
                    if (data.IsOk) {

                        showInfo("Данные обновлены"); // showInfo
                        window.location = "@Url.Action("Index", "Home", new { Area = ""})";

                    } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                },
                error: function (data) {
                    showError("Ошибка ajax");
                }
            });
        }
    });

    // клик на добавить
    $(document).on("click", "#addBt", function() {
        if (ValidateInput(false)) {

            $.ajax({
                url: "@Url.Action("AddUser", "Registration")",
                dataType: "json",
                data: $("form").serialize(),
                async: false,
                success: function (data) {
                    if (data.IsOk == true) {

                        showInfo("Регистрация проведена успешно"); // showInfo
                        window.location = "@Url.Action("Index", "Home", new { Area = ""})";

                    } else showError(data.Message); // сообщение об ошибке как -то показать на странице
                },
                error: function (data) {
                    showError("Ошибка ajax");
                }
            });
        }
    });
});

var registrationActor = function () {

    // Изменить страну
    this.changeCountry = function() {
        var idCountry = $("#idCountry").val();
        GetRegions(idCountry);
    };

    // Изменить регион
    this.changeRegion = function() {
        var idRegion = $("#idRegion").val();
        GetShootingClubs(idRegion);
    };

    // валидация ввода
    this.validateInput = function(isEditing) {

        var name = $("#Name").val();
        if (name == undefined || name == "")
        {
            showError("Не введено имя");
            return false;
        }

        var family = $("#Family").val();
        if (family == undefined || family == "") {
            showError("Не введена фамилия");
            return false;
        }

        var login = $("#Login").val();
        if (login == undefined || login == "") {
            showError("Не введена фамилия");
            return false;
        }

        var password = $("#Password").val();
        // если валидация при редактировании, то пароли не валидируем и не сравниваем. Если какой-либо пароль введен, то считаем, что его надо обновить
        if ( ! isEditing || password.length > 0) {
            if (password == undefined || password == "") {
                showError("Не введен пароль");
                return false;
            }

            if ($("#Password").val() != $("#Password2").val()) {
                showError("Пароли не совпадают");
                return false;
            }
        }

        if ($("#IdRole").val() == undefined) {
            showError("Не введена роль");
            return false;
        }

        if ( $("#Email").val() == undefined || $("#Email").val() == "") {
            showError("Не введен email");
            return false;
        }

        if ($("#IdRole").val() == 1) {
            // доп. проверка организатора
        }
        else {
            // доп. проверка стрелка
            if ($("#IdWeaponType").val() == undefined) {
                showError("Не введен тип оружия стрелка");
                return false;
            }

            if ($("#IdClub").val() == undefined) {
                showError("Не введен стрелковый клуб");
                return false;
            }

            if ($("#IdShooterCategory").val() == undefined) {
                showError("Не введен разряд стрелка");
                return false;
            }

            var dateBirthday = $("#DateBirthday").val();
            if (dateBirthday == undefined || dateBirthday == "") {
                showError("Не введена дата рождения");
                return false;
            }
        }

        return true;
    }
}