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

var protocolIdToOpen;

$(document).ready(function () {

    $('.active-tab-btn').click(function () {
        $('.active-protocols').empty();
        $('.archived-protocols').empty();

        $('.active-protocols').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Protocols/GetActiveProtocols",
            success: function (result) {
                $('.active-protocols').html(result);
                if (protocolIdToOpen) {
                    $('a[href="#' + protocolIdToOpen + '"]').click();
                    protocolIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на активните протоколи</div>");
                $('.active-protocols').html(errorMsg);
            }
        })
    });

    $('.archived-tab-btn').click(function () {
        $('.active-protocols').empty();
        $('.archived-protocols').empty();

        $('.archived-protocols').html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Protocols/GetArchivedProtocols",
            success: function (result) {
                $('.archived-protocols').html(result);
                if (protocolIdToOpen) {
                    $('a[href="#' + protocolIdToOpen + '"]').click();
                    protocolIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на архивираните протоколи</div>");
                $('.archived-protocols').html(errorMsg);
            }
        })
    });
});

function DeleteProtocol(btn) {
    var id = $(btn).attr('href');
    var url = '/Protocols/Delete?id=' + id;
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