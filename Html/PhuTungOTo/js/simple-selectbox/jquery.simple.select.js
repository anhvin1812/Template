(function ($) {

    $.fn.selectBoxes = function (options) {

        var settings = $.extend({}, {
                maxHeight: 210,
                liHeight: 30
            }, options);
        var $this = $(this);


        $this.wrap("<div class='select'></div>")
        $this.closest('div.select').append("<div class='select-element'><span class='active-option'></span><div class='option-list' style='max-height:" + settings.maxHeight + "px;'><ul></ul></div>");

        var $activeSelect = $this.closest('div.select').find('span.active-option'),
        $optionList = $this.closest('div.select').find('.option-list ul'),
        $select = $this.closest('div.select').find('select');


        $(this).each(function (i, selectBox) {
        var optionsCount = $(this).find('option').length;

        $(this).closest('div.select').find('.option-list').css({
        "height": optionsCount * settings.liHeight
        });

        var $current = $(this);
        // declare all additional markup
        var $container = $('div.select'),
        $activeOption = $current.closest('div.select').find('span.active-option'),
        $selectList = $current.closest('div.select').find('.option-list ul'),
        $currentValue = $current.find('option:selected'),
        currentValue = $current.find('option:selected').html(),
        values = [];


        $current.find("option").each(function () {
            values.push([$(this).attr('value'), $(this).html()]);
        });

        for (i = 0; i < values.length; ++i) {
            $selectList.append('<li data-value=' + values[i][0] + '>' + values[i][1] + '</li>');
        }

        if (!$currentValue.length) {
            $activeOption.html(values[0][1]);
        } else {
            $activeOption.html(currentValue);
        }
    });


    $('span.active-option').on("click", function () {
        var $active = $(this);
        $active.closest('div.select').find('.option-list').toggleClass('show');
        $active.closest('div.select').toggleClass('open');
    });

    $('.option-list li').on("click", function () {
        var $option = $(this);
        var value = $option.attr("data-value");
        var text = $option.html();

        $option.closest('div.select').find('select').val(value).change();
        $option.closest('div.select').find('.active-option').html(text);
        $option.closest('div.option-list').removeClass('show');
        $('div.select').removeClass('open');

    });

    $(document).mouseup(function (e) {
        var $container = $('div.select');
        if (!$container.is(e.target) && $container.has(e.target).length === 0) {
            $container.find('div.option-list').removeClass('show');
            $container.removeClass('open');
        }
    });

    };

})(jQuery);
