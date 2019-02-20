
$(document).ready(function() {
    var actor = new registrationActor();
    $("#shooterTable").hide();
    
    if (isAuthorize) {
        var roleId = $("#idRole").val();
        actor.registrationFormShifterByName(roleId);
    }

    $.datepicker.setDefaults($.datepicker.regional['ru']);
    $(".datepicker").datepicker(
        $("#anim").on("change", function () {
            $("#datepicker").datepicker("option", "showAnim", $(this).val());
        })
    );


    $(document).on("change", "#idCountry", function () {
        actor.changeCountry();
    });

    $(document).on("change", "#idRegion", function() {
        actor.changeRegion();
    });

    $(document).on("change", "#idRole", function() {
        var roleId = $("#idRole").val();
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
                method: "POST",
                success: function (data) {
                    if (data.IsOk) {
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

    actor.changeCountry();
    actor.changeRegion();
});

var registrationActor = function () {
    var inputRegions = $("#idRegion");
    var inputCountry = $("#idCountry");
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
        var idCountry = inputCountry.val();
        var regions = getRegions(idCountry, inputRegions);
        renderJsonArrayToSelect(inputRegions, "Id", "Name", regions);
        var idRegion = inputRegions.val();
        var shootingClubs = getShootingClubs(idCountry, idRegion);
        this.renderClubs(shootingClubs);
    };

    // Изменить регион
    this.changeRegion = function() {
        var idRegion = inputRegions.val();
        var idCountry = inputCountry.val();
        var shootingClubs = getShootingClubs(idCountry, idRegion);
        this.renderClubs(shootingClubs);
    };

    this.renderClubs = function(data)
    {
        renderJsonArrayToSelect($("#idClub"), "Id", "Name", data);
    };
    
    // валидация ввода
    this.validateInput = function(isEditing) {

        var name = $("#name").val();
        if (!name)
        {
            showError("Не введено имя");
            return false;
        }

        var family = $("#family").val();
        if (!family) {
            showError("Не введена фамилия");
            return false;
        }

        var login = $("#login").val();
        if (!login) {
            showError("Не введена фамилия");
            return false;
        }

        var password = $("#password").val();
        // если валидация при редактировании, то пароли не валидируем и не сравниваем. Если какой-либо пароль введен, то считаем, что его надо обновить
        if (!isEditing || password.length > 0) {
            if (!password) {
                showError("Не введен пароль");
                return false;
            }

            if ($("#password").val() != $("#password2").val()) {
                showError("Пароли не совпадают");
                return false;
            }
        }

        if (!$("#idRole").val()) {
            showError("Не введена роль");
            return false;
        }

        if (!$("#email").val()) {
            showError("Не введен email");
            return false;
        }

        if ($("#idRole").val() != roles.organizationRoleId) 
        {
            // доп. проверка стрелка
            if (!$("#idWeaponType").val()) {
                showError("Не введен тип оружия стрелка");
                return false;
            }

            if (!$("#idShooterCategory").val()) {
                showError("Не введен разряд стрелка");
                return false;
            }

            var dateBirthday = $("#dateBirthday").val();
            if (!dateBirthday) {
                showError("Не введена дата рождения");
                return false;
            }
        }

        return true;
    }
};