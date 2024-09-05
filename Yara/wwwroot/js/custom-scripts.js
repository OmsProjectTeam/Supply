// ================== COMMON ===================
var loadFile = function (event) {
    var image = document.getElementById('output');
    image.src = URL.createObjectURL(event.target.files[0]);
};
$("#url").val(window.location.href);

$(function () {
    $("#example2").DataTable().fnDestroy();
    $('#example2').DataTable({
        "paging": false,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
    });
});

$(function () {
    $("#example3").DataTable().fnDestroy();
    $('#example3').DataTable({
        "paging": false,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
    });
});

$(function () {
    $("#example4").DataTable().fnDestroy();
    $('#example4').DataTable({
        "paging": false,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
    });
});

$(function () {
    $("#example5").DataTable().fnDestroy();
    $('#example5').DataTable({
        "paging": false,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
    });
});

//$(document).ready(function () {
//    function updateQRCode() {
//        $('.QRCodeImage').each(function () {
//            var code = $(this).closest('tr').find('.Cod').text().trim();

//            console.log(code);
//            if (code) {
//                console.log(code);
//                $(this).attr('src', '@Url.Action("GenerateQRCode", "ProductInformation")?text=' + encodeURIComponent(code));
//            } else {
//                console.log("code");
//                $(this).attr('src', '');
//            }
//        });
//    }
//});

$(document).ready(function () {
    // Function to truncate text to a specified length
    function truncateText(text, maxLength) {
        if (text.length > maxLength) {
            return text.substring(0, maxLength) + '...'; // Add ellipsis if text exceeds maxLength
        } else {
            return text;
        }
    }

    // Loop through each table cell with class 'truncate-50'
    $('.truncate-50').each(function () {
        var text = $(this).text(); // Get the text content of the cell
        var truncatedText = truncateText(text, 100); // Truncate the text to 50 characters
        $(this).text(truncatedText); // Set the truncated text back to the cell
    });
});

$(document).ready(function () {
    $('.select2_1').select2({
        placeholder: "Select an option",
        allowClear: true
    });

    $('#merchant-select').change(function () {
        var userId = $(this).val();
        if (userId) {
            $.getJSON('@Url.Action("GetUserDetails", "CustomerMessages")', { id: userId }, function (data) {
                $('input[name="CustomerMessages.CompanyName"]').val(data.companyName);
                $('input[name="CustomerMessages.PhoneCompany"]').val(data.phoneCompany);
                $('input[name="CustomerMessages.PhoneCompanySecand"]').val(data.phoneCompanySecand);
                $('input[name="CustomerMessages.EmailCompany"]').val(data.emailCompany);

            });
        }
    });
});

$(document).ready(function () {
    $('#city-select').change(function () {
        var cityId = $(this).val();
        if (cityId) {
            $.getJSON('@Url.Action("GetAreasByCity", "CityDeliveryTariffs")', { cityId: cityId }, function (data) {
                var $areaSelect = $('#area-select');
                $areaSelect.empty();
                $.each(data, function (index, item) {
                    $areaSelect.append($('<option>', {
                        value: item.id,
                        text: item.description
                    }));
                });
            });
        } else {
            $('#area-select').empty();
        }
    });
});

// Bind change event to the selected company
$('#SelectFAQ').change(function () {
    console.log("Selected FAQ changed");
    updateCosting();
});
// ================== COMMON ===================

// ================== CHAT ===================
let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message, pathImg, img, time) {
    appendMessage(user, message, pathImg, img, time);
});

async function sendMessage() {
    const message = document.getElementById("messageInput1").value;
    const to = document.getElementById("sendTo").value;

    if (!to) {
        console.error("Recipient name (to) is null or undefined. Cannot send message.");
        alert("Please select a user to chat with.");
        return;
    }

    const fileInput = document.getElementById("ImgSend");
    let filePath = null;
    if (fileInput && fileInput.files.length > 0) {
        const file = fileInput.files[0];
        try {
            filePath = await uploadFile(file);
        } catch (error) {
            console.error("Error uploading file:", error);
            return;
        }
    }

    document.getElementById("messageInput1").value = "";

    connection.invoke("SendMessageToClients", message, to, filePath).then(() => {
        // Append the message to the message list in the UI
        appendMessage('@ViewBag.UserId', message, filePath, '@ViewBag.img', new Date().toLocaleTimeString());
    }).catch(function (err) {
        console.error("Error sending message:", err.toString());
    });
}

