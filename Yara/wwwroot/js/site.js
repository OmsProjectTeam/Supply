﻿///// الكيوأر كود  الخاص بال ماي وير هوس برنش 
//$(document).ready(function () {
//    function updateQRCode() {
//        $('.QRCodeImage').each(function () {
//            var code = $(this).siblings('.Code').text().trim();  // قم بإزالة المسافات الفارغة من النص
//            if (code) {
//                var qrCodeUrl = '@Url.Action("GenerateQRCode", "WareHouse")?text=' + encodeURIComponent(code);
//                $(this).attr('src', qrCodeUrl);
//            } else {
//                $(this).attr('src', '');  // في حال لم يكن هناك كود
//            }
//        });
//    }

//    // تحديث الـ QR Code عند النقر على أزرار التصفح
//    $(document).on('click', '.paginate_button', function () {
//        updateQRCode();
//    });

//    // استدعاء التحديث عند تحميل الصفحة
//    updateQRCode();
//});

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
$(function () {
    $("#example6").DataTable().fnDestroy();
    $('#example6').DataTable({
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
=======
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
var loadFile = function (event) {
    var image = document.getElementById('output');
    image.src = URL.createObjectURL(event.target.files[0]);
};
$("#url").val(window.location.href);







  

