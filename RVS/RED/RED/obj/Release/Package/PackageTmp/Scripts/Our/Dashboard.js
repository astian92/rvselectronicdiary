var spiner = '<div class="spiner-example" style="height:auto; padding-top:0;">\
                <div class="sk-spinner sk-spinner-cube-grid">\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                    <div class="sk-cube"></div>\
                </div>\
            </div>';

$(document).ready(function () {
    var url = '/Home/TestsReference';

    $('.tests-reference').html(spiner);
    $.ajax({
        cache: false,
        type: 'POST',
        url: url,
        data: { type: 1 },
        success: function (result) {
            $('.tests-reference').html(result);
        },
        error: function () {
            var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на справката</div>");
            $('.tests-reference').html(errorMsg);
        }
    });

    $('.btn-daily').click(function () {
        $('.tests-reference').html(spiner);
        $.ajax({
            cache: false,
            type: 'POST',
            url: url,
            data: { type: 0 },
            success: function (result) {
                $('.tests-reference').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на справката</div>");
                $('.tests-reference').html(errorMsg);
            }
        });
    });

    $('.btn-monthly').click(function () {
        $('.tests-reference').html(spiner);
        $.ajax({
            cache: false,
            type: 'POST',
            url: url,
            data: { type: 1 },
            success: function (result) {
                $('.tests-reference').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на справката</div>");
                $('.tests-reference').html(errorMsg);
            }
        });
    });

    $('.btn-yearly').click(function () {
        $('.tests-reference').html(spiner);
        $.ajax({
            cache: false,
            type: 'POST',
            url: url,
            data: { type: 2 },
            success: function (result) {
                $('.tests-reference').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на справката</div>");
                $('.tests-reference').html(errorMsg);
            }
        });
    });
});