async function uploadFile(file) {
    const formData = new FormData();
    formData.append("file", file);

    const response = await fetch("/Admin/chat/uploadFile", {
        method: "POST",
        body: formData
    });

    if (response.ok) {
        const data = await response.json();
        return data.filePath;
    } else {
        throw new Error("File upload failed");
    }
}

function appendMessage(user, message, pathImg, img, time) {
    const messageList = document.getElementById("messagesList");
    const isSender = user === '@ViewBag.UserId'; // Compare with the current user's ID or name
    const messageDiv = document.createElement("div");
    messageDiv.classList.add("message", isSender ? "sent" : "received");

    let content = `<div class="message-content">${message}</div><div class="message-time">${time}</div>`;

    // If there is an image attached to the message, add it to the content
    if (pathImg) {
        content = `<img src="${pathImg}" alt="Image message" class="message-image" />` + content;
    }

    messageDiv.innerHTML = content;
    messageList.appendChild(messageDiv);
    messageList.scrollTop = messageList.scrollHeight; // Scroll to the bottom after adding the message
}

connection.start().catch(function (err) {
    return console.error(err.toString());
});

window.onload = function () {
    scrollToBottom();
}

function scrollToBottom() {
    const messagesList = document.getElementById("messagesList");
    messagesList.scrollTop = messagesList.scrollHeight;
}
// ================== CHAT ===================

// ================== ADD ORDER ===================
$(document).ready(function () {
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

    // Bind change events to WareHouseType and Description fields

    $('#sellingPrice').on('change keyup', function () {
        updateCodeField();
    });


    function updateQRCode() {
        var code = $('#Qrcode').val();
        if (code) {
            $('#QRCodeImage').attr('src', '@Url.Action("GenerateQRCode", "Order")?text=' + encodeURIComponent(code));
        } else {
            $('#QRCodeImage').attr('src', '');
        }
    }

    // Function to update the Code field
    function updateCodeField() {
        var Merchant = $('#SelectMerchant option:selected').text();
        var WareHouse = $('#SelectWareHouse option:selected').text();
        var WareHouseBranch = $('#SelectWareHouseBranch option:selected').text();
        var ProductInformation = $('#SelectProductInformation option:selected').text();
        var sellingPrice = $('#sellingPrice').val();

        var randomString = generateRandomString(5);

        if (Merchant && WareHouse) {
            var code = Merchant + WareHouse + WareHouseBranch + ProductInformation + sellingPrice + randomString;
            $('#Qrcode').val(code);
            updateQRCode();
        }
    }
});

$(document).ready(function () {
    function printWarehouseDetails1() {
        var Merchant = $('#SelectMerchant option:selected').text();
        var WareHouse = $('#SelectWareHouse option:selected').text();
        var WareHouseBranch = $('#SelectWareHouseBranch option:selected').text();
        var ProductInformation = $('#SelectProductInformation option:selected').text();
        var sellingPrice = $('#sellingPrice').val();
        var qrCodeSrc = $('#QRCodeImage').attr('src');

        var url1 = '@Url.Action("PrintWareHouseDetails", "Order")';

        // ترميز مكونات URI للتعامل مع الأحرف الخاصة
        url = `${url1}?Merchant=${encodeURIComponent(Merchant)}&WareHouse=${encodeURIComponent(WareHouse)}&ProductInformation=${encodeURIComponent(ProductInformation)}&WareHouseBranch=${encodeURIComponent(WareHouseBranch)}&sellingPrice=${encodeURIComponent(sellingPrice)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}`;

        // إرسال طلب GET للحصول على محتوى الطباعة
        $.get(url, function (data) {
            var printWindow = window.open('', '_blank');
            printWindow.document.open();
            printWindow.document.write(data);
            printWindow.document.close();

            // التأكد من أن المحتوى قد تم تحميله بالكامل قبل الطباعة
            printWindow.onload = function () {
                printWindow.print();
            };
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error: ", textStatus, errorThrown);
            alert("Failed to load print content. Please try again.");
        });
    }
});

