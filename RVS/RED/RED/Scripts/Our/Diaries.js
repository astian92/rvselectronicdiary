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

$(document).ready(function () {

    $('.active-tab-btn').click(function () {
        $('.active-diaries').empty();
        $('.archived-diaries').empty();

        $('.active-diaries').html(spiner);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Diary/ActiveDiaries",
            success: function (result) {
                $('.active-diaries').html(result);
                if (diaryIdtoOpen) {
                    $('a[href="#' + diaryIdtoOpen + '"]').click();
                    diaryIdtoOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на дневниците</div>");
                $('.active-diaries').html(errorMsg);
            }
        })
    });

    $('.archived-tab-btn').click(function () {
        $('.active-diaries').empty();
        $('.archived-diaries').empty();

        $('.archived-diaries').html(spiner);

        $.ajax({
            cache: false,
            type: 'GET',
            url: "/Diary/ArchivedDiaries",
            success: function (result) {
                $('.archived-diaries').html(result);
                if (diaryIdtoOpen) {
                    $('a[href="#' + diaryIdtoOpen + '"]').click();
                    diaryIdtoOpen = undefined;
                }
            },
            error: function () {
                var errorMsg = $("<div class='req-error-msg'>Възникна проблем при зареждането на дневниците</div>");
                $('.archived-diaries').html(errorMsg);
            }
        })
    });

});

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
                diaryIdtoOpen = id;
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