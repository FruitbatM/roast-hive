$(document).ready(function () {
  // Highlight the navigation link on click
  const currentPath = window.location.pathname;
  $('.navbar-nav .nav-link').each(function () {
      const navLinkPath = $(this).attr('href');
      if (navLinkPath === currentPath) {
          $(this).addClass('active');
      }
  });


// Back to Top Arrow - code with modification taken from: https://www.w3schools.com/howto/howto_js_scroll_to_top.asp
let mybutton = document.getElementById('arrow_2top');

// When the user scrolls down 200px from the top of the document, show the button
window.onscroll = function() {
  scrollFunction();
};

// When the user clicks on the button, scroll to the top of the page
function topFunction() {
  document.body.scrollTop = 0; // For Safari
  document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}

function scrollFunction() {
  if (document.body.scrollTop > 200 || document.documentElement.scrollTop > 200) {
    mybutton.style.display = 'block';
  } else {
    mybutton.style.display = 'none';
  }
}

// Attach the click event handler to the button using JavaScript
mybutton.onclick = topFunction;

// Shop details page
// Disable +/- buttons outside 1-99 range
function handleEnableDisable(itemId) {
    let currentValue = parseInt($(`#id_qty_${itemId}`).val());
    let minusDisabled = currentValue < 2;
    let plusDisabled = currentValue > 98;

    $(`#decrement-qty_${itemId}`).prop('disabled', minusDisabled);
    $(`#increment-qty_${itemId}`).prop('disabled', plusDisabled);
}

// Ensure proper enabling/disabling of all inputs on page load
let allQtyInputs = $('.qty_input');
allQtyInputs.each(function () {
    let itemId = $(this).data('item_id');
    handleEnableDisable(itemId);
});

// Check enable/disable every time the input is changed
$('.qty_input').change(function () {
    let itemId = $(this).data('item_id');
    handleEnableDisable(itemId);
});

// Increment quantity
$('.increment-qty').click(function (e) {
    e.preventDefault();
    let closestInput = $(this).closest('.input-group').find('.qty_input')[0];
    let currentValue = parseInt($(closestInput).val());
    $(closestInput).val(currentValue + 1);
    let itemId = $(this).data('item_id');
    handleEnableDisable(itemId);
    updateCartTotalAmount(); // Update cart total amount
});

// Decrement quantity
$('.decrement-qty').click(function (e) {
    e.preventDefault();
    let closestInput = $(this).closest('.input-group').find('.qty_input')[0];
    let currentValue = parseInt($(closestInput).val());
    if (currentValue > 1) {
        $(closestInput).val(currentValue - 1);
        let itemId = $(this).data('item_id');
        handleEnableDisable(itemId);
        updateCartTotalAmount(); // Update cart total amount
    }
});

  
function updateCartTotalAmount() {
  $.ajax({
    url: '/api/Cart/TotalAmount',
    method: 'GET',
    success: function (data) {
      let cartTotal = data;
      $('#cartTotalAmount').text(cartTotal); // Update cartTotalAmount element
      updateCartTotal();
    },
    error: function () {
      console.log('Error retrieving cart total amount.');
    }
  });
}

// Call the function initially to set the cart total amount
updateCartTotalAmount();

// Update cart total amount when quantity is changed
$('.quantity-input').change(function () {
  updateCartTotalAmount();
});

// Update cart total amount when an item is added/removed
$('.update-link, .remove-item').click(function () {
  updateCartTotalAmount();
});

// Update quantity and subtotal on the cart page
$('.update-link').click(function () {
  let itemId = $(this).data('number');
  let quantityInput = $('#quantity-' + itemId);
  let quantity = parseInt(quantityInput.val());
  
  if (isNaN(quantity) || quantity < 1) {
    quantityInput.val('1'); // Reset to minimum quantity if input is invalid
    quantity = 1;
  } else if (quantity > 99) {
    quantityInput.val('99'); // Limit quantity to 99 if it exceeds the maximum
    quantity = 99;
  }
  
  let unitPrice = parseFloat($('#unit-price-' + itemId).text());
  let subtotal = quantity * unitPrice;
  $('#subtotal-' + itemId).text('€' + subtotal.toFixed(2));
  
  // Update cart total amount and cart total
  updateCartTotalAmount();
});

$('.remove-item').click(function () {
  let itemId = $(this).data('id');
  $(this).closest('tr').remove();
  
  // Update cart total amount and cart total
  updateCartTotalAmount();
});

function updateCartTotal() {
  let total = 0;
  $('span[id^="subtotal-"]').each(function () {
    let subtotalText = $(this).text().substring(1); // Remove the currency symbol
    let subtotalValue = parseFloat(subtotalText);
    if (!isNaN(subtotalValue)) {
      total += subtotalValue;
    }
  });
  $('#cart-total').text('€' + total.toFixed(2));
}

  // Handle quantity input validation and limit to maximum 99
  $('.quantity-input').on('input', function () {
    let inputValue = $(this).val();
    let sanitizedValue = inputValue.replace(/[^0-9-]/g, ''); // Remove non-numeric and non-negative sign characters
    let quantity = parseInt(sanitizedValue);
    if (!isNaN(quantity) && quantity > 99) {
        sanitizedValue = '99'; // Limit to maximum 99
    }
    $(this).val(sanitizedValue);
  });
});