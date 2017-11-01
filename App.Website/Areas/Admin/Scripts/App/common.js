function goBack() {
    window.history.back();
}

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}


var common = (function () {
    var my = {
        ResultStatusType: {
            Success: 0,
            Fail: 1
        }
    };

    my.ShowSuccess = function (msg, container) {
        var msgContainer = $(container || "#validation-messages");

        var html = "<div class=\"alert alert-success alert-dismissible\">"
                    + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>"
                    + "<h4><i class=\"icon fa fa-check\"></i> Success!</h4>"
                    + msg    
                    + "</div>";

        msgContainer.html(html);
    }

    my.ShowError = function (errors, container) {
        var msgContainer = $(container || "#validation-messages");

        var msg = "", i;

        for (i = 0; i < errors.length; i++) {
            if (errors[i].ExtraInfos) {
                for (j = 0; j < errors[i].ExtraInfos.length; j++) {
                    msg += "<li>" + errors[i].ExtraInfos[j].Message + "</li>";
                }
            } else {
                msg += "<li>" + errors[i].Message + "</li>";
            }
        }

        var html = "<div class=\"alert alert-danger alert-dismissible\">"
                    + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>"
                    + "<h4><i class=\"icon fa fa-ban\"></i> Alert!</h4>"
                    + "<ul>"
                    + msg
                    + "</ul>"
                    + "</div>";

        msgContainer.html(html);
    }

    my.InitUploadControls = function () {
        // Custom file upload
        $(".input-file-single").on("fileselect", function (event, numFiles, label) {
            var input = $(this);
            // show selected filename
            var txt = input.parents().closest(".input-group").find(".input-file");
            txt.val(label);
            // show selected image
            var reader = new FileReader();

            var previewContainer = input.data("preview");
            reader.onload = function (e) {
                $("#" + previewContainer + " img").attr("src", e.target.result);
            }
            reader.readAsDataURL(input.get(0).files.item(0));

            // show preview box
            $("#" + previewContainer).removeClass("hidden");
        });

        // Show image after selecting
        $(".input-file-multiple").on("fileselect", function (event, numFiles, label) {
            var input = $(this);
            // show selected filename
            var txt = $(this).parents().closest(".input-group").find(".input-file");
            txt.val(label);

            // show selected image
            var previewContainer = input.data("preview");
            for (var i = 0; i < input.get(0).files.length; i++) {
                $("#" + previewContainer + " li.preupload").remove();
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#" + previewContainer).prepend('<li class="text-center preupload"><img src="' + e.target.result + '" class="img-thumbnail" width="70" height="70"><!-- /li-->');
                    $("#" + previewContainer).trigger("create");
                }
                reader.readAsDataURL(input.get(0).files.item(i));
            };

        });

        // Show selected files
        $(document).on("change", ".btn-file :file", function () {
            var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = "";
            for (var i = 0; i < input.get(0).files.length; i++) {
                label += input.get(0).files.item(i).name.replace(/\\/g, "/").replace(/.*\//, "") + ", ";
            };
            input.trigger("fileselect", [numFiles, label.substring(0, label.length - 2)]);
        });
    }

    return my;
}());


$(document).ready(function () {
    // Init upload controls
    common.InitUploadControls();
});


// serialize from json
(function ($) {
    $.fn.serializeFormJSON = function () {

        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || "");
            } else {
                o[this.name] = this.value || "";
            }
        });
        return o;
    };
})(jQuery);