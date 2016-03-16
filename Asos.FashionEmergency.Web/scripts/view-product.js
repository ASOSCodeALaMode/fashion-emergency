var scrollPositionToRestore;

$('.product a').on('click', function(e) {
    $.ajax(e.currentTarget.href, {
        type: 'GET'
    }).done(function(markup) {
        $('.product-info .content')[0].innerHTML = markup;
        $('#overlay').css('display', 'block');
        $('.product-info').css('display', 'block');
        scrollPositionToRestore = $('body').scrollTop();
        $('body').css('overflow', 'hidden');
    });
    e.preventDefault();
});

$('.modal-close').on('click', function() {
    $('#overlay').css('display', 'none');
    $('.product-info').css('display', 'none');
    $('body').css('overflow', 'scroll');
    $('body').scrollTop(scrollPositionToRestore);
});