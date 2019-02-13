"use strict";
(function () {
    window.common = {};
    var createVisualError = function (errorInput, message) {
        errorInput.css("border-color","red");
        alert(message);
    };

    window.common.lightValidationMessagesOnForm = function (form, validationMessages) {
        $.each(validationMessages, function (key, value) {
            var attribute = '[name=\"' + key +'\"]';
            var errorInput = form.find(attribute);
            if (errorInput) {
                createVisualError(errorInput, value);
                setInterval(function () {
                    errorInput.attr('style','');
                }, 20000);
            }
        });
    }
})();

function renderJsonArrayToSelect(select, valueFieldName, textFieldName, json)
{
    var select = $(select);
    select.html("");

    if (!json) {
        return;
    }
    
    if ( typeof(json) === "string")
    {
        json = JSON.parse(json);
    }
    
    $.each(json, function(i, val){
        select.append($('<option />', { value: val[valueFieldName], text: val[textFieldName] }));
    });
}