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
