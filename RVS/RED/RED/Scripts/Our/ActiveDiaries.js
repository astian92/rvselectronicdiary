var savedComment = '';
var savedCommentDiaryId = '';

$(document).ready(function () {
    $('.btn-delete').click(function () {
        var id = $(this).attr('href');
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

    });

    $('.btn-archive').click(function () {
        var id = $(this).attr('buttonId');
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
    });
});

function saveComment(diaryId) {
    var comment = $('#' + diaryId + ' .comment').val();
    //var areaHtml = $('#' + diaryId + ' .comment-container').html();
    //$('#' + diaryId + ' .comment-container').html(spiner);
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
            if (data == 'Ok') {
                html = '<div class="alert alert-success">Заявката е в очакване да бъде приета.\
                            <a class="pull-right" href="/Files/GetRequestFile?diaryId=' + diaryId + '">\
                                <i class="fa fa-download"></i>\
                                Изтегли заявка\
                                </a>\
                            </div>\
                        <button url="/Diary/DeleteRequest?diaryId=' + diaryId + '" diaryId="' + diaryId + '" class="btn btn-default btn-delete-request" onclick="deleteRequest(this)"><i class="fa fa-ban"></i> Изтрий заявката </button>';
            }
            else {
                html = '<div class="alert alert-danger">Възникна грешка при генерирането на заявката.</div>';
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

