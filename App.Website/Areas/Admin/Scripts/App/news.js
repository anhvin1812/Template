
var news = (function () {
    var my = {
        URLs: {
            DeleteGallery: $("#DeleteGalleryUrl").val()
        },
        BtnPreview: $("#btn-preview-news")
    };

    my.Preview = function() {
        my.BtnPreview.click(function() {
            var form = $(this).closest("form");
            var entry = null;

            form.submit(function (event) {
                event.preventDefault();
                if ($(this).valid()) {
                    entry = form.serialize();
                }
            });

            form.submit(); // for validation

            if (entry == null) {
                return;
            }
            
            var options = {
                modalTarget: "#modal-preview",
                url: $(this).data("url"),
                data: entry,
                method: "POST",
                resizable: true,
                draggable: true
            };

            modal.ModalAjax(options);

            // remove submit event
            form.unbind("submit");
        });

        
    }

    return my;
}());



$(document).ready(function () {

    // preview news
    news.Preview();
   
});