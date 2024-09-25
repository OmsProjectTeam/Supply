﻿// Close the modal if the user clicks outside of it
window.onclick = function (event) {
    var modal = document.getElementById("customModal");
    if (event.target == modal) {
        modal.style.display = "none";
    }
};

document.addEventListener('DOMContentLoaded', function () {
    var productInput = document.getElementById('product12');

    // Event listener for the Enter key in the product12 input
    productInput.addEventListener('keypress', function (event) {
        if (event.key === 'Enter') {
            event.preventDefault(); // Prevent the default form submission
            // If the input is not empty, show the hidden content
            if (productInput.value.trim() !== '') {
                document.getElementById('contentWrapper').style.display = 'block';
            } else {
                alert('Please enter a QR code.');
            }
        }
    });
});

let typingTimer;  // Timer identifier
const typingDelay = 3000;  // 3 seconds delay before triggering search

var previousSearchText = '';

function openModal() {
    console.log("openModal");
    previousSearchText = $('#product12').val(); // Store the current search text
    $('#product12').data('previous-value', previousSearchText); // Store it in the data attribute
    document.getElementById("customModal").style.display = "block";
}

function closeModal() {
    document.getElementById("customModal").style.display = "none";
    $('#product12').val($('#product12').data('previous-value')); // Restore the search text from the data attribute
}

// Handle form submission inside the modal
$('form[asp-action="SaveModal"]').on('submit', function (event) {
    event.preventDefault(); // Prevent default form submission

    var form = $(this);

    // Optionally handle the form submission via AJAX
    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function (response) {
            // Close the modal
            closeModal();
            // Repopulate the search field with the stored search text
            $('#product12').val($('#product12').data('previous-value'));
        },
        error: function (response) {
            console.error("Error during form submission", response);
            // Handle the error (display a message, etc.)
        }
    });
});

// Repopulate the search box on page load
$(document).ready(function () {
    var storedSearchText = $('#product12').data('previous-value'); // Get the value from the data attribute
    if (storedSearchText) {
        $('#product12').val(storedSearchText); // Set it in the search box
    }
});

function checkProductAvailability(productId) {
    console.log("checkProductAvailability:" + productId);
    $.ajax({
        url: '/admin/Order/GetProductDetailsForOrder',
        type: 'GET',
        data: { productId: productId },
        success: function (data) {
            if (data && data.productCategoryId > 0) {
                populateProductDetails(data);  // Populate product details
                $('#modalButton').prop('disabled', true);  // Disable modal button
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'No product available',
                    text: 'Create a new one, please click the modal button',
                    showConfirmButton: false,
                    timer: 6000,
                    position: 'top-end',
                    toast: true
                });
                $('#ProductName').val(productId);
                $('#modalButton').prop('disabled', false);
                openModal();
            }
        },
        error: function () {
            console.error("Error fetching product details.");
            $('#ProductImage').attr('src', 'http://placehold.it/220x180');
            $('#GlobalPrice').val("0.00");
            $('#modalButton').prop('disabled', true);
        }
    });
}

function setPurchase() {
    var selectedValue = $('#SelectBondType option').filter(function () {
        return $(this).text().startsWith("Purchase");
    }).val();
    return selectedValue;
}

// Function to populate product details after selecting a product
function populateProductDetails(productDetails) {
    var set = setPurchase();

    if (productDetails && productDetails.productCategoryId > 0) {
        $('#ProductName').val(productDetails.model);
        $('#SelectProductCategory').val(productDetails.productCategoryId).trigger('change');
        $('#SelectBondType').val(set).trigger('change');
        $('#SelectTypesProduct').val(productDetails.typesProductId).trigger('change');
        $('#GlobalPrice').val(productDetails.globalPrice);
        $('#ProductImage').attr('src', productDetails.imageUrl || 'http://placehold.it/220x180');
        $('#SelectProductInformation150').empty().append(new Option(productDetails.productName, productDetails.m, true, true)).trigger('change');
        $('#modalButton').prop('disabled', true);
        updateCodeField();  // Update QR code or other fields if necessary
    }
}

