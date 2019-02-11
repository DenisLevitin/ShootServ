// Получить список регионов по стране ( это много где копируется )
function getRegions(idCountry, tagName, callback) {
    $.ajax({
        url: linksCommon.GetRegionsByCountry,
        dataType: "html",
        type: "GET",
        async: false,
        data: {
            idCountry: idCountry,
            tagName: tagName,
            addAll : true
        },
        success: function (data) {
            callback(data);
        },
        error: function () {
            showError("Ошибка ajax");
        }
    });
}