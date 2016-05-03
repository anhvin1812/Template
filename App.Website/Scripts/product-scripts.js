jQuery(function () {

    // Zoom image
    var $easyzoom = $(".easyzoom").easyZoom();

    // Setup thumbnails
    var thumbnail = $easyzoom.filter(".product-image").data("easyZoom");

    // Thumbnails slide
    $(".thumbnails").bxSlider({
        autoStart: false,
        slideWidth: 60,
        minSlides: 7,
        maxSlides: 7,
        slideMargin: 10,
        moveSlides: 1,
        pager: false,
        hideControlOnEnd: true,
        infiniteLoop: false,
        nextSelector: "#slider-prev",
        prevSelector: "#slider-next",
        nextText: "<span class=\"glyphicon glyphicon-chevron-right\"></span>",
        prevText: "<span class=\"glyphicon glyphicon-chevron-left\"></span>"
    });

    $(".thumbnails").on("click", ".slide", function (e) {
        var $this = $(this);

        e.preventDefault();

        // Use EasyZoom's `swap` method
        thumbnail.swap($this.data("standard"), $this.data("standard"));
        $(".thumbnails .slide").removeClass("active");
        $this.addClass("active");
    });
});
