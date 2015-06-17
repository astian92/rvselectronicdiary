var savedComment = '';

$(document).ready(function () {
    $('.btn-request').click(function () {
        var diaryId = $(this).attr('id');

        $('.status-container').html(spiner);
        $.ajax({
            type: "POST",
            url: '/Diary/GenerateRequest?diaryId=' + diaryId,
            //data: { diaryId : diaryId, comment : "asdfasdf"},
            //contentType: "application/json; charset=utf-8",
            success: function (data) {
                var html = '';
                if (data == 'Ok') {
                    html = '<div class="alert alert-success">Заявката е в очакване да бъде приета.</div>';
                }
                else {
                    html = '<div class="alert alert-danger">Възникна грешка при генерирането на заявката.</div>';
                }

                $('.status-container').html(html);
            }
        });
    });

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

    //$('.btn-archive').click(function (ev) {
    //    alert('не е реализирано!');
    //    ev.preventDefault();
    //    return false;
    //});
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

    $('#' + diaryId + ' .edit-comment').addClass('collapse');
    $('#' + diaryId + ' .save-comment').removeClass('collapse');
    $('#' + diaryId + ' .close-comment').removeClass('collapse');

    $('#' + diaryId + ' .comment').removeAttr('disabled');

    return false;
}

function closeComment(diaryId) {
    $('#' + diaryId + ' .edit-comment').removeClass('collapse');
    $('#' + diaryId + ' .save-comment').addClass('collapse');
    $('#' + diaryId + ' .close-comment').addClass('collapse');

    $('#' + diaryId + ' .comment').val(savedComment);
    $('#' + diaryId + ' .comment').attr('disabled', 'disabled');
}

