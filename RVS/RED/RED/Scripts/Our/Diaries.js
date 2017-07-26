var diaryIdtoOpen;
var tabId = 'active-diaries';
var url = '/Diary/FilterActiveDiaries';
var isActiveTab = true;

var savedComment = '';
var savedCommentDiaryId = '';

$(document).ready(function () {
    $('.' + tabId).html(spiner);
    $.ajax({
        cache: false,
        type: 'POST',
        data: {
            page: 1, pageSize: PAGE_SIZE, number: -1,
            diaryNumber: -1, client: "00000000-0000-0000-0000-000000000000"
        },
        url: url,
        success: function (result) {
            $('.' + tabId).html(result);
        },
        error: function () {
            var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на дневниците</div>");
            $('.' + tabId).html(errorMsg);
        }
    });

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
                page: 1, pageSize: PAGE_SIZE, number: -1,
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
                page: 1, pageSize: PAGE_SIZE, number: -1,
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
    var pageSize = PAGE_SIZE;
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
    var id = $(btn).attr('buttonId');
    var promise = $.ajax({
        type: "POST",
        url: "/Diary/ArchiveDiary",
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

function GetDiary(id)
{
    $.ajax({
        cache: false,
        type: 'GET',
        url: '/Diary/Details',
        data: { Id: id },
        success: function (result) {
            $('#' + id + ' .ibox-content').append(result);
            $('a[href=#' + id + ']').click();
        },
        error: function () {
            var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на заявките</div>");
            $('#' + id + ' .ibox-content').append(errorMsg);
        }
    });
}

function deleteDiary(btn) {
    var id = $(btn).attr('href');
    var url = '/Diary/Delete?id=' + id;
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

function archiveDiary(btn) {
    var id = $(btn).attr('buttonId');
    var url = '/Diary/Archive?id=' + id;
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

function saveComment(diaryId) {
    var comment = $('#' + diaryId + ' .comment').val();
    $.ajax({
        type: "POST",
        url: '/Diary/AddComment',
        data: { diaryId: diaryId, comment: comment },
        success: function (data) {
            if (data == 'Ok') {
                savedComment = comment;
                $('#' + diaryId + ' .comment').attr('disabled', 'disabled');
                $('#' + diaryId + ' .edit-comment').removeClass('collapse');
                $('#' + diaryId + ' .save-comment').addClass('collapse');
                $('#' + diaryId + ' .close-comment').addClass('collapse');
            }
            else {
                var html = '';
                html = '<div class="alert alert-danger">Възникна грешка при запазването на коментара.</div>';
                $('#' + diaryId + ' .comment-container').html(html);
            }

        }
    });

    return false;
}

function editComment(diaryId) {
    savedComment = $('#' + diaryId + ' .comment').val();
    savedCommentDiaryId = diaryId;

    $('#' + diaryId + ' .edit-comment').addClass('collapse');
    $('#' + diaryId + ' .save-comment').removeClass('collapse');
    $('#' + diaryId + ' .close-comment').removeClass('collapse');

    $('#' + diaryId + ' .comment').removeAttr('disabled');

    return false;
}

function closeComment(diaryId) {
    if (diaryId == savedCommentDiaryId) {
        $('#' + diaryId + ' .edit-comment').removeClass('collapse');
        $('#' + diaryId + ' .save-comment').addClass('collapse');
        $('#' + diaryId + ' .close-comment').addClass('collapse');

        $('#' + diaryId + ' .comment').val(savedComment);
        $('#' + diaryId + ' .comment').attr('disabled', 'disabled');
    }
}

function generateRequest(target) {
    var diaryId = $(target).attr('id');
    var testingPeriod = $('#testingPeriod-' + diaryId).val();
    var parent = $(target).parent();
    parent.html(spiner);

    $.ajax({
        type: "POST",
        url: '/Diary/GenerateRequest?diaryId=' + diaryId + '&&testingPeriod=' + testingPeriod,
        //data: { diaryId : diaryId, comment : "asdfasdf"},
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            var html = '';

            if (data == "Failed") {
                html = '<div class="alert alert-danger">Възникна грешка при генерирането на заявката.</div>';
            }
            else {
                if (data.indexOf("A") > -1) { //if there was a A request generated
                    html = '<div class="alert alert-success">Заявката (A) е в очакване да бъде приета.\
                            <a class="pull-right" href="/Files/GetRequestFile?diaryId=' + diaryId + '&category=A">\
                                <i class="fa fa-download"></i>\
                                Изтегли заявка\
                                </a>\
                            </div>'
                }
                if (data.indexOf("B") > -1) {
                    html += '<div class="alert alert-success">Заявката (B) е в очакване да бъде приета.\
                            <a class="pull-right" href="/Files/GetRequestFile?diaryId=' + diaryId + '&category=B">\
                                <i class="fa fa-download"></i>\
                                Изтегли заявка\
                                </a>\
                            </div>'
                }
                html += '<button url="/Diary/DeleteRequest?diaryId=' + diaryId + '" diaryId="' + diaryId + '" class="btn btn-default btn-delete-request" onclick="deleteRequest(this)"><i class="fa fa-ban"></i> Изтрий заявката </button>';
            }

            parent.html(html);
        }
    });

    return false;
}

function deleteRequest(target) {
    var url = $(target).attr('url');
    var diaryId = $(target).attr('diaryId');
    var parent = $(target).parent();

    parent.html(spiner);
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            var html = '';
            if (res == 'Ok') {
                html = '<label for="testingPeriod">Срок на изпитване (дни):</label>' +
                    '<input type="number" min="1" max="365" id="testingPeriod-' + diaryId + '" />' +
                    '<br />' +
                    '<button id="' + diaryId + '" class="btn btn-primary btn-request" onclick="generateRequest(this)"><i class="fa fa-tag"></i> Направи заявка</button>';
            }
            else {
                html = '<div class="alert alert-danger">Възникна грешка при изтриването на заявката.</div>';
            }

            parent.html(html);
        }
    });
}
