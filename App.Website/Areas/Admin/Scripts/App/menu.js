
var menu = (function () {
    var my = {
        URLs: {
            DeleteGallery: $("#DeleteGalleryUrl").val()
        },
        WrapMenu: $("#menu-wrap"),
        RootItem: $("#menu-wrap > .dd-list"),
        BtnAddItem: $("#btn-add-menu-item"),
        MenuItem: "<li class=\"dd-item dd3-item\"><div class=\"dd-handle dd3-handle\"></div><a class=\"dd3-content\" href=\"#\"><span class=\"item-label\">New item</span><div class=\"item-tools pull-right\"><span class=\"btn btn-xs btn-info btn-edit-menu-item\"><i class=\"fa fa-edit\"></i></span>&nbsp;<span class=\"btn btn-xs btn-danger btn-delete-menu-item\"><i class=\"fa fa-close\"></i></span></div></a></li>",
        EditForm: "<div class=\"form-edit-item\"><label>Label</label><input type=\"text\" class=\"form-control input-sm txt-item-label\"/><label>Url</label><input type=\"text\" class=\"form-control input-sm txt-item-url\"/></div>"
    };

    my.AddItem = function () {
       my.BtnAddItem.click(function() {
           my.RootItem.append(my.MenuItem);
        });
    }

    my.DeleteItem = function () {
        my.WrapMenu.on("click", ".btn-delete-menu-item", function () {
            $(this).closest("li.dd-item").remove();
            return false;
        });
    }

    my.EditItem = function () {
        my.WrapMenu.on("click", ".btn-edit-menu-item", function () {
            
            var item = $(this).closest("li.dd-item");
            var editForm = item.find(" > .form-edit-item");
            if (editForm.length > 0) {
                editForm.remove();
            } else {
                var html = $(my.EditForm);
                var label = item.find("> .dd3-content > .item-label:first").text();
                var url = item.find("> .dd3-content:first").attr("href");

                html.insertAfter(item.find("> .dd3-content:first"));
                
                html.find("input.txt-item-label").val(label);
                html.find("input.txt-item-url").val(url);
            }

            return false;
        });

        // Update label
        my.WrapMenu.on("keyup", ".txt-item-label", function () {
            var value = $(this).val();
            $(this).closest("li.dd-item").find("> .dd3-content > .item-label:first").text(value);
        });

        // Update label
        my.WrapMenu.on("keyup", ".txt-item-url", function () {
            var value = $(this).val();
            $(this).closest("li.dd-item").find("> .dd3-content:first").attr("href", value);
        });

        // prevent redirect when click on item
        my.WrapMenu.on("click", "a.dd3-content", function () {
            return false;
        });
    }

    my.SaveMenu = function () {
        function buildMenu() {
            var menu = my.WrapMenu.html();
            menu = menu.find(".form-edit-item").remove();

            return menu;
        }

        $("#btn-save-menu").click(function () {
            var url = "";
            var menu = buildMenu();
            $.post(url, menu, function (result) {

            });
        });
    }


    return my;
}());



$(document).ready(function () {
    menu.AddItem();
    menu.EditItem();
    menu.DeleteItem();
   
});