
$(document).ready(function () {

    $('.not-accepted-tab-btn').click(function () {
        $('.my-requests').empty();
        $('.completed-requests').empty();

        $('.not-accepted-requests').html('<div class="ibox-content"> \
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
                                </div>');

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetNotAcceptedRequests",
            success: function (result) {
                $('.not-accepted-requests').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.not-accepted-requests').html(errorMsg);
            }
        })
    });

    $('.my-requests-tab-btn').click(function () {
        $('.not-accepted-requests').empty();
        $('.completed-requests').empty();

        $('.my-requests').html('<div class="ibox-content"> \
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
                                </div>');

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Requests/GetMyRequests",
            success: function (result) {
                $('.my-requests').html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.my-requests').html(errorMsg);
            }
        })
    });

    $('.completed-tab-btn').click(function () {
        $('.not-accepted-requests').empty();
        $('.my-requests').empty();

        $('.completed-requests').html('<div class="ibox-content"> \
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
                                </div>');

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



});