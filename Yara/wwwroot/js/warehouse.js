$(document).ready(function () {
    console.log("Document is ready");

    // Initialize select2
    $('.select2_1').select2({
        placeholder: "Select an option",
        allowClear: true
    });

    // Generate random 5-character string
    function generateRandomString(length) {
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var result = '';
        var charactersLength = characters.length;
        for (var i = 0; i < length; i++) {
            result += characters.charAt(Math.floor(Math.random() * charactersLength));
        }
        return result;
    }

    // Function to update the Code field
    function updateCodeFieldForWareHouse() {
        var warehouseType = $('#SelectWareHouseType option:selected').text();
        var description = $('#Description').val();
        var randomString = generateRandomString(5);

        if (warehouseType && description) {
            var code = warehouseType + description + randomString;
            $('#Code').val(code);
            updateQRCodeForAddWareHouse(); // Update QR code when code is updated
        }
    }

    // Bind change events to WareHouseType and Description fields
    $('#SelectWareHouseType, #Description').on('change keyup', function () {
        updateCodeFieldForWareHouse();
    });

    // Update the QR code image
    function updateQRCodeForAddWareHouse() {
        var code = $('#Code').val();
        var generateQRCodeUrl = $('#QRCodeImage').data('url'); // Use data attribute to get URL
        if (code) {
            $('#QRCodeImage').attr('src', generateQRCodeUrl + '?text=' + encodeURIComponent(code));
        } else {
            $('#QRCodeImage').attr('src', '');
        }
    }

    // Bind change event to Code field to update QR code
    $('#Code').on('change keyup', function () {
        updateQRCodeForAddWareHouse();
    });

    // Update the URL field with the current page URL
    $("#url").val(window.location.href);
});

// Function to print warehouse details
function printWarehouseDetails() {
    var warehouseType = $('#SelectWareHouseType option:selected').text();
    var description = $('#Description').val();
    var qrCodeSrc = $('#QRCodeImage').attr('src');
    var printUrl = $('#QRCodeImage').data('print-url'); // Use data attribute for the print URL

    printUrl = `${printUrl}?warehouseType=${encodeURIComponent(warehouseType)}&description=${encodeURIComponent(description)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}`;

    $.get(printUrl, function (data) {
        var printWindow = window.open('', '_blank');
        printWindow.document.open();
        printWindow.document.write(data);
        printWindow.document.close();

        // Ensure the image is fully loaded before printing
        printWindow.onload = function () {
            printWindow.print();
        };
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error("Error: ", textStatus, errorThrown);
    });
}
