
function updateQRCodeForPROINFOR() {
    console.log("updateQRCodeupdateQRCodeupdateQRCodeupdateQRCodeupdateQRCode");
    $('.QRCodeImage').each(function () {
        var code = $(this).siblings('.Code').text();
        if (code) {
            
            $(this).attr('src', 'admin/ProductInformationlowes/GenerateQRCode?text='+encodeURIComponent(code));
        } else {
            $(this).attr('src', '');
        }
    });
}

function updateBarFORPROINF() {
    $('.BarCodeUPC').each(function () {
        var upc = $(this).siblings('.UPCFORPR').text();
        console.log(upc);
        if (upc) {
            
            $(this).attr('src', `admin/ProductInformationlowes/GenerateBarcode?text=`+encodeURIComponent(upc));
        } else 
            $(this).attr('src', '');
        }
    });
}


$(document).on('click', '.paginate_button', function () {
    updateQRCodeForPROINFOR();
    updateBarFORPROINF();
});
        $(document).on('input', '.form-control .input-sm', function () {
    updateQRCodeForPROINFOR();
    updateBarFORPROINF();
});

$(document).ready(function () {
    updateQRCodeForPROINFOR();
    updateBarFORPROINF();
});
