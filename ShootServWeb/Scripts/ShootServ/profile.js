
$(document).ready( function() {
    var userData = $(".js-user-data");
    var editButton = $("#editButton");
    var data;
    var addStartData = function (data) {
        var currentInputName;
        for (var i = 0; i < data.length; i++) {
            currentInputName = data[i].name;
            $("input[name*='" + currentInputName + "']").prop('value', data[i].value);
        }
    };


    editButton.on('click', function () {
        if (userData.hasClass("no-edit")) {
            userData.removeClass("no-edit");
            $(".js-user-data .form-control").prop('disabled', false);
            data = $(".js-upload-user-data").serializeArray()
            $("#submitButton").removeClass("hidden");
            editButton.prop("value", "Отменить редактирование");
        } else { 

            if (data) {
                addStartData(data.slice());
            }
            editButton.prop("value", "Редактирование");
            userData.addClass("no-edit");
            $("#submitButton").addClass("hidden");
            $(".js-user-data .form-control").prop('disabled', true);
        }
    });


});

