jQuery(function() {
    $(".page-slider").bxSlider({
        touchEnabled: false,
        mode: "fade",
        auto: true,
        speed: 600,
        tickerHover: true,
        pager: false,
        adaptiveHeight: true
});

    menu(null);
});

jQuery(window).resize(function () {
    var wWith = $(window).width();
    if (wWith >= 900) {
        $("#collapse-menu").removeClass("collapse");
    }
    else {
        menu(wWith);
    }
});

function menu(wWith) {
    wWith = (wWith == null) ? $(window).width() : wWith;
    if (wWith < 900) {
        var collapsed = $("#logo-container .navbar-toggle").hasClass("collapsed");
        if (collapsed) {
            $("#collapse-menu").addClass("collapse");
        } else {
            $("#collapse-menu").removeClass("collapse");
        }

        setMenuHeight();
    }
}

function setMenuHeight() {
    var headerHeight = $('#logo-container').outerHeight();
    var top = headerHeight + 5;
    var windowHeight = $(window).height();

    $('#collapse-menu').css('max-height', (windowHeight - top));
}