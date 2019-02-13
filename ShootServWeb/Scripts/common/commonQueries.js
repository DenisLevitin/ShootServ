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

// Получить список стрелковых клубов в регионе
function getShootingClubs(idCountry, idRegion) {
    var result = null;
    $.ajax({
        url: linksClub.Get,
        dataType: "html",
        type: "GET",
        async: false,
        data: {
            idCountry: idCountry,
            idRegion: idRegion
        },
        success: function(data) {
            result = data;
        },
        error: function() {
            showError("Ошибка ajax");
        }
    });
    
    return result;
}

// получить тиры по региону
function getShootingRanges(idRegion)
{
    var result = null;
    $.ajax({
        url: linksShootingRange.GetListByRegion,
        dataType: "html",
        type: "GET",
        async: false,
        data: {
            idRegion: idRegion
        },
        success: function(data) {
            result = data;
        },
        error: function() {
            showError("Ошибка ajax");
        }
    });

    return result;
}