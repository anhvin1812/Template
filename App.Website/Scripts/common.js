jQuery(function() {
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
    var headerHeight = $("#logo-container").outerHeight();
    var top = headerHeight + 5;
    var windowHeight = $(window).height();

    $("#collapse-menu").css("max-height", (windowHeight - top));
}


// Social

function ShareSocial(elm) {
    //event.preventDefault();
    console.log(elm);
    var url = $(elm).attr("href");
    var width = $(window).width() > 600 ? 600 : $(window).width();
    var height = $(window).height() > 400 ? 400 : $(window).height();

    window.open(url, "sharer", "toolbar=0,status=0,width=" + width + ",height=" + height);
    return false;
}