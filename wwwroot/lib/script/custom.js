
function mensaje(element) {
    alert(element);
}

async function CreatePackages(id,cod) {

    $.ajax({
        url: '/account/createpackages',
        type: "POST",
        async: true,
        data: jQuery.param({ Id: id, CodPackage: cod }),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
            if (data.result == 0) {
                $('div#modal-id').removeClass('modal').addClass('modal active')
            } else {
                $('div#modal-id').removeClass('modal active').addClass('modal')
            }
        }
    });
}