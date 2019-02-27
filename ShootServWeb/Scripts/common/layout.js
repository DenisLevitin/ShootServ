 //функция вызова ошибки
function showError(text) {
    $('#showErrorModal').modal('show');
    $('#showErrorModal').on('shown.bs.modal', function () {
        var modal = $(this);
        modal.find('.modal-body').text(text);
    });
}

function showInfo(text, callback) {
  
    $('#showInfoModal').modal('show');
    $('#showInfoModal').on('shown.bs.modal', function () {
        var modal = $(this);
        modal.find('.modal-body').text(text);
    });
    $('#showInfoModal').on('hide.bs.modal', function () {
        callback();
    });
}

var showConfirm = function (text, returnUrl) {
    $('#showConfirmModal').modal('show');
    $('#showConfirmModal').on('shown.bs.modal', function () {
        var modal = $(this);
        modal.find('.modal-body').text(text);
    });
   $('#modalAgree').on('click', function () {
        window.location = window.linkslogin + "?ReturnUrl=" + returnUrl;
    });
};

function redirectToLoginPage(returnUrl) 
{
    showConfirm("Для данного действия требуется выполнить вход. Перейти на страницу авторизации?");

}