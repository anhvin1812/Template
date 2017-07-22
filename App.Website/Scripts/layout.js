﻿
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
                    items: 1
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

        // active datetimepickers inline
        $(".datepicker-inline").datetimepicker({
            inline: true,
            format: "DD/MM/YYYY",
            icons: {
                previous: "fa fa-angle-left",
                next: "fa fa-angle-right"
            }
        });
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

    return my;
}());

$(document).ready(function () {
    // Active controls 
    layout.ActiveControls();

    // Responsive 
    layout.Responsive();

    layout.Navigation();
});