
$(document).ready( function() {
    var userData = $(".js-user-data");
    $("#editButton").on('click', function () {
        if (userData.hasClass("no-edit")) {
            userData.removeClass("no-edit");
            $(".js-user-data .form-control").prop('disabled', false);
            $("#submitButton").removeClass("hidden");
        } else {
            userData.addClass("no-edit");
            $(".js-user-data .form-control").prop('disabled', true);
            $("#submitButton").addClass("hidden");
        }
    });
});

