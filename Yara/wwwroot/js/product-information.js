$(document).ready(function () {
    $('#UPC').on('keyup', function () {
        var upc = this.value;
        CreateBarCode(upc);
        updateCodeFieldForProductInformation();
    });

    $("#modelInput").on("change", function () {
        var model = $(this).val();

        if (model) {
            $.ajax({
                url: $('#modelInput').data('url'), // Use the data-url attribute
                type: 'GET',
                data: { model: model }, // Send the model as a parameter
                success: function (response) {
                    if (response.success) {
                        $("#output").attr("src", response.imageUrl).show(); // Display the fetched image
                        $("#imageUrlInput").val(response.imageUrl); // Set the image URL in the hidden input field
                        $("#ProductName").val(response.productName); // Set the product name
                    } else {
                        alert(response.message); // Show an alert if there was an issue fetching the image
                        $("#output").hide(); // Hide the image element if there's no image to display
                        $("#ProductName").val(''); // Clear the product name field
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error fetching the image."); // Alert the user about the error
                    $("#output").hide(); // Hide the image element in case of an error
                }
            });
        }
    });

    // Bind change events to Category, TypesProduct, ProductName, Model, and UPC
    $('#SelectCategory, #SelectTypesProduct, #ProductName, #modelInput, #UPC').on('change keyup', function () {
        updateCodeFieldForProductInformation();
    });

    $('#UPC').on('keyup change', function () {
        var code = $('#CodeQR').val();
        $('#QRCode').val(code);
    });

    // Set URL value for hidden input
    $("#url").val(window.location.href);
});

function updateQRCodeForAddProductInformation() {
    var code = $('#CodeQR').val();
    if (code) {
        $('#QRCodeImage').attr('src', $('#QRCodeImage').data('url') + '?text=' + encodeURIComponent(code));
    } else {
        $('#QRCodeImage').attr('src', '');
    }
}

function generateRandomString(length) {
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var result = '';
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return result;
}

function updateCodeFieldForProductInformation() {
    var Category = $('#SelectCategory option:selected').text();
    var TypesProduct = $('#SelectTypesProduct option:selected').text();
    var Product = $('#ProductName').val() || "No Name";
    var Model = $('#modelInput').val();
    var UPC = $('#UPC').val();
    var randomString = generateRandomString(5);

    if (Category && TypesProduct && Product && Model && UPC) {
        var code = randomString + Category + TypesProduct + Product + Model + UPC;
        $('#CodeQR').val(code);
        updateQRCodeForAddProductInformation(); // Update QR code when code is updated
    }
}

function CreateBarCode(text) {
    const apiUrl = $('#BarCode').data('url') + '?text=' + encodeURIComponent(text);

    $.ajax({
        url: apiUrl,
        type: 'GET',
        xhrFields: {
            responseType: 'blob' // Set response type to Blob
        },
        success: function (data) {
            if (data) {
                var imageUrl = URL.createObjectURL(data);
                $('#BarCode').attr('src', imageUrl);
                $('#BarCodeNo').text(text);
            } else {
                console.error("Error generating barcode.");
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}

function printWarehouseDetails() {
    var Category = $('#SelectCategory option:selected').text();
    var TypesProduct = $('#SelectTypesProduct option:selected').text();
    var Product = $('#ProductName').val() || "No Name";
    var Model = $('#modelInput').val();
    var UPC = $('#UPC').val();
    var qrCodeSrc = $('#QRCodeImage').attr('src');
    var bar = $('#BarCode').attr('src');

    var url = $('#QRCodeImage').data('print-url');

    url = `${url}?Category=${encodeURIComponent(Category)}&TypesProduct=${encodeURIComponent(TypesProduct)}&Product=${encodeURIComponent(Product)}&Model=${encodeURIComponent(Model)}&UPC=${encodeURIComponent(UPC)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}&bar=${encodeURIComponent(bar)}`;

    $.get(url, function (data) {
        var printWindow = window.open('', '_blank');
        printWindow.document.open();
        printWindow.document.write(data);
        printWindow.document.close();
        printWindow.onload = function () {
            printWindow.print();
        };
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error("Error: ", textStatus, errorThrown);
    });
}
