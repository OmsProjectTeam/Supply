// Initialize DataTables for the provided tables
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

// Handle merchant select change to fetch user details and update form fields
$(document).ready(function () {
    $('.select2_1').select2({
        placeholder: "Select an option",
        allowClear: true
    });

    $('#merchant-select').change(function () {
        var userId = $(this).val();
        if (userId) {
            $.getJSON($('#merchant-select').data('url'), { id: userId }, function (data) {
                $('input[name="Merchants.CompanyName"]').val(data.companyName);
                $('input[name="Merchants.PhoneCompany"]').val(data.phoneCompany);
                $('input[name="Merchants.PhoneCompanySecand"]').val(data.phoneCompanySecand);
                $('input[name="Merchants.EmailCompany"]').val(data.emailCompany);
            });
        }
    });

    // Handle city change to load areas
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

    // Function to handle image preview
    var loadFile = function (event) {
        var image = document.getElementById('output');
        image.src = URL.createObjectURL(event.target.files[0]);
    };
    window.loadFile = loadFile; // Make loadFile globally accessible

    // Function to truncate text
    function truncateText(text, maxLength) {
        if (text.length > maxLength) {
            return text.substring(0, maxLength) + '...';
        } else {
            return text;
        }
    }

    // Loop through each table cell with class 'truncate-50'
    $('.truncate-50').each(function () {
        var text = $(this).text();
        var truncatedText = truncateText(text, 100);
        $(this).text(truncatedText);
    });
});
