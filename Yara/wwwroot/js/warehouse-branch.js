// Function to update QR Codes for warehouse branches
function updateQRCode() {
    $('.QRCodeImage').each(function () {
        var code = $(this).siblings('.Code').text();
        if (code) {
            $(this).attr('src', $('#QRCodeImage').data('url') + '?text=' + encodeURIComponent(code));
        } else {
            $(this).attr('src', '');
        }
    });
}

// Bind event to pagination buttons
$(document).on('click', '.paginate_button', function () {
    updateQRCode();
});

// Initialize on document ready
$(document).ready(function () {
    updateQRCode();
});
