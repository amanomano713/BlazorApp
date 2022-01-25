
function mensaje(element) {
    alert(element);
}

async function CreatePackages(id, cod) {

    var _id = id;
    var _cod = cod;

    $.ajax({
        url: '/account/createpackages',
        type: "POST",
        async: true,
        data: jQuery.param({ parameter1: _id, parameter2:_cod }),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
            if (data.result == 1) {
                $('div#modal-id').removeClass('modal').addClass('modal active')
            } else {
                $('div#modal-id').removeClass('modal active').addClass('modal')
            }
        }
    });
}