// Detect input event for manual typing
$('#product12').on('input', function () {
    clearTimeout(typingTimer);

    const productId = $(this).val().trim();

    if (productSelectedFromAutocomplete) {
        productSelectedFromAutocomplete = false;  // Reset flag after selection
        return;  // Skip if product was selected from autocomplete
    }

    // Start the debounce timer to wait before checking availability
    if (productId.length >= 2) {
        typingTimer = setTimeout(() => {
            checkProductAvailability(productId);  // Check availability after delay
        }, typingDelay);
    } else {
        clearProductDetails();  // Clear if input length is < 2
    }
});

$(document).ready(function () {
    let productSelectedFromAutocomplete = false;  // Track whether product is selected from autocomplete

    $("#product12").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/admin/Order/GetProductSuggestions',
                type: 'GET',
                data: { query: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.model,
                            value: item.model
                        };
                    }));
                },
                error: function () {
                    alert('Error retrieving data');
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            productSelectedFromAutocomplete = true;  // Flag that product was selected from autocomplete
            $('#product12').val(ui.item.value);  // Set selected value in the input box
            populateProductDetails(ui.item);
            // Populate product details
            clearTimeout(typingTimer);  // Clear typing timer as selection is made
            // Ensure product availability check is called just like in manual input
            checkProductAvailability(ui.item.value);  // Trigger the check
            return false;
        }
    });
});

$('#product12').on('input', function () {
    clearTimeout(typingTimer);

    const productId = $(this).val().trim();

    if (productSelectedFromAutocomplete) {
        productSelectedFromAutocomplete = false;  // Reset flag after selection
        return;  // Skip if product was selected from autocomplete
    }

    // Start the debounce timer to wait before checking availability
    if (productId.length >= 2) {
        typingTimer = setTimeout(() => {
            checkProductAvailability(productId);  // Check availability after delay
        }, typingDelay);
    } else {
        clearProductDetails();  // Clear if input length is < 2
    }
});


function GetUPC1(val) {
        //const apiUrl = `@Url.Action("GetUPC", "Order", new {area = "Admin"})?value=${val}`;
    const apiUrl = `/admin/Order/GetUPC?value=${val}`;

$.ajax({
        url: apiUrl,
        type: 'GET',
         success: function (data) {
             if (data) {
             console.log("DData :", data);
             $('#UPCField').val(data.upc || "");
             $('#MakField').val(data.make || "");
             $('#UPCProduct').val(data.upc || "");
             $('#GlobalPrice').val(data.globalPrice || "");
             $('#UPCField').trigger('keyup');
                        } else {
            console.error("Error retrieving UPC.");
                        }
                    },
         error: function (xhr, status, error) {
            console.error("Error: " + error);
              }
         });
}



$(document).ready(function () {
    $('#SelectProductInformation150').on('change', function () {
        console.log("SelectProductInformation150 Change");
        if (this.value === '') {
            console.error('يرجى اختيار خيار صالح.');
        } else {
            GetUPC1(this.value);
        }
    });

});

function CreateBarCode1(text) {
    const apiUrl = `/admin/ProductInformation/GenerateBarcode?text=${encodeURIComponent(text)}`;

    $.ajax({
        url: apiUrl,
    type: 'GET',
    xhrFields: {
        responseType: 'blob'
                },
    success: function (data) {
                    if (data) {
                        var imageUrl = URL.createObjectURL(data);
    $('#BarCode11111111').attr('src', imageUrl);
    $('UPCProduct').text(text);
                    } else {
        console.error("Error generating barcode.");
                    }
                },
    error: function (xhr, status, error) {
        console.error("Error: " + error);
                }
            });
        }


$('#UPCField').on('change keyup', function () {
        var upc = this.value;
        CreateBarCode1(upc);
    });

//////////////////////////////////////////////////////////////////////////////
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

function updateQRCodeForxxx() {
    var code = $('#Qrcode').val();
    if (code) {
        $('#QRCodeImage11').attr('src', `/admin/WareHouse/GenerateQRCode?text=${encodeURIComponent(code)}`);
    } else {
        $('#QRCodeImage11').attr('src', '');
    }
}