$(document).ready(function () {
    let typingTimer;                // Timer identifier
    const typingDelay = 700;        // Time delay in milliseconds (e.g., 500ms)

    $('#product12').on('input', function () {
        clearTimeout(typingTimer);  // Clear the previous timer
        const productId = $(this).val().trim();

        if (productId) {
            typingTimer = setTimeout(function () {
                checkProductAvailability(productId);
            }, typingDelay);
        } else {
            // If the input is cleared, disable the modal button
            $('#modalButton').prop('disabled', true);
        }
    });

    function checkProductAvailability(productId) {
        $.ajax({
            url: '@Url.Action("GetProductDetailsForOrder", "Order")',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                if (data && data.productCategoryId > 0) {
                    // Populate the fields with the received data
                    $('#ProductName').val(productId);  // Set the product name input
                    $('#SelectProductCategory').val(data.productCategoryId).trigger('change');
                    $('#SelectBondType').val(data.bondTypeId).trigger('change');
                    $('#SelectTypesProduct').val(data.typesProductId).trigger('change');
                    $('#GlobalPrice').val(data.globalPrice);
                    $('#ProductImage').attr('src', data.imageUrl || 'http://placehold.it/220x180');

                    // Populate or update the Product Information dropdown
                    $('#SelectProductInformation').empty(); // Clear the dropdown first
                    $('#SelectProductInformation').append(new Option(data.productName, productId, true, true)).trigger('change');

                    // Disable the modal button since a product was found
                    $('#modalButton').prop('disabled', true);
                } else {
                    // Show SweetAlert and enable the modal button
                    Swal.fire({
                        icon: 'warning',
                        title: 'No product available',
                        text: 'Create a new one, please click the modal button',
                        showConfirmButton: false,
                        timer: 3000,
                        position: 'top-end',
                        toast: true
                    });

                    // Set the search text in the modal product name field
                    $('#ProductName').val(productId);

                    // Enable the modal button
                    $('#modalButton').prop('disabled', false);
                    openModal();
                }
            },
            error: function (xhr, status, error) {
                console.error("Error fetching product details: ", error);
                $('#ProductImage').attr('src', 'http://placehold.it/220x180');
                $('#GlobalPrice').val("0.00");

                // Disable the modal button if there is an error
                $('#modalButton').prop('disabled', true);
            }
        });
    }


    // Function to open the modal
    function openModal() {
        document.getElementById("customModal").style.display = "block";
    }

    // Function to close the modal
    function closeModal() {
        // document.getElementById("customModal").style.display = "none";
        var previousSearchText = '';

        function openModal() {
            previousSearchText = $('#product12').val(); // Store the current search text
            document.getElementById("customModal").style.display = "block";
        }

        function closeModal() {
            document.getElementById("customModal").style.display = "none";
            $('#product12').val(previousSearchText); // Restore the search text
        }

        // Close the modal if the user clicks outside of it
        window.onclick = function (event) {
            var modal = document.getElementById("customModal");
            if (event.target == modal) {
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
                    $('#product12').val(previousSearchText);
                },
                error: function (response) {
                    console.error("Error during form submission", response);
                    // Handle the error (display a message, etc.)
                }
            });
        });
    }
});

