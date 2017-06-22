
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

    return my;
}());

$(document).ready(function () {
    // Active controls 
    layout.ActiveControls();

    // Responsive 
    layout.Responsive();
});