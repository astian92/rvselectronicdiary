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