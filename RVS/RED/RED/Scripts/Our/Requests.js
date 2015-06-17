var spinnerString = '<div class="ibox-content"> \
                        <div class="spiner-example">\
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
                        </div>\
                    </div>';


var requestIdToOpen;

$(document).ready(function () {

    $('.not-accepted-tab-btn').click(function () {
        $('.accepted-requests').empty();
        $('.my-requests').empty();
        $('.completed-requests').empty();
        $('.archived-requests').empty();

        $('.not-accepted-requests').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetNotAcceptedRequests",
            success: function (result) {
                $('.not-accepted-requests').html(result);
                if (requestIdToOpen) {
                    $('a[href="#' + requestIdToOpen + '"]').click();
                    requestIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.not-accepted-requests').html(errorMsg);
            }
        })
    });

    $('.accepted-tab-btn').click(function () {
        $('.not-accepted-requests').empty();
        $('.my-requests').empty();
        $('.completed-requests').empty();
        $('.archived-requests').empty();

        $('.accepted-requests').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetAcceptedRequests",
            success: function (result) {
                $('.accepted-requests').html(result);
                if (requestIdToOpen) {
                    $('a[href="#' + requestIdToOpen + '"]').click();
                    requestIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.accepted-requests').html(errorMsg);
            }
        })
    });

    $('.my-requests-tab-btn').click(function () {
        $('.not-accepted-requests').empty();
        $('.accepted-requests').empty();
        $('.completed-requests').empty();
        $('.archived-requests').empty();

        $('.my-requests').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetMyRequests",
            success: function (result) {
                $('.my-requests').html(result);
                if (requestIdToOpen) {
                    $('a[href="#' + requestIdToOpen + '"]').click();
                    requestIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.my-requests').html(errorMsg);
            }
        })
    });

    $('.completed-tab-btn').click(function () {
        $('.not-accepted-requests').empty();
        $('.accepted-requests').empty();
        $('.my-requests').empty();
        $('.archived-requests').empty();

        $('.completed-requests').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetCompletedRequests",
            success: function (result) {
                $('.completed-requests').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.completed-requests').html(errorMsg);
            }
        })
    });

    $('.archived-tab-btn').click(function () {
        $('.not-accepted-requests').empty();
        $('.accepted-requests').empty();
        $('.my-requests').empty();
        $('.completed-requests').empty();

        $('.archived-requests').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetArchivedRequests",
            success: function (result) {
                $('.archived-requests').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.archived-requests').html(errorMsg);
            }
        })
    });
});

function AcceptRequest(btn) {
    $('.ui-state-error-text').empty();
    var id = $(btn).attr('id');
    
    var promise = $.ajax({
        type: "POST",
        url: "AcceptRequest",
        data: { requestId: id },
        success: function (result) {
            if (result == "True") {
                //do the actual stuff
                requestIdToOpen = id;
                $('.my-requests-tab-btn').click();
            }
            else {
                $('#field-error-' + id).append('<p>Възникна грешка при опит за приемане на заявката</p>');
            }
        },
        error: function (error) {
            $('#field-error-' + id).append('<p>Възникна грешка при опит за приемане на заявката</p>');
        }
    });
}

function ConfirmDenyRequest(btn) {
    var id = $(btn).attr('id');

    var url = '/Requests/ConfirmDenyRequest?requestId=' + id;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (view) {
            $('.modal-content').html(view);
        }
    });
}

function DenyRequest(btn) {
    var id = $(btn).attr('id');

    var promise = $.ajax({
        type: "POST",
        url: "DenyRequest",
        data: { requestId: id },
        success: function (result) {
            if (result == "True") {
                //do the actual stuff
                requestIdToOpen = id;
                $('.not-accepted-tab-btn').click();
            }
            else {
                $('.ui-state-error-text').append('<p>Възникна грешка при опит за отказване на заявката</p>');
            }
        },
        error: function (error) {
            $('.ui-state-error-text').append('<p>Възникна грешка при опит за отказване на заявката</p>');
        }
    });
}