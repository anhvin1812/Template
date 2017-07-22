// Social
function ShareSocial(elm) {
    var url = $(elm).attr("href");
    var width = $(window).width() > 600 ? 600 : $(window).width();
    var height = $(window).height() > 400 ? 400 : $(window).height();

    window.open(url, "sharer", "toolbar=0,status=0,width=" + width + ",height=" + height); return false;
}