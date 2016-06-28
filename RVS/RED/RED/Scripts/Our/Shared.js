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

var PAGE_SIZE = 10;

$(document).ready(function () {
    $('#side-menu').metisMenu();

    $('.navbar-minimalize').click(function () {
        $("body").toggleClass("mini-navbar");
        hideMenu();
    })

    $('.modal').appendTo("body");

    $('.collapse-link').click(function () {
        var ibox = $(this).closest('div.ibox');
        var button = $(this).find('i');
        var content = ibox.find('div.ibox-content');
        content.slideToggle(200);
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        ibox.toggleClass('').toggleClass('border-bottom');
        setTimeout(function () {
            ibox.resize();
            ibox.find('[id^=map-]').resize();
        }, 50);
    });

    $('.close-link').click(function () {
        var content = $(this).closest('div.ibox');
        content.remove();
    });
});

$(function () {
    $(window).bind("load resize", function () {
        if ($(this).width() < 769) {
            $('body').addClass('body-small')
        } else {
            $('body').removeClass('body-small')
        }
    })
})

function hideMenu() {
    if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(500);
            }, 100);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(500);
            }, 300);
    } else {
        $('#side-menu').removeAttr('style');
    }
}

function addPlusMinus(e) {
    var Id = $(e).attr('for');
    var value = $('#' + Id).val();

    value += '±';

    $('#' + Id).val(value);
}

function addDegrees(e) {
    var Id = $(e).attr('for');
    var value = $('#' + Id).val();

    value += '°';

    $('#' + Id).val(value);
}