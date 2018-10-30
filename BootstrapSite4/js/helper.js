$(document).ready(function () {
    var scroll_top = 0;
    var wind_height = $(window).height();
    var page_height = $(document).height();

    var canLoad = true;
    //определение, что пора вызвать ленивую загрузку данных
    $(window).bind('scrollstop', function () {
        scroll_top = $(document).scrollTop();
        var page_height = $(document).height();
        wind_height = $(window).height();
        if ((page_height - scroll_top) < wind_height * 2 && canLoad)
            addRows();
    });
    //ленивая загрузка
    function addRows() {
        $.ajax({
            url: $('#lazyurl').val(),
            type: 'get',
            cache: false,
            dataType: 'html',
            data: { rowskip: $('.pull-request').length },
            success: function (data) {
                canLoad = !($('#pull-requests').append(data).find('#noMoreItems').length);
            }
        });
    }
});