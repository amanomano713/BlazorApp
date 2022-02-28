
function getOrientation() {
    return Math.abs(window.orientation) - 90 == 0 ? "landscape" : "portrait";
};
function getMobileWidth() {
    return getOrientation() == "landscape" ? screen.availHeight : screen.availWidth;
};
function getMobileHeight() {
    return getOrientation() == "landscape" ? screen.availWidth : screen.availHeight;
};

function recargar() {
    location.reload();
}

function ocultar() {

    $('div#idsidebar').attr('style', 'visibility : hidden');
    $('div#idsidebar').attr('style', 'width : 0%')

}

function ajustar(pantalla) {

    if (screen.width > 560) {
        if (pantalla == "Subastas") {
            $('div#idsidebar').attr('style', 'height : 150vh');
        } else {
            $('div#idsidebar').removeAttr('style');
        }
        return true;
    } else {
        $('div#idsidebar').removeAttr('style');
        return false;
    }
}

function movil(pantalla) {

    if (screen.width > 560) {
        return false;
    } else {
        return true;
    }
}


function resolucion() {

    var result = + screen.availWidth + " x " + screen.availHeight
    return result;
}

function ajustar01() {

    $('div#idsidebar').attr('style', 'height : 120vh');
}

function mostrar() {

    $('div#idsidebar').attr('style', 'visibility : visible')
}

async function CreatePuja(cadena) {

    $.ajax({
        url: '/account/createpuja',
        headers: { 'Authorization': Bearer },
        type: "POST",
        async: true,
        data: jQuery.param({ param1: cadena }),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
            if (data.result == 1) {
           } else if (data.result == 2) {
                $('div#modalSesion-id').removeClass('modal').addClass('modal active')
            }
            else {
                $('div#modalSesion-id').removeClass('modal active').addClass('modal')
            }
        }
    });
}

async function CreateRetiro(cadena) {

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