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
var tabId = 'not-accepted-tab-btn';
var url = '/Requests/GetNotAcceptedRequests';

$(document).ready(function () {
    $('.not-accepted-tab-btn').click(function () {
        ClearFilters();
        tabId = 'not-accepted-requests';
        url = '/Requests/FilterNotAcceptedRequests';

        $('.accepted-requests').empty();
        $('.my-requests').empty();
        $('.completed-requests').empty();
        $('.archived-requests').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'POST',
            url: url,
            data: {
                page: 1, pageSize: 2, number: -1
            },
            success: function (result) {
                $('.' + tabId).html(result);
                if (requestIdToOpen) {
                    $('a[href="#' + requestIdToOpen + '"]').click();
                    requestIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.accepted-tab-btn').click(function () {
        ClearFilters();
        tabId = 'accepted-requests';
        url = '/Requests/FilterAcceptedRequests';

        $('.not-accepted-requests').empty();
        $('.my-requests').empty();
        $('.completed-requests').empty();
        $('.archived-requests').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: url,
            data: {
                page: 1, pageSize: 2, number: -1
            },
            success: function (result) {
                $('.' + tabId).html(result);
                if (requestIdToOpen) {
                    $('a[href="#' + requestIdToOpen + '"]').click();
                    requestIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.my-requests-tab-btn').click(function () {
        ClearFilters();
        tabId = 'my-requests';
        url = '/Requests/FilterMyRequests';

        $('.not-accepted-requests').empty();
        $('.accepted-requests').empty();
        $('.completed-requests').empty();
        $('.archived-requests').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: url,
            data: {
                page: 1, pageSize: 2, number: -1
            },
            success: function (result) {
                $('.' + tabId).html(result);
                if (requestIdToOpen) {
                    $('a[href="#' + requestIdToOpen + '"]').click();
                    requestIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.completed-tab-btn').click(function () {
        ClearFilters();
        tabId = 'completed-requests';
        url = '/Requests/FilterCompletedRequests';

        $('.not-accepted-requests').empty();
        $('.accepted-requests').empty();
        $('.my-requests').empty();
        $('.archived-requests').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: url,
            data: {
                page: 1, pageSize: 2, number: -1
            },
            success: function (result) {
                $('.' + tabId).html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.archived-tab-btn').click(function () {
        ClearFilters();
        tabId = 'archived-requests';
        url = '/Requests/FilterArchivedRequests';

        $('.not-accepted-requests').empty();
        $('.accepted-requests').empty();
        $('.my-requests').empty();
        $('.completed-requests').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: url,
            data: {
                page: 1, pageSize: 2, number: -1
            },
            success: function (result) {
                $('.' + tabId).html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('#filter').click(function () {
        $('.active-diaries').empty();
        $('.archived-diaries').empty();

        $('.' + tabId).html(spinnerString);

        var data = GetFilters();
        data.page = 1;
        
        $.ajax({
            cache: false,
            type: 'POST',
            url: url,
            data: data,
            data: { page: page, pageSize: pageSize, number: number, fromDate: fromDate, toDate: toDate },
            success: function (result) {
                $('.' + tabId).html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });
});

function AcceptRequest(btn) {
    $('.ui-state-error-text').empty();
    var id = $(btn).attr('id');
    
    var promise = $.ajax({
        type: "POST",
        url: "/Requests/AcceptRequest",
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

function GetFilters() {
    var pageSize = 2;
    var number = $('#RequestNumber').val();
    if (number == '') {
        number = -1;
    }

    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();

    return { pageSize: pageSize, number: number, fromDate: fromDate, toDate: toDate };
}

function ClearFilters() {
    $('#RequestNumber').val('');
    $('#fromDate').val('');
    $('#toDate').val('');
}