
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
<<<<<<< Updated upstream
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
=======
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

$(function () {
    $("#example7").DataTable().fnDestroy();
    $('#example7').DataTable({
        "paging": false,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
    });
});

$(document).ready(function () {
>>>>>>> Stashed changes
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


