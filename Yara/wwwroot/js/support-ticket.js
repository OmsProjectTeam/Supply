$(document).ready(function () {
    $('.select2_1').select2({
        placeholder: "Select an option",
        allowClear: true
    });

    $('#merchant-select').change(function () {
        var userId = $(this).val();
        if (userId) {
            $.getJSON($('#merchant-select').data('url'), { id: userId }, function (data) {
                $('input[name="SupportTicket.CompanyName"]').val(data.companyName);
                $('input[name="SupportTicket.PhoneCompany"]').val(data.phoneCompany);
                $('input[name="SupportTicket.PhoneCompanySecand"]').val(data.phoneCompanySecand);
                $('input[name="SupportTicket.EmailCompany"]').val(data.emailCompany);
            });
        }
    });
});

var loadFile = function (event) {
    var image = document.getElementById('output');
    image.src = URL.createObjectURL(event.target.files[0]);
};

$("#url").val(window.location.href);

// JavaScript for loading areas based on the city selection
$(document).ready(function () {
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
});
