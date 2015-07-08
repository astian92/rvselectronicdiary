$(document).ready(function () {
    var url = '/Home/TestsReference'
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