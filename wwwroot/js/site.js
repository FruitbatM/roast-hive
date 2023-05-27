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