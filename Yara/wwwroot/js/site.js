
$(function () {
    // Array of table IDs to initialize DataTables on
    var tableIds = ["example", "example1", "example2", "example3", "example4", "example5", "example6", "example7"];

    // Loop through each table ID and initialize DataTable
    $.each(tableIds, function (index, tableId) {
        // Destroy any existing DataTable instance on this table
        $("#" + tableId).DataTable().fnDestroy();

        // Initialize DataTable with options
        $('#' + tableId).DataTable({
            "paging": false,
            "lengthChange": false,
            "searching": false,
            "ordering": true,
            "info": true,
            "autoWidth": false
        });
    });
}
);

function updateQRCode() {
    $('.QRCodeImage').each(function () {
        var code = $(this).siblings('.Code').text();
        if (code) {
            $(this).attr('src', '@Url.Action("GenerateQRCode", "WareHouse")?text=' + encodeURIComponent(code));
        } else {
            $(this).attr('src', '');
        }
    });
}

function updateBar() {
    $('.BarCodeUPC').each(function () {
        var upc = $(this).siblings('.UPCFORPR').text();
        console.log(upc);
        if (upc) {
            $(this).attr('src', `@Url.Action("GenerateBarcode", "ProductInformation")?text=` + encodeURIComponent(upc));
        } else {
            $(this).attr('src', '');
        }
    });
}

$(document).ready(function () {
    updateQRCode();
    updateBar();
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
        var truncatedText = truncateText(text, 50); // Truncate the text to 50 characters
        $(this).text(truncatedText); // Set the truncated text back to the cell
    });
});


