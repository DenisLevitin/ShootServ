"use strict";
(function () {
    window.common = {};
    var createVisualError = function (errorInput, message) {
        errorInput.css("border-color","red");
        alert(message);
    }


    window.common.lightValidationMessagesOnForm = function (form, validationMessages) {
        $.each(validationMessages, function (key, value) {
            var attribute = '[name=\"' + key +'\"]';
            var errorInput = form.find(attribute);
            if (errorInput) {
                createVisualError(errorInput, value);
                setInterval(function () {
                    errorInput.attr('style','');
                }, 20000)
            }
        })
    }
})();