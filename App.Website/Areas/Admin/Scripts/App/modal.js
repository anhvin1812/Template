
var modal = (function () {
    var my = {};

    function generateHeader(title) {
       return String.format("<div class=\"modal-header\"><button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button><h4 class=\"modal-title\">{0}</h4></div>", title);
    }

    function configModal(options) {
        var modalContent = $(options.modalTarget).find(".modal-content");

        // header
        if (options.title !== "" && options.title !== null) {
            modalContent.prepend(generateHeader(title));
        }

        // loading overlay
        modalContent.html("<div class=\"overlay\"><i class=\"fa fa-refresh fa-spin\"></i></div>");

        // iframe
        if (options.iframe) {
            modalContent.prepend("<iframe width=\"100%\"></iframe>");
        }

        $(options.modalTarget).modal("show");
    }

    function addContent(options, html) {
        var modalContent = $(options.modalTarget).find(".modal-content");

        if (options.iframe) {
            var iframe = $(options.modalTarget).find("iframe:first");

            var body = iframe.contents().find("body");
            body.html(html);
        } else {
            modalContent.html(html);
        }

        if (options.resizable) {
            modalContent.resizable({
                //alsoResize: ".modal-dialog",
                minHeight: 480,
                minWidth: 370,
                maxWidth: 920
            });

            //destroy resizable
            $(options.modalTarget).on("hidden.bs.modal", function (e) {
                modalContent.resizable("destroy");
            });
        }

        if (options.draggable) {
            $(options.modalTarget).find(".modal-dialog").draggable();
        }
    }

    function getDataAndShowModal(inputOptions) {
        var defaultOptions = {
            url: null,
            data: null,
            method: "GET",
            modalTarget: "#modal",
            title: "",
            iframe: false,
            resizable: false,
            draggable: false
        };

        // merge inputOptions into defaultOptions
        var options = $.extend(defaultOptions, inputOptions);
        
        // show modal
        configModal(options);
        

        // request to server
        $.ajax({
            url: options.url,
            method: options.method,
            data: options.data,
            dataType: "html",
            beforeSend: function(xhr) {}
        }).fail(function(jqXHR, textStatus, errorThrown) {
            addContent(options, errorThrown);
        }).done(function(data, textStatus, jqXHR) {
            addContent(options, data);
        });
    }

    my.ModalAjax = function(inputOptions) {
        getDataAndShowModal(inputOptions);// request to server
    }

    my.ModalAjaxGet = function () {
        // click button to open ajax modal
        $("body").on("click", "[data-toggle=\"modalajax\"]", function () {
            var inputOptions = {
                modalId: $(this).data("target"),
                url: $(this).data("url")
            };

            getDataAndShowModal(inputOptions);
        });
    }

    return my;
}());


$(document).ready(function () {
    // Init modalajax
    modal.ModalAjaxGet();
});