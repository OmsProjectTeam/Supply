// Close the modal if the user clicks outside of it
window.onclick = function (event) {
    var modal = document.getElementById("customModal");
    if (event.target == modal) {
        modal.style.display = "none";
    }
};

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
const typingDelay = 3000;  // 3 seconds delay before triggering search

var previousSearchText = '';

function openModal() {
    console.log("openModal");
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

function checkProductAvailability(productId) {
    console.log("checkProductAvailability:" + productId);
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

// Function to populate product details after selecting a product
function populateProductDetails(productDetails) {
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
        updateCodeField();  // Update QR code or other fields if necessary
    }
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
    let productSelectedFromAutocomplete = false;  // Track whether product is selected from autocomplete

    $("#product12").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/admin/Order/GetProductSuggestions',
                type: 'GET',
                data: { query: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.model,
                            value: item.model
                        };
                    }));
                },
                error: function () {
                    alert('Error retrieving data');
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            productSelectedFromAutocomplete = true;  // Flag that product was selected from autocomplete
            $('#product12').val(ui.item.value);  // Set selected value in the input box
            populateProductDetails(ui.item);
            // Populate product details
            clearTimeout(typingTimer);  // Clear typing timer as selection is made
            // Ensure product availability check is called just like in manual input
            checkProductAvailability(ui.item.value);  // Trigger the check
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
