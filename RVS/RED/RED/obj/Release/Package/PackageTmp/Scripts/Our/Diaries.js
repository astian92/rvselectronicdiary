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

var diaryIdtoOpen;
var tabId = 'active-diaries';
var url = '/Diary/FilterActiveDiaries';
var isActiveTab = true;

$(document).ready(function () {

    $('.active-tab-btn').click(function () {
        ClearFilters();
        tabId = 'active-diaries';
        url = '/Diary/FilterActiveDiaries';
        isActiveTab = true;
        $('.active-diaries').empty();
        $('.archived-diaries').empty();

        $('.' + tabId).html(spiner);

        $.ajax({
            cache: false,
            type: 'POST',
            data: {
                page: 1, pageSize: 2, number: -1,
                diaryNumber: -1, client: "00000000-0000-0000-0000-000000000000"
            },
            url: url,
            success: function (result) {
                $('.' + tabId).html(result);
                if (diaryIdtoOpen) {
                    $('a[href="#' + diaryIdtoOpen + '"]').click();
                    diaryIdtoOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на дневниците</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });

    $('.archived-tab-btn').click(function () {
        ClearFilters();
        tabId = 'archived-diaries';
        url = '/Diary/FilterArchivedDiaries';
        isActiveTab = false;
        $('.active-diaries').empty();
        $('.archived-diaries').empty();

        $('.' + tabId).html(spiner);

        $.ajax({
            cache: false,
            type: 'POST',
            data: {
                page: 1, pageSize: 2, number: -1,
                diaryNumber: -1, client: "Всички"
            },
            url: '/Diary/FilterArchivedDiaries',
            success: function (result) {
                $('.' + tabId).html(result);
                if (diaryIdtoOpen) {
                    $('a[href="#' + diaryIdtoOpen + '"]').click();
                    diaryIdtoOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на дневниците</div>");
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
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на дневниците</div>");
                $('.' + tabId).html(errorMsg);
            }
        })
    });
});

function GetFilters() {
    var pageSize = 2;
    var number = $('#LetterNumber').val();
    if (number == '') {
        number = -1;
    }

    var diaryNumber = $('#DiaryNumber').val();
    if (diaryNumber == '') {
        diaryNumber = -1;
    }

    var client = '';
    if (isActiveTab) {
        client = $('#ClientId').val();
    }
    else {
        client = $('#ClientId option:selected').text();
    }
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();

    return { pageSize: pageSize, number: number, diaryNumber: diaryNumber, client: client, fromDate: fromDate, toDate: toDate }
}

function ClearFilters() {
    $('#LetterNumber').val('');
    $('#DiaryNumber').val('');
    $('#ClientId').val('00000000-0000-0000-0000-000000000000').change();
    text = client = $('#ClientId option:selected').text();
    $('#ClientId_chosen .chosen-single span').text(text);
    $('#fromDate').val('');
    $('#toDate').val('');
}

function ArchiveDiary(btn) {
    //$('.ui-state-error-text').empty();

    var id = $(btn).attr('buttonId');
    var promise = $.ajax({
        type: "POST",
        url: "ArchiveDiary",
        data: { diaryId: id },
        success: function (response) {
            if (response.IsSuccess == true) {
                //do the actual stuff
                diaryIdtoOpen = response.ResponseObject;
                $('.archived-tab-btn').click();
            }
            else {
                $('#field-error-' + id).append('<p>Възникна грешка при опит за архивиране на дневника</p>');
            }
        },
        error: function (error) {
            $('#field-error-' + id).append('<p>Възникна грешка при опит за архивиране на дневника</p>');
        }

    });
    
    return false;
}