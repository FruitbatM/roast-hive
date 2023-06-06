// Highlight the navigation link on click 

$(document).ready(function () {
    // Get the current URL path
    const currentPath = window.location.pathname;

    // Add active class to the corresponding navigation link
    $('.navbar-nav .nav-link').each(function () {
        const navLinkPath = $(this).attr('href');
        if (navLinkPath === currentPath) {
            $(this).addClass('active');
        }
    });
});

// Back to Top Arrow - code with modification taken from: https://www.w3schools.com/howto/howto_js_scroll_to_top.asp
mybutton = document.getElementById('arrow_2top');

// When the user scrolls down 200px from the top of the document, show the button
window.onscroll = function() {scrollFunction()};

function scrollFunction() {
  if (document.body.scrollTop > 200|| document.documentElement.scrollTop > 200) {
    mybutton.style.display = 'block';
  } else {
    mybutton.style.display = 'none';
  }
}

// When the user clicks on the button, scroll to the top of the page
function topFunction() {
  document.body.scrollTop = 0; // For Safari
  document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}


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
allQtyInputs.each(function() {
    let itemId = $(this).data('item_id');
    handleEnableDisable(itemId);
});

// Check enable/disable every time the input is changed
$('.qty_input').change(function() {
    let itemId = $(this).data('item_id');
    handleEnableDisable(itemId);
});

// Increment quantity
$('.increment-qty').click(function(e) {
  e.preventDefault();
  let closestInput = $(this).closest('.input-group').find('.qty_input')[0];
  let currentValue = parseInt($(closestInput).val());
  $(closestInput).val(currentValue + 1);
  let itemId = $(this).data('item_id');
  handleEnableDisable(itemId);
});

// Decrement quantity
$('.decrement-qty').click(function(e) {
    e.preventDefault();
    let closestInput = $(this).closest('.input-group').find('.qty_input')[0];
    let currentValue = parseInt($(closestInput).val());
    if (currentValue > 1) {
        $(closestInput).val(currentValue - 1);
    }
    let itemId = $(this).data('item_id');
    handleEnableDisable(itemId);
});