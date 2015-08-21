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
var tabId = 'active-protocols';
var url = '/Protocols/FilterActiveProtocols';
var isActiveTab = true;



$(document).ready(function () {
    $('.active-tab-btn').click(function () {
        ClearFilters();
        tabId = 'active-protocols';
        url = '/Protocols/FilterActiveProtocols';
        isActiveTab = true;

        $('.active-protocols').empty();
        $('.archived-protocols').empty();

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
                if (protocolIdToOpen) {
                    $('a[href="#' + protocolIdToOpen + '"]').click();
                    $('html, body').animate({
                        scrollTop: $("#" + protocolIdToOpen).offset().top - 50
                    }, 500);
                    protocolIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на активните протоколи</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.archived-tab-btn').click(function () {
        ClearFilters();
        tabId = 'archived-protocols';
        url = '/Protocols/FilterArchivedProtocols';
        isActiveTab = false;

        $('.active-protocols').empty();
        $('.archived-protocols').empty();

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
                if (protocolIdToOpen) {
                    $('a[href="#' + protocolIdToOpen + '"]').click();
                    $('html, body').animate({
                        scrollTop: $("#" + protocolIdToOpen).offset().top - 50
                    }, 500);
                    protocolIdToOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на архивираните протоколи</div>");
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
            success: function (result) {
                $('.' + tabId).html(result);
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.add-remark-btn').click(function () {
        var remarks = $('.remark');
        var index = remarks.length;
        var content = '<tr><td class="col-xs-1"><span class="label label-primary">Добавен</span></td>' +
            '<td class="issue-info remark">' +
                $('#RemarkId option:selected').text() +
                '<input class="remarkId" type="hidden" value="' + $('#RemarkId').val() + '" name="ProtocolsRemarks[' + index + '].RemarkId">' +
                '<input class="remarkProtocolId" type="hidden" value="' + $('#protocolId').val() + '" name="ProtocolsRemarks[' + index + '].ProtocolId">' +
                '<input class="remarkRowId" type="hidden" value="' + guid() + '" name="ProtocolsRemarks[' + index + '].Id">' +
                '</td><td class="col-xs-1">' +
                '<a class="delete-remark" onclick="deleteRemark(this)"><h3 style="margin: 0px">x</h3></a></td></tr>';

        $('.remarks-list-table tbody').append(content);
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

function GetFilters() {
    var pageSize = 2;
    var number = $('#ProtocolNumber').val();
    if (number == '') {
        number = -1;
    }

    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();

    return { pageSize: pageSize, number: number, fromDate: fromDate, toDate: toDate };
}

function ClearFilters() {
    $('#ProtocolNumber').val('');
    $('#fromDate').val('');
    $('#toDate').val('');
}

function deleteRemark(e, number) {
    $(e).parent().parent().remove();

    var remarks = $('.remark');

    for (var i = 0; i < remarks.length; i++) {
        var remark = remarks[i];
        
        $(remark).find('.remarkId').attr('name', "ProtocolsRemarks[" + i + "].RemarkId");
        $(remark).find('.remarkProtocolId').attr('name', "ProtocolsRemarks[" + i + "].ProtocolId");
        $(remark).find('.remarkRowId').attr('name', "ProtocolsRemarks[" + i + "].Id");
    }
}

function recalcuateRemarkNames() {

}

function addPlusMinus(item) {
    var textArea = $(item).parent().parent().find('textarea');
    var value = textArea.val();
    value += "±";
    textArea.val(value);
}

function addDegrees(item) {
    var textArea = $(item).parent().parent().find('textarea');
    var value = textArea.val();
    value += "°";
    textArea.val(value);
}
