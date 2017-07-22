
var common = (function () {
    var my = {
        ResultStatusType: {
            Success: 0,
            Fail: 1
        }
    };

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