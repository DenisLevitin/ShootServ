$(document).ready(function () {
    var $validator = $("#addform").validate({
        rules: {
            emailfield: {
                required: true,
                email: true,
                minlength: 3
            },
            namefield: {
                required: true,
                minlength: 3
            },
            password: {
                required: true,
                minlength: 7
            },
            password2: {
                required: true,
                minlength: 7,
                equalTo: password
            },
            login: {
                required: true
            },
            adress: {
                minlength: 20,
                required: true
            }

        },
        messages: {
            name: "Пожалуйста, введите Ваше имя",
            family: "Пожалуйста, введите Вашу Фамилию",
            password: {
                required: "Это поле необходимо заполнить",
                minlength: "Пароль должен быть не менее 7 символов"
            },
            adress: {
                required: "Это поле необходимо заполнить",
                minlength: "Адрес должен быть не менее 20 символов"
            },
            password2: {
                required: "Это поле необходимо заполнить",
                minlength: "Пароль должен быть не менее 7 символов",
                equalTo: "Пароли не совпадают"
            }
            ,
            email: {
                required: "Это поле необходимо заполнить",
                email: "Формат: name@domain.com"
            },
            login: {
                required: "Это поле необходимо заполнить"
            }
        }
    });

    $('#rootwizard').bootstrapWizard({
        'onNext': function (tab, navigation, index) {
            var $valid = $("#addform").valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
        },
        onTabClick: function (tab, navigation, index) {
            return false;
        },
        onTabShow: function (tab, navigation, index) {
            var $total = navigation.find('li').length;
            var $current = index + 1;
            var $percent = ($current / $total) * 100;
        }
    });

    var actor = new registrationActor();
    $(".divShooterInput").hide();

    if (isAuthorize) {
        var roleId = $("#idRole").val();
        actor.registrationFormShifterByName(roleId);
    }




    $(document).on("change", "#idCountry", function () {
        actor.changeCountry();
    });

    $(document).on("change", "#idRegion", function () {
        actor.changeRegion();
    });

    $(document).on("change", "#idRole", function () {
        var roleId = $("#idRole").val();
        actor.registrationFormShifterByName(roleId);
    });

    // клик на сохранить
    $(document).on("click", "#editBt", function () {
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
    });

    // клик на добавить
    $(document).on("click", "#addBt", function () {
        $.ajax({
            url: linksRegistration.AddUser,
            dataType: "json",
            data: $("form").serialize(),
            async: false,
            method: "POST",
            success: function (data) {
                if (data.IsOk) {
                    showInfo("Регистрация проведена успешно", function () {
                        window.location = linkHome;
                    }); // showInfo
                 
                } else showError(data.Message); // сообщение об ошибке как -то показать на странице
            },
            error: function (data) {
                showError("Ошибка ajax");
            }
        });
    });

    actor.changeCountry();
    actor.changeRegion();
});

var registrationActor = function () {
    var inputRegions = $("#idRegion");
    var inputCountry = $("#idCountry");
    this.registrationFormShifterByName = function (idRole) {
        if (idRole === roles.shooterRoleId) {
            $(".divShooterInput").show();
        }
        else {
            $(".divShooterInput").hide();
        }
    };

    // Изменить страну
    this.changeCountry = function () {
        var idCountry = inputCountry.val();
        var regions = getRegions(idCountry, inputRegions);
        renderJsonArrayToSelect(inputRegions, "Id", "Name", regions);
        var idRegion = inputRegions.val();
        var shootingClubs = getShootingClubs(idCountry, idRegion);
        this.renderClubs(shootingClubs);
    };

    // Изменить регион
    this.changeRegion = function () {
        var idRegion = inputRegions.val();
        var idCountry = inputCountry.val();
        var shootingClubs = getShootingClubs(idCountry, idRegion);
        this.renderClubs(shootingClubs);
    };

    this.renderClubs = function (data) {
        renderJsonArrayToSelect($("#idClub"), "Id", "Name", data);
    };
};