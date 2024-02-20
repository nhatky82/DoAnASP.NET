$(document).ready(function () {
    var header = $(".header__top");
    var sticky = header.offset().top;
    $(window).scroll(function () {
        if (window.pageYOffset > sticky) {
            header.addClass("fixed");
        }
        else {
            header.removeClass("fixed");
        }
    });

});
