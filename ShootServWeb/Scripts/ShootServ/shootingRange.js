$(document).ready(function ()
{
    var actor = new shootingRangePageActor();
    
    $(document).on("change", "#CountryId", function () {
        actor.changeCountry();
    });
    
    $(document).on("click", "#addBt", function ()
    {
        if( isAuthorize) {
            if (actor.validateInput()) {
                $.ajax({
                    url: linksShootingRange.Add,
                    dataType: "json",
                    data: $("form").serialize(),
                    method: "POST",
                    async: false,
                    success: function (data) {
                        if (data.IsOk) {
                            var relocation = function () { window.location = linksShootingRange.List; };
                            showInfo("Тир успешно добавлен", relocation);
                
                        } else {
                            if (data.Message) {
                                showError(data.Message); // сообщение об ошибке как -то показать на странице 
                            }
                            // чтобы было совсем хорошо
                            window.common.lightValidationMessagesOnForm($("form"), data.ValidationMessages);
                        }
                    },
                    error: function (data) {
                        showError("Ошибка ajax");
                    }
                });
            }
        }
        else {
            redirectToLoginPage(linksShootingRange.Index);
        }
    });

    actor.changeCountry();
});

var shootingRangePageActor = function () {
    
    // Нужно вызвать эту функцию при изменении региона
    this.changeCountry = function() {
        var idCountry = $("#CountryId").val();
        var regions = getRegions(idCountry); // Ошибка!!!!

            var idRegion = $("#RegionId");
            renderJsonArrayToSelect(idRegion, "Id", "Name", regions);
    };

    this.construct = function(){
    };
    
    // Проверить ввод
    this.validateInput = function()
    {
        if ($("#RegionId").val() <= 0)
        {
            showError("Не выбран регион");
            return false;
        }
    
        if (!$("#Name").val())
        {
            showError("Не введено название тира");
            return false;
        }
    
        if (!$("#Address").val()) {
            showError("Не введен адрес тира");
            return false;
        }
    
        return true;
    };
};