// Function to update the Code field
function updateCodeField() {
    var Merchant = $('#SelectMerchant option:selected').text();
    var WareHouse = $('#SelectWareHouse option:selected').text();
    var BondType = $('#SelectBondType option:selected').text();
    var WareHouseBranch = $('#SelectWareHouseBranch option:selected').text();
    var ProductInformation = $('#SelectProductInformation150 option:selected').text();
    var sellingPrice = $('#sellingPrice').val();
    var PurchaseOrderNoumber = $('#PurchaseOrderNoumber11').val();
    var GlobalPr = $('#GlobalPrice').val();
    var QouantityIn = $('#QuantityIn').val();
    var SpecialSalePrice = $('#SpecialSalePrice').val();

    var randomString = generateRandomString(5);

    if (Merchant && WareHouse) {
        var code = Merchant + '-' + WareHouse + '-' + BondType + '-' + WareHouseBranch + '-' + ProductInformation + '-' + 'selling Price:' + sellingPrice + '-' + 'Purchase Order Noumber:' + PurchaseOrderNoumber + '-' + 'GlobalPr' + GlobalPr + '-' + 'Qouantity In:' + QouantityIn + '-' + 'Special Sale Price:' + SpecialSalePrice + '-' + randomString;
        $('#Qrcode').val(code);
        updateQRCodeForxxx();
    }
}



// Bind change events to WareHouseType and Description fields

$('#QuantityIn, #SpecialSalePrice, #GlobalPrice, #PurchaseOrderNoumber11, #sellingPrice, #SelectProductInformation150, #SelectWareHouseBranch, #SelectBondType, #SelectWareHouse, #SelectMerchant').on('change keyup', function () {
    updateCodeField();
});
function printWarehouseDetails1() {
    var Merchant = $('#SelectMerchant option:selected').text();
    var WareHouse = $('#SelectWareHouse option:selected').text();
    var BondType = $('#SelectBondType option:selected').text();
    var WareHouseBranch = $('#SelectWareHouseBranch option:selected').text();
    var ProductInformation = $('#SelectProductInformation150 option:selected').text();
    var sellingPrice = $('#sellingPrice').val();
    var PurchaseOrderNoumber = $('#PurchaseOrderNoumber11').val();
    var GlobalPr = $('#GlobalPrice').val();
    var QouantityIn = $('#QuantityIn').val();
    var PurchasePrice = $('#PurchasePrice').val();
    var SpecialSalePrice = $('#SpecialSalePrice').val();
    var qrCodeSrc = $('#QRCodeImage11').attr('src');
    var bar = $('#BarCode11111111').attr('src');

    var upc = $('#UPCField').val();


    var url1 = `/admin/Order/PrintWareHouseDetails`;

    // ترميز مكونات URI للتعامل مع الأحرف الخاصة
    url = `${url1}?Merchant=${encodeURIComponent(Merchant)}&WareHouse=${encodeURIComponent(WareHouse)}&BondType=${encodeURIComponent(BondType)}&PurchaseOrderNoumber=${encodeURIComponent(PurchaseOrderNoumber)}&ProductInformation=${encodeURIComponent(ProductInformation)}&WareHouseBranch=${encodeURIComponent(WareHouseBranch)}&sellingPrice=${encodeURIComponent(sellingPrice)}&QouantityIn=${encodeURIComponent(QouantityIn)}&PurchasePrice=${encodeURIComponent(PurchasePrice)}&SpecialSalePrice=${encodeURIComponent(SpecialSalePrice)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}&bar=${encodeURIComponent(bar)}&upc=${encodeURIComponent(upc)}`;


    // إرسال طلب GET للحصول على محتوى الطباعة
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
        alert("Failed to load print content. Please try again.");
    });
}

$(document).ready(function () {
    var merchantValue = $('#MerchantComm').text();
    if (merchantValue) {
        $('#SelectMerchant').val(merchantValue).trigger('change');
    } else {
        console.log("Merchant value is empty or null");
    }
});