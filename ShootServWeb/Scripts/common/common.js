﻿"use strict";
(function () {
    window.common = {};
    var createVisualError = function (errorInput, message) {
        errorInput.css("border-color","red");
        alert(message);
    };

    window.common.lightValidationMessagesOnForm = function (form, validationMessages) {
        $.each(validationMessages, function (key, value) {
            var attribute = '[name=\"' + key + '\"]';
            var errorInput = form.find(attribute);
            if (errorInput) {
                createVisualError(errorInput, value);
                setInterval(function () {
                    errorInput.attr('style', '');
                }, 20000);
            }
        });
    };

    $.ajaxSetup({
        statusCode: {
            401: function(){ // выполнить функцию если код ответа HTTP 401
                showInfo("пользователь не авторизован");
            }
        }
    });


})();

function renderJsonArrayToSelect(select, valueFieldName, textFieldName, json)
{
    var selectElement = $(select);
    selectElement.html("");

    if (!json) {
        return;
    }
    
    if ( typeof(json) === "string")
    {
        json = JSON.parse(json);
    }
    
    $.each(json, function(i, val){
        selectElement.append($('<option />', { value: val[valueFieldName], text: val[textFieldName] }));
    });
}

var getJQGridSettings = function () {
    return {
        styleUI: 'Bootstrap',
        gridview: true,
        autoencode: true,
        viewrecords: true,
        loadonce: true,
        shrinkToFit: true,
        postData: { expediente: "expediente" },
        sortorder: 'desc',
        autorowheight: true,
        autoheight: true,
        autowidth: true,
        columnsresize: true,
        forceFit: true,
        hidegrid: true
    };
};