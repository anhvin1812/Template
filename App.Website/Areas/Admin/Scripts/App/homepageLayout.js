
var homepageLayout = (function () {
    var my = {
        Urls: {
            UpdateHomepageLayout: $("#update-homepage-layout-url").val()
        },
        LayoutContainer: $("#homepage-layout")
    };
    my.SaveLayouts = function () {
        function getData() {

            var layouts = new Array();
            $("#homepage-layout > li").each(function(i) {
                var id = $(this).data("id");
                var categoryId = $(this).data("category-id");
                var mediaType = $(this).data("media-type");
                var layoutType = $(this).data("layout-type");
                var index = i+1;

                var layout = {
                    Id : id,
                    CategoryId: categoryId,
                    MediaType: mediaType,
                    LayoutType: layoutType,
                    SortOrder : index
                };

                layouts.push(layout);
            });
            
            return layouts;
        }

        function refreshLayout(layouts) {
            var html = "", i;

            for (i = 0; i < layouts.length; i++) {
                html += "<li class=\"ui-state-highlight dd-handle\" data-id=\"" + layouts[i].Id + "\" data-category-id=\"" + layouts[i].CategoryId + "\" data-media-type=\"" + layouts[i].MediaType + "\" data-layout-type=\"" + layouts[i].LayoutType + "\" >" + layouts[i].Title + "</li>";
            }

            $("#homepage-layout").html(html);
        }

        $("#btn-save-homepage-layout").click(function (event) {
            var data = { entries: getData() };
            $.post(my.Urls.UpdateHomepageLayout, data, function(result) {
                if (result.Status === common.ResultStatusType.Success) {
                    refreshLayout(result.Data);
                    common.ShowSuccess(result.Message);
                } else {
                    common.ShowError(result.Errors);
                }
            });
        });
    }

    my.InitSortable = function () {
        $("#categories, #homepage-layout").sortable({
            connectWith: ".connectedSortable",
            placeholder: "dd-placeholder"
        }).disableSelection();
    }

    return my;
}());



$(document).ready(function () {

    homepageLayout.SaveLayouts();

    homepageLayout.InitSortable();
});