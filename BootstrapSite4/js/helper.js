$(document).ready(function () {
    var scroll_top = 0;        //высота прокрученной области
    var wind_height = $(window).height();//высота окна браузера
    var page_height = $(document).height();//высота всей страницы

    var canLoad = true;

    $(window).bind('scrollstop', function () {
        scroll_top = $(document).scrollTop();//высота прокрученной области
        var page_height = $(document).height();//высота всей страницы
        wind_height = $(window).height();//высота окна браузера
        //если непрокрученной области больше чем высота окна обраузера, то подгружаем след. объекты
        if ((page_height - scroll_top) < wind_height * 2&&canLoad)            
                    addRows( );
        
    });
    function addRows() {
        $.ajax({
            url: $('#lazyurl').val(),
            type: 'get',
            cache: false,
            dataType: 'html',
            data: { rowskip: $('.pull-request').length },
            success: function (data) {               
                 canLoad =     !( $('#pull-requests').append(data).find('#noMoreItems').length);
            } 
        });       
    }
});