$(document).ready(function () {
    var previousSearchText = '';

    function openModal() {
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
});

$(document).ready(function () {
    $("#modelInput").on("change", function () {
        var model = $(this).val(); // Get the model input value

        if (model) {
            $.ajax({
                url: '@Url.Action("FetchImageByModel", "ProductInformation", new { area = "Admin" })',
                type: 'GET',
                data: { model: model }, // Send the model as a parameter
                success: function (response) {
                    if (response.success) {
                        $("#output").attr("src", response.imageUrl).show(); // Display the fetched image
                        $("#imageUrlInput").val(response.imageUrl); // Set the image URL in the hidden input field
                    } else {
                        alert(response.message); // Show an alert if there was an issue fetching the image
                        $("#output").hide(); // Hide the image element if there's no image to display
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error fetching the image."); // Alert the user about the error
                    $("#output").hide(); // Hide the image element in case of an error
                }
            });
        }
    });

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
    function updateCodeField() {
        var Category = $('#SelectCategory option:selected').text();
        var TypesProduct = $('#SelectTypesProduct option:selected').text();
        var Product = $('#ProductName').val();
        var Model = $('#modelInput').val();
        var UPC = $('#UPC').val();

        var randomString = generateRandomString(5);

        if (Category && TypesProduct && Product && Model && UPC) {
            var code = randomString + Category + TypesProduct + Product + Model + UPC;
            $('#Code').val(code);
            updateQRCode(); // Update QR code when code is updated
        }
    }

    // Bind change events to WareHouseType and Description fields
    $('#SelectCategory, #SelectTypesProduct, #ProductName, #modelInput, #UPC').on('change keyup', function () {
        updateCodeField();
    });

    // Update the QR code image
    function updateQRCode() {
        var code = $('#Code').val();
        if (code) {
            $('#QRCodeImage').attr('src', '@Url.Action("GenerateQRCode", "ProductInformation")?text=' + encodeURIComponent(code));
        } else {
            $('#QRCodeImage').attr('src', '');
        }
    }

    // Bind change event to Code field to update QR code
    $(document).ready(function () {
        $('#Code').on('change', function () {
            var code = $('#Code').val(); // احصل على قيمة حقل الإدخال
            $('#QRCode').val(code); // تحديث قيمة الـ textarea
        });
    });

    function printWarehouseDetails() {
        var Category = $('#SelectCategory option:selected').text();
        var TypesProduct = $('#SelectTypesProduct option:selected').text();
        var Product = $('#ProductName').val();
        var Model = $('#modelInput').val();
        var UPC = $('#UPC').val();
        var qrCodeSrc = $('#QRCodeImage').attr('src');

        var url = '@Url.Action("PrintWareHouseDetails", "ProductInformation")';

        // Encode URI components to handle special characters
        url = `${url}?Category=${encodeURIComponent(Category)}&TypesProduct=${encodeURIComponent(TypesProduct)}&Product=${encodeURIComponent(Product)}&Model=${encodeURIComponent(Model)}&UPC=${encodeURIComponent(UPC)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}`;

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

    // Set the URL of the form to the current page URL
    $("#url").val(window.location.href);
});

$(document).ready(function () {
    $('#exampleModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this)
        modal.find('.modal-title').text('New message to ' + recipient)
        modal.find('.modal-body input').val(recipient)
    })
});

$(document).ready(function () {
    function openModal() {
        document.getElementById("customModal").style.display = "block";
    }

    // Function to close the modal
    function closeModal() {
        document.getElementById("customModal").style.display = "none";
    }

    // Close the modal if the user clicks outside of it
    window.onclick = function (event) {
        var modal = document.getElementById("customModal");
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});

$(document).ready(function () {
    $(document).ready(function () {
        // Sync the product12 input with the table search box
        $('#product12').on('input', function () {
            var searchText = $(this).val();
            var searchBox = $('#example_filter input[type="search"]');
            searchBox.val(searchText);

            // Trigger keyup event on the search box
            searchBox.trigger('keyup');

            // Simulate 'Enter' key press to start the search
            var e = jQuery.Event('keypress');
            e.which = 13; // Character code for Enter key
            searchBox.trigger(e);
        });
    });
});
// ================== ADD ORDER ===================

// ================== PRODUCT INFORMATION ===================
$(document).ready(function () {
    var loadFile = function (event) {
        var image = document.getElementById('output');
        image.src = URL.createObjectURL(event.target.files[0]);
    };

    // Event listener for when the model input field changes
    $("#modelInput").on("change", function () {
        var model = $(this).val(); // Get the model input value

        if (model) {
            $.ajax({
                url: '@Url.Action("FetchImageByModel", "ProductInformation", new { area = "Admin" })',
                type: 'GET',
                data: { model: model }, // Send the model as a parameter
                success: function (response) {
                    if (response.success) {
                        $("#output").attr("src", response.imageUrl).show(); // Display the fetched image
                        $("#imageUrlInput").val(response.imageUrl); // Set the image URL in the hidden input field
                    } else {
                        alert(response.message); // Show an alert if there was an issue fetching the image
                        $("#output").hide(); // Hide the image element if there's no image to display
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error fetching the image."); // Alert the user about the error
                    $("#output").hide(); // Hide the image element in case of an error
                }
            });
        }
    });

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
    function updateCodeField() {
        var Category = $('#SelectCategory option:selected').text();
        var TypesProduct = $('#SelectTypesProduct option:selected').text();
        var Product = $('#ProductName').val();
        var Model = $('#modelInput').val();
        var UPC = $('#UPC').val();

        var randomString = generateRandomString(5);

        if (Category && TypesProduct && Product && Model && UPC) {
            var code = randomString + Category + TypesProduct + Product + Model + UPC;
            $('#Code').val(code);
            updateQRCode(); // Update QR code when code is updated
        }
    }

    // Bind change events to WareHouseType and Description fields
    $('#SelectCategory, #SelectTypesProduct, #ProductName, #modelInput, #UPC').on('change keyup', function () {
        updateCodeField();
    });

    // Update the QR code image
    function updateQRCode() {
        var code = $('#Code').val();
        if (code) {
            $('#QRCodeImage').attr('src', '@Url.Action("GenerateQRCode", "ProductInformation")?text=' + encodeURIComponent(code));
        } else {
            $('#QRCodeImage').attr('src', '');
        }
    }

    // Bind change event to Code field to update QR code
    $(document).ready(function () {
        $('#Code').on('change', function () {
            var code = $('#Code').val(); // احصل على قيمة حقل الإدخال
            $('#QRCode').val(code); // تحديث قيمة الـ textarea
        });
    });

    function printWarehouseDetails() {
        var Category = $('#SelectCategory option:selected').text();
        var TypesProduct = $('#SelectTypesProduct option:selected').text();
        var Product = $('#ProductName').val();
        var Model = $('#modelInput').val();
        var UPC = $('#UPC').val();
        var qrCodeSrc = $('#QRCodeImage').attr('src');

        var url = '@Url.Action("PrintWareHouseDetails", "ProductInformation")';

        // Encode URI components to handle special characters
        url = `${url}?Category=${encodeURIComponent(Category)}&TypesProduct=${encodeURIComponent(TypesProduct)}&Product=${encodeURIComponent(Product)}&Model=${encodeURIComponent(Model)}&UPC=${encodeURIComponent(UPC)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}`;

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

    // Set the URL of the form to the current page URL
    $("#url").val(window.location.href);
});
// ================== PRODUCT INFORMATION ===================


// ================== WAREHOUSE ===================
$(document).ready(function () {
    console.log("Document is ready");
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
    function updateCodeField() {
        var warehouseType = $('#SelectWareHouseType option:selected').text();
        var description = $('#Description').val();
        var randomString = generateRandomString(5);

        if (warehouseType && description) {
            var code = warehouseType + description + randomString;
            $('#Code').val(code);
            updateQRCode(); // Update QR code when code is updated
        }
    }

    // Bind change events to WareHouseType and Description fields
    $('#SelectWareHouseType, #Description').on('change keyup', function () {
        updateCodeField();
    });

    // Update the QR code image
    function updateQRCode() {
        var code = $('#Code').val();
        if (code) {
            $('#QRCodeImage').attr('src', '@Url.Action("GenerateQRCode", "WareHouse")?text=' + encodeURIComponent(code));
        } else {
            $('#QRCodeImage').attr('src', '');
        }
    }

    // Bind change event to Code field to update QR code
    $('#Code').on('change keyup', function () {
        updateQRCode();
    });

    // Update the URL field with the current page URL
    $("#url").val(window.location.href);
});

function printWarehouseDetails() {
    var warehouseType = $('#SelectWareHouseType option:selected').text();
    var description = $('#Description').val();
    var qrCodeSrc = $('#QRCodeImage').attr('src');

    var url = '@Url.Action("PrintWareHouseDetails", "WareHouse")';

    // Encode URI components to handle special characters
    url = `${url}?warehouseType=${encodeURIComponent(warehouseType)}&description=${encodeURIComponent(description)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}`;

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
// ================== WAREHOUSE ===================

// ================== CLIENT AREA ===================

// ===================== FAQ =====================
$(document).ready(function () {
    $("#accordion-1").accordion({
        collapsible: true,
        active: false
    });
});
// ===================== FAQ =====================
// ================== CLIENT AREA ===================