
var token = localStorage.getItem("access_token");

var key = localStorage.getItem("key");

var Bearer = 'Bearer' + token +'|'+ 'key' + key;

function recargar() {

    location.reload();
}

function ocultar() {

    $('div#idsidebar').attr('style', 'visibility : hidden');
    $('div#idsidebar').attr('style', 'width : 0%')

}

function mostrar() {

    $('div#idsidebar').attr('style', 'visibility : visible')
}

async function CreateRetiro(cadena) {

/*    var token = localStorage.getItem("access_token");*/

/*    alert(localStorage.getItem("access_token"));*/

/*    var Bearer = 'Bearer ' + token;*/

    $.ajax({
        url: '/account/createretiro',
        headers: { 'Authorization': Bearer },
        type: "POST",
        async: true,
        data: jQuery.param({ param1: cadena }),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
            if (data.result == 1) {
                $('div#modal-id').removeClass('modal').addClass('modal active')
            } else if (data.result == 2){
                $('div#modal-id').removeClass('modal active').addClass('modal')
                $('div#modalSesion-id').removeClass('modal').addClass('modal active')
            }
            else{
                $('div#modal-id').removeClass('modal active').addClass('modal')
                $('div#modalSesion-id').removeClass('modal active').addClass('modal')
            }
        }
    });
}


async function CreatePackages(cadena) {

    /*alert(cadena);*/

    $.ajax({
        url: '/account/createpackages',
        headers: { 'Authorization': Bearer },
        type: "POST",
        async: true,
        data: jQuery.param({ parameter1: cadena }),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
            if (data.result == 1) {
                $('div#modal-id').removeClass('modal').addClass('modal active')
            } else if (data.result == 2) {
                $('div#modal-id').removeClass('modal active').addClass('modal')
                $('div#modalSesion-id').removeClass('modal').addClass('modal active')
            }
            else {
                $('div#modal-id').removeClass('modal active').addClass('modal')
                $('div#modalSesion-id').removeClass('modal active').addClass('modal')
            }
        }
    });
}


async function CreateTransfer(cadena) {

 /*   alert(cadena);*/

    $.ajax({
        url: '/account/createtransfer',
        headers: { 'Authorization': Bearer },
        type: "POST",
        async: true,
        data: jQuery.param({ param1:cadena }),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
            if (data.result == 1) {
                $('div#modal-id').removeClass('modal').addClass('modal active')
            } else if (data.result == 2) {
                $('div#modal-id').removeClass('modal active').addClass('modal')
                $('div#modalSesion-id').removeClass('modal').addClass('modal active')
            }
            else {
                $('div#modal-id').removeClass('modal active').addClass('modal')
                $('div#modalSesion-id').removeClass('modal active').addClass('modal')
            }
        }
    });
}