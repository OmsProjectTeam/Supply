

function generateGUIDChar() {
    let chars = '0123456789';
    let guid = '';
    for (let i = 0; i < 11; i++) {
        guid += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return '10' + guid;
}


function CreateBarCode(text) {

    const apiUrl = `/Admin/ProductInformationLowes/GenerateBarcode?text=${encodeURIComponent(text)}`;

    $.ajax({
        url: apiUrl,
        type: 'GET',
        xhrFields: {
            responseType: 'blob' // تعيين نوع الاستجابة إلى Blob
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


$(document).ready(function () {
    $('#UPC12').on('change keyup', function () {
        var upc = this.value;
        CreateBarCode(upc);
    });


});

function printWarehouseDetails() {
    var Category = $('#SelectCategory option:selected').text();
    var TypesProduct = $('#SelectTypesProduct option:selected').text();
    var Product = $('#ProductName').val() || "aaaaaaa";
    var Model = $('#modelInput').val();
    var UPC = $('#UPC').val();
    var qrCodeSrc = $('#QRCodeImage123').attr('src');
    var bar = $('#BarCode0936').attr('src');

    var url1 = '/Admin/ProductInformation/PrintWareHouseDetails';

    url = `${url1}?Category=${encodeURIComponent(Category)}&TypesProduct=${encodeURIComponent(TypesProduct)}&Product=${encodeURIComponent(Product)}&Model=${encodeURIComponent(Model)}&UPC=${encodeURIComponent(UPC)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}&bar=${encodeURIComponent(bar)}`;


    $.get(url, function (data) {
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


$('#UPC12, #storeSku ,#storeSoSku, #modelNumber, #brand, #ProductName').on('change keyup', function () {
    updateCodeFieldmodal();
});





function generateRandomStringrr(length) {
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var result = '';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

// Bind change events to WareHouseType and Description fields

function updateQRCodeForAA() {
    var code = $('#CodeForQR').val();
    if (code) {
        
        $('#QRCodeImage').attr('src', '/Admin/WareHouse/GenerateQRCode?text=' + encodeURIComponent(code));
    } else {
        $('#QRCodeImage').attr('src', '');
    }
}

// Function to update the Code field
function updateCodeFieldmodal() {
    var Category = $('#SelectCategory option:selected').text();
    var HtmlTitle = $('#ScrapingHtmlTitle option:selected').text();
    var TypesProduct = $('#SelectTypesProduct option:selected').text();
    var Product = $('#ProductName').val() || "No Name";
    var Model = $('#modelInput').val

    var ModNumber = $('#modelNumber').val();
    var StoreSku = $('#storeSku').val();
    var StoreSoSku = $('#storeSoSku').val();
    var Brand = $('#brand').val();

    var UPC = $('#UPC12').val();

    var randomString = generateRandomStringrr(5);

    if (Category && TypesProduct) {

        var code = randomString + Category + TypesProduct + HtmlTitle + Product + Model + ModNumber + StoreSku + StoreSoSku + Brand + UPC;

        $('#CodeForQR').val(code);
        updateQRCodeForAA(); // Update QR code when code is updated
    }

}

// Close the modal if the user clicks outside of it
window.onclick = function (event) {
    var modal = document.getElementById("customModal");
    if (event.target == modal) {
        modal.style.display = "none";
    }
};

// Function to populate product name in the modal based on the model number
function populateProductNameInModal(model, brand1) {  
    if (model) {
        $.ajax({
            url: '/admin/Order/FetchImageByModelOrder',  
            type: 'GET',
            data: { model: model, breand: brand1 }, 
            success: function (response) {
                if (response.success) {
                    $("#ProductName").val(response.productName);  // **NEW CHANGE**: Populate the product name in the modal
                    $("#output").attr('src', response.imageUrl);
                    $("#storeSku").val(response.storeSku);
                    $("#storeSoSku").val(response.storeSoSku);
                    $("#brand").val(response.brand);
                    $("#modelNumber").val(response.modelNumber);
                    var xxxxx = response.imageUrl;
                    $("#xaxaxaxaxa").val(xxxxx);
                } else {
                    alert(response.message);  // Show an alert if there was an issue
                    $("#ProductName").val('');  // Clear product name if not found
                      // Clear product name if not found
                }
            },
            error: function () {
                alert("Error fetching the product name.");  // Alert the user about the error
                $("#MyProduct").val('');  // Clear product name in case of an error
            }
        });
    }
}


// Clear product details if no product is selected
function clearProductDetails() {
    $('#ProductName').val('');
    $('#SelectProductCategory').val('').trigger('change');
    $('#SelectBondType').val('').trigger('change');
    $('#SelectTypesProduct').val('').trigger('change');
    $('#GlobalPrice').val('');
    $('#ProductImage').attr('src', 'http://placehold.it/220x180');
}

// Modal functions

// Close modal if clicking outside
window.onclick = function (event) {
    if (event.target == $('#customModal')[0]) {
        closeModal();
    }
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

var previousSearchText = '';
// Close modal if clicking outside
window.onclick = function (event) {
    if (event.target == $('#customModal')[0]) {
        closeModal();
    }
}

// Modal functions
var previousSearchText = '';

function openModal() {

    $('#ScrapingHtmlTitle').val('');
    var productId = $('#product12').val();
    $('#modelInput').val(productId).trigger('change');
    previousSearchText = $('#product12').val(); // Store the current search text
    $('#product12').data('previous-value', previousSearchText); // Store it in the data attribute
    document.getElementById("customModal").style.display = "block";
    //var brand = $('#BrandName123 option:selected').text();


    //populateProductNameInModal(productId, brand);

}

$('#ScrapingHtmlTitle').on('change', function () {
    var modell = $('#modelInput').val();
    var brand = $('#ScrapingHtmlTitle option:selected').text();
    populateProductNameInModal(modell, brand);
});

function closeModal() {
    document.getElementById("customModal").style.display = "none";
    $('#product12').val($('#product12').data('previous-value')); // Restore the search text from the data attribute
}


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
const typingDelay = 1500;  // 3 seconds delay before triggering search

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



function FetchPrice() {

    var mm = $('#mmoodel').val();
    var mk = $('#ScrapingHtmlTitle1212').val();
    $.ajax({
        url: '/admin/Order/FetchGlobalPrice',
        type: 'GET',
        data: { model: mm, Make: mk },
        success: function (data) {
            if (data) {
                $('#GlobalPrice').val(data);
            } else {
                console.log('No data returned');
            }
        },
        error: function (err) {
            console.error("Error fetching price: ", err);
        }
    });
}


// Function to populate product details after selecting a product
function populateProductDetails(productDetails) {

    console.log("populateProductDetails xxxxxxxxxxxxxxxxxxxxxxxxxxxx");
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
        $('#Brand').val(productDetails.brand);
        $('#ScrapingHtmlTitle1212').val(productDetails.scrapingHtmlTitle);
        $('#sku1212').val(productDetails.storeSku);
        $('#UPCx').val(productDetails.uPC).trigger('change');
        $('#soSku').val(productDetails.sstoreSoSku);
        $('#mmoodel').val(productDetails.mmodel);
        var x = generateGUIDChar();
        $('#UPCxv').val(x);

        FetchPrice();

        updateCodeField();  // Update QR code or other fields if necessary
    }

}


function checkProductAvailability(productId) {
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
                console.log("openModal in in in  checkProductAvailability ");

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
    let productSelectedFromAutocomplete = false;  // لتتبع ما إذا تم تحديد المنتج من الـ autocomplete

    $("#product12").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/admin/Order/GetProductSuggestions',
                type: 'GET',
                data: { query: request.term },
                success: function (data) {
                    console.log(data);
                    response($.map(data, function (item) {
                        switch (item.matchingField) {
                            case "Qrcode":
                                return {
                                    label: item.qrcode,
                                    value: item.model
                                };
                            case "Model":
                                return {
                                    label: item.model,
                                    value: item.model
                                };
                            case "Brand":
                                return {
                                    label: item.brand,
                                    value: item.model
                                };
                            case "UPC":
                                return {
                                    label: item.upc,
                                    value: item.model
                                };
                            case "StoreSku":
                                return {
                                    label: item.storeSku,
                                    value: item.model
                                };
                            case "StoreSoSku":
                                return {
                                    label: item.storeSoSku,
                                    value: item.model
                                };
                            case "ProductName":
                                return {
                                    label: item.productName,
                                    value: item.model
                                };
                        }
                    }));
                },
                error: function () {
                    openModal();  // فتح النافذة في حال حدوث خطأ
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            productSelectedFromAutocomplete = true;  // الإشارة إلى أن المنتج تم اختياره
            $('#product12').val(ui.item.value);  // وضع القيمة المختارة في الحقل
            checkProductAvailability(ui.item.value);  // التحقق من توافر المنتج
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


$('#UPCx').on('change keyup', function () {
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
    var SpecialSalePrice = $('#SpecialSalePrice').val();

    var randomString = generateRandomString(5);

    if (Merchant && WareHouse) {
        var code = Merchant + '-' + WareHouse + '-' + BondType + '-' + WareHouseBranch + '-' + ProductInformation + '-' + 'selling Price:' + sellingPrice + '-' + 'Purchase Order Noumber:' + PurchaseOrderNoumber + '-' + 'GlobalPr' + GlobalPr + '-' + '-' + 'Special Sale Price:' + SpecialSalePrice + '-' + randomString;
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
    } else {}
});
function searchAboutProduct(model) {

    $('#product12').val(model);

    checkProductAvailability(model);
    setTimeout(() => {
        document.getElementById('contentWrapper').style.display = 'block';
    }, 100);
}

$(document).ready(function () {
    var val = $('#AfterSave').text();
    console.log("AfterSave" + val);
    if (val) {
        searchAboutProduct(val);
    }
});
function PrintInvoice(IdPurchaseOrder) {

    var url = "/Admin/Order/printInvoce?IdPurchaseOrder=" + IdPurchaseOrder;

    // فتح صفحة الطباعة في نافذة جديدة
    window.open(url, '_blank');
}



$(document).ready(function () {

    var selectedValue = $('#SelectBondType option:selected').text();

    if (selectedValue == "Purchase order") {
        $('#SelectMerchantdiv').css("display", "block");
        $('#QuantityIndiv').css("display", "block");
        $('#printWarehouseDetails').css("display", "block");

        $('#SelectCustomersdiv').css("display", "none");
        $('#QuantityOutediv').css("display", "none");
        $('#Printinvoice').css("display", "none");
    }
    else {
        $('#SelectMerchantdiv').css("display", "none");
        $('#QuantityIndiv').css("display", "none");
        $('#printWarehouseDetails').css("display", "none");

        $('#SelectCustomersdiv').css("display", "block");
        $('#QuantityOutediv').css("display", "block");
        $('#Printinvoice').css("display", "block");
    }

    $('#SelectBondType').change(function () {

        var selectedValue1 = $('#SelectBondType option:selected').text();
        if (selectedValue1 == "Purchase order") {
            $('#SelectMerchantdiv').css("display", "block");
            $('#QuantityIndiv').css("display", "block");
            $('#printWarehouseDetails').css("display", "block");

            $('#SelectCustomersdiv').css("display", "none");
            $('#QuantityOutediv').css("display", "none");
            $('#Printinvoice').css("display", "none");
        }
        else {
            $('#SelectMerchantdiv').css("display", "none");
            $('#QuantityIndiv').css("display", "none");
            $('#printWarehouseDetails').css("display", "none");

            $('#SelectCustomersdiv').css("display", "block");
            $('#QuantityOutediv').css("display", "block");
            $('#Printinvoice').css("display", "block");
        }
    });
});
$(document).ready(function () {

    var selectedValue1 = $('#SelectBondType option:selected').text();

    if (selectedValue1 == "Purchase order") {

        var selectedValue = $('#SelectMerchant option:selected').val();
        $('#IdUserAAAA').val(selectedValue);
    }

    else {
        var selectedValue = $('#SelectCustomers option:selected').val();
        $('#IdUserAAAA').val(selectedValue);
    }

    $('#SelectBondType').change(function () {
        var selectedValue1 = $('#SelectBondType option:selected').text();

        if (selectedValue1 == "Purchase order") {
            var selectedValue = $('#SelectMerchant option:selected').val();
            $('#IdUserAAAA').val(selectedValue);
        }
        else {
            var selectedValue = $('#SelectCustomers option:selected').val();
            $('#IdUserAAAA').val(selectedValue);
        }
    });



    $('#SelectMerchant').change(function () {
        var selectedValue = $('#SelectMerchant option:selected').val();
        $('#IdUserAAAA').val(selectedValue);
    });

    $('#SelectCustomers').change(function () {
        var selectedValue = $('#SelectCustomers option:selected').val();
        $('#IdUserAAAA').val(selectedValue);
    });
});



function loadBranchData(wareHouseId) {
    $.ajax({
        url: '/admin/Order/LoadWareHouseBranches/' + wareHouseId,
        type: 'GET',
        success: function (data) {
            console.log(data);
            $('#SelectWareHouseBranch').empty();
            $.each(data, function (index, branch) {
                $('#SelectWareHouseBranch').append('<option value="' + branch.idBWareHouseBranch + '">' + branch.wareHouseBranchName + '</option>');
            });
        },
        error: function () {
            console.error("Error fetching branch details.");
        }
    });
}

$('#SelectWareHouse').change(function () {
    var id = $(this).val();
    loadBranchData(id);
});


$(document).ready(function () {
    $('#SelectWareHouse').trigger('change');
});


