$(document).ready(function () {
    // Initialize Select2 dropdowns
    $('.select2_1').select2({
        placeholder: "Select an option",
        allowClear: true
    });

    // Function to generate a random string
    function generateRandomString(length) {
        var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var result = '';
        for (var i = 0; i < length; i++) {
            result += characters.charAt(Math.floor(Math.random() * characters.length));
        }
        return result;
    }

    // Bind change event to WareHouseType and Description fields
    $('#QuantityIn').on('change keyup', function () {
        updateCodeField();
    });

    // Update the Code field based on selected inputs
    function updateCodeField() {
        var Merchant = $('#SelectMerchant option:selected').text();
        var WareHouse = $('#SelectWareHouse option:selected').text();
        var BondType = $('#SelectBondType option:selected').text();
        var WareHouseBranch = $('#SelectWareHouseBranch option:selected').text();
        var ProductInformation = $('#SelectProductInformation option:selected').text();
        var sellingPrice = $('#sellingPrice').val();
        var PurchaseOrderNoumber = $('#PurchaseOrderNoumber11').val();
        var GlobalPr = $('#GlobalPrice').val();
        var QouantityIn = $('#QuantityIn').val();
        var SpecialSalePrice = $('#SpecialSalePrice').val();

        var randomString = generateRandomString(5);

        if (Merchant && WareHouse) {
            var code = `${Merchant}-${WareHouse}-${BondType}-${WareHouseBranch}-${ProductInformation}-selling Price:${sellingPrice}-Purchase Order Noumber:${PurchaseOrderNoumber}-GlobalPr${GlobalPr}-Qouantity In:${QouantityIn}-Special Sale Price:${SpecialSalePrice}-${randomString}`;
            $('#Qrcode').val(code);
            updateQRCodeForxxx();
        }
    }

    // Update QRCode for warehouse
    function updateQRCodeForxxx() {
        var code = $('#Qrcode').val();
        if (code) {
            $('#QRCodeImage11').attr('src', generateQRCodeUrl + '?text=' + encodeURIComponent(code));
        } else {
            $('#QRCodeImage11').attr('src', '');
        }
    }

    // Print warehouse details
    function printWarehouseDetails1() {
        var Merchant = $('#SelectMerchant option:selected').text();
        var WareHouse = $('#SelectWareHouse option:selected').text();
        var BondType = $('#SelectBondType option:selected').text();
        var PurchaseOrderNoumber = $('#PurchaseOrderNoumber11').val();
        var ProductInformation = $('#SelectProductInformation option:selected').text();
        var WareHouseBranch = $('#SelectWareHouseBranch option:selected').text();
        var sellingPrice = $('#sellingPrice').val();
        var GlobalPr = $('#GlobalPrice').val();
        var QouantityIn = $('#QuantityIn').val();
        var PurchasePrice = $('#PurchasePrice').val();
        var SpecialSalePrice = $('#SpecialSalePrice').val();
        var qrCodeSrc = $('#QRCodeImage11').attr('src');

        var url = `${printWareHouseDetailsUrl}?Merchant=${encodeURIComponent(Merchant)}&WareHouse=${encodeURIComponent(WareHouse)}&BondType=${encodeURIComponent(BondType)}&PurchaseOrderNoumber=${encodeURIComponent(PurchaseOrderNoumber)}&ProductInformation=${encodeURIComponent(ProductInformation)}&WareHouseBranch=${encodeURIComponent(WareHouseBranch)}&sellingPrice=${encodeURIComponent(sellingPrice)}&QouantityIn=${encodeURIComponent(QouantityIn)}&PurchasePrice=${encodeURIComponent(PurchasePrice)}&SpecialSalePrice=${encodeURIComponent(SpecialSalePrice)}&qrCodeSrc=${encodeURIComponent(qrCodeSrc)}`;

        $.get(url, function (data) {
            var printWindow = window.open('', '_blank');
            printWindow.document.open();
            printWindow.document.write(data);
            printWindow.document.close();
            printWindow.onload = function () {
                printWindow.print();
            };
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert("Failed to load print content. Please try again.");
        });
    }

    // QR Code Update
    function updateQRCode() {
        $('.QRCodeImage').each(function () {
            var code = $(this).closest('tr').find('.Codee').text().trim();
            if (code) {
                $(this).attr('src', generateQRCodeUrl + '?text=' + encodeURIComponent(code));
            } else {
                $(this).attr('src', '');
            }
        });
    }

    // Sync product12 input with the table search box
    $('#product12').on('input', function () {
        var searchText = $(this).val();
        var searchBox = $('#example_filter input[type="search"]');
        searchBox.val(searchText);
        searchBox.trigger('keyup');
    });

    // Fetch image by model
    $("#modelInput").on("change", function () {
        var model = $(this).val();
        if (model) {
            $.ajax({
                url: fetchImageByModelUrl,
                type: 'GET',
                data: { model: model },
                success: function (response) {
                    if (response.success) {
                        $("#output").attr("src", response.imageUrl).show();
                        $("#imageUrlInput").val(response.imageUrl);
                    } else {
                        alert(response.message);
                        $("#output").hide();
                    }
                },
                error: function () {
                    alert("Error fetching the image.");
                    $("#output").hide();
                }
            });
        }
    });

    // Handle opening/closing of modals
    function openModal() {
        previousSearchText = $('#product12').val();
        $('#product12').data('previous-value', previousSearchText);
        $('#customModal').show();
    }

    function closeModal() {
        $('#customModal').hide();
        $('#product12').val($('#product12').data('previous-value'));
    }

    // Modal form submission handling
    $('form[asp-action="SaveModal"]').on('submit', function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                closeModal();
                $('#product12').val($('#product12').data('previous-value'));
            }
        });
    });

    // Pagination button click handler
    $(document).on('click', '.paginate_button', function () {
        updateQRCode();
    });

    // Load file for previewing image
    var loadFile = function (event) {
        var image = document.getElementById('output');
        image.src = URL.createObjectURL(event.target.files[0]);
    };
});
