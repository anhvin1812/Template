
var crawl = (function () {
    var my = {
        Urls: {
            GetSourcePages: $("#hdGetPagesUrl").val(),
            GetArticleDetail: $("#hdGetArticleDetailUrl").val(),
            Preview: $("#hdPreviewUrl").val()
        },
        BtnPreview: $("#btn-preview-news"),
        DdlCrawlSource: $("#ddlCrawlSources"),
        DdlCrawlSourcePage: $("#ddlCrawlSourcePages"),
        FormCrawlFilter: $("#formCrawlFilter"),
        CrawlResultsContainer: $("#tblCrawlResults > tbody"),
        SaveForm: $("#tblCrawlResults .crawl-form form")

    };

    my.LoadPagesFromSource = function () {

        function loadPages(sourceId) {
            var data = { sourceId: sourceId };

            $.get(my.Urls.GetSourcePages, data, function (result) {
                if (result.Status === common.ResultStatusType.Success) {
                    my.DdlCrawlSourcePage.html("").select2({ data: result.Data });
                } else {
                    common.ShowError(result.Errors);
                }
            });
        }

        my.DdlCrawlSource.change(function (event) {
            var sourceId = $(this).val();
            
            if(sourceId !== "")
                loadPages(sourceId);
        });
    }

    my.Scan = function () {

        my.FormCrawlFilter.submit(function(event) {
            event.preventDefault();

            var url = $(this).attr("action");
            var formData = $(this).serializeFormJSON();

            $("#crawlLoading").removeClass("hide");

            $.post(url, { filter: formData }, function (result) {
                if (result.Status && (result.Status === common.ResultStatusType.Fail)) {
                    common.ShowError(result.Errors);
                } else {
                    my.CrawlResultsContainer.html(result);
                }
            }).always(function () {
                $("#crawlLoading").addClass("hide");
            });
        });

    }

    my.ScanArticleDetail = function() {
        my.CrawlResultsContainer.on("click", ".btn-crawl-article-detail", function() {
            var sourceId = $(this).data("source-id"),
                articleUrl = $(this).data("article-url"),
                crawlIndex = $(this).data("crawl-index");

            var crawlItemContainerId = "#crawl-item-" + crawlIndex,
                imgageSrc = $(crawlItemContainerId + " .feature-image-url:first").val();
            
            $.post(my.Urls.GetArticleDetail, { sourceId: sourceId, linkDetail: articleUrl }, function (result) {
                if (result.Status && (result.Status === common.ResultStatusType.Fail)) {
                    var messageContainer = $(crawlItemContainerId + " .save-messages");
                    common.ShowError(result.Errors, messageContainer);

                    $(crawlItemContainerId + " .btn-crawl-article-detail").addClass("hide"); //hide scan button
                } else {
                    $(crawlItemContainerId + " .save-form-container").html(result);

                    select2.Select2(crawlItemContainerId + " .select2");
                    select2.Select2Ajax(crawlItemContainerId + " .select2-ajax");
                    $(crawlItemContainerId + " [data-toggle='toggle']").bootstrapToggle();

                    $(crawlItemContainerId + " input[name='FeaturedImage']").val(imgageSrc);

                    // show tags & categories
                    $(crawlItemContainerId + " .crawl-categories").replaceWith($(crawlItemContainerId + " .temp-categories").removeClass("hide"));
                    $(crawlItemContainerId + " .crawl-tags").replaceWith($(crawlItemContainerId + " .temp-tags").removeClass("hide"));
                }
            }).always(function () {
                //$("#crawlLoading").addClass("hide");
            });
        });
    }

    my.SaveArticle = function () {
        
        my.CrawlResultsContainer.on("submit", "form", function(event) {
            event.preventDefault();
            
            var formContainer = $(this).closest(".crawl-form"),
                form = $(this);
            // run validation
            $.validator.unobtrusive.parse(form);
            form.validate();

            if (form.valid() === true) {
                var url = form.attr("action"),
                formData = form.serializeFormJSON();

                $.post(url, { entry: formData }, function (result) {
                    var messageContainer = formContainer.find(".save-messages");

                    if (result.Status === common.ResultStatusType.Success) {
                        var formWrapper = formContainer.find(".save-form-container");

                        common.ShowSuccess(result.Message, messageContainer);
                        formWrapper.addClass("hide"); // hide form
                    } else {
                        common.ShowError(result.Errors, messageContainer);
                    }
                }).always(function () { });
            }
        });
    }

    my.Preview = function () {
        my.CrawlResultsContainer.on("click", ".btn-preview-crawl", function() {
            var form = $(this).closest("form");

            // run validation
            $.validator.unobtrusive.parse(form);
            form.validate();

            if (form.valid() === true) {
                var url = my.Urls.Preview,
                formData = form.serializeFormJSON();

                var options = {
                    modalTarget: "#modal-preview",
                    url: url,
                    data: { entry: formData },
                    method: "POST",
                    resizable: true,
                    draggable: true
                };

                modal.ModalAjax(options);
            }
        });
    }

    return my;
}());



$(document).ready(function () {
    // select source event
    crawl.LoadPagesFromSource();

    // submit form event
    crawl.Scan();

    // scan article detail event
    crawl.ScanArticleDetail();

    // save article event
    crawl.SaveArticle();

    // preview event
    crawl.Preview();
});