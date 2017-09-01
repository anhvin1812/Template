
var select2 = (function() {
    var my = {};

    my.Select2 = function (select2Elements) {

        var elements;
        if ($(select2Elements).length > 0) {
            elements = $(select2Elements);
        } else {
            elements = $(".select2");
        }

        elements.select2();
    }

    my.Select2Ajax = function (select2Elements) {
        var elements;
        if ($(select2Elements).length > 0) {
            elements = $(select2Elements);
        } else {
            elements = $(".select2-ajax");
        }

        elements.each(function(index, item) {
            var url = $(item).data("ajax-url"),
                placeholder = $(item).data("placeholder"),
                multiple = $(item).prop("multiple");

            $(item).select2({
                placeholder: placeholder,
                //Does the user have to enter any data before sending the ajax request
                minimumInputLength: 2,
                multiple: multiple,
                ajax: {
                    quietMillis: 300,
                    url: function (params) {
                        return url;
                    },
                    data: function (params) {
                        var query = {
                            query: params.term
                        }
                        // Query paramters will be ?query=[term]
                        return query;
                    },
                    processResults: function (data, params) {
                        return {
                            results: data.Data
                        };
                    }
                },
                escapeMarkup: function (markup) { return markup; }
            });
        });
    }

    return my;
}());


$(document).ready(function () {
    // Init select2
    select2.Select2();

    // Init select2 ajax
    select2.Select2Ajax();
});