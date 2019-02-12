
$(document).ready(function() {
    var actor = new registrationActor();
    $("#shooterTable").hide();
    
    if (isAuthorize) {
        var roleId = $("#IdRole").val();
        actor.registrationFormShifterByName(roleId);
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
        var roleId = $("#IdRole").val();
        actor.registrationFormShifterByName(roleId);
    });

    // клик на сохранить
    $(document).on("click", "#editBt", function () {
        if (actor.validateInput(true))
        {
            $.ajax({
                url: linksRegistration.UpdateUser,
                dataType: "json",
                data: $("form").serialize(),
                async: false,
                method: "POST",
                success: function (data) {
                    if (data.IsOk) {
                        showInfo("Данные обновлены"); // showInfo
                        window.location = linkHome;
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
        if (actor.validateInput(false)) {
            $.ajax({
                url: linksRegistration.AddUser,
                dataType: "json",
                data: $("form").serialize(),
                async: false,
                success: function (data) {
                    if (data.IsOk == true) {
                        showInfo("Регистрация проведена успешно"); // showInfo
                        window.location = linkHome;
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

    this.registrationFormShifterByName = function (idRole) {
        if ( idRole == roles.shooterRoleId)
        {
            $("#shooterTable").show();
        }
        else {
            $("#shooterTable").hide();
        }
    };
    
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
        if (!name)
        {
            showError("Не введено имя");
            return false;
        }

        var family = $("#Family").val();
        if (!family) {
            showError("Не введена фамилия");
            return false;
        }

        var login = $("#Login").val();
        if (!login) {
            showError("Не введена фамилия");
            return false;
        }

        var password = $("#Password").val();
        // если валидация при редактировании, то пароли не валидируем и не сравниваем. Если какой-либо пароль введен, то считаем, что его надо обновить
        if (!isEditing || password.length > 0) {
            if (!password) {
                showError("Не введен пароль");
                return false;
            }

            if ($("#Password").val() != $("#Password2").val()) {
                showError("Пароли не совпадают");
                return false;
            }
        }

        if (!$("#IdRole").val()) {
            showError("Не введена роль");
            return false;
        }

        if (!$("#Email").val()) {
            showError("Не введен email");
            return false;
        }

        if ($("#IdRole").val() == roles.organizationRoleId) {
            // доп. проверка организатора
        }
        else {
            // доп. проверка стрелка
            if (!$("#IdWeaponType").val()) {
                showError("Не введен тип оружия стрелка");
                return false;
            }

            if (!$("#IdClub").val()) {
                showError("Не введен стрелковый клуб");
                return false;
            }

            if (!$("#IdShooterCategory").val()) {
                showError("Не введен разряд стрелка");
                return false;
            }

            var dateBirthday = $("#DateBirthday").val();
            if (!dateBirthday) {
                showError("Не введена дата рождения");
                return false;
            }
        }

        return true;
    }
};