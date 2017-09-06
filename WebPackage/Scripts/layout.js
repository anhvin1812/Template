
var layout = (function () {
    var my = {};

    my.ActiveControls = function () {
        // active hot news slider
        $(".hot-news-slider .owl-carousel").owlCarousel({
            loop: false,
            margin: 30,
            nav: false,
            responsive: {
                0: {
                    items: 1,
                    margin: 30
                },
                600: {
                    items: 2
                },
                768: {
                    items: 3
                },
                1000: {
                    items: 4
                }
            }
        });

        // active achive datetimepickers 
        $(".datepicker-archive").datetimepicker({
            useCurrent: false,
            inline: true,
            format: "MM-dd-YYYY",
            icons: {
                previous: "fa fa-angle-left",
                next: "fa fa-angle-right"
            }
        }).on("dp.change", function (e) {
            var url = $(this).data("url");
            var date = (e.date.month() + 1) + "-" + e.date.date() + "-" + e.date.year();
            window.location.href = url + "?startDate=" + date + "&endDate=" + date;
        });;
    };
	
    my.Responsive = function () {
        $(window).on("load, resize", function () {
            var wWidth = $(window).width();

            if (wWidth < 768) {
                $("#carousel-highlight").carousel({
                    interval: 50000

                });
            }
        });
    };

    my.Navigation = function () {
        function openCloseMenu(close) {
            if (close) {
                $("#btn-open-menu").removeClass("open");
                $("#main-nav").removeClass("open");
                $(".wrap").removeClass("push");
            } else {
                // open menu
                $("#btn-open-menu").addClass("open");
                $("#main-nav").addClass("open");
                $(".wrap").addClass("push");
            }
        }

        function openCloseSearchBar(close) {
            if (close) {
                $("#btn-open-search").removeClass("open");
                $("#search-form").removeClass("open");
            } else {
                // open menu
                $("#btn-open-search").addClass("open");
                $("#search-form").addClass("open");
                $("#txt-search").focus();
            }
        }

        //open mobile menu
        $("#btn-open-menu").click(function(event) {
            event.preventDefault();
            event.stopPropagation();

            var close = $(this).hasClass("open");
            openCloseMenu(close);
        });

        // open search bar
        $("#btn-open-search").click(function (event) {
            event.preventDefault();
            event.stopPropagation();

            var close = $(this).hasClass("open");
            openCloseSearchBar(close);
        });

        // typing event
        $(".txt-search").on("keyup", function() {
            var term = $(this).val();
            if (term.length > 0) {
                $(".btn-search-clear").removeClass("hide");
            } else {
                $(".btn-search-clear").addClass("hide");
            }
        });

        // clear search box
        $(".btn-search-clear").click(function () {
            $(".txt-search").val("");
            $(this).addClass("hide");
        });

        // close menu
        $(".close-menu").click(function () {
            openCloseMenu(true); // close menu
        });

        // click outside close navigation
        $(document).click(function(event) {
            if (!$(event.target).is("#main-nav, #main-nav *")) {
                openCloseMenu(true); // close menu
            }

            if (!$(event.target).is("#search-form, #search-form *")) {
                openCloseSearchBar(true); // close search form
            }
        });
    }

    my.BuildMainMenu = function () {
        $("#main-nav > .main-nav-scroll > ul.dd-list").addClass("multi-level");
        $("#main-nav .dd-handle").remove();
        $("#main-nav .dd-list").removeClass("dd-list");
        $("#main-nav li").removeClass("dd-item").removeClass("dd3-item");
        $("#main-nav .item-tools").remove(); 
        $("#main-nav .dd3-content").removeClass("dd3-content");

        $("#main-nav").removeClass("hidden");
    }


    return my;
}());

$(document).ready(function () {
    // Active controls 
    layout.ActiveControls();

    // Responsive 
    layout.Responsive();

    layout.Navigation();

    // Build main menu
    layout.BuildMainMenu();
});