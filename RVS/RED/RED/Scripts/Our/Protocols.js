//var protocolIdToOpen;
var tabId = 'active-protocols';
var url = '/Protocols/FilterActiveProtocols';
var isActiveTab = true;

$(document).ready(function () {
    $('.' + tabId).html(spiner);

    if (isArchived) {
        url = '/Protocols/FilterArchivedProtocols';
    }
    else {
        url = '/Protocols/FilterActiveProtocols';
    }

    $.ajax({
        cache: false,
        type: 'GET',
        url: url,
        data: {
            page: 1, pageSize: PAGE_SIZE, number: -1
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

            if (isArchived) {
                $('.nav.nav-tabs').children().removeClass('active');
                $('.archived-tab-btn').parent().addClass('active');
            }
        },
        error: function () {
            var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на активните протоколи</div>");
            $('.' + tabId).html(errorMsg);
        }
    });

    $('.active-tab-btn').click(function () {
        ClearFilters();
        tabId = 'active-protocols';
        url = '/Protocols/FilterActiveProtocols';
        isActiveTab = true;

        $('.active-protocols').empty();
        $('.archived-protocols').empty();

        $('.' + tabId).html(spiner);

        $.ajax({
            cache: false,
            type: 'GET',
            url: url,
            data: {
                page: 1, pageSize: PAGE_SIZE, number: -1
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

        $('.' + tabId).html(spiner);

        $.ajax({
            cache: false,
            type: 'GET',
            url: url,
            data: {
                page: 1, pageSize: PAGE_SIZE, number: -1
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

        $('.' + tabId).html(spiner);

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

    $('.add-a-remark-btn').click(function () {
        var aremarks = $('.a-remark');
        var index = aremarks.length;
        var content = '<tr><td class="col-xs-1"><span class="label label-primary">Добавен</span></td>' +
            '<td class="issue-info remark a-remark">' +
                $('.a-remarksDd option:selected').text() +
                '<input class="remarkId" type="hidden" value="' + $('.a-remarksDd').val() + '" name="ProtocolsRemarksA[' + index + '].RemarkId">' +
                '<input class="remarkProtocolId" type="hidden" value="' + $('#protocolId').val() + '" name="ProtocolsRemarksA[' + index + '].ProtocolId">' +
                '<input class="remarkRowId" type="hidden" value="' + guid() + '" name="ProtocolsRemarksA[' + index + '].Id">' +
                '<input class="remarkNumber" type="hidden" value="' + (index + 1) + '" name="ProtocolsRemarksA[' + index + '].Number">' +
                '</td><td class="col-xs-1">' +
                '<a class="delete-remark" onclick="deleteRemarkA(this)"><h3 style="margin: 0px">x</h3></a></td></tr>';

        $('.remarks-a-list-table tbody').append(content);
    });

    $('.add-b-remark-btn').click(function () {
        var bremarks = $('.b-remark');
        var index = bremarks.length;
        var content = '<tr><td class="col-xs-1"><span class="label label-primary">Добавен</span></td>' +
            '<td class="issue-info remark b-remark">' +
                $('.b-remarksDd option:selected').text() +
                '<input class="remarkId" type="hidden" value="' + $('.b-remarksDd').val() + '" name="ProtocolsRemarksB[' + index + '].RemarkId">' +
                '<input class="remarkProtocolId" type="hidden" value="' + $('#protocolId').val() + '" name="ProtocolsRemarksB[' + index + '].ProtocolId">' +
                '<input class="remarkRowId" type="hidden" value="' + guid() + '" name="ProtocolsRemarksB[' + index + '].Id">' +
                '<input class="remarkNumber" type="hidden" value="' + (index + 1) + '" name="ProtocolsRemarksB[' + index + '].Number">' +
                '</td><td class="col-xs-1">' +
                '<a class="delete-remark" onclick="deleteRemarkB(this)"><h3 style="margin: 0px">x</h3></a></td></tr>';

        $('.remarks-b-list-table tbody').append(content);
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
    var pageSize = PAGE_SIZE;
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

function deleteRemarkA(e, number) {
    $(e).parent().parent().remove();

    var remarks = $('.a-remark');

    for (var i = 0; i < remarks.length; i++) {
        var remark = remarks[i];

        $(remark).find('.remarkId').attr('name', "ProtocolsRemarksA[" + i + "].RemarkId");
        $(remark).find('.remarkProtocolId').attr('name', "ProtocolsRemarksA[" + i + "].ProtocolId");
        $(remark).find('.remarkRowId').attr('name', "ProtocolsRemarksA[" + i + "].Id");
        $(remark).find('.remarkNumber').attr('name', "ProtocolsRemarksA[" + i + "].Number");
        $(remark).find('.remarkNumber').val(i + 1);
    }
}

function deleteRemarkB(e, number) {
    $(e).parent().parent().remove();

    var remarks = $('.b-remark');

    for (var i = 0; i < remarks.length; i++) {
        var remark = remarks[i];

        $(remark).find('.remarkId').attr('name', "ProtocolsRemarksB[" + i + "].RemarkId");
        $(remark).find('.remarkProtocolId').attr('name', "ProtocolsRemarksB[" + i + "].ProtocolId");
        $(remark).find('.remarkRowId').attr('name', "ProtocolsRemarksB[" + i + "].Id");
        $(remark).find('.remarkNumber').attr('name', "ProtocolsRemarksB[" + i + "].Number");
        $(remark).find('.remarkNumber').val(i + 1);
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
