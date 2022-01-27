
async function mensaje(element) {
    alert(element);
}


async function CreateRetiro(cadena) {

    /*alert(cadena);*/

    $.ajax({
        url: '/account/createretiro',
        type: "POST",
        async: true,
        data: jQuery.param({ param1: cadena }),
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


async function CreatePackages(cadena) {

    /*alert(cadena);*/

    $.ajax({
        url: '/account/createpackages',
        type: "POST",
        async: true,
        data: jQuery.param({ parameter1: cadena }),
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


async function CreateTransfer(cadena) {

 /*   alert(cadena);*/

    $.ajax({
        url: '/account/createtransfer',
        type: "POST",
        async: true,
        data: jQuery.param({ param1:cadena }),
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