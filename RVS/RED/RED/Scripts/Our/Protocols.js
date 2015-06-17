
$(document).ready(function () {
    $('.btn-delete').click(function () {
        var id = $(this).attr('href');
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

    });
});