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
        var truncatedText = truncateText(text, 100); // Truncate the text to 100 characters
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
            $.getJSON($('#merchant-select').data('url'), { id: userId }, function (data) {
                $('input[name="CustomerMessages.CompanyName"]').val(data.companyName);
                $('input[name="CustomerMessages.PhoneCompany"]').val(data.phoneCompany);
                $('input[name="CustomerMessages.PhoneCompanySecand"]').val(data.phoneCompanySecand);
                $('input[name="CustomerMessages.EmailCompany"]').val(data.emailCompany);
            });
        }
    });

    // Handle city and area selection changes
    $('#city-select').change(function () {
        var cityId = $(this).val();
        if (cityId) {
            $.getJSON($('#city-select').data('url'), { cityId: cityId }, function (data) {
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

    // Load file for image preview
    var loadFile = function (event) {
        var image = document.getElementById('output');
        image.src = URL.createObjectURL(event.target.files[0]);
    };

    // Set URL value
    $("#url").val(window.location.href);
});

