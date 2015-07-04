﻿var spinnerString = '<div class="ibox-content"> \
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
        tabId = 'active-protocols';
        url = '/Protocols/FilterActiveProtocols';
        isActiveTab = true;

        $('.active-protocols').empty();
        $('.archived-protocols').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Protocols/GetActiveProtocols",
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
        tabId = 'archived-protocols';
        url = '/Protocols/FilterArchivedProtocols';
        isActiveTab = false;

        $('.active-protocols').empty();
        $('.archived-protocols').empty();

        $('.' + tabId).html(spinnerString);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Protocols/GetArchivedProtocols",
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

        var page = 1;
        var pageSize = 10;
        var number = $('#ProtocolNumber').val();
        if (number == '') {
            number = -1;
        }

        var fromDate = $('#fromDate').val();
        var toDate = $('#toDate').val();

        $.ajax({
            cache: false,
            type: 'POST',
            url: url,
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