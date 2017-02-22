
var product = (function () {
    var my = {
        URLs: {
            DeleteGallery: $("#DeleteGalleryUrl").val()
        }
    };

    my.DeleteGallery = function() {
        $("body").on("click", ".btn-delete-gallery", function() {
            var productId = $(this).data("product-id");
            var galleryId = $(this).data("gallery-id");
            var data = { productId: productId, galleryId: galleryId };

            $.post(my.URLs.DeleteGallery, data, function(result) {
                if (result.Status === common.ResultStatusType.Success) {
                    $("#gallery-item-" + galleryId).remove();
                }
                console.log(result);
            });
        });
    }

    my.InitUploadControls = function() {
        // featured image functions
        $("#btnRemoveFeaturedImage").on("click", function (event) {
            $("#previewFeaturedImage").addClass("hidden");
            // do ajax function to remove featured image

            $("#previewFeaturedImage img").attr("src", "");
            $(this).remove();
        });

        // Custom file upload
        $("#fileFeaturedImage").on("fileselect", function (event, numFiles, label) {
            var input = $(this);
            // show selected filename
            var txt = input.parents().closest(".input-group").find(".input-file");
            txt.val(label);
            // show selected image
            var reader = new FileReader();

            reader.onload = function (e) {
                $("#previewFeaturedImage img").attr("src", e.target.result);
            }
            reader.readAsDataURL(input.get(0).files.item(0));

            // show preview box
            $('#previewFeaturedImage').removeClass("hidden");
        });

        // Show image after selecting
        $("#fileGallery").on("fileselect", function (event, numFiles, label) {
            var input = $(this);
            // show selected filename
            var txt = $(this).parents().closest(".input-group").find(".input-file");
            txt.val(label);
            // show selected image
            for (var i = 0; i < input.get(0).files.length; i++) {
                $("#previewGallery li.preupload").remove();
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#previewGallery").prepend('<li class="text-center preupload"><img src="' + e.target.result + '" class="img-thumbnail" width="70" height="70"><!-- /li-->');
                    $("#previewGallery").trigger("create");
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
    product.InitUploadControls();

    // Delete gallery
    product.DeleteGallery();
   
});