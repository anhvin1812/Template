jQuery(function ($) {
    $("#categories .filter-button").click(function (event) {
        calculateHeightOfSlideNavigation();
        $("#categories .slide-navigation").toggle("slide", { direction: "left" }, 200);
        $("#categories").toggleClass("active");

        event.stopPropagation();
    });

    $(window).resize(function () {
        $("#categories .slide-navigation").hide("slide", { direction: "left" }, 200);
    });

    $("html").click(function () {
        $("#categories .slide-navigation").hide("slide", { direction: "left" }, 200);
        $("#categories").removeClass("active");
    });
});

var calculateHeightOfSlideNavigation = function () {
    var headerHeight = $("header").outerHeight();
    var categoriesHeight = $("#categories").outerHeight();
    var top = headerHeight + categoriesHeight;
    var windowHeight = $(window).height();

    $("#categories .slide-navigation").css("max-height", (windowHeight - top));
}